using System;
using System.Windows.Forms;

using QPass.Core.Utilities.Extension;

namespace QPass.Forms
{
	public partial class QPassMainForm : Form
	{
		#region Member

		protected Database.SQLiteDatabase _Database = null;
		protected byte[] _MasterPassword = null;

		#endregion Member
		
		public QPassMainForm()
		{
			InitializeComponent();
			this.ViewToolStripMenuItem.Visible = false;
		}

		~QPassMainForm()
		{
			this.SaveToolStripMenuItem.PerformClick();
			this.AccountTable.Dispose();
			this.MainMenuStrip.Dispose();
			this.Dispose();
		}

		#region Main Form Event

		private void QPassMainForm_Load(object sender, EventArgs e)
		{
			//load form mazimun size from setting
			//int count = Properties.Settings.Default.Resolution.Count;
			//string[] temp_resolution = new string[count];
			//Properties.Settings.Default.Resolution.CopyTo(temp_resolution, 0);
			//int width = int.Parse(temp_resolution[count - 1].Substring(0, temp_resolution[count - 1].IndexOf("x")).Trim()) + 16;
			//int height = int.Parse(temp_resolution[count - 1].Substring(temp_resolution[count - 1].IndexOf("x") + 1).Trim()) + 39;
			//this.MaximumSize = new Size(width, height);
			
			//load form size from setting
			this.Width = Properties.Settings.Default.Width;
			this.Height = Properties.Settings.Default.Height;

			//load font from setting
			this.Font = Properties.Settings.Default.Font;
			this.MenuStrip.Font = Properties.Settings.Default.Font;
		}

		private void QPassMainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			var result = MessageBox.Show(this, "Close the application ?", "Warning!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

			if (result == DialogResult.Yes)
			{
				e.Cancel = false;
			}
			else
			{
				e.Cancel = true;
			}
		}

		private void QPassMainForm_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control)
			{
				if (e.KeyCode == Keys.N)
				{
					NewToolStripMenuItem.PerformClick();
				}

				if (e.KeyCode == Keys.O)
				{
					OpenToolStripMenuItem.PerformClick();
				}

				if (e.KeyCode == Keys.W)
				{
					this.CloseToolStripMenuItem.PerformClick();
				}
				
				if (e.KeyCode == Keys.S)
				{
					this.SaveToolStripMenuItem.PerformClick();
				}
				
				if (e.KeyCode == Keys.F4)
				{
					this.CloseToolStripMenuItem.PerformClick();
					this.Close();
				}
			}
		}

		#endregion Main Form Event

		#region File

		/// <summary>
		/// create new database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//close opened database 
			this.CloseToolStripMenuItem.PerformClick();

			bool new_database = false;
			using (MasterPasswordForm master = new MasterPasswordForm())
			{				
				while (true)
				{
					master.ShowDialog(this);
					if (master.DialogResult == DialogResult.OK)
					{
						if (master.PasswordRequiment() == true)
						{
							this._MasterPassword = master.GetMasterPassword();
							new_database = true;
							break;
						}
					}
					else
					{
						break;
					}
				}
			}

			string filename = "";
			if (new_database == true)
			{
				//create new database
				using (SaveFileDialog dialog = new SaveFileDialog())
				{
					dialog.Title = "Save database";
					dialog.InitialDirectory = Application.StartupPath;
					dialog.Filter = "SQLite database | *.db";
					dialog.ShowDialog(this);

					if (dialog.FileName.Trim() != "")
					{
						filename = dialog.FileName.Trim();
					}
				}
			}

			if (filename != "")
			{
				try
				{
					if (System.IO.File.Exists(filename))
					{
						System.IO.File.Delete(filename);
					}
				}
				catch
				{
					MessageBox.Show("Can't replace the file.");
					return;
				}
				
				//Database.SQLiteDatabase.CreateDatabase(dialog.FileName);
				this._Database = new Database.SQLiteDatabase();
				this._Database.CreateDatabase(filename);
				this._Database = new Database.SQLiteDatabase(filename);
				this._Database.CreateQPassDatabase(this._MasterPassword);

				//show account list
				this._Database.LoadAccount(ref this.AccountTable);
			}
		}

		/// <summary>
		/// open existing database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OpenToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//close opened database
			this.CloseToolStripMenuItem.PerformClick();

			string filename = "";

			//get database location
			using (OpenFileDialog dialog = new OpenFileDialog())
			{
				dialog.Title = "Open database";
				dialog.InitialDirectory = Application.StartupPath;
				dialog.Filter = "SQLite database | *.db";
				dialog.ShowDialog(this);

				if (dialog.FileName.Trim() != "")
				{
					filename = dialog.FileName.Trim();
				}
			}

			//open database
			if (filename.Trim() != "")
			{
				this._Database = new Database.SQLiteDatabase(filename);

				//check master password
				bool MasterPassword = false;
				while (true)
				{
					using (MasterPasswordForm master = new MasterPasswordForm(this._Database))
					{
						master.ShowDialog(this);
						if (master.DialogResult == DialogResult.OK)
						{
							if (master.IsValid() == true)
							{
								this._MasterPassword = master.GetMasterPassword();
								MasterPassword = true;
								break;
							}
						}
						else
						{
							break;
						}
					}
				}

				//if master password true, show all account
				if (MasterPassword == true)
				{
					//this.AccountTable.DataSource = this._Database.LoadAccount();
					this._Database.LoadAccount(ref this.AccountTable);
				}
			}
		}

		/// <summary>
		/// close opened database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this._Database != null)
			{
				this._Database.CloseDatabase();
				this._Database = null;
				this.AccountTable.DataSource = null;
				this._MasterPassword = null;
				if (this._MasterPassword != null)
				{
					System.Array.Clear(this._MasterPassword, 0, this._MasterPassword.Length);
					this._MasterPassword = null;
				}
			}
		}

		/// <summary>
		/// save opened database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//save database
			this._Database.ExecuteQuery("vacuum");
		}

		/// <summary>
		/// quit from application
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void QuitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//close opened database / connected database
			this.CloseToolStripMenuItem.PerformClick();

			//close the application
			this.Close();
		}

		#endregion File

		#region Edit

		/// <summary>
		/// add new account into database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AddAccountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this._Database == null)
			{
				return;
			}

			//add new account
			using (Form form = new AccountForm(this._Database, this._MasterPassword))
			{
				form.ShowDialog(this);
				this._Database.LoadAccount(ref this.AccountTable);
			}
		}

		/// <summary>
		/// delete account in database
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DeleteAccountToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (this.AccountTable.CurrentRow != null)
			{
				//get id from selected cell or row in datagridview row
				int id = (int)this.AccountTable.CurrentRow.Cells[0].Value.ToString().ToInteger();
				this._Database.DeleteAccount(id); // delete account in database
				this.AccountTable.Rows.Remove(this.AccountTable.CurrentRow); //remove row from table
			}
		}

		#endregion Edit

		#region Tool

		private void GeneratePasswordToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (GenerateRandomPasswordForm form = new GenerateRandomPasswordForm())
			{
				form.ShowDialog(this);
			}
		}

		private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (FontDialog font = new FontDialog())
			{
				font.ShowDialog(this);
				this.Font = font.Font;
				Properties.Settings.Default.Font = font.Font;
			}
		}

		#endregion Tool 

		#region Help

		private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (Form about = new AboutForm())
			{
				about.ShowDialog(this);
			}
		}

		#endregion Help

		#region Account Table

		private void AccountTable_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex < 0 || e.ColumnIndex < 0)
			{
				return;
			}

			int id = int.Parse(this.AccountTable.Rows[e.RowIndex].Cells[0].Value.ToString());

			using (Form form = new AccountForm(id, this._Database, this._MasterPassword))
			{
				form.ShowDialog(this);
				this._Database.LoadAccount(ref this.AccountTable);
			}
		}

		private void AccountTable_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete && this.AccountTable.CurrentRow != null)
			{
				//delete selected account
				this.DeleteAccountToolStripMenuItem.PerformClick();
				////get id from selected cell or row in datagridview row
				//int id = (int)this.AccountTable.CurrentRow.Cells[0].Value.ToString().ToInteger();
				//this._Database.DeleteAccount(id);
				//this.AccountTable.Rows.Remove(this.AccountTable.CurrentRow);
			}
		}

		#endregion Account Table
	}
}
