using System;

using QPass.Core.Utilities.Extension;
using QPass.Crypto;

namespace QPass.Database
{
	/// <summary>
	/// QPass Database
	/// </summary>
	[Serializable]
	class QPDB
	{
		#region Member

		private byte[] _Header;
		private byte[] _Checksum;
		private byte[] _MasterPassword;
		private byte[] _DatabaseFile;

		#endregion Member

		#region Constructor & Destructor

		public QPDB()
		{
			this._Header = Properties.Resources.Header.DecodeBase16();
		}

		public QPDB(byte[] password, byte[] database, byte[] checksum)
		{
			this._Header = Properties.Resources.Header.DecodeBase16();
			this.SetMasterPassword(password);
			this.SetDatabaseFile(database);
			this.SetChecksum(checksum);
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~QPDB()
		{
			Array.Clear(this._Header, 0, this._Header.Length);
			Array.Clear(this._MasterPassword, 0, this._MasterPassword.Length);
			Array.Clear(this._DatabaseFile, 0, this._DatabaseFile.Length);
			Array.Clear(this._Checksum, 0, this._Checksum.Length);
		}

		#endregion Constructor & Destructor

		#region Protected

		protected byte[] CreateChecksum()
		{
			var hash = new Core.Security.Hash.Blake2b(512);
			return hash.ComputeHash(this._DatabaseFile);
		}
		
		#endregion Protected

		#region Public

		public bool CheckHeader()
		{
			return this._Header == Properties.Resources.Header.DecodeBase16(); 
		}

		public void SetMasterPassword(byte[] password)
		{
			if (password == null || password.Length == 0)
			{
				throw new Exception("Password can't empty.");
			}

			this._MasterPassword = password;
		}

		public byte[] GetMasterPassword()
		{
			return this._MasterPassword;
		}

		public void SetDatabaseFile(byte[] database)
		{
			if (database == null || database.Length == 0)
			{
				throw new Exception("Database can't empty.");
			}

			this._DatabaseFile = database;
			this.SetChecksum (this.CreateChecksum());
		}

		public byte[] GetDatabaseFile()
		{
			return this._DatabaseFile;
		}

		public void SetChecksum(byte[] checksum)
		{
			if (checksum == null || checksum.Length == 0)
			{
				throw new Exception("Checksum can't empty.");
			}

			this._Checksum = checksum;
		}

		public byte[] GetChecksum()
		{
			return this._Checksum;
		}
		
		#endregion Public

	}
}
