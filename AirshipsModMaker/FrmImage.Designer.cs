namespace AirshipsModMaker
{
    partial class FrmImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImage));
            this.pictureBoxDisplay = new System.Windows.Forms.PictureBox();
            this.labelSize = new System.Windows.Forms.Label();
            this.listBoxSelect = new System.Windows.Forms.ListBox();
            this.buttonSelect = new System.Windows.Forms.Button();
            this.textBoxSeach = new System.Windows.Forms.TextBox();
            this.buttonGetImage = new System.Windows.Forms.Button();
            this.buttonDelete = new System.Windows.Forms.Button();
            this.buttonImport = new System.Windows.Forms.Button();
            this.labelName = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxDisplay
            // 
            this.pictureBoxDisplay.BackColor = System.Drawing.Color.Chocolate;
            resources.ApplyResources(this.pictureBoxDisplay, "pictureBoxDisplay");
            this.pictureBoxDisplay.Name = "pictureBoxDisplay";
            this.pictureBoxDisplay.TabStop = false;
            // 
            // labelSize
            // 
            resources.ApplyResources(this.labelSize, "labelSize");
            this.labelSize.Name = "labelSize";
            // 
            // listBoxSelect
            // 
            this.listBoxSelect.FormattingEnabled = true;
            resources.ApplyResources(this.listBoxSelect, "listBoxSelect");
            this.listBoxSelect.Name = "listBoxSelect";
            this.listBoxSelect.SelectedIndexChanged += new System.EventHandler(this.listBoxSelect_SelectedIndexChanged);
            // 
            // buttonSelect
            // 
            this.buttonSelect.BackColor = System.Drawing.Color.White;
            this.buttonSelect.DialogResult = System.Windows.Forms.DialogResult.Yes;
            resources.ApplyResources(this.buttonSelect, "buttonSelect");
            this.buttonSelect.Name = "buttonSelect";
            this.buttonSelect.UseVisualStyleBackColor = false;
            this.buttonSelect.Click += new System.EventHandler(this.buttonSelect_Click);
            // 
            // textBoxSeach
            // 
            resources.ApplyResources(this.textBoxSeach, "textBoxSeach");
            this.textBoxSeach.Name = "textBoxSeach";
            this.textBoxSeach.TextChanged += new System.EventHandler(this.textBoxSeach_TextChanged);
            // 
            // buttonGetImage
            // 
            this.buttonGetImage.BackColor = System.Drawing.Color.WhiteSmoke;
            resources.ApplyResources(this.buttonGetImage, "buttonGetImage");
            this.buttonGetImage.Name = "buttonGetImage";
            this.buttonGetImage.UseVisualStyleBackColor = false;
            this.buttonGetImage.Click += new System.EventHandler(this.buttonGetImage_Click);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.LightSalmon;
            resources.ApplyResources(this.buttonDelete, "buttonDelete");
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelete_Click);
            // 
            // buttonImport
            // 
            this.buttonImport.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            resources.ApplyResources(this.buttonImport, "buttonImport");
            this.buttonImport.Name = "buttonImport";
            this.buttonImport.UseVisualStyleBackColor = false;
            this.buttonImport.Click += new System.EventHandler(this.buttonImport_Click);
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // saveFileDialog1
            // 
            resources.ApplyResources(this.saveFileDialog1, "saveFileDialog1");
            // 
            // openFileDialog1
            // 
            resources.ApplyResources(this.openFileDialog1, "openFileDialog1");
            // 
            // FrmImage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.buttonImport);
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.buttonGetImage);
            this.Controls.Add(this.textBoxSeach);
            this.Controls.Add(this.buttonSelect);
            this.Controls.Add(this.listBoxSelect);
            this.Controls.Add(this.labelSize);
            this.Controls.Add(this.pictureBoxDisplay);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FrmImage";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmModSet_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxDisplay)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxDisplay;
        private System.Windows.Forms.Label labelSize;
        private System.Windows.Forms.ListBox listBoxSelect;
        private System.Windows.Forms.TextBox textBoxSeach;
        private System.Windows.Forms.Button buttonGetImage;
        private System.Windows.Forms.Button buttonDelete;
        private System.Windows.Forms.Button buttonImport;
        private System.Windows.Forms.Label labelName;
        public System.Windows.Forms.Button buttonSelect;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}