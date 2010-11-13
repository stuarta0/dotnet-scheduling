namespace Scheduling.WinForms
{
    partial class ScheduleView
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
            this.tblSchedule = new System.Windows.Forms.TableLayoutPanel();
            this.lblRepeats = new System.Windows.Forms.Label();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.bsTypes = new System.Windows.Forms.BindingSource(this.components);
            this.lblDescription = new System.Windows.Forms.Label();
            this.bsSchedule = new System.Windows.Forms.BindingSource(this.components);
            this.lblEnding = new System.Windows.Forms.Label();
            this.tblEnding = new System.Windows.Forms.TableLayoutPanel();
            this.chkEndNever = new System.Windows.Forms.RadioButton();
            this.chkEndOn = new System.Windows.Forms.RadioButton();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this.tblPeriod = new System.Windows.Forms.TableLayoutPanel();
            this.txtTimeframe = new System.Windows.Forms.TextBox();
            this.lblPeriod = new System.Windows.Forms.Label();
            this.lblEvery = new System.Windows.Forms.Label();
            this.lblStarting = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.tblSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSchedule)).BeginInit();
            this.tblEnding.SuspendLayout();
            this.tblPeriod.SuspendLayout();
            this.SuspendLayout();
            // 
            // tblSchedule
            // 
            this.tblSchedule.AutoSize = true;
            this.tblSchedule.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblSchedule.ColumnCount = 2;
            this.tblSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblSchedule.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSchedule.Controls.Add(this.lblRepeats, 0, 1);
            this.tblSchedule.Controls.Add(this.cboType, 1, 1);
            this.tblSchedule.Controls.Add(this.lblDescription, 0, 0);
            this.tblSchedule.Controls.Add(this.lblEnding, 0, 4);
            this.tblSchedule.Controls.Add(this.tblEnding, 1, 4);
            this.tblSchedule.Controls.Add(this.tblPeriod, 1, 3);
            this.tblSchedule.Controls.Add(this.lblEvery, 0, 3);
            this.tblSchedule.Controls.Add(this.lblStarting, 0, 2);
            this.tblSchedule.Controls.Add(this.dtpStartDate, 1, 2);
            this.tblSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblSchedule.Location = new System.Drawing.Point(0, 0);
            this.tblSchedule.Name = "tblSchedule";
            this.tblSchedule.RowCount = 6;
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblSchedule.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblSchedule.Size = new System.Drawing.Size(450, 271);
            this.tblSchedule.TabIndex = 0;
            // 
            // lblRepeats
            // 
            this.lblRepeats.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblRepeats.AutoSize = true;
            this.lblRepeats.Location = new System.Drawing.Point(3, 67);
            this.lblRepeats.Name = "lblRepeats";
            this.lblRepeats.Size = new System.Drawing.Size(50, 13);
            this.lblRepeats.TabIndex = 0;
            this.lblRepeats.Text = "Repeats:";
            // 
            // cboType
            // 
            this.cboType.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cboType.DataSource = this.bsTypes;
            this.cboType.DisplayMember = "Name";
            this.cboType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboType.FormattingEnabled = true;
            this.cboType.Location = new System.Drawing.Point(59, 63);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(144, 21);
            this.cboType.TabIndex = 3;
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // bsTypes
            // 
            this.bsTypes.DataSource = typeof(Scheduling.WinForms.ScheduleDisplayOption);
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tblSchedule.SetColumnSpan(this.lblDescription, 2);
            this.lblDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSchedule, "Description", true));
            this.lblDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblDescription.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.Location = new System.Drawing.Point(4, 4);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Padding = new System.Windows.Forms.Padding(6);
            this.lblDescription.Size = new System.Drawing.Size(442, 52);
            this.lblDescription.TabIndex = 4;
            this.lblDescription.Text = "Every 2 weeks on Monday and Thursday, ending 12 April 2012";
            // 
            // bsSchedule
            // 
            this.bsSchedule.DataSource = typeof(Scheduling.Schedule);
            // 
            // lblEnding
            // 
            this.lblEnding.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEnding.AutoSize = true;
            this.lblEnding.Location = new System.Drawing.Point(3, 157);
            this.lblEnding.Name = "lblEnding";
            this.lblEnding.Size = new System.Drawing.Size(43, 13);
            this.lblEnding.TabIndex = 2;
            this.lblEnding.Text = "Ending:";
            // 
            // tblEnding
            // 
            this.tblEnding.AutoSize = true;
            this.tblEnding.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblEnding.ColumnCount = 2;
            this.tblEnding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEnding.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblEnding.Controls.Add(this.chkEndNever, 0, 0);
            this.tblEnding.Controls.Add(this.chkEndOn, 0, 1);
            this.tblEnding.Controls.Add(this.dtpEndDate, 1, 1);
            this.tblEnding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblEnding.Location = new System.Drawing.Point(56, 139);
            this.tblEnding.Margin = new System.Windows.Forms.Padding(0);
            this.tblEnding.Name = "tblEnding";
            this.tblEnding.RowCount = 3;
            this.tblEnding.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEnding.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEnding.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblEnding.Size = new System.Drawing.Size(394, 49);
            this.tblEnding.TabIndex = 5;
            // 
            // chkEndNever
            // 
            this.chkEndNever.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEndNever.AutoSize = true;
            this.chkEndNever.Checked = true;
            this.tblEnding.SetColumnSpan(this.chkEndNever, 2);
            this.chkEndNever.Location = new System.Drawing.Point(3, 3);
            this.chkEndNever.Name = "chkEndNever";
            this.chkEndNever.Size = new System.Drawing.Size(54, 17);
            this.chkEndNever.TabIndex = 0;
            this.chkEndNever.TabStop = true;
            this.chkEndNever.Text = "Never";
            this.chkEndNever.UseVisualStyleBackColor = true;
            // 
            // chkEndOn
            // 
            this.chkEndOn.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.chkEndOn.AutoSize = true;
            this.chkEndOn.Location = new System.Drawing.Point(3, 27);
            this.chkEndOn.Name = "chkEndOn";
            this.chkEndOn.Size = new System.Drawing.Size(39, 17);
            this.chkEndOn.TabIndex = 0;
            this.chkEndOn.Text = "On";
            this.chkEndOn.UseVisualStyleBackColor = true;
            this.chkEndOn.CheckedChanged += new System.EventHandler(this.chkEndOn_CheckedChanged);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpEndDate.Enabled = false;
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(48, 26);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(110, 20);
            this.dtpEndDate.TabIndex = 1;
            this.dtpEndDate.ValueChanged += new System.EventHandler(this.dtpEndDate_ValueChanged);
            // 
            // tblPeriod
            // 
            this.tblPeriod.AutoSize = true;
            this.tblPeriod.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblPeriod.ColumnCount = 2;
            this.tblPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPeriod.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblPeriod.Controls.Add(this.txtTimeframe, 0, 0);
            this.tblPeriod.Controls.Add(this.lblPeriod, 1, 0);
            this.tblPeriod.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblPeriod.Location = new System.Drawing.Point(56, 113);
            this.tblPeriod.Margin = new System.Windows.Forms.Padding(0);
            this.tblPeriod.Name = "tblPeriod";
            this.tblPeriod.RowCount = 1;
            this.tblPeriod.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tblPeriod.Size = new System.Drawing.Size(394, 26);
            this.tblPeriod.TabIndex = 6;
            // 
            // txtTimeframe
            // 
            this.txtTimeframe.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.txtTimeframe.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSchedule, "Frequency", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.txtTimeframe.Location = new System.Drawing.Point(3, 3);
            this.txtTimeframe.Name = "txtTimeframe";
            this.txtTimeframe.Size = new System.Drawing.Size(39, 20);
            this.txtTimeframe.TabIndex = 0;
            // 
            // lblPeriod
            // 
            this.lblPeriod.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblPeriod.AutoSize = true;
            this.lblPeriod.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsSchedule, "Period", true));
            this.lblPeriod.Location = new System.Drawing.Point(48, 6);
            this.lblPeriod.Name = "lblPeriod";
            this.lblPeriod.Size = new System.Drawing.Size(36, 13);
            this.lblPeriod.TabIndex = 2;
            this.lblPeriod.Text = "period";
            // 
            // lblEvery
            // 
            this.lblEvery.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblEvery.AutoSize = true;
            this.lblEvery.Location = new System.Drawing.Point(3, 119);
            this.lblEvery.Name = "lblEvery";
            this.lblEvery.Size = new System.Drawing.Size(37, 13);
            this.lblEvery.TabIndex = 2;
            this.lblEvery.Text = "Every:";
            // 
            // lblStarting
            // 
            this.lblStarting.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblStarting.AutoSize = true;
            this.lblStarting.Location = new System.Drawing.Point(3, 93);
            this.lblStarting.Name = "lblStarting";
            this.lblStarting.Size = new System.Drawing.Size(46, 13);
            this.lblStarting.TabIndex = 2;
            this.lblStarting.Text = "Starting:";
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.dtpStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsSchedule, "StartDate", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(59, 90);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(110, 20);
            this.dtpStartDate.TabIndex = 1;
            // 
            // ScheduleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tblSchedule);
            this.Name = "ScheduleView";
            this.Size = new System.Drawing.Size(450, 271);
            this.tblSchedule.ResumeLayout(false);
            this.tblSchedule.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsSchedule)).EndInit();
            this.tblEnding.ResumeLayout(false);
            this.tblEnding.PerformLayout();
            this.tblPeriod.ResumeLayout(false);
            this.tblPeriod.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tblSchedule;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label lblRepeats;
        private System.Windows.Forms.Label lblStarting;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.Label lblEnding;
        private System.Windows.Forms.TableLayoutPanel tblEnding;
        private System.Windows.Forms.RadioButton chkEndNever;
        private System.Windows.Forms.RadioButton chkEndOn;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label lblEvery;
        private System.Windows.Forms.TableLayoutPanel tblPeriod;
        private System.Windows.Forms.TextBox txtTimeframe;
        private System.Windows.Forms.Label lblPeriod;
        private System.Windows.Forms.BindingSource bsSchedule;
        private System.Windows.Forms.BindingSource bsTypes;
    }
}
