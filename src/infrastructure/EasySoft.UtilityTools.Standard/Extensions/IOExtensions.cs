using System.Diagnostics;
using System.Security;

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// IOExtensions
/// </summary>
public static class IOExtensions
{
    #region ExistDir

    /// <summary>
    /// 检测补录是否存在
    /// </summary>
    /// <param name="directory"></param>
    /// <returns></returns>
    public static bool ExistDirectory(this string directory)
    {
        if (string.IsNullOrWhiteSpace(directory))
            throw new Exception("ExistDirectory params directory disallow null or empty");

        return Directory.Exists(directory);
    }

    #endregion

    #region IsEmptyDirectory

    /// <summary>
    /// 检测指定目录是否为空
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>        
    public static bool IsEmptyDirectory(this string directoryPath)
    {
        //判断是否存在文件
        var fileNames = directoryPath.GetFileNames();
        if (fileNames.Length > 0) return false;

        //判断是否存在文件夹
        var directoryNames = directoryPath.GetDirectories();
        if (directoryNames.Length > 0) return false;

        return true;
    }

    #endregion

    #region CreatorDirectory

    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="directory">要创建的目录路径包括目录名</param>
    public static void CreateDirectory(this string directory)
    {
        if (directory.Length == 0) return;

        if (directory.ExistDirectory()) return;

        Directory.CreateDirectory(directory);
    }

    #endregion

    #region DeleteDirectory

    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="dir"></param>
    public static void DeleteDirectory(this string dir)
    {
        if (!dir.ExistDirectory()) return;

        var dirInfo = new DirectoryInfo(dir);

        dirInfo.DeleteDirectory();
    }

    /// <summary>
    /// Deletes directory and all content found within it
    /// </summary>
    /// <param name="info">Directory info object</param>
    public static void DeleteDirectory(this DirectoryInfo info)
    {
        if (!info.Exists) return;

        info.DeleteFiles();

        info.EnumerateDirectories().ForEach(x => x.DeleteDirectory());

        info.Delete(true);
    }

    #endregion

    #region ExistFile

    /// <summary>
    /// ExistFile
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static bool ExistFile(this string input)
    {
        if (input.IsNullOrEmpty()) return false;

        return File.Exists(input);
    }

    #endregion

    #region CreateFile

    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="file"></param>
    /// <param name="defaultContent"></param>
    public static void CreateFile(this string file, string defaultContent = "")
    {
        if (file.ExistFile()) return;

        if (!File.Exists(file))
        {
            if (file.IndexOf('\\') < 0) throw new Exception("该字符串{0}不是有效的路径".FormatValue(file));

            var dir = file.Substring(0, file.LastIndexOf('\\'));

            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
        }

        using var fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Read);
        var sw = new StreamWriter(fs);
        sw.Write(defaultContent);
        sw.Close();
    }

    #endregion

    #region Get

    /// <summary>
    /// 获取指定目录中所有文件列表
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>        
    public static string[] GetFileNames(this string directoryPath)
    {
        return !directoryPath.ExistDirectory() ? Array.Empty<string>() : Directory.GetFiles(directoryPath);
    }

    /// <summary>
    /// 获取指定目录及子目录中所有文件列表
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>
    /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。
    /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>
    /// <param name="isSearchChild">是否搜索子目录</param>
    public static string[] GetFileNames(this string directoryPath, string searchPattern, bool isSearchChild = false)
    {
        //如果目录不存在
        if (!directoryPath.ExistDirectory()) return Array.Empty<string>();

        return Directory.GetFiles(
            directoryPath,
            searchPattern,
            isSearchChild ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
        );
    }

    /// <summary>
    /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法.
    /// </summary>
    /// <param name="directoryPath">指定目录的绝对路径</param>        
    public static string[] GetDirectories(this string directoryPath)
    {
        return !directoryPath.ExistDirectory() ? Array.Empty<string>() : Directory.GetDirectories(directoryPath);
    }

    #endregion

    #region Size

    /// <summary>
    /// Gets the size of all files within a directory
    /// </summary>
    /// <param name="directory">Directory</param>
    /// <param name="searchPattern">Search pattern used to tell what files to include (defaults to all)</param>
    /// <param name="recursive">determines if this is a recursive call or not</param>
    /// <returns>The directory size</returns>
    public static long Size(this DirectoryInfo directory, string searchPattern = "*", bool recursive = false)
    {
        return directory.EnumerateFiles(
                searchPattern,
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
            )
            .Sum(x => x.Length);
    }

    #endregion

    #region Read

    /// <summary>
    /// Reads a file to the end as a string
    /// </summary>
    /// <param name="file">File to read</param>
    /// <returns>A string containing the contents of the file</returns>
    public static async Task<string> Read(this FileInfo file)
    {
        if (!file.Exists)
            return "";

        using var reader = file.OpenText();

        var contents = await reader.ReadToEndAsync();

        return contents;
    }

    /// <summary>
    /// Reads a file to the end as a string
    /// </summary>
    /// <param name="location">File to read</param>
    /// <returns>A string containing the contents of the file</returns>
    public static async Task<string> ReadFile(this string location)
    {
        return await new FileInfo(location).Read();
    }

    #endregion

    #region ReadBinary

    /// <summary>
    /// Reads a file to the end and returns a binary array
    /// </summary>
    /// <param name="file">File to open</param>
    /// <returns>A binary array containing the contents of the file</returns>
    public static byte[] ReadBinary(this FileInfo file)
    {
        if (!file.Exists)
            return Array.Empty<byte>();

        using var reader = file.OpenRead();

        var output = reader.ReadAllBinary();

        return output;
    }

    /// <summary>
    /// Reads a file to the end and returns a binary array
    /// </summary>
    /// <param name="location">File to open</param>
    /// <returns>A binary array containing the contents of the file</returns> 
    public static byte[] ReadBinary(this string location)
    {
        return new FileInfo(location).ReadBinary();
    }

    /// <summary>
    /// 读取文件进入流中
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns></returns>
    public static MemoryStream? ReadFileToStream(this string path)
    {
        if (!path.ExistFile()) return null;

        var bytes = path.ReadBinary();

        return new MemoryStream(bytes);
    }

    #endregion

    #region Execute

    /// <summary>
    /// Executes the file
    /// </summary>
    /// <param name="file">File to execute</param>
    /// <param name="arguments">Arguments sent to the executable</param>
    /// <param name="domain">Domain of the user</param>
    /// <param name="password">Password of the user</param>
    /// <param name="user">User to run the file as</param>
    /// <param name="windowStyle">Window style</param>
    /// <param name="workingDirectory">Working directory</param>
    /// <returns>The process object created when the executable is started</returns>
    public static Process? Execute(
        this FileInfo file,
        string arguments = "",
        string domain = "",
        string user = "",
        string password = "",
        ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal,
        string workingDirectory = ""
    )
    {
        if (!file.Exists) throw new FileNotFoundException("file does not exist");

        var info = new ProcessStartInfo
        {
            Arguments = arguments,
            Domain = domain,
            Password = new SecureString()
        };

        foreach (var c in password)
            info.Password.AppendChar(c);

        info.UserName = user;
        info.WindowStyle = windowStyle;
        info.UseShellExecute = false;
        info.WorkingDirectory = string.IsNullOrEmpty(workingDirectory) ? file.DirectoryName : workingDirectory;

        return file.Execute(info);
    }

    /// <summary>
    /// Executes the file
    /// </summary>
    /// <param name="file">File to execute</param>
    /// <param name="arguments">Arguments sent to the executable</param>
    /// <param name="domain">Domain of the user</param>
    /// <param name="password">Password of the user</param>
    /// <param name="user">User to run the file as</param>
    /// <param name="windowStyle">Window style</param>
    /// <param name="workingDirectory">Working directory</param>
    /// <returns>The process object created when the executable is started</returns>
    public static Process? Execute(
        this string file,
        string arguments = "",
        string domain = "",
        string user = "",
        string password = "",
        ProcessWindowStyle windowStyle = ProcessWindowStyle.Normal, string workingDirectory = ""
    )
    {
        var info = new ProcessStartInfo
        {
            Arguments = arguments,
            Domain = domain,
            Password = new SecureString()
        };

        foreach (var c in password)
            info.Password.AppendChar(c);

        info.UserName = user;
        info.WindowStyle = windowStyle;
        info.UseShellExecute = false;
        info.WorkingDirectory = workingDirectory;

        return file.Execute(info);
    }

    /// <summary>
    /// Executes the file
    /// </summary>
    /// <param name="file">File to execute</param>
    /// <param name="info">Info used to execute the file</param>
    /// <returns>The process object created when the executable is started</returns>
    public static Process? Execute(this FileInfo file, ProcessStartInfo info)
    {
        if (!file.Exists) throw new FileNotFoundException("File not found");

        info.FileName = file.FullName;

        return Process.Start(info);
    }

    /// <summary>
    /// Executes the file
    /// </summary>
    /// <param name="file">File to execute</param>
    /// <param name="info">Info used to execute the file</param>
    /// <returns>The process object created when the executable is started</returns>
    public static Process? Execute(this string file, ProcessStartInfo info)
    {
        info.FileName = file;

        return Process.Start(info);
    }

    #endregion

    #region Write

    /// <summary>
    /// 向文件中追加文本
    /// </summary>
    /// <param name="source"></param>
    /// <param name="text">写入的文本</param>
    /// <param name="writeNewLine"></param>
    public static void AppendFile(this string source, string text, bool writeNewLine = false)
    {
        if (!source.ExistFile()) source.CreateFile();

        WriteFile(source, text, Encoding.UTF8, FileMode.Append, writeNewLine);
    }

    /// <summary>
    /// 向文件中追加文本
    /// </summary>
    /// <param name="source"></param>
    /// <param name="text">写入的文本</param>
    /// <param name="encoding">编码</param>
    /// <param name="writeNewLine"></param>
    public static void AppendFile(
        this string source,
        string text,
        Encoding encoding,
        bool writeNewLine = false
    )
    {
        if (!source.ExistFile()) source.CreateFile();

        WriteFile(source, text, encoding, FileMode.Append, writeNewLine);
    }

    /// <summary>
    /// 向文件中追加文本
    /// </summary>
    /// <param name="source"></param>
    /// <param name="text">写入的文本</param>
    public static void WriteFile(this string source, string text)
    {
        if (!source.ExistFile()) source.CreateFile();

        WriteFile(source, text, Encoding.UTF8, FileMode.Create);
    }

    /// <summary>
    /// 向文件中追加文本
    /// </summary>
    /// <param name="source"></param>
    /// <param name="text">写入的文本</param>
    /// <param name="encoding">编码</param>
    public static void WriteFile(this string source, string text, Encoding encoding)
    {
        if (!source.ExistFile()) source.CreateFile();

        WriteFile(source, text, encoding, FileMode.Create);
    }

    /// <summary>
    /// 写入文件
    /// </summary>
    /// <param name="source"></param>
    /// <param name="text"></param>
    /// <param name="encoding"></param>
    /// <param name="fileMode"></param>
    /// <param name="writeNewLine"></param>
    public static void WriteFile(
        this string source,
        string text,
        Encoding encoding,
        FileMode fileMode,
        bool writeNewLine = false
    )
    {
        using var fs = new FileStream(source, fileMode, FileAccess.Write, FileShare.Read);
        var sw = new StreamWriter(fs, encoding);

        if (writeNewLine)
            sw.WriteLine(text);
        else
            sw.Write(text);

        sw.Close();
    }

    #endregion

    #region DeleteFiles

    /// <summary>
    /// Deletes files from a directory
    /// </summary>
    /// <param name="directory">Directory to delete the files from</param>
    /// <param name="recursive">Should this be recursive?</param>
    /// <returns>The directory that is sent in</returns>
    public static void DeleteFiles(this DirectoryInfo directory, bool recursive = false)
    {
        if (!directory.Exists) return;

        directory.EnumerateFiles(
                "*",
                recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly
            )
            .ForEach(x => x.Delete());
    }

    #endregion
}