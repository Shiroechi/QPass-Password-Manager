using QPass.Core.Utilities.Base;
using QPass.Core.Utilities.Convert;
using System;

namespace QPass.Core.Utilities.Extension
{
	/// <summary>
	/// Byte[] Extension.
	/// </summary>
	public static class ByteExtension
	{
		/// <summary>
		/// Convert string to byte[].
		/// Encoding UTF-8.
		/// </summary>
		/// <param name="str">this string.</param>
		/// <returns>byte[] from string.</returns>
		public static byte[] GetBytes(this string str)
		{
			return System.Text.Encoding.UTF8.GetBytes(str);
		}

		/// <summary>
		/// Convert char[] to byte[].
		/// Encoding UTF-8.
		/// </summary>
		/// <param name="str">this string.</param>
		/// <returns>byte[] from string.</returns>
		public static byte[] GetBytes(this char[] str)
		{
			return System.Text.Encoding.UTF8.GetBytes(str);
		}

		/// <summary>
		/// Convert Object to byte[].
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static byte[] GetBytes(this object obj)
		{
			return Objects.Serialize(obj);
		}
		
		/// <summary>
		/// Convert Base16 to byte[].
		/// </summary>
		/// <param name="str">Base16 byte[].</param>
		/// <returns>Decoded byte[].</returns>
		public static byte[] DecodeBase16(this byte[] data)
		{
			return Base16.Decode(data.GetString());
		}

		/// <summary>
		/// Convert Base16 to byte[].
		/// </summary>
		/// <param name="str">Base16 string.</param>
		/// <returns>Decoded byte[].</returns>
		public static byte[] DecodeBase16(this string str)
		{
			return Base16.Decode(str);
		}

		/// <summary>
		/// Convert Base64 to byte[].
		/// </summary>
		/// <param name="str">Base64 byte[].</param>
		/// <returns>Decoded byte[].</returns>
		public static byte[] DecodeBase64(this byte[] data)
		{
			return Base64.Decode(data);
		}

		/// <summary>
		/// Convert Base64 to byte[].
		/// </summary>
		/// <param name="str">Base64 string.</param>
		/// <returns>Decoded byte[].</returns>
		public static byte[] DecodeBase64(this string str)
		{
			return Base64.Decode(str);
		}

		/// <summary>
		/// Convert Base91 to byte[].
		/// </summary>
		/// <param name="str">Base91 byte[].</param>
		/// <returns>Decoded byte[].</returns>
		public static byte[] DecodeBase91(this byte[] data)
		{
			return Base91.Decode(data);
		}

		/// <summary>
		/// Convert Base91 to byte[].
		/// </summary>
		/// <param name="str">Base64 string.</param>
		/// <returns>Decoded byte[].</returns>
		public static byte[] DecodeBase91(this string str)
		{
			return Base91.Decode(str);
		}

		/// <summary>
		/// Clear array.
		/// </summary>
		/// <param name="arr"></param>
		public static void Clear(this Array arr)
		{
			Array.Clear(arr, 0, arr.Length);
		}

		public static byte[] SubByte(this byte[] arr, int offset)
		{
			return arr.SubByte(offset, arr.Length);
		}

		public static byte[] SubByte(this byte[] arr, int ffset, int length)
		{
			byte[] temp = new byte[length - ffset];
			Array.Copy(arr, ffset, temp, 0, length);
			return temp;
		}
	}
}
