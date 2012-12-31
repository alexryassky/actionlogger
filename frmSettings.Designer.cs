namespace ActionLogger
{
    partial class frmSettings
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
            this.btnOK = new System.Windows.Forms.Button();
            this.edtDelay = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnClear = new System.Windows.Forms.Button();
            this.chbForceMaximize = new System.Windows.Forms.CheckBox();
            this.lbl2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnOK.Location = new System.Drawing.Point(0, 55);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(177, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // edtDelay
            // 
            this.edtDelay.Location = new System.Drawing.Point(100, 6);
            this.edtDelay.Name = "edtDelay";
            this.edtDelay.Size = new System.Drawing.Size(45, 20);
            this.edtDelay.TabIndex = 1;
            this.edtDelay.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Задержка, сек";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(6, 57);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(79, 23);
            this.btnClear.TabIndex = 3;
            this.btnClear.Text = "Очистка";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Visible = false;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // chbForceMaximize
            // 
            this.chbForceMaximize.AutoSize = true;
            this.chbForceMaximize.Location = new System.Drawing.Point(130, 32);
            this.chbForceMaximize.Name = "chbForceMaximize";
            this.chbForceMaximize.Size = new System.Drawing.Size(15, 14);
            this.chbForceMaximize.TabIndex = 4;
            this.chbForceMaximize.UseVisualStyleBackColor = true;
            this.chbForceMaximize.CheckedChanged += new System.EventHandler(this.chbForceMaximize_CheckedChanged);
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(12, 32);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(101, 13);
            this.lbl2.TabIndex = 5;
            this.lbl2.Text = "Максимизировать";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 78);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.chbForceMaximize);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.edtDelay);
            this.Controls.Add(this.btnOK);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmSettings";
            this.Text = "Настройки";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox edtDelay;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox chbForceMaximize;
        private System.Windows.Forms.Label lbl2;
    }
}