namespace OrderingRegistrator
{
    partial class PriceCalculator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PriceCalculator));
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbBxCurrency = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbxPrice = new System.Windows.Forms.TextBox();
            this.txtbxPackWeight = new System.Windows.Forms.TextBox();
            this.txtbxGetPrice = new System.Windows.Forms.TextBox();
            this.btnCalc = new System.Windows.Forms.Button();
            this.labAns = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnClean3 = new System.Windows.Forms.Button();
            this.btnClean2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnPlus3 = new System.Windows.Forms.Button();
            this.btnPlus2 = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label7.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label7.Location = new System.Drawing.Point(524, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 20);
            this.label7.TabIndex = 33;
            this.label7.Text = "შეიყვანეთ ამანათის წონა და ფასი";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label6.Location = new System.Drawing.Point(485, 210);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(301, 20);
            this.label6.TabIndex = 32;
            this.label6.Text = "სასურველი ამანათის სავარაუდო ფასი?";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.label5.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label5.Location = new System.Drawing.Point(666, 187);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(120, 20);
            this.label5.TabIndex = 31;
            this.label5.Text = "გსურთ გაიგოთ";
            // 
            // cmbBxCurrency
            // 
            this.cmbBxCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBxCurrency.FormattingEnabled = true;
            this.cmbBxCurrency.Items.AddRange(new object[] {
            "USD",
            "EUR",
            "RUB",
            "GRP"});
            this.cmbBxCurrency.Location = new System.Drawing.Point(449, 135);
            this.cmbBxCurrency.Name = "cmbBxCurrency";
            this.cmbBxCurrency.Size = new System.Drawing.Size(71, 21);
            this.cmbBxCurrency.TabIndex = 29;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label3.Location = new System.Drawing.Point(221, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(222, 24);
            this.label3.TabIndex = 28;
            this.label3.Text = "ამანათის ფასი კურსით:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label2.Location = new System.Drawing.Point(221, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(239, 24);
            this.label2.TabIndex = 27;
            this.label2.Text = "ამანათის წონა (გრამებში)";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.label1.Location = new System.Drawing.Point(116, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(415, 24);
            this.label1.TabIndex = 26;
            this.label1.Text = "ტრანსპორტერი კომპანიის კურსი კილოგრამზე";
            // 
            // txtbxPrice
            // 
            this.txtbxPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbxPrice.Location = new System.Drawing.Point(17, 135);
            this.txtbxPrice.Name = "txtbxPrice";
            this.txtbxPrice.Size = new System.Drawing.Size(143, 22);
            this.txtbxPrice.TabIndex = 4;
            this.txtbxPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxPrice_KeyPress);
            this.txtbxPrice.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxPrice_Validating);
            // 
            // txtbxPackWeight
            // 
            this.txtbxPackWeight.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbxPackWeight.Location = new System.Drawing.Point(17, 81);
            this.txtbxPackWeight.Name = "txtbxPackWeight";
            this.txtbxPackWeight.Size = new System.Drawing.Size(143, 22);
            this.txtbxPackWeight.TabIndex = 3;
            this.txtbxPackWeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxPackWeight_KeyPress);
            this.txtbxPackWeight.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxPackWeight_Validating);
            // 
            // txtbxGetPrice
            // 
            this.txtbxGetPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtbxGetPrice.Location = new System.Drawing.Point(17, 17);
            this.txtbxGetPrice.Name = "txtbxGetPrice";
            this.txtbxGetPrice.Size = new System.Drawing.Size(53, 22);
            this.txtbxGetPrice.TabIndex = 2;
            this.txtbxGetPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtbxGetPrice_KeyPress);
            this.txtbxGetPrice.Validating += new System.ComponentModel.CancelEventHandler(this.txtbxGetPrice_Validating);
            // 
            // btnCalc
            // 
            this.btnCalc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCalc.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnCalc.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCalc.Location = new System.Drawing.Point(12, 194);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(223, 59);
            this.btnCalc.TabIndex = 1;
            this.btnCalc.Text = "ფასის გამოთვლა";
            this.btnCalc.UseVisualStyleBackColor = false;
            this.btnCalc.Click += new System.EventHandler(this.btnCalc_Click);
            // 
            // labAns
            // 
            this.labAns.AutoSize = true;
            this.labAns.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.labAns.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.labAns.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labAns.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.labAns.Location = new System.Drawing.Point(12, 259);
            this.labAns.Name = "labAns";
            this.labAns.Size = new System.Drawing.Size(373, 27);
            this.labAns.TabIndex = 21;
            this.labAns.Text = "გამოთვალე ამანათის სავარაუდო ფასი";
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("groupBox1.BackgroundImage")));
            this.groupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.groupBox1.Location = new System.Drawing.Point(12, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 177);
            this.groupBox1.TabIndex = 34;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPlus2);
            this.groupBox2.Controls.Add(this.btnPlus3);
            this.groupBox2.Controls.Add(this.btnClean3);
            this.groupBox2.Controls.Add(this.btnClean2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.cmbBxCurrency);
            this.groupBox2.Controls.Add(this.txtbxGetPrice);
            this.groupBox2.Controls.Add(this.txtbxPackWeight);
            this.groupBox2.Controls.Add(this.txtbxPrice);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(241, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(545, 177);
            this.groupBox2.TabIndex = 35;
            this.groupBox2.TabStop = false;
            // 
            // btnClean3
            // 
            this.btnClean3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnClean3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClean3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClean3.Location = new System.Drawing.Point(155, 132);
            this.btnClean3.Name = "btnClean3";
            this.btnClean3.Size = new System.Drawing.Size(27, 28);
            this.btnClean3.TabIndex = 32;
            this.btnClean3.Text = "C";
            this.btnClean3.UseVisualStyleBackColor = false;
            this.btnClean3.Click += new System.EventHandler(this.btnClean3_Click);
            // 
            // btnClean2
            // 
            this.btnClean2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnClean2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnClean2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClean2.Location = new System.Drawing.Point(155, 77);
            this.btnClean2.Name = "btnClean2";
            this.btnClean2.Size = new System.Drawing.Size(27, 28);
            this.btnClean2.TabIndex = 31;
            this.btnClean2.Text = "C";
            this.btnClean2.UseVisualStyleBackColor = false;
            this.btnClean2.Click += new System.EventHandler(this.btnClean2_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.button1.Location = new System.Drawing.Point(66, 13);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(27, 28);
            this.button1.TabIndex = 30;
            this.button1.Text = "C";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPlus3
            // 
            this.btnPlus3.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPlus3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlus3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPlus3.Location = new System.Drawing.Point(181, 132);
            this.btnPlus3.Name = "btnPlus3";
            this.btnPlus3.Size = new System.Drawing.Size(27, 28);
            this.btnPlus3.TabIndex = 33;
            this.btnPlus3.Text = "+";
            this.btnPlus3.UseVisualStyleBackColor = false;
            this.btnPlus3.Click += new System.EventHandler(this.btnPlus3_Click);
            // 
            // btnPlus2
            // 
            this.btnPlus2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnPlus2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnPlus2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnPlus2.Location = new System.Drawing.Point(181, 77);
            this.btnPlus2.Name = "btnPlus2";
            this.btnPlus2.Size = new System.Drawing.Size(27, 28);
            this.btnPlus2.TabIndex = 34;
            this.btnPlus2.Text = "+";
            this.btnPlus2.UseVisualStyleBackColor = false;
            this.btnPlus2.Click += new System.EventHandler(this.btnPlus2_Click);
            // 
            // PriceCalculator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(798, 293);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.labAns);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PriceCalculator";
            this.Text = "PriceCalculator - წონის კალკულატორი";
            this.Load += new System.EventHandler(this.PriceCalculator_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbBxCurrency;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbxPrice;
        private System.Windows.Forms.TextBox txtbxPackWeight;
        private System.Windows.Forms.TextBox txtbxGetPrice;
        private System.Windows.Forms.Button btnCalc;
        private System.Windows.Forms.Label labAns;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnClean3;
        private System.Windows.Forms.Button btnClean2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnPlus3;
        private System.Windows.Forms.Button btnPlus2;



    }
}