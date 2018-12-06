using System.Collections.Generic;

using QPass.Core.Utilities.Extension;

namespace QPass.Core.Utilities.Base
{
	/// <summary>
	/// Encode and decode in base91.
	/// </summary>
	public static class Base91
    {
        private static char[] EncodeTable = new char[]
        {
            'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M',
            'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
            'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm',
            'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '!', '#', '$',
            '%', '&', '(', ')', '*', '+', ',', '.', '/', ':', ';', '<', '=',
            '>', '?', '@', '[', ']', '^', '_', '`', '{', '|', '}', '~', '"'
        };

        private static Dictionary<byte, int> DecodeTable;

        static Base91()
        {
            InitDecodeTable();
        }

        private static void InitDecodeTable()
        {
            DecodeTable = new Dictionary<byte, int>();
            //for (int i = 0; i < 255; i++)
            //{
            //    DecodeTable[(byte)i] = -1;
            //}
            for (int i = 0; i < EncodeTable.Length; i++)
            {
                DecodeTable[(byte)EncodeTable[i]] = i;
            }
        }

        /// <summary>
        /// Convert to Base91 string.
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">String to Encode.</param>
        /// <returns>Encoded String.</returns>
        public static string EncodeToString(string input)
        {
            string output = "";
            int b = 0;
            int n = 0;
            int v = 0;
            for (int i = 0; i < input.Length; i++)
            {
                b |= (byte)input[i] << n;
                n += 8;
                if (n > 13)
                {
                    v = b & 8191;
                    if (v > 88)
                    {
                        b >>= 13;
                        n -= 13;
                    }
                    else
                    {
                        v = b & 16383;
                        b >>= 14;
                        n -= 14;
                    }
                    output += (char)EncodeTable[v % 91];
                    output += (char)EncodeTable[v / 91];
                }
            }

            if (n != 0)
            {
                output += (char)EncodeTable[b % 91];
                if (n > 7 || b > 90)
                {
                    output += (char)EncodeTable[b / 91];
                }
            }
            return output;
        }

        /// <summary>
        /// Convert to Base91 string.
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">byte[] to Encode.</param>
        /// <returns>Encoded String.</returns>
        public static string EncodeToString(byte[] input)
        {
			return EncodeToString(input.GetString());
        }

        /// <summary>
        /// Convert to Base91 byte[].
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">byte[] to Encode.</param>
        /// <returns>Encoded byte[].</returns>
        public static byte[] Encode(string input)
        {
            return EncodeToString(input).GetBytes();
        }

        /// <summary>
        /// Convert to Base91 byte[].
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">byte[] to Encode.</param>
        /// <returns>Encoded byte[].</returns>
        public static byte[] Encode(byte[] input)
        {
            return EncodeToString(input.GetString()).GetBytes();
        }

        /// <summary>
        /// Convert Base91 string to byte[].
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">Base91 string.</param>
        /// <returns>Decoded byte[].</returns>
        public static byte[] Decode(string input)
        {
            return DecodeToString(input).GetBytes();
        }

        /// <summary>
        /// Convert Base91 byte[] to byte[].
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">Base91 byte[].</param>
        /// <returns>Decoded byte[].</returns>
        public static byte[] Decode(byte[] input)
        {
            return Decode(input.GetString());
        }

        /// <summary>
        /// Convert Base91 string to original string.
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">Base91 string.</param>
        /// <returns>Decoded string.</returns>
        public static string DecodeToString(string input)
        {
            string output = "";
            int c = 0;
            int v = -1;
            int b = 0;
            int n = 0;
            for (int i = 0; i < input.Length; i++)
            {
                c = DecodeTable[(byte)input[i]];
                if (c == -1)
                {
                    continue;
                }
                if (v < 0)
                {
                    v = c;
                }
                else
                {
                    v += c * 91;
                    b |= v << n;
                    n += (v & 8191) > 88 ? 13 : 14;
                    do
                    {
                        output += (char)(b & 255);
                        b >>= 8;
                        n -= 8;
                    } while (n > 7);
                    v = -1;
                }
            }
            if (v + 1 != 0)
            {
                output += (char)((b | v << n) & 255);
            }
            return output;
        }

        /// <summary>
        /// Convert Base91 byte[] to original string.
        /// Encoding UTF-8.
        /// </summary>
        /// <param name="input">Base91 byte[].</param>
        /// <returns>Decoded string.</returns>
        public static string DecodeToString(byte[] input)
        {
            return DecodeToString(input.GetString());
        }
    }
}