using System;

using QPass.Core.Utilities.Extension;
using QPass.Crypto;

namespace QPass.Entities
{
	/// <summary>
	/// 
	/// </summary>
	public class Account
	{
		#region Member

		protected ulong _ID;
		protected string _Title;
		protected string _Username;
		protected string _Password;
		protected byte[] _Key;
		protected byte[] _IV;
		protected byte[] _Salt;
		protected string _Note;
		protected DateTime _Created;
		protected DateTime _Modified;
		protected ulong _GroupID;

		private byte _SaltLength = 64; //512 bit
		private byte _IVLength = 64; //512 bit
		private byte _KeyLength = 64; //512 bit
		private string _DateTimeFormat = "yyyy-mm-dd";

		private bool _Encrypted = false;

		#endregion Member

		#region Constructor & Destructor

		/// <summary>
		/// Create new account.
		/// </summary>
		public Account()
		{
			this._ID = 0;
			this._Title = "QPass";
			this._Username = "";
			this._Password = "";
			this._Salt = this.CreateSalt();
			this._IV = this.CreateIV();
			this._Note = "";
			this._Created = DateTime.Now;
			this._Modified = this._Created;
			this._GroupID = 1;
		}

		/// <summary>
		/// create new account
		/// </summary>
		/// <param name="master_password"></param>
		public Account(byte[] master_password)
		{
			this._ID = 0;
			this._Title = "QPass";
			this._Username = "";
			this._Password = "";
			this._Salt = this.CreateSalt();
			this._IV = this.CreateIV();
			this._Key = this.CreateKey(master_password);
			this._Note = "";
			this._Created = DateTime.Now;
			this._Modified = this._Created;
			this._GroupID = 1;
		}

		/// <summary>
		/// Create new account.
		/// </summary>
		/// <param name="id">Account id.</param>
		/// <param name="title">Account title.</param>
		/// <param name="username">Account username.</param>
		/// <param name="password">Account password.</param>
		public Account(ulong id, string title = "", string username = "", string password = "")
		{
			this._ID = id;
			this._Title = title;
			this._Username = username;
			this._Password = password;
			this._Salt = this.CreateSalt();
			this._IV = this.CreateIV();
			this._Note = "";
			this._Created = DateTime.Now;
			this._Modified = this._Created;
			this._GroupID = 0;
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~Account()
		{
			this.Reset();
		}

		#endregion Constructor & Destructor

		#region Private

		/// <summary>
		/// Create a random salt with cryptographic random number.
		/// </summary>
		/// <returns></returns>
		private byte[] CreateSalt()
		{
			QPass.Core.Security.RNG.CSPRNG.CryptGenRandom rng;
			rng = new QPass.Core.Security.RNG.CSPRNG.CryptGenRandom();
			return rng.GetBytes(this._SaltLength);
		}

		/// <summary>
		/// Create a random IV with cryptographic random number.
		/// </summary>
		/// <returns></returns>
		private byte[] CreateIV()
		{
			QPass.Core.Security.RNG.CSPRNG.CryptGenRandom rng;
			rng = new QPass.Core.Security.RNG.CSPRNG.CryptGenRandom();
			return rng.GetBytes(this._IVLength);
		}

		/// <summary>
		/// Create key.
		/// </summary>
		/// <param name="master_password"></param>
		/// <returns></returns>
		private byte[] CreateKey(byte[] master_password)
		{
			byte[] result = new byte[this._KeyLength];
			QPass.Core.Security.Hash.Blake2b hash = new QPass.Core.Security.Hash.Blake2b();
			hash.Update(master_password);
			hash.Update(this._Salt);
			hash.Update(this._IV);
			hash.DoFinal(result);
			return result;
		}

		/// <summary>
		/// encrypt data
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private byte[] Encrypt(byte[] data)
		{
			QPDBCrypto cipher = new QPDBCrypto();
			return cipher.Encrypt(data, this._Key, this._IV);
		}

		/// <summary>
		/// decrypt data
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		private byte[] Decrypt(byte[] data)
		{
			QPDBCrypto cipher = new QPDBCrypto();
			return cipher.Decrypt(data, this._Key, this._IV);
		}
		
		#endregion Private

		#region Public

		/// <summary>
		/// Clear all used resource.
		/// </summary>
		public void Reset()
		{
			this._ID = this._GroupID = 0;
			this._Title = this._Username = 
			this._Password = this._Note = "";
			if (this._Key != null)
			{
				Array.Clear(this._Key, 0, this._Key.Length);
			}
			
			Array.Clear(this._Salt, 0, this._Salt.Length);
			Array.Clear(this._IV, 0, this._IV.Length);
		}

		/// <summary>
		/// Set account ID.
		/// </summary>
		/// <param name="id">Account id.</param>
		public void SetID(ulong id)
		{
			if (id <= 0)
			{
				throw new Exception("ID cant lower or equal than zero (0).");
			}

			this._ID = id;
		}

		/// <summary>
		/// Get account id.
		/// </summary>
		/// <returns></returns>
		public ulong GetID()
		{
			return this._ID;
		}

		/// <summary>
		/// Set account group id.
		/// </summary>
		/// <param name="id">Account group id.</param>
		public void SetGroup(ulong id)
		{
			if (id < 0)
			{
				throw new Exception("ID cant lower or equal than zero (0).");
			}

			this._GroupID = id;
		}

		/// <summary>
		/// Get account group id.
		/// </summary>
		/// <returns></returns>
		public ulong GetGroup()
		{
			return this._GroupID;
		}

		/// <summary>
		/// Set account title.
		/// </summary>
		/// <param name="username">Account title.</param>
		public void SetTitle(string title)
		{
			if (title == null || title.Trim() == "" || title == string.Empty)
			{
				throw new Exception("Title can't empty.");
			}
			this._Title = title;
		}

		/// <summary>
		/// Get account title.
		/// </summary>
		/// <returns></returns>
		public string GetTitle()
		{
			return this._Title;
		}

		/// <summary>
		/// Set account username.
		/// </summary>
		/// <param name="username">Account username.</param>
		public void SetUsername(string username)
		{
			if (username == null || username.Trim() == "" || username == string.Empty)
			{
				throw new Exception("Username can't empty.");
			}
			this._Username = username;
		}

		/// <summary>
		/// Get account username.
		/// </summary>
		/// <returns></returns>
		public string GetUsername()
		{
			return this._Username;
		}

		/// <summary>
		/// Set account password.
		/// </summary>
		/// <param name="password">Account password.</param>
		public void SetPassword(string password)
		{
			if (password == null || password.Trim() == "" || password  == string.Empty)
			{
				throw new Exception("Password can't empty.");
			}
			this._Password = password;
		}

		/// <summary>
		/// Get account password.
		/// </summary>
		/// <returns></returns>
		public string GetPassword()
		{
			return this._Password;
		}

		/// <summary>
		/// Set account key.
		/// </summary>
		/// <param name="master_password">Account master password.</param>
		public void SetKey(byte[] master_password)
		{
			this._Key = this.CreateKey(master_password);
		}

		/// <summary>
		/// Get account key.
		/// </summary>
		/// <returns></returns>
		public byte[] GetKey()
		{
			return this._Key;
		}

		/// <summary>
		/// Set account salt.
		/// </summary>
		/// <param name="salt">Account salt.</param>
		public void SetSalt(byte[] salt)
		{
			if (salt == null)
			{
				throw new Exception("Salt can't empty.");
			}

			if (salt.Length < this._SaltLength)
			{
				throw new Exception("Salt is not long enough.");
			}

			this._Salt = new byte[this._SaltLength];
			Array.Copy(salt, this._Salt, this._SaltLength);
		}

		/// <summary>
		/// Set account salt.
		/// </summary>
		/// <param name="salt">Account salt.</param>
		public void SetSalt(string salt)
		{
			if (salt == null)
			{
				throw new Exception("Salt can't empty.");
			}

			if (salt.Length / 2 < this._SaltLength)
			{
				throw new Exception("Salt is not long enough.");
			}
			this._Salt = salt.DecodeBase16();
		}

		/// <summary>
		/// Get account salt.
		/// </summary>
		/// <returns></returns>
		public byte[] GetSalt()
		{
			return this._Salt;
		}

		/// <summary>
		/// Get salt length.
		/// </summary>
		/// <returns></returns>
		public int GetSaltLength()
		{
			return this._SaltLength;
		}

		/// <summary>
		/// Set account IV.
		/// </summary>
		/// <param name="iv">Account IV.</param>
		public void SetIV(byte[] iv)
		{
			if (iv == null)
			{
				throw new Exception("IV can't empty.");
			}

			if (iv.Length < this._IVLength)
			{
				throw new Exception("IV is not long enough.");
			}

			this._IV = new byte[this._IVLength];
			Array.Copy(iv, this._IV, this._IVLength);
		}
		
		/// <summary>
		/// Set account IV.
		/// </summary>
		/// <param name="iv">Account IV.</param>
		public void SetIV(string iv)
		{
			if (iv == null)
			{
				throw new Exception("IV can't empty.");
			}

			if (iv.Length / 2 < this._IVLength)
			{
				throw new Exception("IV is not long enough.");
			}
			this._IV = iv.DecodeBase16();
		}

		/// <summary>
		/// Get account IV.
		/// </summary>
		/// <returns></returns>
		public byte[] GetIV()
		{
			return this._IV;
		}

		/// <summary>
		/// Get account IV length.
		/// </summary>
		/// <returns></returns>
		public int GetIVLength()
		{
			return this._IVLength;
		}

		/// <summary>
		/// Set account note.
		/// </summary>
		/// <param name="note">Note to set.</param>
		public void SetNote(string note)
		{
			if (note == null)
			{
				note = "";
			}

			this._Note = note.Trim();
		}

		/// <summary>
		/// Get account note.
		/// </summary>
		/// <returns></returns>
		public string GetNote()
		{
			return this._Note;
		}

		/// <summary>
		/// Set account created date.
		/// </summary>
		/// <param name="date">Created date.</param>
		public void SetCreatedDate(DateTime date)
		{
			this._Created = date;
		}

		/// <summary>
		/// Set account created date.
		/// </summary>
		/// <param name="date">Created date.</param>
		public void SetCreatedDate(string date)
		{
			this._Created = DateTime.Parse(date);
		}

		/// <summary>
		/// Get account created date.
		/// </summary>
		/// <returns></returns>
		public DateTime GetCreatedDate()
		{
			return this._Created;
		}

		/// <summary>
		/// Get account created date.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string GetCreatedDate(bool str = true)
		{
			return this._Created.ToString(this._DateTimeFormat);
		}

		/// <summary>
		/// Set account modified date.
		/// </summary>
		/// <param name="date">Modified date.</param>
		public void SetModifiedDate(DateTime date)
		{
			this._Modified = date;
		}

		/// <summary>
		/// Set account modified date.
		/// </summary>
		/// <param name="date">Modified date.</param>
		public void SetModifiedDate(string date)
		{
			this._Modified  = DateTime.Parse(date);
		}

		/// <summary>
		/// Get account modified date.
		/// </summary>
		/// <returns></returns>
		public DateTime GetModifiedDate()
		{
			return this._Modified;
		}

		/// <summary>
		/// Get account modified date.
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public string GetModifiedDate(bool str = true)
		{
			return this._Created.ToString(this._DateTimeFormat);
		}

		#endregion Public

		#region Method
		
		/// <summary>
		/// Encrypt account data.
		/// </summary>
		/// <returns></returns>
		public bool EncryptData()
		{
			try
			{
				string temp_passsword = this._Password + this._Salt.EncodeBase16();
				this._Username = this.Encrypt(this._Username.GetBytes()).EncodeBase16();
				this._Password = this.Encrypt(temp_passsword.GetBytes()).EncodeBase16();
				this._Note = this.Encrypt(this._Note.GetBytes()).EncodeBase16();
				return true;
			}
			catch
			{
				return false;
			}
		}

		/// <summary>
		/// Decrypt account data.
		/// </summary>
		/// <returns></returns>
		public bool DecryptData()
		{
			try
			{
				this._Username = this.Decrypt(this._Username.DecodeBase16()).GetString();
				this._Password = this.Decrypt(this._Password.DecodeBase16()).GetString();
				this._Note = this.Decrypt(this._Note.DecodeBase16()).GetString();
				this._Password = this._Password.Replace(this._Salt.EncodeBase16(),"");

				return true;
			}
			catch
			{
				return false;
			}
		}

		#endregion Method
	}
}
