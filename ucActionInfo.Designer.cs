namespace ActionLogger
{
    partial class ucActionInfo
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timDelay = new System.Windows.Forms.Timer(this.components);
            this.panActionController = new System.Windows.Forms.Panel();
            this.panScreenshot = new System.Windows.Forms.Panel();
            this.panContainer2 = new System.Windows.Forms.Panel();
            this.btnRepeat = new System.Windows.Forms.Button();
            this.btnSetCursor = new System.Windows.Forms.Button();
            this.btnRepeatTill = new System.Windows.Forms.Button();
            this.lblFull = new ActionLogger.ctlTransparentLabel();
            this.lblBriefInfo = new ActionLogger.ctlTransparentLabel();
            this.panActionController.SuspendLayout();
            this.panContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panActionController
            // 
            this.panActionController.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.panActionController.Controls.Add(this.btnRepeatTill);
            this.panActionController.Controls.Add(this.panContainer2);
            this.panActionController.Dock = System.Windows.Forms.DockStyle.Right;
            this.panActionController.Location = new System.Drawing.Point(272, 0);
            this.panActionController.Name = "panActionController";
            this.panActionController.Size = new System.Drawing.Size(142, 63);
            this.panActionController.TabIndex = 3;
            // 
            // panScreenshot
            // 
            this.panScreenshot.Dock = System.Windows.Forms.DockStyle.Left;
            this.panScreenshot.Location = new System.Drawing.Point(0, 0);
            this.panScreenshot.MaximumSize = new System.Drawing.Size(79, 63);
            this.panScreenshot.Name = "panScreenshot";
            this.panScreenshot.Size = new System.Drawing.Size(35, 63);
            this.panScreenshot.TabIndex = 4;
            // 
            // panContainer2
            // 
            this.panContainer2.Controls.Add(this.btnRepeat);
            this.panContainer2.Controls.Add(this.btnSetCursor);
            this.panContainer2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panContainer2.Location = new System.Drawing.Point(0, 0);
            this.panContainer2.Name = "panContainer2";
            this.panContainer2.Size = new System.Drawing.Size(74, 63);
            this.panContainer2.TabIndex = 3;
            // 
            // btnRepeat
            // 
            this.btnRepeat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRepeat.Location = new System.Drawing.Point(0, 21);
            this.btnRepeat.Name = "btnRepeat";
            this.btnRepeat.Size = new System.Drawing.Size(74, 42);
            this.btnRepeat.TabIndex = 3;
            this.btnRepeat.Text = "Повторить событие";
            this.btnRepeat.UseVisualStyleBackColor = true;
            this.btnRepeat.Click += new System.EventHandler(this.btnRepeat_Click);
            // 
            // btnSetCursor
            // 
            this.btnSetCursor.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSetCursor.Location = new System.Drawing.Point(0, 0);
            this.btnSetCursor.Name = "btnSetCursor";
            this.btnSetCursor.Size = new System.Drawing.Size(74, 21);
            this.btnSetCursor.TabIndex = 2;
            this.btnSetCursor.Text = "Курсор";
            this.btnSetCursor.UseVisualStyleBackColor = true;
            this.btnSetCursor.Click += new System.EventHandler(this.btnCursor_Click);
            // 
            // btnRepeatTill
            // 
            this.btnRepeatTill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnRepeatTill.Location = new System.Drawing.Point(74, 0);
            this.btnRepeatTill.Name = "btnRepeatTill";
            this.btnRepeatTill.Size = new System.Drawing.Size(68, 63);
            this.btnRepeatTill.TabIndex = 5;
            this.btnRepeatTill.Text = "Повтор до события";
            this.btnRepeatTill.UseVisualStyleBackColor = true;
            this.btnRepeatTill.Click += new System.EventHandler(this.btnRepeatTill_Click);
            // 
            // lblFull
            // 
            this.lblFull.AutoSize = true;
            this.lblFull.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblFull.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblFull.ForeColor = System.Drawing.Color.Crimson;
            this.lblFull.Location = new System.Drawing.Point(34, 25);
            this.lblFull.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.lblFull.Name = "lblFull";
            this.lblFull.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.lblFull.Size = new System.Drawing.Size(50, 13);
            this.lblFull.TabIndex = 2;
            // 
            // lblBriefInfo
            // 
            this.lblBriefInfo.AutoSize = true;
            this.lblBriefInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblBriefInfo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblBriefInfo.ForeColor = System.Drawing.Color.Black;
            this.lblBriefInfo.Location = new System.Drawing.Point(34, 4);
            this.lblBriefInfo.Margin = new System.Windows.Forms.Padding(5, 0, 3, 0);
            this.lblBriefInfo.Name = "lblBriefInfo";
            this.lblBriefInfo.Padding = new System.Windows.Forms.Padding(50, 0, 0, 0);
            this.lblBriefInfo.Size = new System.Drawing.Size(50, 13);
            this.lblBriefInfo.TabIndex = 1;
            // 
            // ucActionInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panScreenshot);
            this.Controls.Add(this.panActionController);
            this.Controls.Add(this.lblFull);
            this.Controls.Add(this.lblBriefInfo);
            this.Name = "ucActionInfo";
            this.Size = new System.Drawing.Size(414, 63);
            this.panActionController.ResumeLayout(false);
            this.panContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public ActionLogger.ctlTransparentLabel lblBriefInfo;
        public ActionLogger.ctlTransparentLabel lblFull;
        private System.Windows.Forms.Timer timDelay;
        private System.Windows.Forms.Panel panActionController;
        private System.Windows.Forms.Panel panScreenshot;
        private System.Windows.Forms.Panel panContainer2;
        private System.Windows.Forms.Button btnRepeat;
        private System.Windows.Forms.Button btnSetCursor;
        private System.Windows.Forms.Button btnRepeatTill;
    }
}
