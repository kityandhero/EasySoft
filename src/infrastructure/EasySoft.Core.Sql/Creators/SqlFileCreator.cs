using System.Collections.Specialized;
using EasySoft.Core.Sql.Assists;
using EasySoft.UtilityTools.Standard.Exceptions;

namespace EasySoft.Core.Sql.Creators;

/// <summary>
/// SqlFileCreator
/// </summary>
public class SqlFileCreator
{
    private static string _targetRelativeDirectoryPath = "";

    private string _targetDir = "";

    private readonly NameValueCollection _assemblyAndNameSpaces;

    /// <summary>
    /// SqlFileCreator   
    /// </summary>
    /// <param name="targetRelativeDirectoryPath"></param>
    /// <param name="assemblyAndNameSpaces"></param>
    public SqlFileCreator(string targetRelativeDirectoryPath, NameValueCollection? assemblyAndNameSpaces)
    {
        _targetRelativeDirectoryPath = targetRelativeDirectoryPath;

        _assemblyAndNameSpaces = assemblyAndNameSpaces ?? new NameValueCollection();

        CheckDir();
    }

    /// <summary>
    /// CreateFile
    /// </summary>
    public void CreateFile()
    {
        if (!_assemblyAndNameSpaces.HasKeys())
        {
            return;
        }

        var builder = new StringBuilder();

        foreach (string? key in _assemblyAndNameSpaces.Keys)
        {
            builder.AppendLine(CreateSqlFileWithAssembly(key, _assemblyAndNameSpaces[key]));
        }

        $"{_targetDir}/AllScript.sql".WriteFile(builder.ToString());
    }

    private void CheckDir()
    {
        var directoryInfo = new DirectoryInfo(
            $"{AppDomain.CurrentDomain.BaseDirectory}${_targetRelativeDirectoryPath}"
        );

        Console.WriteLine($"target folder path:{directoryInfo.FullName}");

        if (!directoryInfo.Exists)
        {
            directoryInfo.Create();
        }

        directoryInfo.DeleteFiles(true);

        _targetDir = directoryInfo.FullName;
    }

    /// <summary>
    /// CreateSqlFileWithType
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private string CreateSqlFileWithType(Type type)
    {
        var name = type.Name;

        var sql = SqlAssist.Create(type);

        $"{_targetDir}/{name}.sql".WriteFile(sql);

        return sql;
    }

    private string CreateSqlFileWithAssembly(string? assemblyName, string? fullNameSpace)
    {
        if (string.IsNullOrWhiteSpace(assemblyName))
        {
            throw new UnhandledException("assemblyName disallow null or empty");
        }

        if (string.IsNullOrWhiteSpace(fullNameSpace))
        {
            throw new UnhandledException("fullNameSpace disallow null or empty");
        }

        var builder = new StringBuilder();

        var types = Assembly.Load(assemblyName).GetTypes();

        var filterTypes = types
            .ToList()
            .Where(o => o.Namespace == fullNameSpace && o is { IsPublic: true, IsVisible: true })
            .ToList();

        filterTypes.ForEach(
            o =>
            {
                var sqlContent = CreateSqlFileWithType(o);

                builder.AppendLine(sqlContent);
            }
        );

        return builder.ToString();
    }
}