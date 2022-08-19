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

using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using EasySoft.UtilityTools.Serializers.Interfaces;

namespace EasySoft.UtilityTools.Serializers
{
    /// <summary>
    /// XML serializer
    /// </summary>
    public sealed class XMLSerializer : ISerializer<string>
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="encoding">Encoding that the serializer should use (defaults to ASCII)</param>
        public XMLSerializer(Encoding? encoding = null)
        {
            EncodingUsing = encoding ?? new ASCIIEncoding();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Encoding that the serializer should use
        /// </summary>
        public Encoding EncodingUsing { get; set; }

        #endregion

        #region Functions

        /// <summary>
        /// Serializes the object
        /// </summary>
        /// <param name="data">Object to serialize</param>
        /// <returns>The serialized object</returns>
        public string Serialize(object data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            using var stream = new MemoryStream();

            var serializer = new XmlSerializer(data.GetType());

            serializer.Serialize(stream, data);
            stream.Flush();

            return EncodingUsing.GetString(stream.GetBuffer(), 0, (int)stream.Position);
        }

        /// <summary>
        /// Deserializes the data
        /// </summary>
        /// <param name="objectType">Object type</param>
        /// <param name="data">Data to deserialize</param>
        /// <returns>The resulting object</returns>
        public object? Deserialize(string data, Type objectType)
        {
            if (string.IsNullOrEmpty(data))
            {
                throw new Exception("data disallow null");
            }

            using var stream = new MemoryStream(EncodingUsing.GetBytes(data));

            var serializer = new XmlSerializer(objectType);

            return serializer.Deserialize(stream);
        }

        #endregion
    }
}