namespace QPass.Forms
{
	partial class QPassMainForm
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
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.AccountTable = new System.Windows.Forms.DataGridView();
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.FileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.NewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.CloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.SaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.QuitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.EditToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AddAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.DeleteAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ViewAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.ToolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.GeneratePasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.HelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.AboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AccountTable)).BeginInit();
			this.MenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.AutoScroll = true;
			this.toolStripContainer1.ContentPanel.Controls.Add(this.AccountTable);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(640, 453);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.toolStripContainer1.LeftToolStripPanelVisible = false;
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(640, 480);
			this.toolStripContainer1.TabIndex = 1;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.MenuStrip);
			// 
			// AccountTable
			// 
			this.AccountTable.AllowUserToAddRows = false;
			this.AccountTable.AllowUserToDeleteRows = false;
			this.AccountTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.AccountTable.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AccountTable.Location = new System.Drawing.Point(0, 0);
			this.AccountTable.Name = "AccountTable";
			this.AccountTable.ReadOnly = true;
			this.AccountTable.Size = new System.Drawing.Size(640, 453);
			this.AccountTable.TabIndex = 0;
			this.AccountTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.AccountTable_CellDoubleClick);
			this.AccountTable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AccountTable_KeyDown);
			// 
			// MenuStrip
			// 
			this.MenuStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.MenuStrip.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileToolStripMenuItem,
            this.EditToolStripMenuItem,
            this.ViewToolStripMenuItem,
            this.ToolsToolStripMenuItem,
            this.HelpToolStripMenuItem});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.MenuStrip.Size = new System.Drawing.Size(640, 27);
			this.MenuStrip.TabIndex = 0;
			this.MenuStrip.Text = "menuStrip1";
			// 
			// FileToolStripMenuItem
			// 
			this.FileToolStripMenuItem.AutoSize = false;
			this.FileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NewToolStripMenuItem,
            this.OpenToolStripMenuItem,
            this.CloseToolStripMenuItem,
            this.toolStripSeparator2,
            this.SaveToolStripMenuItem,
            this.toolStripSeparator1,
            this.QuitToolStripMenuItem});
			this.FileToolStripMenuItem.Name = "FileToolStripMenuItem";
			this.FileToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.FileToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
			this.FileToolStripMenuItem.Text = "File";
			// 
			// NewToolStripMenuItem
			// 
			this.NewToolStripMenuItem.Name = "NewToolStripMenuItem";
			this.NewToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
			this.NewToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
			this.NewToolStripMenuItem.Text = "New";
			this.NewToolStripMenuItem.Click += new System.EventHandler(this.NewToolStripMenuItem_Click);
			// 
			// OpenToolStripMenuItem
			// 
			this.OpenToolStripMenuItem.Name = "OpenToolStripMenuItem";
			this.OpenToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.OpenToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
			this.OpenToolStripMenuItem.Text = "Open";
			this.OpenToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
			// 
			// CloseToolStripMenuItem
			// 
			this.CloseToolStripMenuItem.Name = "CloseToolStripMenuItem";
			this.CloseToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
			this.CloseToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
			this.CloseToolStripMenuItem.Text = "Close";
			this.CloseToolStripMenuItem.Click += new System.EventHandler(this.CloseToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
			// 
			// SaveToolStripMenuItem
			// 
			this.SaveToolStripMenuItem.Name = "SaveToolStripMenuItem";
			this.SaveToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
			this.SaveToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
			this.SaveToolStripMenuItem.Text = "Save";
			this.SaveToolStripMenuItem.Click += new System.EventHandler(this.SaveToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(165, 6);
			// 
			// QuitToolStripMenuItem
			// 
			this.QuitToolStripMenuItem.Name = "QuitToolStripMenuItem";
			this.QuitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.QuitToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
			this.QuitToolStripMenuItem.Text = "Quit";
			this.QuitToolStripMenuItem.Click += new System.EventHandler(this.QuitToolStripMenuItem_Click);
			// 
			// EditToolStripMenuItem
			// 
			this.EditToolStripMenuItem.AutoSize = false;
			this.EditToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddAccountToolStripMenuItem,
            this.DeleteAccountToolStripMenuItem});
			this.EditToolStripMenuItem.Name = "EditToolStripMenuItem";
			this.EditToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
			this.EditToolStripMenuItem.Text = "Edit";
			// 
			// AddAccountToolStripMenuItem
			// 
			this.AddAccountToolStripMenuItem.Name = "AddAccountToolStripMenuItem";
			this.AddAccountToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
			this.AddAccountToolStripMenuItem.Size = new System.Drawing.Size(226, 24);
			this.AddAccountToolStripMenuItem.Text = "Add account";
			this.AddAccountToolStripMenuItem.Click += new System.EventHandler(this.AddAccountToolStripMenuItem_Click);
			// 
			// DeleteAccountToolStripMenuItem
			// 
			this.DeleteAccountToolStripMenuItem.Name = "DeleteAccountToolStripMenuItem";
			this.DeleteAccountToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
			this.DeleteAccountToolStripMenuItem.Size = new System.Drawing.Size(226, 24);
			this.DeleteAccountToolStripMenuItem.Text = "Delete account";
			this.DeleteAccountToolStripMenuItem.Click += new System.EventHandler(this.DeleteAccountToolStripMenuItem_Click);
			// 
			// ViewToolStripMenuItem
			// 
			this.ViewToolStripMenuItem.AutoSize = false;
			this.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem";
			this.ViewToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
			this.ViewToolStripMenuItem.Text = "View";
			// 
			// ViewUsernameToolStripMenuItem
			// 
			this.ViewUsernameToolStripMenuItem.Name = "ViewUsernameToolStripMenuItem";
			this.ViewUsernameToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.U)));
			this.ViewUsernameToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
			this.ViewUsernameToolStripMenuItem.Text = "View username";
			// 
			// ViewPasswordToolStripMenuItem
			// 
			this.ViewPasswordToolStripMenuItem.Name = "ViewPasswordToolStripMenuItem";
			this.ViewPasswordToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
			this.ViewPasswordToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
			this.ViewPasswordToolStripMenuItem.Text = "View password";
			// 
			// ViewAllToolStripMenuItem
			// 
			this.ViewAllToolStripMenuItem.Name = "ViewAllToolStripMenuItem";
			this.ViewAllToolStripMenuItem.Size = new System.Drawing.Size(228, 24);
			this.ViewAllToolStripMenuItem.Text = "View all";
			// 
			// ToolsToolStripMenuItem
			// 
			this.ToolsToolStripMenuItem.AutoSize = false;
			this.ToolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GeneratePasswordToolStripMenuItem,
            this.SettingsToolStripMenuItem});
			this.ToolsToolStripMenuItem.Name = "ToolsToolStripMenuItem";
			this.ToolsToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
			this.ToolsToolStripMenuItem.Text = "Tools";
			// 
			// GeneratePasswordToolStripMenuItem
			// 
			this.GeneratePasswordToolStripMenuItem.Name = "GeneratePasswordToolStripMenuItem";
			this.GeneratePasswordToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
			this.GeneratePasswordToolStripMenuItem.Text = "Generate password";
			this.GeneratePasswordToolStripMenuItem.Click += new System.EventHandler(this.GeneratePasswordToolStripMenuItem_Click);
			// 
			// SettingsToolStripMenuItem
			// 
			this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
			this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(204, 24);
			this.SettingsToolStripMenuItem.Text = "Settings";
			this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
			// 
			// HelpToolStripMenuItem
			// 
			this.HelpToolStripMenuItem.AutoSize = false;
			this.HelpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AboutToolStripMenuItem});
			this.HelpToolStripMenuItem.Name = "HelpToolStripMenuItem";
			this.HelpToolStripMenuItem.Size = new System.Drawing.Size(80, 23);
			this.HelpToolStripMenuItem.Text = "Help";
			// 
			// AboutToolStripMenuItem
			// 
			this.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem";
			this.AboutToolStripMenuItem.Size = new System.Drawing.Size(180, 24);
			this.AboutToolStripMenuItem.Text = "About";
			this.AboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
			// 
			// QPassMainForm
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(640, 480);
			this.Controls.Add(this.toolStripContainer1);
			this.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.KeyPreview = true;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "QPassMainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "QPass";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.QPassMainForm_FormClosing);
			this.Load += new System.EventHandler(this.QPassMainForm_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.QPassMainForm_KeyDown);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.AccountTable)).EndInit();
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem FileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem EditToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ToolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem HelpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem NewToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem SaveToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem QuitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem GeneratePasswordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem AboutToolStripMenuItem;
		private System.Windows.Forms.DataGridView AccountTable;
		private System.Windows.Forms.ToolStripMenuItem AddAccountToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewUsernameToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewPasswordToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem ViewAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem CloseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem DeleteAccountToolStripMenuItem;
	}
}