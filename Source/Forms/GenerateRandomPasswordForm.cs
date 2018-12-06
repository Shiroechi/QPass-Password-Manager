using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace QPass.Forms
{
	public partial class GenerateRandomPasswordForm : Form
	{
		public GenerateRandomPasswordForm()
		{
			InitializeComponent();
		}

		private void GenerateButton_Click(object sender, EventArgs e)
		{
			char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
			byte[] data = new byte[(int)this.Length.Value];
			using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
			{
				crypto.GetBytes(data);
			}
			StringBuilder result = new StringBuilder((int)this.Length.Value);
			foreach (byte b in data)
			{
				result.Append(chars[b % (chars.Length)]);
			}
			this.ResultTextBox.Text = result.ToString();
		}
	}
}
