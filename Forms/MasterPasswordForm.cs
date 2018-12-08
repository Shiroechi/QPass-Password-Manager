using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using QPass.Core.Security.Hash;
using QPass.Core.Security.KDF;
using QPass.Core.Utilities.Extension;
using QPass.Database;

namespace QPass.Forms
{
	public partial class MasterPasswordForm : Form
	{
		#region Member

		private byte[] _Password;
		private int _PasswordIteration = 10;
		private bool _Validate = false;
		private SQLiteDatabase _Database = null;
		private byte _MinimalPasswordLength = 8;
		private bool _NewDatabase = false;
		private bool _PasswordRequirement = false;
		
		#endregion Member

		public MasterPasswordForm()
		{
			InitializeComponent();
			this.PasswordTextBox.Text = "";
			this._NewDatabase = true;
			this.CancelButton.DialogResult = DialogResult.Cancel;
			this.OkButton.DialogResult = DialogResult.OK;
		}

		public MasterPasswordForm(SQLiteDatabase db)
		{
			InitializeComponent();
			this.PasswordTextBox.Text = "";
			this._Database = db;
			this.CancelButton.DialogResult = DialogResult.Cancel;
			this.OkButton.DialogResult = DialogResult.OK;
		}

		~MasterPasswordForm()
		{
			Array.Clear(this._Password, 0, this._Password.Length);
			this.PasswordTextBox.Text = "";
		}

		#region Form Event

		private void MasterPasswordForm_Load(object sender, EventArgs e)
		{
			//load font from setting
			this.Font = Properties.Settings.Default.Font;
		}

		private void OkButton_Click(object sender, EventArgs e)
		{
			if (this._NewDatabase == true)
			{
				//chek password requirement
				if (this.ValidatePassword(this.PasswordTextBox.Text.Trim()) == false)
				{
					//exit, if all condition is not met
					return;
				}
			}

			if (this._NewDatabase == false && this._Database != null)
			{
				this._Password = this.HashPassword(this.PasswordTextBox.Text.Trim());
				this._Validate = this._Database.CheckMasterPassword(this._Password);

				if (this._Validate == false)
				{
					this.PasswordTextBox.Text = "";
					this.ErrorMessageLabel.Visible = true;
					this.ErrorMessageLabel.Text = "Wrong password.";
				}
			}
		}

		private void CancelButton_Click(object sender, EventArgs e)
		{

		}

		private void PasswordTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				this.OkButton.PerformClick();
			}
		}

		#endregion Form Event

		/// <summary>
		/// Check if the password met the requirement
		/// </summary>
		/// <param name="password"></param>
		/// <param name="lower"></param>
		/// <param name="upper"></param>
		/// <param name="digit"></param>
		/// <param name="symbol"></param>
		/// <returns></returns>
		private bool ValidatePassword(string password, bool lower = true, bool upper = true, bool digit = true, bool symbol = false)
		{
			if (password.Length < this._MinimalPasswordLength)
			{
				MessageBox.Show("Password need at least " + this._MinimalPasswordLength.ToString() + " characters.", "Attention!!");
				this.ErrorMessageLabel.Visible = true;
				this.ErrorMessageLabel.Text = "Password need at least " + this._MinimalPasswordLength.ToString() + " characters.";
				return false;
			}

			Regex regex = new Regex("");
			if (lower == true)
			{
				regex = new Regex("[a-z]+");
				if (regex.IsMatch(password) == false)
				{
					//MessageBox.Show(this, "Password need at least 1 lower character.", "Attention!");
					this.ErrorMessageLabel.Visible = true;
					this.ErrorMessageLabel.Text = "Password need at least 1 lower character.";
					return false;
				}
			}

			if (upper == true)
			{
				regex = new Regex("[A-Z]+");
				if (regex.IsMatch(password) == false)
				{
					//MessageBox.Show(this, "Password need at least 1 upper character.", "Attention!");
					this.ErrorMessageLabel.Visible = true;
					this.ErrorMessageLabel.Text = "Password need at least 1 upper character.";
					return false;
				}
			}

			if (digit == true)
			{
				regex = new Regex("[0-9]+");
				if (regex.IsMatch(password) == false)
				{
					//MessageBox.Show(this, "Password need at least 1 digit number.", "Attention!");
					this.ErrorMessageLabel.Visible = true;
					this.ErrorMessageLabel.Text = "Password need at least 1 digit number.";
					return false;
				}
			}

			if (symbol == true)
			{
				regex = new Regex("[!@#$%^&*()_+=\\[{]};:<>|./?,-]");
				if (regex.IsMatch(password) == false)
				{
					MessageBox.Show(this, "Password need at least 1 symbol.", "Attention!");
					this.ErrorMessageLabel.Visible = true;
					this.ErrorMessageLabel.Text = "Password need at least 1 symbol.";
					return false;
				}
			}

			this.ErrorMessageLabel.Visible = false;
			this._PasswordRequirement = true;
			return true;
		}

		/// <summary>
		/// hash the password
		/// </summary>
		/// <param name="password"></param>
		/// <returns></returns>
		private byte[] HashPassword(string password)
		{
			//Org.BouncyCastle.Crypto.Digests.Sha3Digest sha = new Org.BouncyCastle.Crypto.Digests.Sha3Digest(512);
			//this._Password = new byte[sha.GetByteLength()];
			//sha.BlockUpdate(password.GetBytes(), 0, password.Length);
			//sha.DoFinal(this._Password, 0);
			//return this._Password;

			Blake2b hash = new Blake2b(512);
			//this._Password = new byte[sha.GetHashLength()];
			//sha.Update(password);
			//sha.DoFinal(this._Password, 0);
			//return this._Password;

			PBKDF2 pbkdf = new PBKDF2(hash, this._PasswordIteration);
			return pbkdf.Derive(password.GetBytes(), null, (this._PasswordIteration) * hash.GetHashLength());
			
		}
		
		/// <summary>
		/// get hashed master password
		/// </summary>
		/// <returns></returns>
		public byte[] GetMasterPassword()
		{
			return this.HashPassword(this.PasswordTextBox.Text.Trim());
			//return this._Password;
		}

		/// <summary>
		/// check inputed password and registered password
		/// </summary>
		/// <returns></returns>
		public bool IsValid()
		{
			return this._Validate;
		}

		/// <summary>
		/// return is password requirement met or not.
		/// </summary>
		/// <returns></returns>
		public bool PasswordRequiment()
		{
			return this._PasswordRequirement;
		}
	}
}
