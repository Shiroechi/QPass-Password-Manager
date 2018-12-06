using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Crypto.Parameters;

using QPass.Core.Utilities.Extension;

namespace QPass.Crypto
{
	class QPDBCrypto
	{
		public QPDBCrypto()
		{

		}

		#region Private

		private void Check(byte[] key, byte[] iv)
		{
			if (key == null)
			{
				throw new System.Exception("Key can't null.");
			}
			if (key.Length < 32)
			{
				throw new System.Exception("Key not 256 bit.");
			}
			if (iv == null)
			{
				throw new System.Exception("IV can't null.");
			}
			if (iv.Length < 32)
			{
				throw new System.Exception("IV not 256 bit.");
			}
		}

		#endregion Private

		#region Public

		/// <summary>
		/// encrypt data using rijndael
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public byte[] Encrypt(byte[] data, byte[] key, byte[] iv)
		{
			this.Check(key, iv);
			//Set up
			//AesEngine engine = new AesEngine();
			RijndaelEngine engine = new RijndaelEngine(256);
			CbcBlockCipher blockCipher = new CbcBlockCipher(engine); //CBC
			PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding()); //Default scheme is PKCS5/PKCS7
			KeyParameter keyParam = new KeyParameter(key.SubByte(0, 32));
			ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, iv, 0, 32);

			// Encrypt
			cipher.Init(true, keyParamWithIV);
			byte[] outputBytes = new byte[cipher.GetOutputSize(data.Length)];
			int length = cipher.ProcessBytes(data, outputBytes, 0);
			cipher.DoFinal(outputBytes, length); //Do the final block

			//string encryptedInput = outputBytes.EncodeBase16();//Convert.ToBase64String(outputBytes);
			return outputBytes;
		}

		/// <summary>
		/// decrypt data using rijdael
		/// </summary>
		/// <param name="data"></param>
		/// <param name="key"></param>
		/// <param name="iv"></param>
		/// <returns></returns>
		public byte[] Decrypt(byte[] data, byte[] key, byte[] iv)
		{
			this.Check(key, iv);
			//Set up
			//AesEngine engine = new AesEngine();
			RijndaelEngine engine = new RijndaelEngine(256);
			CbcBlockCipher blockCipher = new CbcBlockCipher(engine); //CBC
			PaddedBufferedBlockCipher cipher = new PaddedBufferedBlockCipher(blockCipher, new Pkcs7Padding()); //Default scheme is PKCS5/PKCS7
			KeyParameter keyParam = new KeyParameter(key.SubByte(0, 32));
			ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, iv, 0, 32);

			cipher.Init(false, keyParamWithIV);
			byte[] outputBytes = new byte[cipher.GetOutputSize(data.Length)];
			int length = cipher.ProcessBytes(data, outputBytes, 0);
			cipher.DoFinal(outputBytes, length); //Do the final block
			return outputBytes;
		}

		#endregion Public

	}
}
