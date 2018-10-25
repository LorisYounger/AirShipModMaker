namespace AirshipsModMaker
{
    partial class UCSettingItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelInfoName = new System.Windows.Forms.Label();
            this.textBoxinput = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.buttonDelete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelInfoName
            // 
            this.labelInfoName.BackColor = System.Drawing.Color.WhiteSmoke;
            this.labelInfoName.Location = new System.Drawing.Point(3, 9);
            this.labelInfoName.Name = "labelInfoName";
            this.labelInfoName.Size = new System.Drawing.Size(164, 17);
            this.labelInfoName.TabIndex = 0;
            this.labelInfoName.Text = "InfoName";
            this.labelInfoName.Click += new System.EventHandler(this.UCSettingItem_Click);
            // 
            // textBoxinput
            // 
            this.textBoxinput.Location = new System.Drawing.Point(173, 5);
            this.textBoxinput.Name = "textBoxinput";
            this.textBoxinput.Size = new System.Drawing.Size(228, 26);
            this.textBoxinput.TabIndex = 1;
            this.textBoxinput.Click += new System.EventHandler(this.UCSettingItem_Click);
            this.textBoxinput.TextChanged += new System.EventHandler(this.textBoxinput_TextChanged);
            // 
            // buttonDelete
            // 
            this.buttonDelete.BackColor = System.Drawing.Color.OrangeRed;
            this.buttonDelete.FlatAppearance.BorderSize = 0;
            this.buttonDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonDelete.Font = new System.Drawing.Font("Swis721 Blk BT", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDelete.Location = new System.Drawing.Point(405, 5);
            this.buttonDelete.Name = "buttonDelete";
            this.buttonDelete.Size = new System.Drawing.Size(21, 26);
            this.buttonDelete.TabIndex = 2;
            this.buttonDelete.Text = "X";
            this.buttonDelete.UseVisualStyleBackColor = false;
            this.buttonDelete.Click += new System.EventHandler(this.buttonDelect_Click);
            // 
            // UCSettingItem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.buttonDelete);
            this.Controls.Add(this.textBoxinput);
            this.Controls.Add(this.labelInfoName);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UCSettingItem";
            this.Size = new System.Drawing.Size(429, 35);
            this.toolTip1.SetToolTip(this, "Nomal");
            this.Click += new System.EventHandler(this.UCSettingItem_Click);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelInfoName;
        private System.Windows.Forms.TextBox textBoxinput;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button buttonDelete;
    }
}
