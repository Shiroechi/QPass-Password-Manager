using System;
using System.IO;

using QPass.Core.Utilities.Extension;
using QPass.Database;

namespace QPass.Entities
{
	public class FileManager
	{
		#region Member

		private string _Location; 
		private QPDB _QPDBFile; //QPDB file
		private string _Extension = ".QPDB";

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
		/// Destructor.
		/// </summary>
		~FileManager()
		{
			this._Location = string.Empty;
		}

		#endregion Constructor & Destructor

		#region Protected

		/// <summary>
		/// write file to hard disk
		/// </summary>
		/// <param name="filename"></param>
		/// <param name="data"></param>
		public void Write(string filename, byte[] data)
		{
			using (FileStream fs = new FileStream(filename, FileMode.Create))
			{
				using (BinaryWriter bw = new BinaryWriter(fs))
				{
					bw.Write(data);
					fs.Close();
					bw.Close();
				}
			}
		}

		/// <summary>
		/// read file from hard disk
		/// </summary>
		/// <param name="filename"></param>
		/// <returns></returns>
		public byte[] Read(string filename)
		{
			if (File.Exists(filename) == false)
			{
				throw new Exception("File not exist.");
			}

			byte[] buffer;

			using (FileStream fs = new FileStream(filename, FileMode.Open))
			{
				using (BinaryReader br = new BinaryReader(fs))
				{
					buffer = br.ReadBytes((int)br.BaseStream.Length);
					fs.Close();
					br.Close();
				}
			}

			return buffer;
		}

		/// <summary>
		/// save file
		/// </summary>
		protected void Save()
		{
			byte[] buffer = Core.Utilities.Convert.Objects.Serialize(this._QPDBFile);
			this.Write(this._Location, buffer);
			System.Array.Clear(buffer, 0, buffer.Length);
		}

		/// <summary>
		/// load file
		/// </summary>
		protected void Load()
		{
			//read or load QPDB file
			byte[] buffer = this.Read(this._Location);
			this._QPDBFile = (QPDB)QPass.Core.Utilities.Convert.Objects.Deserialize(buffer);

			//delete QPDB file in hard disk
			File.Delete(this._Location);

			System.Array.Clear(buffer, 0, buffer.Length);
		}

		#endregion Protected

		#region Public

		public void SetMasterPassword(byte[] password)
		{
			this._QPDBFile.SetMasterPassword(password);
		}

		public bool ValidMasterPassword(byte[] password)
		{
			bool valid = true;
			byte[] temp = this._QPDBFile.GetMasterPassword();

			for (int i = 0; i < password.Length; i++)
			{
				if (password[i] != temp[i])
				{
					valid = false;
					break;
				}
			}
			return valid;
		}

		#endregion Public
	}
}
