using System;

using QPass.Core.Security.Hash;
using QPass.Core.Utilities.Extension;

//HMAC_SHA1("", "")   = fbdb1d1b18aa6c08324b7d64b71fb76370690e1d

namespace QPass.Core.Security.MAC
{
	/// <summary>
	/// HMAC implementation based on RFC2104
	/// H(K XOR opad, H(K XOR ipad, text))
	/// </summary>
	public class HMAC : IMAC
	{
		#region Member

		private Boolean _Initilized;
		private const byte IPAD = (byte)0x36;
		private const byte OPAD = (byte)0x5C;

		private IHash hash;

		private readonly int DigestSize;
		private readonly int BlockSize;

		private byte[] InnerPadding;
		private byte[] OuterPadding;
		private byte[] KeyValue;

		#endregion Member

		/// <summary>
		/// Base constructor for one of the standard hash algorithms that the
		/// byteLength of the algorithm is know for.
		/// </summary>
		/// <param name="digest">Hash function to use.</param>
		public HMAC(IHash digest)
		{
			digest.Reset();
			this.hash = digest;
			this.DigestSize = digest.GetHashLength();
			this.BlockSize = digest.GetByteLength();
			this.InnerPadding = new byte[this.BlockSize];
			this.OuterPadding = new byte[this.BlockSize/* + DigestSize*/];
			this.KeyValue = new byte[0];
			this._Initilized = false;
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~HMAC()
		{
			this.hash.Reset();
			this.InnerPadding.Clear();
			this.OuterPadding.Clear();
			this.KeyValue.Clear();
		}

		#region Private
		
		/// <summary>
		/// Initialize the MAC.
		/// </summary>
		/// <param name="key">key required by the MAC</param>
		private void InitializeKey(byte[] key)
		{
			this.hash.Reset();

			if (key.Length > this.BlockSize)
			{
				this.KeyValue = new byte[this.hash.GetHashLength()];
				this.KeyValue = this.hash.ComputeHash(key);
			}

			if (key.Length < this.BlockSize)
			{
				this.KeyValue = this.PadKey(key);
			}

			this.UpdatePad();
		}

		private void UpdatePad()
		{
			if (this.InnerPadding.Length != this.BlockSize)
			{
				this.InnerPadding.Clear();
				this.InnerPadding = new byte[this.BlockSize];
			}
			if (this.OuterPadding.Length != this.BlockSize)
			{
				this.OuterPadding.Clear();
				this.OuterPadding = new byte[this.BlockSize];
			}

			//this.InnerPadding.Clear();
			//this.OuterPadding.Clear();

			////copy key to padding
			//for (int i = 0; i < this.KeyValue.Length; i++)
			//{
			//	this.InnerPadding[i] = this.KeyValue[i];
			//	this.OuterPadding[i] = this.KeyValue[i];
			//}

			////XOR padding with ipad and opad
			//for (int i = 0; i < this.BlockSize; i++)
			//{
			//	this.InnerPadding[i] ^= IPAD;
			//	this.OuterPadding[i] ^= OPAD;
			//}

			//initialize padding
			for (int i = 0; i < this.BlockSize; i++)
			{
				this.InnerPadding[i] = IPAD;
				this.OuterPadding[i] = OPAD;
			}

			//XOR padding with key
			for (int i = 0; i < this.KeyValue.Length; i++)
			{
				this.InnerPadding[i] ^= this.KeyValue[i];
				this.OuterPadding[i] ^= this.KeyValue[i];
			}
		}

		private byte[] PadKey(byte[] key)
		{
			byte[] result = new byte[this.hash.GetByteLength()];
			Buffer.BlockCopy(key, 0, result, 0, key.Length);
			return result;
		}

		#endregion Private

		#region Public

		/// <summary>
		/// Initialize the HMAC function.
		/// </summary>
		/// <param name="key">Key.</param>
		public virtual void Initialize(byte[] key)
		{
			this.InitializeKey(key);
			this._Initilized = true;
			this.hash.Update(this.InnerPadding);
		}

		/// <summary>
		/// Return the name of the algorithm the MAC implements.
		/// </summary>
		public virtual string AlgorithmName()
		{
			return hash.AlgorithmName() + "/HMAC";
		}

		/// <summary>
		/// Reset the mac generator.
		/// </summary>
		public virtual void Reset()
		{
			// Reset underlying digest
			this.hash.Reset();

			if (this.KeyValue == null)
			{
				this._Initilized = false;
			}
			else
			{
				this.Initialize(KeyValue);
			}

			//if (this.InnerPadding.Length >= 0 || this.InnerPadding != null)
			//{
			//	this.InnerPadding.Clear();
			//}

			//if (this.OuterPadding.Length >= 0 || this.OuterPadding != null)
			//{
			//	this.OuterPadding.Clear();
			//}

			//if (this.KeyValue.Length  >= 0 || this.KeyValue != null)
			//{
			//	this.KeyValue.Clear();
			//	this.KeyValue = null;
			//}
			
			// Initialise the digest
			//this.digest.Update(inputPad, 0, inputPad.Length);
		}

		/// <summary>
		/// Return the size (in bytes) of the hash value produced 
		/// by this hash function.
		/// </summary>
		/// <returns></returns>
		public int GetHashLength()
		{
			return this.hash.GetHashLength();
		}

		/// <summary>
		/// Get used hash funtion in the MAC.
		/// </summary>
		/// <returns></returns>
		public IHash GetHashFunction()
		{
			return this.hash;
		}

		protected virtual IHash GetUnderlyingDigest()
		{
			return hash;
		}

		protected virtual int GetMacSize()
		{
			return this.DigestSize;
		}

		/// <summary>
		/// Update the HMAC value with a array of bytes.
		/// </summary>
		/// <param name="input">Input byte array to be hashed.</param>
		/// <returns></returns>
		public void Update(byte[] input)
		{
			this.Update(input, 0, input.Length);
		}

		/// <summary>
		/// Update the HMAC value with a string.
		/// </summary>
		/// <param name="input">Input string to be hashed.</param>
		/// <returns></returns>
		public void Update(string input)
		{
			this.Update(input.GetBytes(), 0, input.Length);
		}

		/// <summary>
		/// Update the HMAC value with a array of bytes.
		/// </summary>
		/// <param name="input">Input array of bytes to be hashed.</param>
		/// <param name="start_index">Offset where the data starts.</param>
		/// <param name="length">Length of data.</param>
		/// <returns></returns>
		public void Update(byte[] input, int start_index, int length)
		{
			if (this._Initilized == false)
			{
				throw new Exception("Please initialize the HMAC first.");
			}

			this.hash.Update(input, start_index, length);
		}

		/// <summary>
		/// Copy final HMAC value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <returns></returns>
		public int DoFinal(byte[] output)
		{
			return this.DoFinal(output, 0);
		}

		/// <summary>
		/// Copy final HMAC value and reset the hash function.
		/// </summary>
		/// <param name="output">Byte array the hash value is to be copied into.</param>
		/// <param name="start_index">Offset into the output array the hash value is to start at.</param>
		/// <returns></returns>
		public int DoFinal(byte[] output, int start_index)
		{
			//1st pass
			this.hash.DoFinal(output, start_index);

			//2nd pass
			this.hash.Reset();
			this.hash.Update(this.OuterPadding, 0, this.OuterPadding.Length);
			this.hash.Update(output);
			this.hash.DoFinal(output, start_index);

			//reset HMAC 
			this.Reset();

			return this.hash.GetHashLength();
		}

		/// <summary>
		/// Computes the HMAC value for the specified byte array.
		/// </summary>
		/// <param name="keys">Secret keys.</param>
		/// <param name="data">Array bytes to be authenticated.</param>
		/// <returns></returns>
		public byte[] ComputeHash(byte[] keys, byte[] data)
		{
			this.InitializeKey(keys);
			this.hash.Reset();

			var result = new byte[this.hash.GetHashLength()];

			//1st pass
			this.hash.Update(this.InnerPadding, 0, this.InnerPadding.Length);
			this.hash.Update(data, 0, data.Length);
			this.hash.DoFinal(result, 0);

			//2nd pass
			this.hash.Reset();
			this.hash.Update(this.OuterPadding, 0, this.OuterPadding.Length);
			this.hash.Update(result);
			this.hash.DoFinal(result, 0);

			return result;
		}

		/// <summary>
		/// Computes the HMAC value for the specified byte array.
		/// </summary>
		/// <param name="keys">Secret keys.</param>
		/// <param name="data">Array bytes to be authenticated.</param>
		/// <returns></returns>
		public byte[] ComputeHash(string keys, string data)
		{
			return this.ComputeHash(keys.GetBytes(), data.GetBytes());
		}

		#endregion Public
	}
}
