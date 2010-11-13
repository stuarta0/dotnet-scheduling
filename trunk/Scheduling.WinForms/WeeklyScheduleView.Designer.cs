namespace Scheduling.WinForms
{
    partial class WeeklyScheduleView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlWeekDays = new System.Windows.Forms.FlowLayoutPanel();
            this.chkWeekSunday = new System.Windows.Forms.CheckBox();
            this.chkWeekMonday = new System.Windows.Forms.CheckBox();
            this.chkWeekTuesday = new System.Windows.Forms.CheckBox();
            this.chkWeekWednesday = new System.Windows.Forms.CheckBox();
            this.chkWeekThursday = new System.Windows.Forms.CheckBox();
            this.chkWeekFriday = new System.Windows.Forms.CheckBox();
            this.chkWeekSaturday = new System.Windows.Forms.CheckBox();
            this.bsWeeklySchedule = new System.Windows.Forms.BindingSource(this.components);
            this.pnlWeekDays.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsWeeklySchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlWeekDays
            // 
            this.pnlWeekDays.AutoSize = true;
            this.pnlWeekDays.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.pnlWeekDays.Controls.Add(this.chkWeekSunday);
            this.pnlWeekDays.Controls.Add(this.chkWeekMonday);
            this.pnlWeekDays.Controls.Add(this.chkWeekTuesday);
            this.pnlWeekDays.Controls.Add(this.chkWeekWednesday);
            this.pnlWeekDays.Controls.Add(this.chkWeekThursday);
            this.pnlWeekDays.Controls.Add(this.chkWeekFriday);
            this.pnlWeekDays.Controls.Add(this.chkWeekSaturday);
            this.pnlWeekDays.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlWeekDays.Location = new System.Drawing.Point(0, 0);
            this.pnlWeekDays.Name = "pnlWeekDays";
            this.pnlWeekDays.Size = new System.Drawing.Size(510, 23);
            this.pnlWeekDays.TabIndex = 9;
            // 
            // chkWeekSunday
            // 
            this.chkWeekSunday.AutoSize = true;
            this.chkWeekSunday.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsWeeklySchedule, "Sunday", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWeekSunday.Location = new System.Drawing.Point(3, 3);
            this.chkWeekSunday.Name = "chkWeekSunday";
            this.chkWeekSunday.Size = new System.Drawing.Size(62, 17);
            this.chkWeekSunday.TabIndex = 0;
            this.chkWeekSunday.Text = "Sunday";
            this.chkWeekSunday.UseVisualStyleBackColor = true;
            // 
            // chkWeekMonday
            // 
            this.chkWeekMonday.AutoSize = true;
            this.chkWeekMonday.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsWeeklySchedule, "Monday", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWeekMonday.Location = new System.Drawing.Point(71, 3);
            this.chkWeekMonday.Name = "chkWeekMonday";
            this.chkWeekMonday.Size = new System.Drawing.Size(64, 17);
            this.chkWeekMonday.TabIndex = 0;
            this.chkWeekMonday.Text = "Monday";
            this.chkWeekMonday.UseVisualStyleBackColor = true;
            // 
            // chkWeekTuesday
            // 
            this.chkWeekTuesday.AutoSize = true;
            this.chkWeekTuesday.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsWeeklySchedule, "Tuesday", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWeekTuesday.Location = new System.Drawing.Point(141, 3);
            this.chkWeekTuesday.Name = "chkWeekTuesday";
            this.chkWeekTuesday.Size = new System.Drawing.Size(67, 17);
            this.chkWeekTuesday.TabIndex = 0;
            this.chkWeekTuesday.Text = "Tuesday";
            this.chkWeekTuesday.UseVisualStyleBackColor = true;
            // 
            // chkWeekWednesday
            // 
            this.chkWeekWednesday.AutoSize = true;
            this.chkWeekWednesday.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsWeeklySchedule, "Wednesday", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWeekWednesday.Location = new System.Drawing.Point(214, 3);
            this.chkWeekWednesday.Name = "chkWeekWednesday";
            this.chkWeekWednesday.Size = new System.Drawing.Size(83, 17);
            this.chkWeekWednesday.TabIndex = 0;
            this.chkWeekWednesday.Text = "Wednesday";
            this.chkWeekWednesday.UseVisualStyleBackColor = true;
            // 
            // chkWeekThursday
            // 
            this.chkWeekThursday.AutoSize = true;
            this.chkWeekThursday.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsWeeklySchedule, "Thursday", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWeekThursday.Location = new System.Drawing.Point(303, 3);
            this.chkWeekThursday.Name = "chkWeekThursday";
            this.chkWeekThursday.Size = new System.Drawing.Size(70, 17);
            this.chkWeekThursday.TabIndex = 0;
            this.chkWeekThursday.Text = "Thursday";
            this.chkWeekThursday.UseVisualStyleBackColor = true;
            // 
            // chkWeekFriday
            // 
            this.chkWeekFriday.AutoSize = true;
            this.chkWeekFriday.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsWeeklySchedule, "Friday", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWeekFriday.Location = new System.Drawing.Point(379, 3);
            this.chkWeekFriday.Name = "chkWeekFriday";
            this.chkWeekFriday.Size = new System.Drawing.Size(54, 17);
            this.chkWeekFriday.TabIndex = 0;
            this.chkWeekFriday.Text = "Friday";
            this.chkWeekFriday.UseVisualStyleBackColor = true;
            // 
            // chkWeekSaturday
            // 
            this.chkWeekSaturday.AutoSize = true;
            this.chkWeekSaturday.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsWeeklySchedule, "Saturday", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.chkWeekSaturday.Location = new System.Drawing.Point(439, 3);
            this.chkWeekSaturday.Name = "chkWeekSaturday";
            this.chkWeekSaturday.Size = new System.Drawing.Size(68, 17);
            this.chkWeekSaturday.TabIndex = 0;
            this.chkWeekSaturday.Text = "Saturday";
            this.chkWeekSaturday.UseVisualStyleBackColor = true;
            // 
            // bsWeeklySchedule
            // 
            this.bsWeeklySchedule.DataSource = typeof(Scheduling.WeeklySchedule);
            // 
            // WeeklyScheduleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.pnlWeekDays);
            this.Name = "WeeklyScheduleView";
            this.Size = new System.Drawing.Size(510, 23);
            this.pnlWeekDays.ResumeLayout(false);
            this.pnlWeekDays.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsWeeklySchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel pnlWeekDays;
        private System.Windows.Forms.CheckBox chkWeekSunday;
        private System.Windows.Forms.CheckBox chkWeekMonday;
        private System.Windows.Forms.CheckBox chkWeekTuesday;
        private System.Windows.Forms.CheckBox chkWeekWednesday;
        private System.Windows.Forms.CheckBox chkWeekThursday;
        private System.Windows.Forms.CheckBox chkWeekFriday;
        private System.Windows.Forms.CheckBox chkWeekSaturday;
        private System.Windows.Forms.BindingSource bsWeeklySchedule;
    }
}
