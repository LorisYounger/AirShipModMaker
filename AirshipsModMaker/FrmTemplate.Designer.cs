namespace AirshipsModMaker
{
    partial class FrmTemplate
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
            this.components = new System.ComponentModel.Container();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeaderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderAuthor = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderVer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderInfo = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addTemplatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reMoveTemplateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showOnExplorerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.reloadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderName,
            this.columnHeaderAuthor,
            this.columnHeaderVer,
            this.columnHeaderInfo});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.Location = new System.Drawing.Point(0, 0);
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(751, 299);
            this.listView1.TabIndex = 0;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Text = "Name";
            this.columnHeaderName.Width = 140;
            // 
            // columnHeaderAuthor
            // 
            this.columnHeaderAuthor.Text = "Author";
            this.columnHeaderAuthor.Width = 80;
            // 
            // columnHeaderVer
            // 
            this.columnHeaderVer.Text = "Ver";
            this.columnHeaderVer.Width = 40;
            // 
            // columnHeaderInfo
            // 
            this.columnHeaderInfo.Text = "Info";
            this.columnHeaderInfo.Width = 460;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addTemplatesToolStripMenuItem,
            this.reMoveTemplateToolStripMenuItem,
            this.showOnExplorerToolStripMenuItem,
            this.toolStripSeparator1,
            this.reloadToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 98);
            // 
            // addTemplatesToolStripMenuItem
            // 
            this.addTemplatesToolStripMenuItem.Name = "addTemplatesToolStripMenuItem";
            this.addTemplatesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.addTemplatesToolStripMenuItem.Text = "Install/Update";
            this.addTemplatesToolStripMenuItem.Click += new System.EventHandler(this.addTemplatesToolStripMenuItem_Click);
            // 
            // reMoveTemplateToolStripMenuItem
            // 
            this.reMoveTemplateToolStripMenuItem.Name = "reMoveTemplateToolStripMenuItem";
            this.reMoveTemplateToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.reMoveTemplateToolStripMenuItem.Text = "ReMove Template";
            this.reMoveTemplateToolStripMenuItem.Click += new System.EventHandler(this.reMoveTemplateToolStripMenuItem_Click);
            // 
            // showOnExplorerToolStripMenuItem
            // 
            this.showOnExplorerToolStripMenuItem.Name = "showOnExplorerToolStripMenuItem";
            this.showOnExplorerToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.showOnExplorerToolStripMenuItem.Text = "Show On Explorer";
            this.showOnExplorerToolStripMenuItem.Click += new System.EventHandler(this.showOnExplorerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            this.reloadToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.reloadToolStripMenuItem.Text = "Reload";
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // FrmTemplate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(751, 299);
            this.Controls.Add(this.listView1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTemplate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Template Manager";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeaderName;
        private System.Windows.Forms.ColumnHeader columnHeaderAuthor;
        private System.Windows.Forms.ColumnHeader columnHeaderInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem addTemplatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reMoveTemplateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showOnExplorerToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem reloadToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader columnHeaderVer;
    }
}