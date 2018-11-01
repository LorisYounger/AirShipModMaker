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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmModSet));
            this.textBoxSeach = new System.Windows.Forms.TextBox();
            this.listBoxSelect = new System.Windows.Forms.ListBox();
            this.textBoxlable = new System.Windows.Forms.TextBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxSeach
            // 
            resources.ApplyResources(this.textBoxSeach, "textBoxSeach");
            this.textBoxSeach.Name = "textBoxSeach";
            this.textBoxSeach.TextChanged += new System.EventHandler(this.textBoxSeach_TextChanged);
            // 
            // listBoxSelect
            // 
            this.listBoxSelect.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxSelect, "listBoxSelect");
            this.listBoxSelect.Name = "listBoxSelect";
            this.listBoxSelect.Click += new System.EventHandler(this.listBoxSelect_Click);
            // 
            // textBoxlable
            // 
            this.textBoxlable.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.textBoxlable, "textBoxlable");
            this.textBoxlable.Name = "textBoxlable";
            this.textBoxlable.ReadOnly = true;
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            resources.ApplyResources(this.buttonSelect, "buttonSelect");
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // FrmModSet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.textBoxlable);
            this.Controls.Add(this.listBoxSelect);
            this.Controls.Add(this.textBoxSeach);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmModSet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
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