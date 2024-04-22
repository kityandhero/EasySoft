// See https://aka.ms/new-console-template for more information

using System.Collections.Specialized;
using EasySoft.Core.Sql.Creators;

Console.WriteLine("Hello, World!");

var nv = new NameValueCollection();

nv["EasySoft.Core.Project.DataEntity.Dapper.Infrastructure"] =
    "EasySoft.Core.Project.DataEntity.Dapper.Infrastructure.Entities";

var sqlFileCreator = new SqlFileCreator(
    "./sqlScripts",
    nv
);

sqlFileCreator.CreateFile();