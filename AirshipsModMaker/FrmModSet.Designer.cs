namespace AirshipsModMaker
{
    partial class FrmModSet
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
            this.textBoxSeach = new System.Windows.Forms.TextBox();
            this.listBoxSelect = new System.Windows.Forms.ListBox();
            this.textBoxlable = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSeach
            // 
            this.textBoxSeach.Location = new System.Drawing.Point(12, 12);
            this.textBoxSeach.Name = "textBoxSeach";
            this.textBoxSeach.Size = new System.Drawing.Size(260, 26);
            this.textBoxSeach.TabIndex = 0;
            this.textBoxSeach.TextChanged += new System.EventHandler(this.textBoxSeach_TextChanged);
            // 
            // listBoxSelect
            // 
            this.listBoxSelect.FormattingEnabled = true;
            this.listBoxSelect.ItemHeight = 16;
            this.listBoxSelect.Location = new System.Drawing.Point(12, 53);
            this.listBoxSelect.Name = "listBoxSelect";
            this.listBoxSelect.Size = new System.Drawing.Size(260, 196);
            this.listBoxSelect.TabIndex = 1;
            this.listBoxSelect.Click += new System.EventHandler(this.listBoxSelect_Click);
            // 
            // textBoxlable
            // 
            this.textBoxlable.BackColor = System.Drawing.Color.White;
            this.textBoxlable.Location = new System.Drawing.Point(278, 12);
            this.textBoxlable.Multiline = true;
            this.textBoxlable.Name = "textBoxlable";
            this.textBoxlable.ReadOnly = true;
            this.textBoxlable.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxlable.Size = new System.Drawing.Size(294, 187);
            this.textBoxlable.TabIndex = 17;
            this.textBoxlable.Text = "<-- Enter text to seach\r\n\r\n\r\n\r\n<-- Select the ListBox To Show The HELP\r\n\r\n\r\n    C" +
    "lick this to Select ModSet\r\n      |\r\n     \\|/";
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSelect.Font = new System.Drawing.Font("宋体", 24F);
            this.buttonSelect.Location = new System.Drawing.Point(278, 205);
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.Size = new System.Drawing.Size(294, 44);
            this.buttonSelect.TabIndex = 18;
            this.buttonSelect.Text = "Select ModSet";
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // FrmModSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 261);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.textBoxlable);
            this.Controls.Add(this.listBoxSelect);
            this.Controls.Add(this.textBoxSeach);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmModSet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Stuff";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmModSet_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxSeach;
        private System.Windows.Forms.ListBox listBoxSelect;
        private System.Windows.Forms.TextBox textBoxlable;
        public System.Windows.Forms.Button buttonSelect;
    }
}