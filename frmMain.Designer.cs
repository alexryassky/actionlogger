namespace ActionLogger
{
    partial class formStart
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
            this.panActions = new System.Windows.Forms.Panel();
            this.mainmnu = new System.Windows.Forms.MenuStrip();
            this.mnuReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoadReport = new System.Windows.Forms.ToolStripMenuItem();
            this.слайдшоуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.edtHandle = new System.Windows.Forms.ToolStripTextBox();
            this.mnuClearImmediately = new System.Windows.Forms.ToolStripMenuItem();
            this.mainmnu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panActions
            // 
            this.panActions.AutoScroll = true;
            this.panActions.AutoScrollMargin = new System.Drawing.Size(10, 0);
            this.panActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panActions.Location = new System.Drawing.Point(0, 23);
            this.panActions.Name = "panActions";
            this.panActions.Size = new System.Drawing.Size(573, 423);
            this.panActions.TabIndex = 1;
            // 
            // mainmnu
            // 
            this.mainmnu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuReport,
            this.mnuSettings,
            this.edtHandle,
            this.mnuClearImmediately});
            this.mainmnu.Location = new System.Drawing.Point(0, 0);
            this.mainmnu.Name = "mainmnu";
            this.mainmnu.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.mainmnu.Size = new System.Drawing.Size(573, 27);
            this.mainmnu.TabIndex = 5;
            this.mainmnu.Text = "menuStrip1";
            // 
            // mnuReport
            // 
            this.mnuReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowReport,
            this.mnuLoadReport,
            this.слайдшоуToolStripMenuItem});
            this.mnuReport.Name = "mnuReport";
            this.mnuReport.Size = new System.Drawing.Size(51, 23);
            this.mnuReport.Text = "Отчет";
            // 
            // mnuShowReport
            // 
            this.mnuShowReport.Name = "mnuShowReport";
            this.mnuShowReport.Size = new System.Drawing.Size(161, 22);
            this.mnuShowReport.Text = "Показать отчёт";
            this.mnuShowReport.Click += new System.EventHandler(this.mnuShowReport_Click);
            // 
            // mnuLoadReport
            // 
            this.mnuLoadReport.Name = "mnuLoadReport";
            this.mnuLoadReport.Size = new System.Drawing.Size(161, 22);
            this.mnuLoadReport.Text = "Загрузить отчет";
            this.mnuLoadReport.Click += new System.EventHandler(this.mnuLoadReport_Click);
            // 
            // слайдшоуToolStripMenuItem
            // 
            this.слайдшоуToolStripMenuItem.Name = "слайдшоуToolStripMenuItem";
            this.слайдшоуToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.слайдшоуToolStripMenuItem.Text = "Слайдшоу";
            // 
            // mnuSettings
            // 
            this.mnuSettings.Name = "mnuSettings";
            this.mnuSettings.Size = new System.Drawing.Size(79, 23);
            this.mnuSettings.Text = "Настройки";
            this.mnuSettings.Click += new System.EventHandler(this.mnuShowSettings_Click);
            // 
            // edtHandle
            // 
            this.edtHandle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.edtHandle.Name = "edtHandle";
            this.edtHandle.Size = new System.Drawing.Size(100, 23);
            // 
            // mnuClearImmediately
            // 
            this.mnuClearImmediately.Name = "mnuClearImmediately";
            this.mnuClearImmediately.Size = new System.Drawing.Size(112, 23);
            this.mnuClearImmediately.Text = "Очистить сейчас";
            this.mnuClearImmediately.Click += new System.EventHandler(this.mnuClearImmediately_Click);
            // 
            // formStart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 446);
            this.Controls.Add(this.mainmnu);
            this.Controls.Add(this.panActions);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(650, 480);
            this.MinimumSize = new System.Drawing.Size(450, 480);
            this.Name = "formStart";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Action Logger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.formStart_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainmnu.ResumeLayout(false);
            this.mainmnu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panActions;
        private System.Windows.Forms.MenuStrip mainmnu;
        private System.Windows.Forms.ToolStripMenuItem mnuReport;
        private System.Windows.Forms.ToolStripMenuItem mnuShowReport;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadReport;
        private System.Windows.Forms.ToolStripMenuItem mnuSettings;
        private System.Windows.Forms.ToolStripTextBox edtHandle;
        private System.Windows.Forms.ToolStripMenuItem слайдшоуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuClearImmediately;
    }
}

