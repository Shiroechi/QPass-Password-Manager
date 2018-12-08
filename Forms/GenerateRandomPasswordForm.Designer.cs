namespace QPass.Forms
{
	partial class GenerateRandomPasswordForm
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
			this.PasswordLength = new System.Windows.Forms.NumericUpDown();
			this.GenerateButton = new System.Windows.Forms.Button();
			this.ResultTextBox = new System.Windows.Forms.TextBox();
			this.CopyButton = new System.Windows.Forms.Button();
			this.UpperLetterCheckBox = new System.Windows.Forms.CheckBox();
			this.LowerLetterCheckBox = new System.Windows.Forms.CheckBox();
			this.NumericCheckBox = new System.Windows.Forms.CheckBox();
			this.SymbolCheckBox = new System.Windows.Forms.CheckBox();
			((System.ComponentModel.ISupportInitialize)(this.PasswordLength)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30, 100);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 27);
			this.label1.TabIndex = 0;
			this.label1.Text = "Length";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// PasswordLength
			// 
			this.PasswordLength.Location = new System.Drawing.Point(150, 100);
			this.PasswordLength.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.PasswordLength.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.PasswordLength.Name = "PasswordLength";
			this.PasswordLength.Size = new System.Drawing.Size(120, 27);
			this.PasswordLength.TabIndex = 5;
			this.PasswordLength.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// GenerateButton
			// 
			this.GenerateButton.Location = new System.Drawing.Point(330, 96);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(100, 30);
			this.GenerateButton.TabIndex = 6;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.UseVisualStyleBackColor = true;
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// ResultTextBox
			// 
			this.ResultTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ResultTextBox.Location = new System.Drawing.Point(30, 140);
			this.ResultTextBox.MaxLength = 1024;
			this.ResultTextBox.Multiline = true;
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ResultTextBox.Size = new System.Drawing.Size(400, 70);
			this.ResultTextBox.TabIndex = 3;
			// 
			// CopyButton
			// 
			this.CopyButton.Location = new System.Drawing.Point(330, 219);
			this.CopyButton.Name = "CopyButton";
			this.CopyButton.Size = new System.Drawing.Size(100, 30);
			this.CopyButton.TabIndex = 7;
			this.CopyButton.Text = "Copy";
			this.CopyButton.UseVisualStyleBackColor = true;
			this.CopyButton.Click += new System.EventHandler(this.CopyButton_Click);
			// 
			// UpperLetterCheckBox
			// 
			this.UpperLetterCheckBox.Location = new System.Drawing.Point(30, 30);
			this.UpperLetterCheckBox.Name = "UpperLetterCheckBox";
			this.UpperLetterCheckBox.Size = new System.Drawing.Size(150, 30);
			this.UpperLetterCheckBox.TabIndex = 1;
			this.UpperLetterCheckBox.Text = "Upper letter";
			this.UpperLetterCheckBox.UseVisualStyleBackColor = true;
			// 
			// LowerLetterCheckBox
			// 
			this.LowerLetterCheckBox.Location = new System.Drawing.Point(30, 60);
			this.LowerLetterCheckBox.Name = "LowerLetterCheckBox";
			this.LowerLetterCheckBox.Size = new System.Drawing.Size(150, 30);
			this.LowerLetterCheckBox.TabIndex = 2;
			this.LowerLetterCheckBox.Text = "Lower letter";
			this.LowerLetterCheckBox.UseVisualStyleBackColor = true;
			// 
			// NumericCheckBox
			// 
			this.NumericCheckBox.Location = new System.Drawing.Point(200, 30);
			this.NumericCheckBox.Name = "NumericCheckBox";
			this.NumericCheckBox.Size = new System.Drawing.Size(150, 30);
			this.NumericCheckBox.TabIndex = 3;
			this.NumericCheckBox.Text = "Numeric";
			this.NumericCheckBox.UseVisualStyleBackColor = true;
			// 
			// SymbolCheckBox
			// 
			this.SymbolCheckBox.Location = new System.Drawing.Point(200, 60);
			this.SymbolCheckBox.Name = "SymbolCheckBox";
			this.SymbolCheckBox.Size = new System.Drawing.Size(150, 30);
			this.SymbolCheckBox.TabIndex = 4;
			this.SymbolCheckBox.Text = "Symbol";
			this.SymbolCheckBox.UseVisualStyleBackColor = true;
			// 
			// GenerateRandomPasswordForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(484, 261);
			this.Controls.Add(this.SymbolCheckBox);
			this.Controls.Add(this.NumericCheckBox);
			this.Controls.Add(this.LowerLetterCheckBox);
			this.Controls.Add(this.UpperLetterCheckBox);
			this.Controls.Add(this.ResultTextBox);
			this.Controls.Add(this.CopyButton);
			this.Controls.Add(this.GenerateButton);
			this.Controls.Add(this.PasswordLength);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "GenerateRandomPasswordForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Generate Random Password";
			((System.ComponentModel.ISupportInitialize)(this.PasswordLength)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown PasswordLength;
		private System.Windows.Forms.Button GenerateButton;
		private System.Windows.Forms.TextBox ResultTextBox;
		private System.Windows.Forms.Button CopyButton;
		private System.Windows.Forms.CheckBox UpperLetterCheckBox;
		private System.Windows.Forms.CheckBox LowerLetterCheckBox;
		private System.Windows.Forms.CheckBox NumericCheckBox;
		private System.Windows.Forms.CheckBox SymbolCheckBox;
	}
}