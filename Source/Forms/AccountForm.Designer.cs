namespace QPass.Forms
{
	partial class AccountForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.TitleTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.UsernameTextBox = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.PasswordTextBox = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.NoteTextBox = new System.Windows.Forms.TextBox();
			this.SaveButton = new System.Windows.Forms.Button();
			this.ErrorMessageLabel = new System.Windows.Forms.Label();
			this.PasswordStatusButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// TitleTextBox
			// 
			this.TitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TitleTextBox.Location = new System.Drawing.Point(180, 30);
			this.TitleTextBox.Name = "TitleTextBox";
			this.TitleTextBox.Size = new System.Drawing.Size(250, 27);
			this.TitleTextBox.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(30, 70);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 27);
			this.label2.TabIndex = 0;
			this.label2.Text = "Username";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UsernameTextBox
			// 
			this.UsernameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.UsernameTextBox.Location = new System.Drawing.Point(180, 70);
			this.UsernameTextBox.Name = "UsernameTextBox";
			this.UsernameTextBox.Size = new System.Drawing.Size(250, 27);
			this.UsernameTextBox.TabIndex = 2;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(30, 110);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 27);
			this.label3.TabIndex = 0;
			this.label3.Text = "Password";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PasswordTextBox
			// 
			this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.PasswordTextBox.Location = new System.Drawing.Point(180, 110);
			this.PasswordTextBox.Name = "PasswordTextBox";
			this.PasswordTextBox.Size = new System.Drawing.Size(250, 27);
			this.PasswordTextBox.TabIndex = 3;
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(30, 150);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 27);
			this.label4.TabIndex = 0;
			this.label4.Text = "Note";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// NoteTextBox
			// 
			this.NoteTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.NoteTextBox.Location = new System.Drawing.Point(30, 190);
			this.NoteTextBox.Multiline = true;
			this.NoteTextBox.Name = "NoteTextBox";
			this.NoteTextBox.Size = new System.Drawing.Size(400, 160);
			this.NoteTextBox.TabIndex = 4;
			// 
			// SaveButton
			// 
			this.SaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.SaveButton.Location = new System.Drawing.Point(192, 390);
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(100, 30);
			this.SaveButton.TabIndex = 5;
			this.SaveButton.Text = "Save";
			this.SaveButton.UseVisualStyleBackColor = true;
			this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
			// 
			// ErrorMessageLabel
			// 
			this.ErrorMessageLabel.BackColor = System.Drawing.Color.Transparent;
			this.ErrorMessageLabel.ForeColor = System.Drawing.Color.Red;
			this.ErrorMessageLabel.Location = new System.Drawing.Point(30, 353);
			this.ErrorMessageLabel.Name = "ErrorMessageLabel";
			this.ErrorMessageLabel.Size = new System.Drawing.Size(400, 30);
			this.ErrorMessageLabel.TabIndex = 6;
			this.ErrorMessageLabel.Text = "Error Message";
			this.ErrorMessageLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ErrorMessageLabel.Visible = false;
			// 
			// PasswordStatusButton
			// 
			this.PasswordStatusButton.Image = global::QPass.Properties.Resources.icons8_eye_hide_32;
			this.PasswordStatusButton.Location = new System.Drawing.Point(436, 110);
			this.PasswordStatusButton.Name = "PasswordStatusButton";
			this.PasswordStatusButton.Size = new System.Drawing.Size(36, 27);
			this.PasswordStatusButton.TabIndex = 7;
			this.PasswordStatusButton.UseVisualStyleBackColor = true;
			this.PasswordStatusButton.Click += new System.EventHandler(this.PasswordStatusButton_Click);
			// 
			// AccountForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.PasswordStatusButton);
			this.Controls.Add(this.ErrorMessageLabel);
			this.Controls.Add(this.SaveButton);
			this.Controls.Add(this.NoteTextBox);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.PasswordTextBox);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.UsernameTextBox);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.TitleTextBox);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MinimumSize = new System.Drawing.Size(500, 500);
			this.Name = "AccountForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Account";
			this.Load += new System.EventHandler(this.AccountForm_Load);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox TitleTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox UsernameTextBox;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox PasswordTextBox;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox NoteTextBox;
		private System.Windows.Forms.Button SaveButton;
		private System.Windows.Forms.Label ErrorMessageLabel;
		private System.Windows.Forms.Button PasswordStatusButton;
	}
}