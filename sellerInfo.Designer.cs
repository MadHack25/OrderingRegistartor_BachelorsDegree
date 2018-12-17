namespace OrderingRegistrator
{
    partial class txtbxsellerInfo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(txtbxsellerInfo));
            this.txtbxsellerName = new System.Windows.Forms.TextBox();
            this.txtbxsellerTrackService = new System.Windows.Forms.TextBox();
            this.txtbxExtraInfo = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SuspendLayout();
            // 
            // txtbxsellerName
            // 
            this.txtbxsellerName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbxsellerName.Location = new System.Drawing.Point(175, 45);
            this.txtbxsellerName.Name = "txtbxsellerName";
            this.txtbxsellerName.Size = new System.Drawing.Size(379, 24);
            this.txtbxsellerName.TabIndex = 32;
            this.txtbxsellerName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtbxsellerTrackService
            // 
            this.txtbxsellerTrackService.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbxsellerTrackService.Location = new System.Drawing.Point(175, 120);
            this.txtbxsellerTrackService.Name = "txtbxsellerTrackService";
            this.txtbxsellerTrackService.Size = new System.Drawing.Size(379, 24);
            this.txtbxsellerTrackService.TabIndex = 33;
            this.txtbxsellerTrackService.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtbxExtraInfo
            // 
            this.txtbxExtraInfo.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtbxExtraInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbxExtraInfo.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtbxExtraInfo.Location = new System.Drawing.Point(11, 150);
            this.txtbxExtraInfo.Multiline = true;
            this.txtbxExtraInfo.Name = "txtbxExtraInfo";
            this.txtbxExtraInfo.Size = new System.Drawing.Size(724, 217);
            this.txtbxExtraInfo.TabIndex = 34;
            this.txtbxExtraInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnUpdate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUpdate.Location = new System.Drawing.Point(599, 111);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(136, 33);
            this.btnUpdate.TabIndex = 36;
            this.btnUpdate.Text = "განახლება";
            this.btnUpdate.UseVisualStyleBackColor = false;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label19.Location = new System.Drawing.Point(264, 21);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(212, 25);
            this.label19.TabIndex = 37;
            this.label19.Text = "გამყიდველის სახელი";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(172, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(384, 25);
            this.label1.TabIndex = 38;
            this.label1.Text = "გამყიველის ტრანსპორტირების მეთოდი";
            // 
            // groupBox1
            // 
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 132);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // txtbxsellerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(746, 374);
            this.Controls.Add(this.txtbxExtraInfo);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtbxsellerTrackService);
            this.Controls.Add(this.txtbxsellerName);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "txtbxsellerInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SellersInfo - ინფორმაცია გამყიდველის შესახებ";
            this.Load += new System.EventHandler(this.sellerInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox txtbxsellerName;
        public System.Windows.Forms.TextBox txtbxsellerTrackService;
        public System.Windows.Forms.TextBox txtbxExtraInfo;
        private System.Windows.Forms.Button btnUpdate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}