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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTemplate));
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
            resources.ApplyResources(this.listView1, "listView1");
            this.listView1.FullRowSelect = true;
            this.listView1.GridLines = true;
            this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.listView1.MultiSelect = false;
            this.listView1.Name = "listView1";
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderName
            // 
            this.columnHeaderName.Tag = "columnHeaderName";
            resources.ApplyResources(this.columnHeaderName, "columnHeaderName");
            // 
            // columnHeaderAuthor
            // 
            this.columnHeaderAuthor.Tag = "columnHeaderAuthor";
            resources.ApplyResources(this.columnHeaderAuthor, "columnHeaderAuthor");
            // 
            // columnHeaderVer
            // 
            this.columnHeaderVer.Tag = "columnHeaderVer";
            resources.ApplyResources(this.columnHeaderVer, "columnHeaderVer");
            // 
            // columnHeaderInfo
            // 
            this.columnHeaderInfo.Tag = "columnHeaderInfo";
            resources.ApplyResources(this.columnHeaderInfo, "columnHeaderInfo");
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
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // addTemplatesToolStripMenuItem
            // 
            this.addTemplatesToolStripMenuItem.Name = "addTemplatesToolStripMenuItem";
            resources.ApplyResources(this.addTemplatesToolStripMenuItem, "addTemplatesToolStripMenuItem");
            this.addTemplatesToolStripMenuItem.Click += new System.EventHandler(this.addTemplatesToolStripMenuItem_Click);
            // 
            // reMoveTemplateToolStripMenuItem
            // 
            this.reMoveTemplateToolStripMenuItem.Name = "reMoveTemplateToolStripMenuItem";
            resources.ApplyResources(this.reMoveTemplateToolStripMenuItem, "reMoveTemplateToolStripMenuItem");
            this.reMoveTemplateToolStripMenuItem.Click += new System.EventHandler(this.reMoveTemplateToolStripMenuItem_Click);
            // 
            // showOnExplorerToolStripMenuItem
            // 
            this.showOnExplorerToolStripMenuItem.Name = "showOnExplorerToolStripMenuItem";
            resources.ApplyResources(this.showOnExplorerToolStripMenuItem, "showOnExplorerToolStripMenuItem");
            this.showOnExplorerToolStripMenuItem.Click += new System.EventHandler(this.showOnExplorerToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // reloadToolStripMenuItem
            // 
            this.reloadToolStripMenuItem.Name = "reloadToolStripMenuItem";
            resources.ApplyResources(this.reloadToolStripMenuItem, "reloadToolStripMenuItem");
            this.reloadToolStripMenuItem.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // FrmTemplate
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listView1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTemplate";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
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