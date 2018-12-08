using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace QPass.Forms
{
	public partial class GenerateRandomPasswordForm : Form
	{
		#region Member

		private int _MinPasswordLength = 1;
		private int _MaxPasswordLength = 2048;

		private string _UpperLetter = "ABCDEFGHIJKLMNOPQRSTUVWXY";
		private string _LowerLetter = "abcdefghijklmnopqrstuvwxyz";
		private string _Numeric = "0123456789";
		private string _Symbol = " !#$%&()*+,-./:;<=>?@[]^_`{|}~";

		#endregion Member

		public GenerateRandomPasswordForm()
		{
			InitializeComponent();
			this.PasswordLength.Minimum = this._MinPasswordLength;
			this.PasswordLength.Maximum = this._MaxPasswordLength;
		}

		/// <summary>
		/// generate random password from specified parameter
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void GenerateButton_Click(object sender, EventArgs e)
		{
			this.PasswordParameterCheck();
		}

		/// <summary>
		/// copy generated password into clipboard
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CopyButton_Click(object sender, EventArgs e)
		{
			if (this.ResultTextBox.Text.Trim() == null || this.ResultTextBox.Text.Trim() == "")
			{
				return;
			}

			Clipboard.SetText(this.ResultTextBox.Text.Trim());
		}

		/// <summary>
		/// build password parameter
		/// </summary>
		private void PasswordParameterCheck()
		{
			bool default_parameter = false;
			if (this.UpperLetterCheckBox.Checked == false &&
			   this.LowerLetterCheckBox.Checked == false &&
			   this.NumericCheckBox.Checked == false &&
			   this.SymbolCheckBox.Checked == false)
			{
				default_parameter = true;
			}

			string parameter = "";
			if (default_parameter == true)
			{
				parameter += this._LowerLetter;
				parameter += this._UpperLetter;
				parameter += this._Numeric;
				parameter += this._Symbol;
			}

			if (this.LowerLetterCheckBox.Checked == true)
			{
				parameter += this._LowerLetter;
			}
			if (this.UpperLetterCheckBox.Checked == true)
			{
				parameter += this._UpperLetter;
			}
			if (this.NumericCheckBox.Checked == true)
			{
				parameter += this._Numeric;
			}
			if (this.SymbolCheckBox.Checked == true)
			{
				parameter += this._Symbol;
			}

			this.PasswordBuilder(parameter);
		}

		/// <summary>
		/// build random password
		/// </summary>
		/// <param name="parameter"></param>
		private void PasswordBuilder(string parameter)
		{
			char[] chars = parameter.ToCharArray();
			byte[] data = new byte[(int)this.PasswordLength.Value];
			using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
			{
				crypto.GetBytes(data);
			}
			StringBuilder result = new StringBuilder((int)this.PasswordLength.Value);
			foreach (byte b in data)
			{
				result.Append(chars[b % (chars.Length)]);
			}
			this.ResultTextBox.Text = result.ToString();
		}
	}
}
