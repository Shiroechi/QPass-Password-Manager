using System;

using QPass.Core.Utilities;
using QPass.Core.Utilities.Extension;

// BLAKE2b-512("") = 
// 786A02F742015903C6C6FD852552D272912F4740E15847618A86E217F71F5419D25E1031AFEE585313896444934EB04B903A685B1448B755D56F701AFE9BE2CE

// BLAKE2b-512("The quick brown fox jumps over the lazy dog") =
// A8ADD4BDDDFD93E4877D2746E62817B116364A1FA7BC148D95090BC7333B3673F82401CF7AA2E4CB1ECD90296E3F14CB5413F8ED77BE73045B13914CDCD6A918

//The BLAKE2 cryptographic hash function was designed by Jean-Philippe Aumasson,
//Samuel Neves, Zooko Wilcox-O'Hearn, and Christian Winnerlein.

//Reference Implementation and Description can be found at: https://blake2.net/
//Internet Draft: https://tools.ietf.org/html/draft-saarinen-blake2-02

//This implementation does not support the Tree Hashing Mode.

//For unkeyed hashing, developers adapting BLAKE2 to ASN.1 - based
//message formats SHOULD use the OID tree at x = 1.3.6.1.4.1.1722.12.2.

//	   Algorithm   | Target | Collision | Hash | Hash ASN.1 |
//     Identifier  | Arch   |  Security |  nn  | OID Suffix |
//	 --------------+--------+-----------+------+------------+
//   id-blake2b160 | 64-bit |   2**80   |  20  |   x.1.20   |
//   id-blake2b256 | 64-bit |   2**128  |  32  |   x.1.32   |
//   id-blake2b384 | 64-bit |   2**192  |  48  |   x.1.48   |
//   id-blake2b512 | 64-bit |   2**256  |  64  |   x.1.64   |
//   ---------------+--------+-----------+------+------------+

namespace QPass.Core.Security.Hash
{
	/// <summary>
	/// Implementation of the cryptographic hash function Blakbe2b.
	/// 
	/// Blake2b offers a built-in keying mechanism to be used directly
	/// for authentication ("Prefix-MAC") rather than a HMAC construction.
	/// 
	/// Blake2b offers a built-in support for a salt for randomized hashing
	/// and a personal string for defining a unique hash function for each application.
	/// 
	/// BLAKE2b is optimized for 64-bit platforms and produces digests of any 
	/// between 1 and 64 bytes.
	/// </summary>
	public class Blake2b : IHash
	{
		#region Member
		
		private const int ROUNDS = 12; // to use for Catenas H'
		private const int BLOCK_LENGTH_BYTES = 128;// bytes

		// General parameters:
		private int digestLength = 64; // 1- 64 bytes
		private int keyLength = 0; // 0 - 64 bytes for keyed hashing for MAC
		private byte[] salt = null;// new byte[16];
		private byte[] personalization = null;// new byte[16];

		/// <summary>
		/// Key
		/// </summary>
		private byte[] key = null;

		// whenever this buffer overflows, it will be processed
		// in the Compress() function.
		// For performance issues, long messages will not use this buffer.
		private byte[] buffer = null;// new byte[BLOCK_LENGTH_BYTES];
									 // Position of last inserted byte:
		private int bufferPos = 0;// a value from 0 up to 128

		private ulong[] internalState = new ulong[16]; // In the Blake2b paper it is
													   // called: v
		private ulong[] chainValue = null; // state vector, in the Blake2b paper it
										   // is called: h

		private ulong t0 = 0UL; // holds last significant bits, counter (counts bytes)
		private ulong t1 = 0UL; // counter: Length up to 2^128 are supported
		private ulong f0 = 0UL; // finalization flag, for last block: ~0L

		// Blake2b Initialization Vector:
		// Produced from the square root of primes 2, 3, 5, 7, 11, 13, 17, 19.
		// The same as SHA-512 IV.
		private readonly ulong[] blake2b_IV =
			{
				0x6a09e667f3bcc908UL, 0xbb67ae8584caa73bUL, 0x3c6ef372fe94f82bUL,
				0xa54ff53a5f1d36f1UL, 0x510e527fade682d1UL, 0x9b05688c2b3e6c1fUL,
				0x1f83d9abfb41bd6bUL, 0x5be0cd19137e2179UL
			};

		// Message word permutations:
		private readonly byte[,] blake2b_sigma =
		{
			{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
			{ 14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3 },
			{ 11, 8, 12, 0, 5, 2, 15, 13, 10, 14, 3, 6, 7, 1, 9, 4 },
			{ 7, 9, 3, 1, 13, 12, 11, 14, 2, 6, 5, 10, 4, 0, 15, 8 },
			{ 9, 0, 5, 7, 2, 4, 10, 15, 14, 1, 11, 12, 6, 8, 3, 13 },
			{ 2, 12, 6, 10, 0, 11, 8, 3, 4, 13, 7, 5, 15, 14, 1, 9 },
			{ 12, 5, 1, 15, 14, 13, 4, 10, 0, 7, 6, 3, 9, 2, 8, 11 },
			{ 13, 11, 7, 14, 12, 1, 3, 9, 5, 0, 15, 4, 8, 6, 2, 10 },
			{ 6, 15, 14, 9, 11, 3, 0, 8, 12, 2, 13, 7, 1, 4, 10, 5 },
			{ 10, 2, 8, 4, 7, 6, 1, 5, 15, 11, 9, 14, 3, 12, 13, 0 },
			{ 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 },
			{ 14, 10, 4, 8, 9, 15, 13, 6, 1, 12, 0, 2, 11, 7, 5, 3 }
		};

		#endregion Member
		
		/// <summary>
		/// Constructor. 
		/// </summary>
		/// <param name="digestSize">Size of hash values in bits.</param>
		public Blake2b(int digestSize = 512)
        {
			if (digestSize != 160 && digestSize != 256 && digestSize != 384 && digestSize != 512)
			{
				throw new ArgumentException("BLAKE2b hash function restricted to one of [160, 256, 384, 512] bits only.");
			}

            buffer = new byte[BLOCK_LENGTH_BYTES];
            keyLength = 0;
            this.digestLength = digestSize / 8;
            Init();
        }

		/// <summary>
		/// Blake2b for authentication ("Prefix-MAC mode").
		/// </summary>
		/// <param name="key">A key up to 64 bytes or null.</param>
		public Blake2b(byte[] key)
		{
			buffer = new byte[BLOCK_LENGTH_BYTES];
			if (key != null)
			{
				this.key = new byte[key.Length];
				Array.Copy(key, 0, this.key, 0, key.Length);

				if (key.Length > 64)
				{
					throw new ArgumentException("Keys > 64 are not supported.");
				}

				keyLength = key.Length;
				Array.Copy(key, 0, buffer, 0, key.Length);
				bufferPos = BLOCK_LENGTH_BYTES; // zero padding
			}
			digestLength = 64;
			this.Init();
        }
		
		/// <summary>
		/// Blake2b with key, required digest length (in bytes), salt and personalization.
		/// </summary>
		/// <param name="key">A key up to 64 bytes or null.</param>
		/// <param name="digestLength">From 1 up to 64 bytes.</param>
		/// <param name="salt">16 bytes or null.</param>
		/// <param name="personalization">16 bytes or null.</param>
		public Blake2b(byte[] key, int digestLength, byte[] salt, byte[] personalization)
		{
			if (digestLength < 1 || digestLength > 64)
			{
				throw new ArgumentException("Invalid digest length (required: 1 - 64)");
			}

			this.digestLength = digestLength;
			this.buffer = new byte[BLOCK_LENGTH_BYTES];

			if (salt != null)
			{
				if (salt.Length != 16)
				{
					throw new ArgumentException("salt length must be exactly 16 bytes");
				}

				this.salt = new byte[16];
				Array.Copy(salt, 0, this.salt, 0, salt.Length);
			}

			if (personalization != null)
			{
				if (personalization.Length != 16)
				{
					throw new ArgumentException("personalization length must be exactly 16 bytes");
				}

				this.personalization = new byte[16];
				Array.Copy(personalization, 0, this.personalization, 0, personalization.Length);
			}

			if (key != null)
			{
				if (key.Length > 64)
				{
					throw new ArgumentException("Keys > 64 are not supported");
				}

				this.key = new byte[key.Length];
				Array.Copy(key, 0, this.key, 0, key.Length);

				keyLength = key.Length;
				Array.Copy(key, 0, buffer, 0, key.Length);
				bufferPos = BLOCK_LENGTH_BYTES; // zero padding
			}

			this.Init();
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~Blake2b()
		{
			this.ClearKey();
			this.ClearSalt();
			this.Reset();
		}

		#region Private

		// initialize chainValue
		private void Init()
		{
			if (chainValue == null)
			{
				chainValue = new ulong[8];

				chainValue[0] = blake2b_IV[0] ^ (ulong)(digestLength | (keyLength << 8) | 0x1010000);
				
				chainValue[1] = blake2b_IV[1];
				chainValue[2] = blake2b_IV[2];
				chainValue[3] = blake2b_IV[3];
				chainValue[4] = blake2b_IV[4];
				chainValue[5] = blake2b_IV[5];

				if (salt != null)
				{
					chainValue[4] ^= Pack.LE_To_UInt64(salt, 0);
					chainValue[5] ^= Pack.LE_To_UInt64(salt, 8);
				}

				chainValue[6] = blake2b_IV[6];
				chainValue[7] = blake2b_IV[7];

				if (personalization != null)
				{
					chainValue[6] ^= Pack.LE_To_UInt64(personalization, 0);
					chainValue[7] ^= Pack.LE_To_UInt64(personalization, 8);
				}
			}
		}

		private void InitializeInternalState()
		{
			// initialize v:
			Array.Copy(chainValue, 0, internalState, 0, chainValue.Length);
			Array.Copy(blake2b_IV, 0, internalState, chainValue.Length, 4);
			internalState[12] = t0 ^ blake2b_IV[4];
			internalState[13] = t1 ^ blake2b_IV[5];
			internalState[14] = f0 ^ blake2b_IV[6];
			internalState[15] = blake2b_IV[7];// ^ f1 with f1 = 0
		}

		private void Compress(byte[] message, int messagePos)
		{
			this.InitializeInternalState();

			ulong[] m = new ulong[16];
			for (int j = 0; j < 16; j++)
			{
				m[j] = Pack.LE_To_UInt64(message, messagePos + j * 8);
			}

			for (int round = 0; round < ROUNDS; round++)
			{
				// G apply to columns of internalState:m[blake2b_sigma[round][2 * blockPos]] /+1
				this.G(m[blake2b_sigma[round, 0]], m[blake2b_sigma[round, 1]], 0, 4, 8, 12);
				this.G(m[blake2b_sigma[round, 2]], m[blake2b_sigma[round, 3]], 1, 5, 9, 13);
				this.G(m[blake2b_sigma[round, 4]], m[blake2b_sigma[round, 5]], 2, 6, 10, 14);
				this.G(m[blake2b_sigma[round, 6]], m[blake2b_sigma[round, 7]], 3, 7, 11, 15);
				
				// G apply to diagonals of internalState:
				this.G(m[blake2b_sigma[round, 8]], m[blake2b_sigma[round, 9]], 0, 5, 10, 15);
				this.G(m[blake2b_sigma[round, 10]], m[blake2b_sigma[round, 11]], 1, 6, 11, 12);
				this.G(m[blake2b_sigma[round, 12]], m[blake2b_sigma[round, 13]], 2, 7, 8, 13);
				this.G(m[blake2b_sigma[round, 14]], m[blake2b_sigma[round, 15]], 3, 4, 9, 14);
			}

			// update chain values:
			for (int offset = 0; offset < chainValue.Length; offset++)
			{
				this.chainValue[offset] = chainValue[offset] ^ internalState[offset] ^ internalState[offset + 8];
			}
		}

		private void G(ulong m1, ulong m2, int posA, int posB, int posC, int posD)
		{
			this.internalState[posA] = internalState[posA] + internalState[posB] + m1;
			this.internalState[posD] = Rotr64(internalState[posD] ^ internalState[posA], 32);
			this.internalState[posC] = internalState[posC] + internalState[posD];
			this.internalState[posB] = Rotr64(internalState[posB] ^ internalState[posC], 24); // replaces 25 of BLAKE
			this.internalState[posA] = internalState[posA] + internalState[posB] + m2;
			this.internalState[posD] = Rotr64(internalState[posD] ^ internalState[posA], 16);
			this.internalState[posC] = internalState[posC] + internalState[posD];
			this.internalState[posB] = Rotr64(internalState[posB] ^ internalState[posC], 63); // replaces 11 of BLAKE
		}

		private ulong Rotr64(ulong x, int rot)
		{
			return x >> rot | x << -rot;
		}

		#endregion Private

		#region Public 

		/// <summary>
		/// Hash algorithm name.
		/// </summary>
		/// <returns></returns>
		public string AlgorithmName()
		{
			return "Blake2b - " + (this.digestLength * 8); 
		}

		/// <summary>
		/// Reset the hash function and 
		/// clear all used resource.
		/// </summary>
		public void Reset()
		{
			this.bufferPos = 0;
			this.f0 = 0L;
			this.t0 = 0L;
			this.t1 = 0L;
			this.chainValue = null;
			Array.Clear(this.buffer, 0, this.buffer.Length);
			if (this.key != null)
			{
				Array.Copy(this.key, 0, this.buffer, 0, this.key.Length);
				this.bufferPos = BLOCK_LENGTH_BYTES; // zero padding
			}
			this.Init();
		}

		/// <summary>
		/// Return the size (in bytes) of the hash value produced 
		/// by this hash function.
		/// </summary>
		/// <returns></returns>
		public int GetHashLength()
		{
			return this.digestLength;
		}

		/// <summary>
		/// Return the size (in bytes) of the internal 
		/// buffer used by this digest.
		/// </summary>
		/// <returns></returns>
		public int GetByteLength()
		{
			return BLOCK_LENGTH_BYTES;
		}

		/// <summary>
		/// Overwrite the key
		/// if it is no longer used (zeroization).
		/// </summary>
		public virtual void ClearKey()
		{
			if (key != null)
			{
				Array.Clear(key, 0, key.Length);
				Array.Clear(buffer, 0, buffer.Length);
			}
		}

		/// <summary>
		/// Overwrite the salt (pepper) if it
		/// is secret and no longer used (zeroization).
		/// </summary>
		public virtual void ClearSalt()
		{
			if (salt != null)
			{
				Array.Clear(salt, 0, salt.Length);
			}
		}

		/// <summary>
		/// Update the hash value with a single byte.
		/// </summary>
		/// <param name="input">Input byte to be hashed.</param>
		/// <returns></returns>
		public void Update(byte input)
		{
			int remainingLength = 0; // left bytes of buffer

			// process the buffer if full else add to buffer:
			remainingLength = BLOCK_LENGTH_BYTES - bufferPos;
			if (remainingLength == 0)
			{
				// full buffer
				t0 += BLOCK_LENGTH_BYTES;
				if (t0 == 0)
				{
					// if message > 2^64
					t1++;
				}

				this.Compress(buffer, 0);
				Array.Clear(buffer, 0, buffer.Length);// clear buffer
				buffer[0] = input;
				bufferPos = 1;
			}
			else
			{
				buffer[bufferPos] = input;
				bufferPos++;
				return;
			}
		}

		/// <summary>
		/// Update the hash value with a array of bytes.
		/// </summary>
		/// <param name="input">Input byte array to be hashed.</param>
		/// <returns></returns>
		public void Update(byte[] input)
		{
			this.Update(input, 0, input.Length);
		}

		/// <summary>
		/// Update the hash value with a string.
		/// </summary>
		/// <param name="input">Input string to be hashed.</param>
		/// <returns></returns>
		public void Update(string input)
		{
			this.Update(input.GetBytes());
		}

		/// <summary>
		/// Update the hash value with a array of bytes.
		/// </summary>
		/// <param name="input">Input array of bytes to be hashed.</param>
		/// <param name="start_index">Offset where the data starts.</param>
		/// <param name="length">Length of data.</param>
		/// <returns></returns>
		public void Update(byte[] input, int start_index, int length)
		{
			if (input == null || length == 0)
			{
				return;
			}

			int remainingLength = 0; // left bytes of buffer

			if (bufferPos != 0)
			{
				// commenced, incomplete buffer

				// complete the buffer:
				remainingLength = BLOCK_LENGTH_BYTES - bufferPos;
				if (remainingLength < length)
				{
					// full buffer + at least 1 byte
					Array.Copy(input, start_index, buffer, bufferPos, remainingLength);
					t0 += BLOCK_LENGTH_BYTES;
					if (t0 == 0)
					{
						// if message > 2^64
						t1++;
					}

					this.Compress(buffer, 0);
					bufferPos = 0;
					Array.Clear(buffer, 0, buffer.Length);// clear buffer
				}
				else
				{
					Array.Copy(input, start_index, buffer, bufferPos, length);
					bufferPos += length;
					return;
				}
			}

			// process blocks except last block (also if last block is full)
			int messagePos;
			int blockWiseLastPos = start_index + length - BLOCK_LENGTH_BYTES;
			for (messagePos = start_index + remainingLength; messagePos < blockWiseLastPos; messagePos += BLOCK_LENGTH_BYTES)
			{ // block wise 128 bytes
			  // without buffer:
				t0 += BLOCK_LENGTH_BYTES;
				if (t0 == 0)
				{
					t1++;
				}
				Compress(input, messagePos);
			}

			// fill the buffer with left bytes, this might be a full block
			Array.Copy(input, messagePos, buffer, 0, start_index + length
				- messagePos);
			bufferPos += start_index + length - messagePos;
		}

		/// <summary>
		/// Copy final hash value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <returns></returns>
		public int DoFinal(byte[] output)
		{
			return this.DoFinal(output, 0);
		}

		/// <summary>
		/// Copy final hash value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <param name="start_index">Offset into the output array the hash value is to start at.</param>
		/// <returns></returns>
		public int DoFinal(byte[] output, int start_index)
		{
			f0 = 0xFFFFFFFFFFFFFFFFUL;
			t0 += (ulong)bufferPos;
			if (bufferPos > 0 && t0 == 0)
			{
				t1++;
			}

			this.Compress(buffer, 0);
			Array.Clear(buffer, 0, buffer.Length);// Holds eventually the key if input is null
			Array.Clear(internalState, 0, internalState.Length);

			for (int i = 0; i < chainValue.Length && (i * 8 < digestLength); i++)
			{
				byte[] bytes = Pack.UInt64_To_LE(chainValue[i]);

				if (i * 8 < digestLength - 8)
				{
					Array.Copy(bytes, 0, output, start_index + i * 8, 8);
				}
				else
				{
					Array.Copy(bytes, 0, output, start_index + i * 8, digestLength - (i * 8));
				}
			}

			Array.Clear(chainValue, 0, chainValue.Length);

			this.Reset();

			return this.digestLength;
		}

		/// <summary>
		/// Computes the hash value for the specified byte array.
		/// </summary>
		/// <param name="input">The input to compute the hash value for.</param>
		/// <returns></returns>
		public byte[] ComputeHash(byte[] input)
		{
			return this.ComputeHash(input, 0, input.Length);
		}

		/// <summary>
		/// Computes the hash value for the specified string.
		/// </summary>
		/// <param name="input">The input to compute the hash value for.</param>
		/// <returns></returns>
		public byte[] ComputeHash(string input)
		{
			return this.ComputeHash(input.GetBytes());
		}

		/// <summary>
		/// Computes the hash value for the specified byte array.
		/// </summary>
		/// <param name="input">The input to compute the hash value for.</param>
		/// <param name="start_index">Offset where the data starts.</param>
		/// <param name="length">Length of data.</param>
		/// <returns></returns>
		public byte[] ComputeHash(byte[] input, int start_index, int length)
		{
			byte[] result = new byte[this.digestLength];
			this.Update(input, start_index, length);
			this.DoFinal(result, 0);
			return result;
		}

		/// <summary>
		/// Create clone of current object.
		/// </summary>
		/// <typeparam name="T">Type of the object.</typeparam>
		/// <returns></returns>
		public IHash Clone()
		{
			return new Blake2b();
		}

		#endregion Public
    }
}
