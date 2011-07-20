using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduling.WinForms
{
    public partial class ScheduleView : UserControl, IScheduleView 
    {
        private bool _noEndAllowed;
        public bool NoEndDateAllowed
        {
            get { return _noEndAllowed; }
            set
            {
                if (_noEndAllowed == value)
                    return;

                _noEndAllowed = value;

                chkEndNever.Visible = _noEndAllowed;
                chkEndOn.Visible = _noEndAllowed;
                lblEnding.Text = (_noEndAllowed ? "Ending:" : "Ending On:");

                if (!_noEndAllowed)
                {
                    chkEndNever.Checked = false;
                    chkEndOn.Checked = true;
                }

                dtpEndDate.Enabled = chkEndOn.Checked;
            }
        }

        public ScheduleView()
        {
            InitializeComponent();

            NoEndDateAllowed = true;
            Binding b = lblPeriod.DataBindings["Text"];
            b.Format += new ConvertEventHandler(Period_Format);
        }

        void Period_Format(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(string))
            {
                if (_schedule != null)
                    e.Value = Strings.Plural(_schedule.Frequency, e.Value.ToString().ToLower(), e.Value.ToString().ToLower() + "s");
            }
        }

        public void SetOptions(IEnumerable<ScheduleDisplayOption> options)
        {
            if (options == null)
                return;

            bsTypes.DataSource = options;

            IEnumerator<ScheduleDisplayOption> enumerator = options.GetEnumerator();
            if (enumerator.MoveNext())
                SetSchedule(enumerator.Current);
        }


        private void chkEndOn_CheckedChanged(object sender, EventArgs e)
        {
            dtpEndDate.Enabled = chkEndOn.Checked;
            SetEndDate(false);
        }

        private void dtpEndDate_ValueChanged(object sender, EventArgs e)
        {
            SetEndDate(false);
        }

        private void SetEndDate(bool fromDataSource)
        {
            if (_schedule == null)
                return;

            if (fromDataSource)
            {
                DateTime? toSet = CurrentSchedule.EndDate;
                chkEndOn.Checked = toSet.HasValue;
                chkEndNever.Checked = !chkEndOn.Checked;
                dtpEndDate.Value = (toSet.HasValue ? toSet.Value : DateTime.Today);
            }
            else
            {
                if (chkEndOn.Checked)
                    _schedule.EndDate = dtpEndDate.Value;
                else
                    _schedule.EndDate = null;
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboType.SelectedItem is ScheduleDisplayOption)
                SetSchedule((ScheduleDisplayOption)cboType.SelectedItem);
        }

        private void SetSchedule(ScheduleDisplayOption option)
        {
            try
            {
                CurrentSchedule = option.CreateSchedule();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "There was a problem creating the requested schedule.\n\n" + ex.Message, "Create Schedule", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // unload the last control
            Control last = tblSchedule.GetControlFromPosition(1, 5);
            if (last != null)
            {
                tblSchedule.Controls.Remove(last);
                last.Dispose();
            }

            // load the custom control for specifics of this schedule type if necessary
            if (option.ControlType != null)
            {
                Control extra = null;
                try
                {
                    extra = option.CreateControl();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "There was a problem creating the requested schedule controls.\n\n" + ex.Message, "Create Schedule", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                extra.Dock = DockStyle.Fill;
                tblSchedule.Controls.Add(extra, 1, 5);
                ((IScheduleView)extra).CurrentSchedule = CurrentSchedule;
            }
        }

        #region IScheduleView Members

        private Schedule _schedule = null;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Schedule CurrentSchedule
        {
            get
            {
                return _schedule;
            }
            set
            {
                if (_schedule != null)
                    _schedule.PropertyChanged -= new PropertyChangedEventHandler(Schedule_PropertyChanged);

                _schedule = value;
                if (_schedule == null)
                {
                    bsSchedule.DataSource = typeof(Schedule);
                }
                else
                {
                    bsSchedule.DataSource = _schedule;
                    _schedule.PropertyChanged += new PropertyChangedEventHandler(Schedule_PropertyChanged);
                    SetEndDate(true);
                }

                this.Enabled = (_schedule != null);
            }
        }

        void Schedule_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EndDate")
            {
                SetEndDate(true);
            }
        }

        #endregion

    }
}
