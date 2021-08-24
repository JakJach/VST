
namespace VST.Host
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.label1 = new System.Windows.Forms.Label();
            this.PluginListVw = new System.Windows.Forms.ListView();
            this.NameHdr = new System.Windows.Forms.ColumnHeader();
            this.ProductHdr = new System.Windows.Forms.ColumnHeader();
            this.VendorHdr = new System.Windows.Forms.ColumnHeader();
            this.VersionHdr = new System.Windows.Forms.ColumnHeader();
            this.AssemblyHdr = new System.Windows.Forms.ColumnHeader();
            this.PluginPathTxt = new System.Windows.Forms.TextBox();
            this.BrowseBtn = new System.Windows.Forms.Button();
            this.AddBtn = new System.Windows.Forms.Button();
            this.DeleteBtn = new System.Windows.Forms.Button();
            this.OpenFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.ViewPluginBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Plugin Path";
            this.label1.UseWaitCursor = true;
            // 
            // PluginListVw
            // 
            this.PluginListVw.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginListVw.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.NameHdr,
            this.ProductHdr,
            this.VendorHdr,
            this.VersionHdr,
            this.AssemblyHdr});
            this.PluginListVw.FullRowSelect = true;
            this.PluginListVw.HideSelection = false;
            this.PluginListVw.Location = new System.Drawing.Point(14, 65);
            this.PluginListVw.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PluginListVw.MultiSelect = false;
            this.PluginListVw.Name = "PluginListVw";
            this.PluginListVw.Size = new System.Drawing.Size(755, 449);
            this.PluginListVw.TabIndex = 0;
            this.PluginListVw.UseCompatibleStateImageBehavior = false;
            this.PluginListVw.UseWaitCursor = true;
            this.PluginListVw.View = System.Windows.Forms.View.Details;
            // 
            // NameHdr
            // 
            this.NameHdr.Text = "Name";
            this.NameHdr.Width = 120;
            // 
            // ProductHdr
            // 
            this.ProductHdr.DisplayIndex = 2;
            this.ProductHdr.Text = "Product";
            this.ProductHdr.Width = 120;
            // 
            // VendorHdr
            // 
            this.VendorHdr.DisplayIndex = 3;
            this.VendorHdr.Text = "Vendor";
            this.VendorHdr.Width = 120;
            // 
            // VersionHdr
            // 
            this.VersionHdr.DisplayIndex = 1;
            this.VersionHdr.Text = "Version";
            // 
            // AssemblyHdr
            // 
            this.AssemblyHdr.Text = "Assemlby";
            this.AssemblyHdr.Width = 120;
            // 
            // PluginPathTxt
            // 
            this.PluginPathTxt.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PluginPathTxt.Location = new System.Drawing.Point(14, 33);
            this.PluginPathTxt.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.PluginPathTxt.Name = "PluginPathTxt";
            this.PluginPathTxt.Size = new System.Drawing.Size(620, 23);
            this.PluginPathTxt.TabIndex = 1;
            this.PluginPathTxt.UseWaitCursor = true;
            // 
            // BrowseBtn
            // 
            this.BrowseBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowseBtn.Location = new System.Drawing.Point(642, 31);
            this.BrowseBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.BrowseBtn.Name = "BrowseBtn";
            this.BrowseBtn.Size = new System.Drawing.Size(36, 27);
            this.BrowseBtn.TabIndex = 3;
            this.BrowseBtn.Text = "...";
            this.BrowseBtn.UseVisualStyleBackColor = true;
            this.BrowseBtn.UseWaitCursor = true;
            this.BrowseBtn.Click += new System.EventHandler(this.BrowseBtn_Click);
            // 
            // AddBtn
            // 
            this.AddBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddBtn.Location = new System.Drawing.Point(685, 31);
            this.AddBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.AddBtn.Name = "AddBtn";
            this.AddBtn.Size = new System.Drawing.Size(88, 27);
            this.AddBtn.TabIndex = 4;
            this.AddBtn.Text = "Add";
            this.AddBtn.UseVisualStyleBackColor = true;
            this.AddBtn.UseWaitCursor = true;
            this.AddBtn.Click += new System.EventHandler(this.AddBtn_Click);
            // 
            // DeleteBtn
            // 
            this.DeleteBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DeleteBtn.Location = new System.Drawing.Point(14, 521);
            this.DeleteBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.DeleteBtn.Name = "DeleteBtn";
            this.DeleteBtn.Size = new System.Drawing.Size(88, 27);
            this.DeleteBtn.TabIndex = 5;
            this.DeleteBtn.Text = "Delete";
            this.DeleteBtn.UseVisualStyleBackColor = true;
            this.DeleteBtn.UseWaitCursor = true;
            this.DeleteBtn.Click += new System.EventHandler(this.DeleteBtn_Click);
            // 
            // OpenFileDlg
            // 
            this.OpenFileDlg.Filter = "Plugins (*.dll)|*.dll|All Files (*.*)|*.*";
            // 
            // ViewPluginBtn
            // 
            this.ViewPluginBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewPluginBtn.Location = new System.Drawing.Point(681, 521);
            this.ViewPluginBtn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.ViewPluginBtn.Name = "ViewPluginBtn";
            this.ViewPluginBtn.Size = new System.Drawing.Size(88, 27);
            this.ViewPluginBtn.TabIndex = 6;
            this.ViewPluginBtn.Text = "View...";
            this.ViewPluginBtn.UseVisualStyleBackColor = true;
            this.ViewPluginBtn.UseWaitCursor = true;
            this.ViewPluginBtn.Click += new System.EventHandler(this.ViewPluginBtn_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.Controls.Add(this.ViewPluginBtn);
            this.Controls.Add(this.DeleteBtn);
            this.Controls.Add(this.AddBtn);
            this.Controls.Add(this.BrowseBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PluginPathTxt);
            this.Controls.Add(this.PluginListVw);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "VST Host";
            this.UseWaitCursor = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView PluginListVw;
        private System.Windows.Forms.TextBox PluginPathTxt;
        private System.Windows.Forms.Button BrowseBtn;
        private System.Windows.Forms.Button AddBtn;
        private System.Windows.Forms.ColumnHeader NameHdr;
        private System.Windows.Forms.ColumnHeader VersionHdr;
        private System.Windows.Forms.ColumnHeader ProductHdr;
        private System.Windows.Forms.ColumnHeader VendorHdr;
        private System.Windows.Forms.ColumnHeader AssemblyHdr;
        private System.Windows.Forms.Button DeleteBtn;
        private System.Windows.Forms.OpenFileDialog OpenFileDlg;
        private System.Windows.Forms.Button ViewPluginBtn;
        private System.Windows.Forms.Label label1;
    }
}

