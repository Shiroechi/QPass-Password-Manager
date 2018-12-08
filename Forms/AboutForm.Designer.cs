namespace QPass.Forms
{
	partial class AboutForm
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
			this.DescriptionLabel = new System.Windows.Forms.Label();
			this.Logo = new System.Windows.Forms.Label();
			this.GithubLink = new System.Windows.Forms.LinkLabel();
			this.SuspendLayout();
			// 
			// DescriptionLabel
			// 
			this.DescriptionLabel.AutoSize = true;
			this.DescriptionLabel.Location = new System.Drawing.Point(30, 30);
			this.DescriptionLabel.Name = "DescriptionLabel";
			this.DescriptionLabel.Size = new System.Drawing.Size(358, 38);
			this.DescriptionLabel.TabIndex = 0;
			this.DescriptionLabel.Text = "A simple password manager using \r\nrinjdael algortihm as the core for encrypt and " +
    "decrypt.";
			// 
			// Logo
			// 
			this.Logo.Image = global::QPass.Properties.Resources.icons8_github_32;
			this.Logo.Location = new System.Drawing.Point(30, 350);
			this.Logo.Name = "Logo";
			this.Logo.Size = new System.Drawing.Size(32, 32);
			this.Logo.TabIndex = 1;
			// 
			// GithubLink
			// 
			this.GithubLink.Location = new System.Drawing.Point(70, 350);
			this.GithubLink.Name = "GithubLink";
			this.GithubLink.Size = new System.Drawing.Size(60, 32);
			this.GithubLink.TabIndex = 2;
			this.GithubLink.TabStop = true;
			this.GithubLink.Text = "QPass";
			this.GithubLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.GithubLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.GithubLink_LinkClicked);
			// 
			// AboutForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(484, 461);
			this.Controls.Add(this.GithubLink);
			this.Controls.Add(this.Logo);
			this.Controls.Add(this.DescriptionLabel);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "AboutForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label DescriptionLabel;
		private System.Windows.Forms.Label Logo;
		private System.Windows.Forms.LinkLabel GithubLink;
	}
}