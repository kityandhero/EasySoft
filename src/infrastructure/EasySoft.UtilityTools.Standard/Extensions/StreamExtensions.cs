/*
Copyright (c) 2012 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

#region Usings

using EasySoft.UtilityTools.Standard.Assists;

#endregion

namespace EasySoft.UtilityTools.Standard.Extensions;

/// <summary>
/// Extension methods for Streams
/// </summary>
public static class StreamExtensions
{
    #region Functions

    #region Copy

    /// <summary>
    /// 复制流数据
    /// </summary>
    /// <param name="source">复制源</param>
    /// <param name="target">目标</param>
    public static void CopyToStream(this Stream source, Stream target)
    {
        var maxlength = (int)source.Length;
        source.CopyToStream(target, maxlength);
    }

    /// <summary>
    /// 复制流数据
    /// </summary>
    /// <param name="source">复制源</param>
    /// <param name="target">目标</param>
    /// <param name="maxSize">复制字节数</param>
    /// <param name="skip"> 跳过的字节数</param>
    public static void CopyToStream(this Stream source, Stream target, int maxSize, int skip = 0)
    {
        source.Position = 0;

        var currentPosition = 0;
        var sourceByte = new byte[maxSize];

        source.Read(sourceByte, 0, Convert.ToInt32(maxSize));

        var currentSkip = 0;
        var startSkip = true;

        if (skip > 0)
            while (startSkip)
                if (skip - currentSkip > 65535)
                {
                    sourceByte = sourceByte.Skip(65535).ToArray();
                    currentSkip += 65535;
                }
                else
                {
                    sourceByte = sourceByte.Skip(skip - currentSkip).ToArray();
                    currentSkip = skip;
                    startSkip = false;
                }

        CycleCopy(ref sourceByte, ref target, maxSize - skip, ref currentPosition);

        target.Position = 0;
        source.Position = 0;
    }

    private static void CycleCopy(ref byte[] source, ref Stream target, int maxSize, ref int position)
    {
        var skip = 0;
        int writeSize;

        if (maxSize - position > 65535)
        {
            writeSize = 65535;
            if (position > 0) skip = 65535;
        }
        else
        {
            writeSize = maxSize - position;
        }

        source = source.Skip(skip).ToArray();
        target.Write(source, 0, writeSize);
        position += writeSize;

        if (maxSize != position) CycleCopy(ref source, ref target, maxSize, ref position);
    }

    #endregion

    #region 数据流查找

    /// <summary>在数据流中查找字节数组的位置，流指针会移动到结尾</summary>
    /// <param name="stream">数据流</param>
    /// <param name="buffer">字节数组</param>
    /// <param name="offset">字节数组中的偏移</param>
    /// <param name="length">字节数组中的查找长度</param>
    /// <returns></returns>
    public static long IndexOf(this Stream stream, byte[] buffer, long offset = 0, long length = 0)
    {
        if (length <= 0) length = buffer.Length - offset;

        // 位置
        long p = -1;

        for (long i = 0; i < length;)
        {
            var c = stream.ReadByte();

            if (c == -1) return -1;

            p++;

            if (c == buffer[offset + i])
            {
                i++;

                // 全部匹配，退出
                if (i >= length) return p - length + 1;
            }
            else
            {
                //i = 0; // 只要有一个不匹配，马上清零
                // 不能直接清零，那样会导致数据丢失，需要逐位探测，窗口一个个字节滑动
                // 上一次匹配的其实就是j=0那个，所以这里从j=1开始
                var n = i;

                i = 0;

                for (var j = 1; j < n; j++)
                {
                    // 在字节数组前(j,n)里面找自己(0,n-j)
                    if (buffer.CompareTo(j, n, buffer, 0, n - j) != 0) continue;

                    // 前面(0,n-j)相等，窗口退回到这里
                    i = n - j;

                    break;
                }
            }
        }

        return -1;
    }

    /// <summary>一个数据流是否以另一个数组开头。如果成功，指针移到目标之后，否则保持指针位置不变。</summary>
    /// <param name="source"></param>
    /// <param name="buffer">缓冲区</param>
    /// <returns></returns>
    public static bool StartsWith(this Stream source, IEnumerable<byte> buffer)
    {
        var p = 0;

        foreach (var t in buffer)
        {
            var b = source.ReadByte();

            if (b == -1)
            {
                source.Seek(-p, SeekOrigin.Current);

                return false;
            }

            p++;

            if (b == t) continue;

            source.Seek(-p, SeekOrigin.Current);

            return false;
        }

        return true;
    }

    /// <summary>一个数据流是否以另一个数组结尾。如果成功，指针移到目标之后，否则保持指针位置不变。</summary>
    /// <param name="source"></param>
    /// <param name="buffer">缓冲区</param>
    /// <returns></returns>
    public static bool EndsWith(this Stream source, byte[] buffer)
    {
        if (source.Length < buffer.Length) return false;

        var p = source.Length - buffer.Length;

        source.Seek(p, SeekOrigin.Current);

        if (source.StartsWith(buffer)) return true;

        source.Seek(-p, SeekOrigin.Current);

        return false;
    }

    #endregion

    #region 数据流转换

    /// <summary>数据流转为字节数组</summary>
    /// <remarks>
    /// 针对MemoryStream进行优化。内存流的Read实现是一个个字节复制，而ToArray是调用内部内存复制方法
    /// 如果要读完数据，又不支持定位，则采用内存流搬运
    /// 如果指定长度超过数据流长度，就让其报错，因为那是调用者所期望的值
    /// </remarks>
    /// <param name="stream">数据流</param>
    /// <param name="length">长度，0表示读到结束</param>
    /// <returns></returns>
    public static byte[] ReadBytes(this Stream stream, long length = 0)
    {
        // 针对MemoryStream进行优化。内存流的Read实现是一个个字节复制，而ToArray是调用内部内存复制方法
        if (stream is MemoryStream { Position: 0 } ms && (length <= 0 || length == ms.Length))
        {
            ms.Position = ms.Length;
            // 如果长度一致
            var buf = ms.GetBuffer();

            if (buf.Length != ms.Length) return ms.ToArray();

            return buf;
        }

        byte[] bytes;
        if (length > 0)
        {
            bytes = new byte[length];

            stream.Read(bytes, 0, bytes.Length);

            return bytes;
        }

        // 如果要读完数据，又不支持定位，则采用内存流搬运
        if (!stream.CanSeek)
        {
            ms = new MemoryStream();

            while (true)
            {
                var buffer = new byte[1024];
                var count = stream.Read(buffer, 0, buffer.Length);
                if (count <= 0) break;

                ms.Write(buffer, 0, count);
                if (count < buffer.Length) break;
            }

            return ms.ToArray();
        }

        //if (length <= 0 || stream.CanSeek && stream.Position + length > stream.Length) length = (Int32)(stream.Length - stream.Position);
        // 如果指定长度超过数据流长度，就让其报错，因为那是调用者所期望的值
        length = (int)(stream.Length - stream.Position);

        bytes = new byte[length];

        stream.Read(bytes, 0, bytes.Length);

        return bytes;
    }

    /// <summary>数据流转为字节数组，从0开始，无视数据流的当前位置</summary>
    /// <param name="stream">数据流</param>
    /// <returns></returns>
    public static byte[] ToArray(this Stream stream)
    {
        if (stream is MemoryStream memoryStream) return memoryStream.ToArray();

        stream.Position = 0;

        return stream.ReadBytes();
    }

    /// <summary>从数据流中读取字节数组，直到遇到指定字节数组</summary>
    /// <param name="stream">数据流</param>
    /// <param name="buffer">字节数组</param>
    /// <param name="offset">字节数组中的偏移</param>
    /// <param name="length">字节数组中的查找长度</param>
    /// <returns>未找到时返回空，0位置范围大小为0的字节数组</returns>
    public static byte[] ReadTo(this Stream stream, byte[] buffer, long offset = 0, long length = 0)
    {
        var ori = stream.Position;
        var p = stream.IndexOf(buffer, offset, length);
        stream.Position = ori;

        if (p < 0) return Array.Empty<byte>();

        if (p == 0) return Array.Empty<byte>();

        return stream.ReadBytes(p);
    }

    /// <summary>
    /// ReadTo
    /// </summary>
    /// <param name="stream"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static byte[] ReadTo(this Stream stream, string target)
    {
        return stream.ReadTo(target, Encoding.UTF8);
    }

    /// <summary>从数据流中读取字节数组，直到遇到指定字节数组</summary>
    /// <param name="stream">数据流</param>
    /// <param name="target"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static byte[] ReadTo(this Stream stream, string target, Encoding encoding)
    {
        return stream.ReadTo(encoding.GetBytes(target));
    }

    /// <summary>从数据流中读取一行，直到遇到换行</summary>
    /// <param name="stream">数据流</param>
    /// <returns>未找到返回null，0位置返回String.Empty</returns>
    public static string ReadLine(this Stream stream)
    {
        return stream.ReadLine(Encoding.UTF8);
    }

    /// <summary>从数据流中读取一行，直到遇到换行</summary>
    /// <param name="stream">数据流</param>
    /// <param name="encoding"></param>
    /// <returns>未找到返回null，0位置返回String.Empty</returns>
    public static string ReadLine(this Stream stream, Encoding encoding)
    {
        var bts = stream.ReadTo(Environment.NewLine, encoding);

        stream.Seek(encoding.GetByteCount(Environment.NewLine), SeekOrigin.Current);

        return bts.Length == 0 ? string.Empty : encoding.GetString(bts);
    }

    /// <summary>
    /// ToString
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    public static string ToString(this Stream stream)
    {
        return stream.ToString(Encoding.UTF8);
    }

    /// <summary>流转换为字符串</summary>  
    /// <param name="stream">目标流</param>
    /// <param name="encoding">编码格式</param>
    /// <returns></returns>
    public static string ToString(this Stream stream, Encoding encoding)
    {
        var buf = stream.ReadBytes();

        if (buf.Length < 1) return "";

        // 可能数据流前面有编码字节序列，需要先去掉
        var idx = 0;
        var preamble = encoding.GetPreamble();

        if (preamble.Length <= 0) return encoding.GetString(buf, idx, buf.Length - idx);

        if (buf.StartsWith(preamble)) idx = preamble.Length;

        return encoding.GetString(buf, idx, buf.Length - idx);
    }

    #endregion

    #region Write

    /// <summary>把一个字节数组写入到一个数据流</summary>
    /// <param name="des">目的数据流</param>
    /// <param name="src">源数据流</param>
    /// <returns></returns>
    public static Stream Write(this Stream des, params byte[] src)
    {
        if (src.Length > 0) des.Write(src, 0, src.Length);

        return des;
    }

    #endregion

    #region ReadAllBinary

    /// <summary>
    /// Takes all of the data in the stream and returns it as an array of bytes
    /// </summary>
    /// <param name="source">Input stream</param>
    /// <returns>A byte array</returns>
    public static byte[] ReadAllBinary(this Stream source)
    {
        if (source.IsNull()) throw new ArgumentNullException(nameof(source));

        if (source is MemoryStream tempInput) return tempInput.ToArray();

        var buffer = new byte[1024];
        byte[] returnValue;

        using (var temp = new MemoryStream())
        {
            while (true)
            {
                var count = source.Read(buffer, 0, buffer.Length);
                if (count <= 0)
                {
                    returnValue = temp.ToArray();
                    break;
                }

                temp.Write(buffer, 0, count);
            }
        }

        return returnValue;
    }

    #endregion

    #region ReadAll

    /// <summary>
    /// Takes all of the data in the stream and returns it as a string
    /// </summary>
    /// <param name="source">Input stream</param>
    /// <returns>A string containing the content of the stream</returns>
    public static string ReadAll(this Stream source)
    {
        return source.ReadAllBinary().ToString(new UTF8Encoding());
    }

    /// <summary>
    /// Takes all of the data in the stream and returns it as a string
    /// </summary>
    /// <param name="source">Input stream</param>
    /// <param name="encodingUsing">Encoding that the string should be in (defaults to UTF8)</param>
    /// <returns>A string containing the content of the stream</returns>
    public static string ReadAll(this Stream source, Encoding encodingUsing)
    {
        return source.ReadAllBinary().ToString(encodingUsing);
    }

    #endregion

    #endregion
}