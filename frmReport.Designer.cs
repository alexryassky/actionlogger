namespace ActionLogger
{
    partial class frmReport
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.rtbReport = new System.Windows.Forms.RichTextBox();
            this.mainmenu = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mainmenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtbReport
            // 
            this.rtbReport.BackColor = System.Drawing.SystemColors.Control;
            this.rtbReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbReport.Location = new System.Drawing.Point(0, 24);
            this.rtbReport.Name = "rtbReport";
            this.rtbReport.ReadOnly = true;
            this.rtbReport.Size = new System.Drawing.Size(376, 368);
            this.rtbReport.TabIndex = 0;
            this.rtbReport.Text = "";
            // 
            // mainmenu
            // 
            this.mainmenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem});
            this.mainmenu.Location = new System.Drawing.Point(0, 0);
            this.mainmenu.Name = "mainmenu";
            this.mainmenu.Size = new System.Drawing.Size(376, 24);
            this.mainmenu.TabIndex = 1;
            this.mainmenu.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // mnuSave
            // 
            this.mnuSave.Name = "mnuSave";
            this.mnuSave.Size = new System.Drawing.Size(151, 22);
            this.mnuSave.Text = "Сохранить";
            this.mnuSave.Click += new System.EventHandler(this.mnuSave_Click);
            // 
            // frmReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 392);
            this.Controls.Add(this.rtbReport);
            this.Controls.Add(this.mainmenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainmenu;
            this.Name = "frmReport";
            this.Text = "Отчет";
            this.Activated += new System.EventHandler(this.frmReport_Activated);
            this.mainmenu.ResumeLayout(false);
            this.mainmenu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbReport;
        private System.Windows.Forms.MenuStrip mainmenu;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuSave;
    }
}