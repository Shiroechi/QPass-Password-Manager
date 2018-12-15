using System;
using System.Windows.Forms;

using QPass.Database;
using QPass.Entities;

namespace QPass.Forms
{
	public partial class AccountForm : Form
	{
		#region Member

		private bool _NewAccount = false;
		private Account _Account;
		private SQLiteDatabase _Database;
		private PasswordStatus _PasswordStatus = PasswordStatus.Show;

		#endregion Member

		public AccountForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// edit existing account
		/// </summary>
		/// <param name="id"></param>
		/// <param name="db"></param>
		public AccountForm(int id, SQLiteDatabase db, byte[] master_password)
		{
			InitializeComponent();
			this._Database = db;
			this._Account = this._Database.GetAccount(id);
			this._Account.SetKey(master_password);
			this._Account.DecryptData();
			this.LoadData();
		}

		/// <summary>
		/// creating new account
		/// </summary>
		/// <param name="db"></param>
		/// <param name="master_password"></param>
		public AccountForm(SQLiteDatabase db, byte[] master_password)
		{
			InitializeComponent();
			this._NewAccount = true;
			this._Account = new Account(master_password);
			this._Database = db;
		}

		~AccountForm()
		{
			this._Account.Reset();
		}

		public void LoadData()
		{
			this.TitleTextBox.Text = this._Account.GetTitle();
			this.UsernameTextBox.Text = this._Account.GetUsername();
			this.PasswordTextBox.Text = this._Account.GetPassword();
			this.NoteTextBox.Text = this._Account.GetNote();
		}

		#region Form Event

		private void AccountForm_Load(object sender, EventArgs e)
		{
			////load form mazimun size from setting
			//int count = Properties.Settings.Default.Resolution.Count;
			//string[] temp_resolution = new string[count];
			//Properties.Settings.Default.Resolution.CopyTo(temp_resolution, 0);
			//int width = int.Parse(temp_resolution[count - 1].Substring(0, temp_resolution[count - 1].IndexOf("x")).Trim()) + 16;
			//int height = int.Parse(temp_resolution[count - 1].Substring(temp_resolution[count - 1].IndexOf("x") + 1).Trim()) + 39;
			//this.MaximumSize = new Size(width, height);

			//load font from setting
			this.Font = Properties.Settings.Default.Font;

			//hide password by default
			this.PasswordTextBox.PasswordChar = '*';
			this._PasswordStatus = PasswordStatus.Hide;
			this.PasswordStatusButton.Image = Properties.Resources.icons8_eye_32;
		}
		
		private void SaveButton_Click(object sender, EventArgs e)
		{
			#region Error Handling

			if (this.TitleTextBox.Text.Trim() == "")
			{
				this.ErrorMessageLabel.Visible = true;
				this.ErrorMessageLabel.Text = "Title can't empty.";
				return;
			}

			if (this.UsernameTextBox.Text.Trim() == "")
			{
				this.ErrorMessageLabel.Visible = true;
				this.ErrorMessageLabel.Text = "Username can't empty.";
				return;
			}

			if (this.PasswordTextBox.Text.Trim() == "")
			{
				this.ErrorMessageLabel.Visible = true;
				this.ErrorMessageLabel.Text = "Password can't empty.";
				return;
			}

			this.ErrorMessageLabel.Visible = false;

			#endregion Error Handling

			#region Set new data

			if (this._Account.GetTitle() != this.TitleTextBox.Text)
			{
				this._Account.SetTitle(this.TitleTextBox.Text);
			}

			if (this._Account.GetUsername() != this.UsernameTextBox.Text)
			{
				this._Account.SetUsername(this.UsernameTextBox.Text);
			}

			if (this._Account.GetPassword() != this.PasswordTextBox.Text)
			{
				this._Account.SetPassword(this.PasswordTextBox.Text);
			}

			if (this._Account.GetNote() != this.NoteTextBox.Text)
			{
				this._Account.SetNote(this.NoteTextBox.Text);
			}
			
			this._Account.SetModifiedDate(DateTime.Now);

			#endregion Set new data

			if (this._NewAccount == true)
			{
				//insert new account
				this._Account.EncryptData();
				this._Database.InsertAccount(this._Account);
			}
			else
			{
				//update account
				this._Account.EncryptData();
				this._Database.UpdateAccount(this._Account);
			}

			this.Close();
		}

		private void PasswordStatusButton_Click(object sender, EventArgs e)
		{
			//hide password
			if (this._PasswordStatus == PasswordStatus.Show)
			{
				this.PasswordTextBox.PasswordChar = '*';
				this._PasswordStatus = PasswordStatus.Hide;
				this.PasswordStatusButton.Image = Properties.Resources.icons8_eye_32;
				return;
			}

			//show password
			if (this._PasswordStatus == PasswordStatus.Hide)
			{
				this.PasswordTextBox.PasswordChar = '\0';
				this._PasswordStatus = PasswordStatus.Show;
				this.PasswordStatusButton.Image = Properties.Resources.icons8_eye_hide_32;
				return;
			}
		}

		#endregion Form Event
	}

	public enum PasswordStatus
	{
		Show, Hide
	}
}
