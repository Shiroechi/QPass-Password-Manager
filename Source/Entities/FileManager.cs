using System;
using System.IO;

using QPass.Core.Utilities.Extension;
using QPass.Database;

namespace QPass.Entities
{
	class FileManager
	{
		#region Member

		private string _Location; 
		private byte[] _File; //QPDB

		#endregion Member

		#region Constructor & Destructor

		/// <summary>
		/// 
		/// </summary>
		/// <param name="location">Location to load and save data.</param>
		public FileManager(string location = "")
		{
			this._Location = location;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <param name="file"></param>
		protected FileManager(string location, byte[] file)
		{
			this._Location = location;
			this._File = file;
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~FileManager()
		{
			Array.Clear(this._File, 0, this._File.Length);
			this._Location = string.Empty;
		}

		#endregion Constructor & Destructor

		#region Protected

		protected void Save()
		{
			using (FileStream fs = new FileStream(this._Location, FileMode.Create))
			{
				using (BinaryWriter bw = new BinaryWriter(fs))
				{
					bw.Write(this._File);
					fs.Close();
					bw.Close();
				}
			}
		}

		protected void Load()
		{
			if (File.Exists(this._Location) == false)
			{
				throw new Exception("File not exist.");
			}

			using (FileStream fs = new FileStream(this._Location, FileMode.Open))
			{
				using (BinaryReader br = new BinaryReader(fs))
				{
					this._File = br.ReadBytes((int)br.BaseStream.Length);
					fs.Close();
					br.Close();
				}
			}
		}
		
		#endregion Protected

		#region Public

		public void SaveData(QPDB qpdb)
		{
			if (qpdb == null)
			{
				throw new Exception("QPDB can't be null.");
			}

			try
			{
				this.SaveData(qpdb.GetBytes());
			}
			catch
			{
				throw new Exception("QPDB can't be serialize.");
			}
		}

		public void SaveData(byte[] file)
		{
			if (file == null || file.Length == 0)
			{
				throw new Exception("File can't empty.");
			}

			this._File = file;
			this.Save();
		}

		public void LoadData()
		{
			this.Load();
		}

		#endregion Public
	}
}
