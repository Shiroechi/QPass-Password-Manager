using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

// to Serialize 
// [Serializable] 
// public class <class name>

namespace QPass.Core.Utilities.Convert
{
	/// <summary>
	/// Convert Object class.
	/// </summary>
	public class Objects
    {
        /// <summary>
        /// Convert object to byte[].
        /// </summary>
        /// <param name="obj">Object to convert.</param>
        /// <returns></returns>
        public static byte[] Serialize(object obj)
        {
            if (obj == null)
            {
                return null;
            }
			
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (MemoryStream ms = new MemoryStream())
                {
                    bf.Serialize(ms, obj);
                    return ms.ToArray();
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Convert byte[] to object.
        /// </summary>
        /// <param name="bytes">Byte[] to convert.</param>
        /// <returns></returns>
        public static object Deserialize(byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }
			
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    ms.Write(bytes, 0, bytes.Length);
                    ms.Seek(0, SeekOrigin.Begin);
                    return bf.Deserialize(ms);
                }
            }
            catch
            {
                return null;
            } 
        }
    }
}