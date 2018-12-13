using System;

using QPass.Core.Security.Hash;
using QPass.Core.Security.MAC;

namespace Akashic.Security.KDF
{
	/// <summary>
	/// <para>
	/// Password-Based Key Derivation Function 2 (PBKDF2).
	/// </para>
	/// PBKDF2 applies a pseudorandom function, 
	/// such as hash-based message authentication code (HMAC)
	/// to the input password along with a salt value and 
	/// repeats the process many times to produce a derived key.
	/// <para>
	/// The added computational work makes password cracking 
	/// much more difficult, and is known as key stretching.
	/// </para>
	/// </summary>
	public class PBKDF2
	{
		#region Member

		private int _Iteration;
		private IHash _HashFunction;
		private IMAC _MACFunction;

		#endregion Member
		
		/// <summary>
		/// Default constructor.
		/// with HMAC-SHA1.
		/// </summary>
		public PBKDF2()
		{
			this._HashFunction = new Blake2b(512);
			this._MACFunction = new HMAC(this._HashFunction);
			this._Iteration = 1000;
		}

		/// <summary>
		/// Construtor with custom hash function.
		/// </summary>
		/// <param name="digest">Hash function.</param>
		public PBKDF2(IHash digest, int iteration = 1000)
		{
			this._HashFunction = digest;
			this._MACFunction = new HMAC(this._HashFunction);
			this._Iteration = iteration;
		}

		/// <summary>
		/// Custom constructor.
		/// </summary>
		/// <param name="digest">Hash functon.</param>
		/// <param name="mac">MAC function.</param>
		/// <param name="iteration">Loop count.</param>
		public PBKDF2(IHash digest, IMAC mac, int iteration = 1000)
		{
			if (digest == null)
			{
				this._HashFunction = new Blake2b(512);
			}
			else
			{
				this._HashFunction = digest;
			}

			if (mac == null)
			{
				this._MACFunction = new HMAC(this._HashFunction);
			}
			else
			{
				this._MACFunction = mac;
			}

			if (iteration <= 0)
			{
				this._Iteration = 1000;
			}
			else
			{
				this._Iteration = iteration;
			}
		}

		#region Private
		
		/// <summary>
		/// Compute derived key with PBKDF2 Algortihm.
		/// </summary>
		/// <param name="password">Password to derive.</param>
		/// <param name="salt">Additional string.</param>
		/// <param name="length">Derive key length.</param>
		/// <param name="iteration">Iteration.</param>
		/// <returns></returns>
		private byte[] DeriveKey(byte[] password, byte[] salt, int length, int iteration)
		{
			int hLen = this._HashFunction.GetHashLength();
			int l = (length + hLen - 1) / hLen;
			byte[] Buffer = new byte[4];
			byte[] result = new byte[l * hLen];

			for (int i = 1; i <= l; i++)
			{
				this.UINT32_TO_BE(Buffer, i);

				this.F(password, salt, iteration, Buffer, result, (i - 1) * hLen);
			}

			byte[] output = new byte[length];

			System.Buffer.BlockCopy(result, 0, output, 0, length);

			return output;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="password">Password.</param>
		/// <param name="salt">Salt.</param>
		/// <param name="iteration">Iteration.</param>
		/// <param name="buffer">Buffer.</param>
		/// <param name="result">Output buffer.</param>
		/// <param name="offset">Output offset.</param>
		private void F(byte[] password, byte[] salt, int iteration, byte[] buffer, byte[] result, int offset)
		{
			byte[] state = new byte[this._HashFunction.GetHashLength()];

			this._MACFunction.Initialize(password);

			if (salt != null)
			{
				this._MACFunction.Update(salt, 0, salt.Length);
			}

			this._MACFunction.Update(buffer, 0, buffer.Length);
			this._MACFunction.DoFinal(state, 0);

			Array.Copy(state, 0, result, offset, state.Length);

			for (int i = 1; i != iteration; i++)
			{
				this._MACFunction.Initialize(password);
				this._MACFunction.Update(state, 0, state.Length);
				this._MACFunction.DoFinal(state, 0);

				for (int j = 0; j != state.Length; j++)
				{
					result[offset + j] ^= state[j];
				}
			}
		}

		/// <summary>
		/// Padding salt with Big Endian format.
		/// </summary>
		/// <param name="src"></param>
		/// <param name="offset"></param>
		/// <param name="i"></param>
		private void UINT32_TO_BE(byte[] src, int offset, int i)
		{
			src[offset + 0] = (byte)((uint)i >> 24);
			src[offset + 1] = (byte)((uint)i >> 16);
			src[offset + 2] = (byte)((uint)i >> 8);
			src[offset + 3] = (byte)(i);
		}

		/// <summary>
		/// Padding salt with Big Endian format.
		/// </summary>
		/// <param name="src"></param>
		/// <param name="offset"></param>
		/// <param name="i"></param>
		private void UINT32_TO_BE(byte[] src, int i)
		{
			src[0] = (byte)((uint)i >> 24);
			src[1] = (byte)((uint)i >> 16);
			src[2] = (byte)((uint)i >> 8);
			src[3] = (byte)(i);
		}

		#endregion Private

		#region Public

		/// <summary>
		/// Return the name of the algorithm the KDF implements.
		/// </summary>
		/// <returns></returns>
		public string AlgorithmName()
		{
			return "PBKDF2-" + this._MACFunction.AlgorithmName();
		}

		/// <summary>
		/// Reset the KDF.
		/// </summary>
		public void Reset()
		{
			this._MACFunction.Reset();
			this._HashFunction.Reset();
		}

		/// <summary>
		/// Computes the derived key for specified byte array. 
		/// </summary>
		/// <param name="password">Data to derive.</param>
		/// <param name="salt">Additional byte array.</param>
		/// <param name="length">Output length.</param>
		/// <returns></returns>
		public byte[] Derive(byte[] password, byte[] salt, int length)
		{
			return this.Derive(password, salt, length, this._Iteration);
		}

		/// <summary>
		/// Computes the derived key for specified byte array. 
		/// </summary>
		/// <param name="password">Data to derive.</param>
		/// <param name="salt">Additional byte array.</param>
		/// <param name="length">Output length.</param>
		/// <param name="iteration">Override loop count.</param>
		/// <returns></returns>
		public byte[] Derive(byte[] password, byte[] salt, int length, int iteration)
		{
			return this.DeriveKey(password, salt, length, iteration);
		}

		#endregion Public
	}
}