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
			this.Length = new System.Windows.Forms.NumericUpDown();
			this.GenerateButton = new System.Windows.Forms.Button();
			this.ResultTextBox = new System.Windows.Forms.TextBox();
			((System.ComponentModel.ISupportInitialize)(this.Length)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(30, 30);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Length";
			// 
			// Length
			// 
			this.Length.Location = new System.Drawing.Point(160, 28);
			this.Length.Maximum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
			this.Length.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.Length.Name = "Length";
			this.Length.Size = new System.Drawing.Size(120, 27);
			this.Length.TabIndex = 1;
			this.Length.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
			// 
			// GenerateButton
			// 
			this.GenerateButton.Location = new System.Drawing.Point(330, 24);
			this.GenerateButton.Name = "GenerateButton";
			this.GenerateButton.Size = new System.Drawing.Size(100, 30);
			this.GenerateButton.TabIndex = 2;
			this.GenerateButton.Text = "Generate";
			this.GenerateButton.UseVisualStyleBackColor = true;
			this.GenerateButton.Click += new System.EventHandler(this.GenerateButton_Click);
			// 
			// ResultTextBox
			// 
			this.ResultTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
			this.ResultTextBox.Location = new System.Drawing.Point(30, 75);
			this.ResultTextBox.MaxLength = 1024;
			this.ResultTextBox.Multiline = true;
			this.ResultTextBox.Name = "ResultTextBox";
			this.ResultTextBox.ReadOnly = true;
			this.ResultTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.ResultTextBox.Size = new System.Drawing.Size(400, 65);
			this.ResultTextBox.TabIndex = 3;
			// 
			// GenerateRandomPasswordForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(484, 161);
			this.Controls.Add(this.ResultTextBox);
			this.Controls.Add(this.GenerateButton);
			this.Controls.Add(this.Length);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "GenerateRandomPasswordForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Generate Random Password";
			((System.ComponentModel.ISupportInitialize)(this.Length)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown Length;
		private System.Windows.Forms.Button GenerateButton;
		private System.Windows.Forms.TextBox ResultTextBox;
	}
}