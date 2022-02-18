namespace GTransactions
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rbYesterday = new System.Windows.Forms.RadioButton();
            this.rbLastMonth = new System.Windows.Forms.RadioButton();
            this.rbToday = new System.Windows.Forms.RadioButton();
            this.rbThisMonth = new System.Windows.Forms.RadioButton();
            this.monthCalendar1 = new System.Windows.Forms.MonthCalendar();
            this.rbSelectday = new System.Windows.Forms.RadioButton();
            this.statistic = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rbYesterday
            // 
            this.rbYesterday.AutoSize = true;
            this.rbYesterday.Location = new System.Drawing.Point(114, 43);
            this.rbYesterday.Name = "rbYesterday";
            this.rbYesterday.Size = new System.Drawing.Size(75, 19);
            this.rbYesterday.TabIndex = 0;
            this.rbYesterday.TabStop = true;
            this.rbYesterday.Text = "Hôm qua";
            this.rbYesterday.UseVisualStyleBackColor = true;
            // 
            // rbLastMonth
            // 
            this.rbLastMonth.AutoSize = true;
            this.rbLastMonth.Location = new System.Drawing.Point(114, 123);
            this.rbLastMonth.Name = "rbLastMonth";
            this.rbLastMonth.Size = new System.Drawing.Size(89, 19);
            this.rbLastMonth.TabIndex = 1;
            this.rbLastMonth.TabStop = true;
            this.rbLastMonth.Text = "Tháng trước";
            this.rbLastMonth.UseVisualStyleBackColor = true;
            // 
            // rbToday
            // 
            this.rbToday.AutoSize = true;
            this.rbToday.Location = new System.Drawing.Point(114, 83);
            this.rbToday.Name = "rbToday";
            this.rbToday.Size = new System.Drawing.Size(74, 19);
            this.rbToday.TabIndex = 2;
            this.rbToday.TabStop = true;
            this.rbToday.Text = "Hôm nay";
            this.rbToday.UseVisualStyleBackColor = true;
            // 
            // rbThisMonth
            // 
            this.rbThisMonth.AutoSize = true;
            this.rbThisMonth.Location = new System.Drawing.Point(114, 163);
            this.rbThisMonth.Name = "rbThisMonth";
            this.rbThisMonth.Size = new System.Drawing.Size(80, 19);
            this.rbThisMonth.TabIndex = 3;
            this.rbThisMonth.TabStop = true;
            this.rbThisMonth.Text = "Tháng này";
            this.rbThisMonth.UseVisualStyleBackColor = true;
            // 
            // monthCalendar1
            // 
            this.monthCalendar1.Location = new System.Drawing.Point(114, 270);
            this.monthCalendar1.MaxSelectionCount = 14;
            this.monthCalendar1.Name = "monthCalendar1";
            this.monthCalendar1.TabIndex = 4;
            // 
            // rbSelectday
            // 
            this.rbSelectday.AutoSize = true;
            this.rbSelectday.Location = new System.Drawing.Point(114, 250);
            this.rbSelectday.Name = "rbSelectday";
            this.rbSelectday.Size = new System.Drawing.Size(86, 19);
            this.rbSelectday.TabIndex = 5;
            this.rbSelectday.TabStop = true;
            this.rbSelectday.Text = "Chọn ngày:";
            this.rbSelectday.UseVisualStyleBackColor = true;
            // 
            // statistic
            // 
            this.statistic.Location = new System.Drawing.Point(387, 81);
            this.statistic.Name = "statistic";
            this.statistic.Size = new System.Drawing.Size(138, 46);
            this.statistic.TabIndex = 6;
            this.statistic.Text = "Thống kê";
            this.statistic.UseVisualStyleBackColor = true;
            this.statistic.Click += new System.EventHandler(this.statistic_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.statistic);
            this.Controls.Add(this.rbSelectday);
            this.Controls.Add(this.monthCalendar1);
            this.Controls.Add(this.rbThisMonth);
            this.Controls.Add(this.rbToday);
            this.Controls.Add(this.rbLastMonth);
            this.Controls.Add(this.rbYesterday);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private RadioButton rbYesterday;
        private RadioButton rbLastMonth;
        private RadioButton rbToday;
        private RadioButton rbThisMonth;
        private MonthCalendar monthCalendar1;
        private RadioButton rbSelectday;
        private Button statistic;
    }
}