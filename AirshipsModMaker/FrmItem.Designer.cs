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
            this.buttonClose = new System.Windows.Forms.Button();
            this.buttonSC = new System.Windows.Forms.Button();
            this.buttonsave = new System.Windows.Forms.Button();
            this.textBoxlable = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxdisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanelShow
            // 
            this.flowLayoutPanelShow.AutoScroll = true;
            this.flowLayoutPanelShow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.flowLayoutPanelShow.Location = new System.Drawing.Point(5, 166);
            this.flowLayoutPanelShow.Name = "flowLayoutPanelShow";
            this.flowLayoutPanelShow.Size = new System.Drawing.Size(458, 338);
            this.flowLayoutPanelShow.TabIndex = 12;
            // 
            // textBoxItemInfo
            // 
            this.textBoxItemInfo.Location = new System.Drawing.Point(121, 73);
            this.textBoxItemInfo.Multiline = true;
            this.textBoxItemInfo.Name = "textBoxItemInfo";
            this.textBoxItemInfo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxItemInfo.Size = new System.Drawing.Size(225, 56);
            this.textBoxItemInfo.TabIndex = 11;
            this.textBoxItemInfo.Text = "You Need Templare To make Item";
            this.toolTip1.SetToolTip(this.textBoxItemInfo, "This text will help player to know what the item was");
            // 
            // comboBoxItemTemplare
            // 
            this.comboBoxItemTemplare.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxItemTemplare.FormattingEnabled = true;
            this.comboBoxItemTemplare.Location = new System.Drawing.Point(121, 6);
            this.comboBoxItemTemplare.Name = "comboBoxItemTemplare";
            this.comboBoxItemTemplare.Size = new System.Drawing.Size(225, 24);
            this.comboBoxItemTemplare.TabIndex = 10;
            this.toolTip1.SetToolTip(this.comboBoxItemTemplare, "Select Templare First");
            this.comboBoxItemTemplare.SelectedIndexChanged += new System.EventHandler(this.comboBoxItemTemplare_SelectedIndexChanged);
            // 
            // labelT2
            // 
            this.labelT2.AutoSize = true;
            this.labelT2.Location = new System.Drawing.Point(3, 9);
            this.labelT2.Name = "labelT2";
            this.labelT2.Size = new System.Drawing.Size(112, 80);
            this.labelT2.TabIndex = 9;
            this.labelT2.Text = "Item Template\r\n\r\nItem Name\r\n\r\nItem Info";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(121, 39);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(225, 26);
            this.textBoxName.TabIndex = 8;
            this.textBoxName.Text = "Select Templare First";
            this.toolTip1.SetToolTip(this.textBoxName, "This is Name");
            // 
            // pictureBoxdisplay
            // 
            this.pictureBoxdisplay.Image = ((System.Drawing.Image)(resources.GetObject("pictureBoxdisplay.Image")));
            this.pictureBoxdisplay.Location = new System.Drawing.Point(359, 18);
            this.pictureBoxdisplay.Name = "pictureBoxdisplay";
            this.pictureBoxdisplay.Size = new System.Drawing.Size(100, 100);
            this.pictureBoxdisplay.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxdisplay.TabIndex = 7;
            this.pictureBoxdisplay.TabStop = false;
            this.toolTip1.SetToolTip(this.pictureBoxdisplay, "Item LookLike");
            // 
            // buttonClose
            // 
            this.buttonClose.AutoSize = true;
            this.buttonClose.BackColor = System.Drawing.Color.Orange;
            this.buttonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonClose.Location = new System.Drawing.Point(283, 134);
            this.buttonClose.Name = "buttonClose";
            this.buttonClose.Size = new System.Drawing.Size(180, 28);
            this.buttonClose.TabIndex = 13;
            this.buttonClose.Text = "Don\'t Save and Close";
            this.toolTip1.SetToolTip(this.buttonClose, "Carful! Every Change you didn\'t save Will LOST!");
            this.buttonClose.UseVisualStyleBackColor = false;
            this.buttonClose.Click += new System.EventHandler(this.buttonClose_Click);
            // 
            // buttonSC
            // 
            this.buttonSC.AutoSize = true;
            this.buttonSC.BackColor = System.Drawing.Color.GreenYellow;
            this.buttonSC.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSC.Location = new System.Drawing.Point(145, 134);
            this.buttonSC.Name = "buttonSC";
            this.buttonSC.Size = new System.Drawing.Size(132, 28);
            this.buttonSC.TabIndex = 14;
            this.buttonSC.Text = "Save and Close";
            this.toolTip1.SetToolTip(this.buttonSC, "Close it After Save");
            this.buttonSC.UseVisualStyleBackColor = false;
            this.buttonSC.Click += new System.EventHandler(this.button2_Click);
            // 
            // buttonsave
            // 
            this.buttonsave.AutoSize = true;
            this.buttonsave.BackColor = System.Drawing.Color.PaleGreen;
            this.buttonsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonsave.Location = new System.Drawing.Point(7, 134);
            this.buttonsave.Name = "buttonsave";
            this.buttonsave.Size = new System.Drawing.Size(132, 28);
            this.buttonsave.TabIndex = 15;
            this.buttonsave.Text = "Save";
            this.toolTip1.SetToolTip(this.buttonsave, "Save The Item");
            this.buttonsave.UseVisualStyleBackColor = false;
            this.buttonsave.Click += new System.EventHandler(this.buttonsave_Click);
            // 
            // textBoxlable
            // 
            this.textBoxlable.BackColor = System.Drawing.Color.White;
            this.textBoxlable.Location = new System.Drawing.Point(5, 506);
            this.textBoxlable.Multiline = true;
            this.textBoxlable.Name = "textBoxlable";
            this.textBoxlable.ReadOnly = true;
            this.textBoxlable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxlable.Size = new System.Drawing.Size(458, 64);
            this.textBoxlable.TabIndex = 16;
            this.textBoxlable.Text = "Click the Text Box To Show The HELP";
            // 
            // FrmItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(470, 573);
            this.Controls.Add(this.textBoxlable);
            this.Controls.Add(this.buttonsave);
            this.Controls.Add(this.buttonSC);
            this.Controls.Add(this.buttonClose);
            this.Controls.Add(this.flowLayoutPanelShow);
            this.Controls.Add(this.textBoxItemInfo);
            this.Controls.Add(this.comboBoxItemTemplare);
            this.Controls.Add(this.labelT2);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.pictureBoxdisplay);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmItem";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Item Editor";
            this.toolTip1.SetToolTip(this, "Close");
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmItem_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxdisplay)).EndInit();
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
        private System.Windows.Forms.Button buttonClose;
        private System.Windows.Forms.Button buttonSC;
        private System.Windows.Forms.Button buttonsave;
        private System.Windows.Forms.TextBox textBoxlable;
    }
}