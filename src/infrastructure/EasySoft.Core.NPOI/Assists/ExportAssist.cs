using System.Data;
using System.Net;
using System.Net.Http.Headers;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace EasySoft.Core.NPOI.Assists;

public static class ExportAssist
{
    public static HttpResponseMessage CreateHttpResponse(
        string fileName,
        string sheetName,
        DataTable dataTableSource,
        Func<DataTable, DataTable>? adjustDataTable = null
    )
    {
        using var memoryStream = new MemoryStream();

        var workbook = CreateWorkbook(
            sheetName,
            dataTableSource,
            adjustDataTable
        );

        workbook.Write(memoryStream);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(memoryStream.ToArray())
        };

        response.Content.Headers.ContentType = new MediaTypeHeaderValue(
            ConstCollection.ExcelMediaType
        );

        response.Content.Headers.ContentDisposition =
            new ContentDispositionHeaderValue(ConstCollection.DispositionTypeAttachment)
            {
                FileName = $"{fileName}.xlsx"
            };

        return response;
    }

    public static byte[] CreateByteArray(
        string sheetName,
        DataTable dataTableSource,
        Func<DataTable, DataTable>? adjustDataTable = null
    )
    {
        using var memoryStream = new MemoryStream();

        var workbook = CreateWorkbook(
            sheetName,
            dataTableSource,
            adjustDataTable
        );

        workbook.Write(memoryStream);

        var response = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new ByteArrayContent(memoryStream.ToArray())
        };

        var byteArray = response.Content.ReadAsByteArrayAsync().Result;

        return byteArray;
    }

    private static IWorkbook CreateWorkbook(
        string sheetName,
        DataTable dataTableSource,
        Func<DataTable, DataTable>? adjustDataTable = null
    )
    {
        if (dataTableSource == null)
        {
            throw new Exception("dataTable not allow null");
        }

        DataTable dataTable;

        if (adjustDataTable != null)
        {
            dataTable = adjustDataTable(dataTableSource);

            if (dataTable == null)
            {
                throw new Exception("无效的导出附加处理");
            }
        }
        else
        {
            dataTable = dataTableSource;
        }

        var headerList = new List<string>();

        var typeList = new List<string>();

        #region Reading property name to generate cell header

        foreach (DataColumn column in dataTable.Columns)
        {
            typeList.Add(column.DataType.Name);

            headerList.Add(column.ColumnName);
        }

        #endregion

        var workbook = new XSSFWorkbook();

        var sheet = workbook.CreateSheet(sheetName);

        var headerStyle = workbook.CreateCellStyle();
        var headerFont = workbook.CreateFont();
        headerFont.IsBold = true;
        headerStyle.SetFont(headerFont);

        for (var i = 0; i < dataTable.Rows.Count; i++)
        {
            var sheetRow = sheet.CreateRow(i + 1);

            for (var j = 0; j < dataTable.Columns.Count; j++)
            {
                // TODO: Below commented code is for Wrapping and Alignment of cell
                // Row1.CellStyle = CellCentertTopAlignment;
                // Row1.CellStyle.WrapText = true;
                // ICellStyle CellCentertTopAlignment = _workbook.CreateCellStyle();
                // CellCentertTopAlignment = _workbook.CreateCellStyle();
                // CellCentertTopAlignment.Alignment = HorizontalAlignment.Center;

                var row = sheetRow.CreateCell(j);
                var cellValue = Convert.ToString(dataTable.Rows[i][j]);

                // TODO: move it to switch case

                if (string.IsNullOrWhiteSpace(cellValue))
                {
                    row.SetCellValue(string.Empty);
                }
                else
                {
                    switch (typeList[j].ToLower())
                    {
                        case ConstCollection.StringType:
                            row.SetCellValue(cellValue);
                            break;

                        case ConstCollection.Int32Type:
                            row.SetCellValue(Convert.ToInt32(dataTable.Rows[i][j]));
                            break;

                        case ConstCollection.Int64Type:
                            row.SetCellValue(Convert.ToString(dataTable.Rows[i][j]));
                            break;

                        case ConstCollection.DecimalType:
                            row.SetCellValue((double)Convert.ToDecimal(dataTable.Rows[i][j]));
                            break;

                        case ConstCollection.DoubleType:
                            row.SetCellValue(Convert.ToDouble(dataTable.Rows[i][j]));
                            break;

                        case ConstCollection.DatetimeType:
                            row.SetCellValue(
                                Convert.ToDateTime(
                                    dataTable.Rows[i][j]
                                ).ToString(ConstCollection.DatetimeFormat)
                            );
                            break;

                        default:
                            row.SetCellValue(Convert.ToString(cellValue));
                            break;
                    }
                }
            }
        }

        var header = sheet.CreateRow(0);

        for (var i = 0; i < headerList.Count; i++)
        {
            var cell = header.CreateCell(i);

            cell.SetCellValue(headerList[i]);
            cell.CellStyle = headerStyle;

            sheet.AutoSizeColumn(i);
        }

        return workbook;
    }
}