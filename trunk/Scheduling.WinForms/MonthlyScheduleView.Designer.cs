namespace Scheduling.WinForms
{
    partial class MonthlyScheduleView
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
            this.bsMonthlySchedule = new System.Windows.Forms.BindingSource(this.components);
            this.radioPanel = new CustomControls.RadioPanel();
            ((System.ComponentModel.ISupportInitialize)(this.bsMonthlySchedule)).BeginInit();
            this.SuspendLayout();
            // 
            // bsMonthlySchedule
            // 
            this.bsMonthlySchedule.DataSource = typeof(Scheduling.MonthlySchedule);
            // 
            // radioPanel
            // 
            this.radioPanel.AutoSize = true;
            this.radioPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.radioPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.radioPanel.Location = new System.Drawing.Point(0, 0);
            this.radioPanel.Margin = new System.Windows.Forms.Padding(0);
            this.radioPanel.Name = "radioPanel";
            this.radioPanel.PanelLayout = CustomControls.RadioPanel.LayoutType.Flow;
            this.radioPanel.Size = new System.Drawing.Size(188, 71);
            this.radioPanel.TabIndex = 3;
            this.radioPanel.ValueMember = "ReoccurrenceType";
            this.radioPanel.FormatEnum += new CustomControls.FormatEventHandler(this.radioPanel_FormatEnum);
            // 
            // MonthlyScheduleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radioPanel);
            this.Name = "MonthlyScheduleView";
            this.Size = new System.Drawing.Size(188, 71);
            ((System.ComponentModel.ISupportInitialize)(this.bsMonthlySchedule)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource bsMonthlySchedule;
        private CustomControls.RadioPanel radioPanel;
    }
}
