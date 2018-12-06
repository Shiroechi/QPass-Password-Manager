using System;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using QPass.Entities;

using QPass.Core.Utilities.Extension;

namespace QPass.Database
{
	public class SQLiteDatabase
	{
		#region Member

		protected SQLiteConnection _Connection;

		#endregion Member

		#region Constructor & Destructor

		/// <summary>
		/// Create SQLite database instance.
		/// </summary>
		public SQLiteDatabase()
		{
			this._Connection = null;
		}

		/// <summary>
		/// Create SQLite database instance and 
		/// connect with the database.
		/// </summary>
		/// <param name="database_location">Database location.</param>
		public SQLiteDatabase(string database_location)
		{
			this.Connect(database_location);
		}

		/// <summary>
		/// Destructor.
		/// </summary>
		~SQLiteDatabase()
		{
			//this.Disconnect();
		}

		#endregion Constructor & Destructor

		#region Protected

		/// <summary>
		/// Open connection into database.
		/// </summary>
		/// <param name="database_location">Database location.</param>
		/// <returns></returns>
		protected bool Connect(string database_location)
		{
			try
			{
				string url = "Data Source=" + database_location + ";Version=3;";
				this._Connection = new SQLiteConnection(url);
				this._Connection.Open();
				return true;
			}
			catch (SQLiteException e)
			{
				//throw new SQLiteException(e.StackTrace);
				MessageBox.Show(e.Message);
				return false;
			}
		}

		/// <summary>
		/// Close database connection.
		/// </summary>
		/// <returns></returns>
		protected bool Disconnect()
		{
			try
			{
				if (this._Connection == null)
				{
					return true;
				}

				if (this._Connection.State != ConnectionState.Closed)
				{
					this._Connection.Close();
					this._Connection.Dispose();
				}
			}
			catch (SQLiteException e)
			{
				throw new SQLiteException(e.StackTrace);
			}
			return false;
		}

		#endregion Protected

		#region Public 

		/// <summary>
		/// Create new database.
		/// </summary>
		/// <param name="database_name">Database name.</param>
		/// <param name="database_location">Database location.</param>
		/// <returns></returns>
		public bool CreateDatabase(string database_name, string database_location)
		{
			return CreateDatabase(database_location + "\\" + database_name);
		}

		/// <summary>
		/// Create new database in specific location.
		/// </summary>
		/// <param name="location">Database location.</param>
		/// <returns></returns>
		public bool CreateDatabase(string location)
		{
			try
			{
				this._Connection = new SQLiteConnection("Data Source = " + location + "; Version = 3");
				this._Connection.Open();
				this._Connection.Close();
				return true;
			}
			catch (Exception e)
			{
				MessageBox.Show(e.Message);
				//throw new Exception(e.StackTrace);
				return false;
			}
		}

		/// <summary>
		/// Execute insert, update, create table, drop table query only.
		/// </summary>
		public void ExecuteQuery(string query)
		{
			if (this._Connection.State != System.Data.ConnectionState.Open)
			{
				throw new Exception("Database not connected.");
			}

			try
			{
				using (SQLiteCommand cmd = new SQLiteCommand(query, this._Connection))
				{
					cmd.ExecuteNonQuery();
				}
			}
			catch (SQLiteException e)
			{
				MessageBox.Show(e.Message);
				//throw new SQLiteException(e.Message);
			}
		}
		
		/// <summary>
		/// Create table.
		/// </summary>
		/// <param name="query"></param>
		public void CreateTable(string query)
		{
			this.ExecuteQuery(query);
		}

		/// <summary>
		/// Insert data into database.
		/// </summary>
		/// <param name="query"></param>
		public void InsertData(string query)
		{
			this.ExecuteQuery(query);
		}

		/// <summary>
		/// Read data from database.
		/// </summary>
		/// <param name="query"></param>
		/// <returns></returns>
		protected DataTable ReadData(string query)
		{
			if (this._Connection.State != System.Data.ConnectionState.Open)
			{
				throw new Exception("Database not connected.");
			}

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(query, this._Connection))
				{
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						using (System.Data.DataTable table = new System.Data.DataTable())
						{
							table.Load(reader);
							return table;
						}
					}
				}
			}
			catch (SQLiteException e)
			{
				MessageBox.Show(e.Message);
			}
			return null;
		}

		/// <summary>
		/// close opened database.
		/// </summary>
		public void CloseDatabase()
		{
			try
			{
				if (this._Connection.State == ConnectionState.Open)
				{
					this._Connection.Close();
					this._Connection.Dispose();
					this._Connection = null;
				}
			}
			catch (SQLiteException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		/// <summary>
		/// create all table that used in QPass 
		/// </summary>
		/// <param name="MasterPassword"></param>
		public void CreateQPassDatabase(byte[] MasterPassword)
		{

			string query = "CREATE TABLE MASTER ( " +
						   "ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
						   "PASSWORD BLOB NOT NULL)";
			this.ExecuteQuery(query);
			this.InsertMasterPassword(MasterPassword);

			query = "CREATE TABLE GROUPS " +
					"( " +
					"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
					"NAME TEXT NOT NULL, " +
					"DESCRIPTION TEXT " +
					")";
			this.CreateTable(query);

			query = "INSERT INTO GROUPS (NAME, DESCRIPTION) " +
					"VALUES ('General', 'Default group.');";
			this.ExecuteQuery(query);

			query = "CREATE TABLE ACCOUNT " +
					"(" +
					"ID INTEGER PRIMARY KEY AUTOINCREMENT, " +
					"TITLE TEXT NOT NULL, " +
					"USERNAME TEXT NOT NULL, " +
					"PASSWORD TEXT NOT NULL, " +
					"SALT TEXT NOT NULL, " +
					"IV TEXT NOT NULL, " +
					"NOTE TEXT, " +
					"CREATED TEXT NOT NULL, " +
					"MODIFIED TEXT NOT NULL, " +
					"GROUPID INTEGER NOT NULL, " +
					"FOREIGN KEY (GROUPID) REFERENCES GROUPS(ID) " +
					")";
			this.CreateTable(query);

		}

		#endregion Public

		#region Master

		/// <summary>
		/// insert master password to database
		/// </summary>
		/// <param name="password"></param>
		public void InsertMasterPassword(byte[] password)
		{
			if (this._Connection.State != System.Data.ConnectionState.Open)
			{
				throw new Exception("Database not connected.");
			}

			try
			{
				using (SQLiteCommand cmd = this._Connection.CreateCommand())
				{
					cmd.CommandText = "INSERT INTO MASTER (ID, PASSWORD) VALUES (NULL, @PASSWORD);";
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(new SQLiteParameter("@PASSWORD", password));
					cmd.ExecuteNonQuery();
				}
			}
			catch (SQLiteException e)
			{
				//MessageBox.Show(e.Message);
				throw new SQLiteException(e.Message);
			}
		}
		
		/// <summary>
		/// check inputed master password and registered master password
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		public bool CheckMasterPassword(byte[] password)
		{
			if (this._Connection.State != System.Data.ConnectionState.Open)
			{
				throw new Exception("Database not connected.");
			}
			
			string query = "";
			query = "SELECT PASSWORD FROM MASTER;";

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(query, this._Connection))
				{
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						byte[] temp = new byte[password.Length];
						while (reader.Read())
						{
							reader.GetBytes(0, 0, temp, 0, temp.Length);
						}

						reader.Close();

						for (int i = 0; i < password.Length; i++)
						{
							if (password[i] != temp[i])
							{
								return false;
							}
						}
						return true;
					}
				}
			}
			catch (SQLiteException e)
			{
				MessageBox.Show(e.Message);
			}
			return false;
		}

		#endregion Master

		#region Account

		/// <summary>
		/// insert new account
		/// </summary>
		/// <param name="data"></param>
		public void InsertAccount(Account data)
		{
			string query = "INSERT INTO ACCOUNT " +
						   "(" + 
						   "TITLE, " +
						   "USERNAME, " + 
						   "PASSWORD,  " + 
						   "SALT, " + 
						   "IV, " + 
						   "NOTE, " + 
						   "CREATED, " + 
						   "MODIFIED, " + 
						   "GROUPID" + 
						   ")" + 
						   "VALUES " +
						   "(" +
						   "'" + data.GetTitle() + "', " +
						   "'" + data.GetUsername() + "', " +
						   "'" + data.GetPassword() + "', " +
						   "'" + data.GetSalt().EncodeBase16() + "', " +
						   "'" + data.GetIV().EncodeBase16() + "', " +
						   "'" + data.GetNote() + "', " +
						   "'" + data.GetCreatedDate() + "', " +
						   "'" + data.GetModifiedDate() + "', " +
						   data.GetGroup() + 
						   ");";

			this.ExecuteQuery(query);
		}

		/// <summary>
		/// update existing account
		/// </summary>
		/// <param name="data"></param>
		public void UpdateAccount(Account data)
		{
			string query = "";

			query = "UPDATE ACCOUNT " +
					"SET TITLE = '" + data.GetTitle() + "', " +
					"USERNAME = '" + data.GetUsername() + "', " +
					"PASSWORD = '" + data.GetPassword() + "', " +
					"SALT = '" + data.GetSalt().EncodeBase16() + "', " +
					"IV = '" + data.GetIV().EncodeBase16() + "', " +
					"NOTE = '" + data.GetNote() + "', " +
					"CREATED = '" + data.GetCreatedDate() + "', " +
					"MODIFIED = '" + data.GetModifiedDate() + "', " +
					"GROUPID = " + data.GetGroup() + " " + 
					"WHERE ID = " + data.GetID() + ";";
			this.ExecuteQuery(query);
		}

		/// <summary>
		/// delete existing account
		/// </summary>
		/// <param name="account_id"></param>
		public void DeleteAccount(int account_id)
		{
			string query = "";
			query = "DELETE FROM ACCOUNT WHERE ID = " + account_id + ";";
			this.ExecuteQuery(query);
		}

		/// <summary>
		/// get existing account
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Account GetAccount(int id)
		{
			if (this._Connection.State != System.Data.ConnectionState.Open)
			{
				throw new Exception("Database not connected.");
			}

			string query = "";
			query = "SELECT * FROM ACCOUNT WHERE ID = " + id + ";";

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(query, this._Connection))
				{
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						Account account = new Account();

						while (reader.Read())
						{
							account.SetID((ulong)reader.GetInt64(0));
							account.SetTitle(reader.GetString(1));
							account.SetUsername(reader.GetString(2));
							account.SetPassword(reader.GetString(3));
							account.SetSalt(reader.GetString(4));
							account.SetIV(reader.GetString(5));
							account.SetNote(reader.GetString(6));
							account.SetCreatedDate(reader.GetString(7));
							account.SetModifiedDate(reader.GetString(8));
							account.SetGroup((ulong)reader.GetInt64(9));
						}

						reader.Close();
						return account;
					}
				}
			}
			catch (SQLiteException e)
			{
				MessageBox.Show(e.Message);
			}
			return null;
		}

		[Obsolete]
		public DataTable LoadAccount()
		{
			string query = "SELECT ID, TITLE FROM ACCOUNT";
			return this.ReadData(query).Clone();
		}

		/// <summary>
		/// load all account 
		/// </summary>
		/// <param name="table"></param>
		public void LoadAccount(ref DataGridView table)
		{
			string query = "SELECT ID, TITLE, CREATED, MODIFIED FROM ACCOUNT";
			if (this._Connection.State != System.Data.ConnectionState.Open)
			{
				throw new Exception("Database not connected.");
			}

			try
			{
				using (SQLiteCommand command = new SQLiteCommand(query, this._Connection))
				{
					using (SQLiteDataReader reader = command.ExecuteReader())
					{
						using (System.Data.DataTable dt = new System.Data.DataTable())
						{
							dt.Load(reader);
							table.DataSource = dt;
						}
					}
				}
			}
			catch (SQLiteException e)
			{
				MessageBox.Show(e.Message);
			}
		}

		#endregion Account
	}
}
