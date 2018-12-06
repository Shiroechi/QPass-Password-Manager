using System;

//https://github.com/ssg/SimpleBase
//https://github.com/KvanTTT/BaseNcoding
//https://github.com/rodion73/BinaryToText
//https://github.com/bwaldvogel/base91

namespace QPass.Core.Utilities.Base
{
	/// <summary>
	/// Encode and decode in base 16 (hexadecimal).
	/// <para>
	/// from https://github.com/CodesInChaos/Chaos.NaCl/blob/master/Chaos.NaCl/CryptoBytes.cs
	/// </para>
	/// </summary>
	public static class Base16
	{
		/// <summary>
		/// Encode byte array to hexadecimal string.
		/// </summary>
		/// <param name="data">Byte array to encode.</param>
		/// <param name="upper">Boolean upper character.</param>
		/// <returns></returns>
		public static string Encode(byte[] data, bool upper = true)
		{
			if (upper)
			{
				return EncodeUpper(data);
			}
			return EncodeLower(data);
		}

		/// <summary>
		/// Encode byte array to hexadecimal string in upper character.
		/// </summary>
		/// <param name="data">Byte array to encode.</param>
		/// <returns></returns>
		public static string EncodeUpper(byte[] data)
		{
			if (data == null)
			{
				return null;
			}

			char[] c = new char[data.Length * 2];
			int b;
			for (int i = 0; i < data.Length; i++)
			{
				b = data[i] >> 4;
				c[i * 2] = (char)(55 + b + (((b - 10) >> 31) & -7));
				b = data[i] & 0xF;
				c[i * 2 + 1] = (char)(55 + b + (((b - 10) >> 31) & -7));
			}
			return new string(c);
		}

		/// <summary>
		/// Encode byte array to hexadecimal string in lower character.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public static string EncodeLower(byte[] data)
		{
			if (data == null)
			{
				return null;
			}

			char[] c = new char[data.Length * 2];
			int b;
			for (int i = 0; i < data.Length; i++)
			{
				b = data[i] >> 4;
				c[i * 2] = (char)(87 + b + (((b - 10) >> 31) & -39));
				b = data[i] & 0xF;
				c[i * 2 + 1] = (char)(87 + b + (((b - 10) >> 31) & -39));
			}
			return new string(c);
		}

		/// <summary>
		/// Decode hexadecimal string to byte array.
		/// </summary>
		/// <param name="data">Hexadecimal string to decode.</param>
		/// <returns></returns>
		public static byte[] Decode(string data)
		{
			if (data == null)
			{
				return null;
			}
			if (data.Length % 2 != 0)
			{
				throw new Exception("The hex string is invalid because it has an odd length");
			}
			var result = new byte[data.Length / 2];

			for (int i = 0; i < result.Length; i++)
			{
				result[i] = System.Convert.ToByte(data.Substring(i * 2, 2), 16);
			}
			return result;
		}	
	}
}