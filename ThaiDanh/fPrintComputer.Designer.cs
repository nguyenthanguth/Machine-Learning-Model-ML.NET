namespace ThaiDanh
{
    partial class fPrintComputer
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fPrintComputer));
            this.cbList = new System.Windows.Forms.CheckedListBox();
            this.btOK = new System.Windows.Forms.Button();
            this.nSoLanIn = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nSoLanIn)).BeginInit();
            this.SuspendLayout();
            // 
            // cbList
            // 
            this.cbList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cbList.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbList.FormattingEnabled = true;
            this.cbList.Items.AddRange(new object[] {
            "Print 1",
            "Print 2"});
            this.cbList.Location = new System.Drawing.Point(0, 0);
            this.cbList.Name = "cbList";
            this.cbList.Size = new System.Drawing.Size(504, 220);
            this.cbList.TabIndex = 0;
            // 
            // btOK
            // 
            this.btOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btOK.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btOK.Location = new System.Drawing.Point(366, 235);
            this.btOK.Name = "btOK";
            this.btOK.Size = new System.Drawing.Size(138, 45);
            this.btOK.TabIndex = 1;
            this.btOK.Text = "Tiếp tục";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new System.EventHandler(this.btOK_Click);
            // 
            // nSoLanIn
            // 
            this.nSoLanIn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.nSoLanIn.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nSoLanIn.Location = new System.Drawing.Point(249, 246);
            this.nSoLanIn.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nSoLanIn.Name = "nSoLanIn";
            this.nSoLanIn.Size = new System.Drawing.Size(80, 26);
            this.nSoLanIn.TabIndex = 2;
            this.nSoLanIn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nSoLanIn.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(208, 248);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 19);
            this.label1.TabIndex = 4;
            this.label1.Text = "in ra";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(335, 248);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "bản";
            // 
            // fPrintComputer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(516, 292);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.nSoLanIn);
            this.Controls.Add(this.btOK);
            this.Controls.Add(this.cbList);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fPrintComputer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Chọn máy in";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fPrintComputer_FormClosing);
            this.Load += new System.EventHandler(this.fPrintComputer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nSoLanIn)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btOK;
        private System.Windows.Forms.CheckedListBox cbList;
        private System.Windows.Forms.NumericUpDown nSoLanIn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}