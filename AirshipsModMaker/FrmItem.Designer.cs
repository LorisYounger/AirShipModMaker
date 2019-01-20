namespace AirshipsModMaker
{
    partial class FrmItem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmItem));
            this.flowLayoutPanelShow = new System.Windows.Forms.FlowLayoutPanel();
            this.textBoxItemInfo = new System.Windows.Forms.TextBox();
            this.comboBoxItemTemplare = new System.Windows.Forms.ComboBox();
            this.labelT2 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.pictureBoxdisplay = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textBoxlable = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAndCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dontSaveAndCloseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addCustomStuffToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonChooseImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxdisplay)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanelShow
            // 
            resources.ApplyResources(this.flowLayoutPanelShow, "flowLayoutPanelShow");
            this.flowLayoutPanelShow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanelShow.Name = "flowLayoutPanelShow";
            // 
            // textBoxItemInfo
            // 
            resources.ApplyResources(this.textBoxItemInfo, "textBoxItemInfo");
            this.textBoxItemInfo.Name = "textBoxItemInfo";
            this.toolTip1.SetToolTip(this.textBoxItemInfo, resources.GetString("textBoxItemInfo.ToolTip"));
            // 
            // comboBoxItemTemplare
            // 
            this.comboBoxItemTemplare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemTemplare.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxItemTemplare, "comboBoxItemTemplare");
            this.comboBoxItemTemplare.Name = "comboBoxItemTemplare";
            this.toolTip1.SetToolTip(this.comboBoxItemTemplare, resources.GetString("comboBoxItemTemplare.ToolTip"));
            this.comboBoxItemTemplare.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemTemplare_SelectedIndexChanged);
            // 
            // labelT2
            // 
            resources.ApplyResources(this.labelT2, "labelT2");
            this.labelT2.Name = "labelT2";
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.toolTip1.SetToolTip(this.textBoxName, resources.GetString("textBoxName.ToolTip"));
            // 
            // pictureBoxdisplay
            // 
            this.pictureBoxdisplay.BackColor = System.Drawing.Color.SandyBrown;
            resources.ApplyResources(this.pictureBoxdisplay, "pictureBoxdisplay");
            this.pictureBoxdisplay.Name = "pictureBoxdisplay";
            this.pictureBoxdisplay.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxdisplay, resources.GetString("pictureBoxdisplay.ToolTip"));
            // 
            // textBoxlable
            // 
            this.textBoxlable.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxlable, "textBoxlable");
            this.textBoxlable.Name = "textBoxlable";
            this.textBoxlable.ReadOnly = true;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripMenuItem,
            this.saveAndCloseToolStripMenuItem,
            this.dontSaveAndCloseToolStripMenuItem,
            this.addCustomStuffToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.BackColor = System.Drawing.Color.PaleGreen;
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAndCloseToolStripMenuItem
            // 
            this.saveAndCloseToolStripMenuItem.BackColor = System.Drawing.Color.GreenYellow;
            this.saveAndCloseToolStripMenuItem.Name = "saveAndCloseToolStripMenuItem";
            resources.ApplyResources(this.saveAndCloseToolStripMenuItem, "saveAndCloseToolStripMenuItem");
            this.saveAndCloseToolStripMenuItem.Click += new System.EventHandler(this.saveAndCloseToolStripMenuItem_Click);
            // 
            // dontSaveAndCloseToolStripMenuItem
            // 
            this.dontSaveAndCloseToolStripMenuItem.BackColor = System.Drawing.Color.Orange;
            this.dontSaveAndCloseToolStripMenuItem.Name = "dontSaveAndCloseToolStripMenuItem";
            resources.ApplyResources(this.dontSaveAndCloseToolStripMenuItem, "dontSaveAndCloseToolStripMenuItem");
            this.dontSaveAndCloseToolStripMenuItem.Click += new System.EventHandler(this.dontSaveAndCloseToolStripMenuItem_Click);
            // 
            // addCustomStuffToolStripMenuItem
            // 
            this.addCustomStuffToolStripMenuItem.BackColor = System.Drawing.Color.Aquamarine;
            this.addCustomStuffToolStripMenuItem.Name = "addCustomStuffToolStripMenuItem";
            resources.ApplyResources(this.addCustomStuffToolStripMenuItem, "addCustomStuffToolStripMenuItem");
            this.addCustomStuffToolStripMenuItem.Click += new System.EventHandler(this.addCustomStuffToolStripMenuItem_Click);
            // 
            // buttonChooseImage
            // 
            this.buttonChooseImage.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.buttonChooseImage, "buttonChooseImage");
            this.buttonChooseImage.Name = "buttonChooseImage";
            this.buttonChooseImage.UseVisualStyleBackColor = false;
            this.buttonChooseImage.Click += new System.EventHandler(this.buttonChooseImage_Click);
            // 
            // FrmItem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonChooseImage);
            this.Controls.Add(this.textBoxlable);
            this.Controls.Add(this.flowLayoutPanelShow);
            this.Controls.Add(this.textBoxItemInfo);
            this.Controls.Add(this.comboBoxItemTemplare);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.pictureBoxdisplay);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmItem";
            this.ShowInTaskbar = false;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmItem_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxdisplay)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelShow;
        private System.Windows.Forms.TextBox textBoxItemInfo;
        private System.Windows.Forms.ComboBox comboBoxItemTemplare;
        private System.Windows.Forms.Label labelT2;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.PictureBox pictureBoxdisplay;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox textBoxlable;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAndCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem dontSaveAndCloseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addCustomStuffToolStripMenuItem;
        private System.Windows.Forms.Button buttonChooseImage;
    }
}