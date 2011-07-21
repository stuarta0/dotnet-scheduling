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

                if (!_noEndAllowed && CurrentSchedule != null)
                    CurrentSchedule.EndDate = DateTime.Today;
            }
        }

        public ScheduleView()
        {
            InitializeComponent();

            NoEndDateAllowed = true;
            lblPeriod.DataBindings["Text"].Format += new ConvertEventHandler(Period_Format);

            chkEndNever.DataBindings["Checked"].Format += new ConvertEventHandler(CheckBoxNeverEnd_Format);
            chkEndNever.DataBindings["Checked"].Parse += new ConvertEventHandler(CheckBoxNeverEnd_Parse);

            chkEndOn.DataBindings["Checked"].Format += new ConvertEventHandler(CheckBoxEndOn_Format);
            chkEndOn.DataBindings["Checked"].Parse += new ConvertEventHandler(CheckBoxEndOn_Parse);

            dtpEndDate.DataBindings["Value"].Format += new ConvertEventHandler(EndDate_Format);
            dtpEndDate.DataBindings["Enabled"].Format += new ConvertEventHandler(EndDate_Format);
        }

        #region Binding Event Handlers

        void EndDate_Format(object sender, ConvertEventArgs e)
        {
            // from object to control
            DateTime? val = e.Value as DateTime?;

            // if desired type is DateTime then we're binding Value
            // if desired type is Boolean then we're binding Enabled
            if (e.DesiredType == typeof(DateTime))
                e.Value = (val.HasValue ? e.Value : dtpEndDate.Value);
            else if (e.DesiredType == typeof(bool))
                e.Value = val.HasValue;
        }

        void CheckBoxNeverEnd_Parse(object sender, ConvertEventArgs e)
        {
            // from control to object
            if (e.DesiredType == typeof(DateTime?) && e.Value is bool)
            {
                //e.Value = ((bool)e.Value == true ? (DateTime?)null : DateTime.Today);
                if ((bool)e.Value == true)
                    e.Value = null;
                else
                    e.Value = ((Schedule)((BindingSource)((Binding)sender).DataSource).Current).EndDate;
            }
        }

        void CheckBoxNeverEnd_Format(object sender, ConvertEventArgs e)
        {
            // from object to control

            // Never Ending should be checked when there is no end date
            if (e.DesiredType == typeof(bool))
                e.Value = (e.Value == null);
        }

        void CheckBoxEndOn_Parse(object sender, ConvertEventArgs e)
        {
            // from control to object
            if (e.DesiredType == typeof(DateTime?) && e.Value is bool)
            {
                //e.Value = ((bool)e.Value == true ? DateTime.Today : (DateTime?)null);
                if ((bool)e.Value == true)
                    e.Value = dtpEndDate.Value;
                else
                    e.Value = ((Schedule)((BindingSource)((Binding)sender).DataSource).Current).EndDate;
            }
        }

        void CheckBoxEndOn_Format(object sender, ConvertEventArgs e)
        {
            // from object to control

            // Ending On should be checked when there is an end date
            if (e.DesiredType == typeof(bool))
                e.Value = (e.Value != null);
        }

        void Period_Format(object sender, ConvertEventArgs e)
        {
            if (e.DesiredType == typeof(string))
            {
                if (_schedule != null)
                    e.Value = Strings.Plural(_schedule.Frequency, e.Value.ToString().ToLower(), e.Value.ToString().ToLower() + "s");
            }
        }

        #endregion


        public void SetOptions(IEnumerable<ScheduleDisplayOption> options)
        {
            if (options == null)
                return;

            bsTypes.DataSource = options;

            IEnumerator<ScheduleDisplayOption> enumerator = options.GetEnumerator();
            ScheduleDisplayOption toSet = null;
            for (int i = 0; enumerator.MoveNext(); i++)
            {
                if (i == 0)
                    toSet = enumerator.Current;

                if (CurrentSchedule != null && enumerator.Current.ScheduleType == CurrentSchedule.GetType())
                {
                    toSet = enumerator.Current;
                    break;
                }
            }

            if (CurrentSchedule == null)
                CreateSchedule(toSet);
            else
            {
                _typeComboCanCreate = false;
                cboType.SelectedItem = toSet;
                _typeComboCanCreate = true;

                RebuildUI(toSet);
            }
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_typeComboCanCreate && cboType.SelectedItem is ScheduleDisplayOption)
                CreateSchedule((ScheduleDisplayOption)cboType.SelectedItem);
        }

        private void CreateSchedule(ScheduleDisplayOption option)
        {
            DateTime? existingStartDate = null;
            if (CurrentSchedule != null)
                existingStartDate = CurrentSchedule.StartDate;

            try
            {
                CurrentSchedule = option.CreateSchedule();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "There was a problem creating the requested schedule.\n\n" + ex.Message, "Create Schedule", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (existingStartDate.HasValue)
                CurrentSchedule.StartDate = existingStartDate.Value;

            if (!NoEndDateAllowed)
                CurrentSchedule.EndDate = dtpEndDate.Value;

            RebuildUI(option);
        }   

        private void RebuildUI(ScheduleDisplayOption option)
        {
            // unload the last control
            Control last = tblSchedule.GetControlFromPosition(1, 5);
            if (last != null)
            {
                tblSchedule.Controls.Remove(last);
                last.Dispose();
            }

            if (option == null)
                return;

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

        private bool _typeComboCanCreate = true;
        private Schedule _schedule = null;

        public event EventHandler CurrentScheduleChanged;
        protected virtual void OnCurrentScheduleChanged()
        {
            if (CurrentScheduleChanged != null)
                CurrentScheduleChanged(this, EventArgs.Empty);
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Schedule CurrentSchedule
        {
            get
            {
                return _schedule;
            }
            set
            {
                _typeComboCanCreate = false;

                if (_schedule != null)
                    _schedule.PropertyChanged -= new PropertyChangedEventHandler(Schedule_PropertyChanged);

                _schedule = value;
                if (_schedule == null)
                {
                    bsSchedule.DataSource = typeof(Schedule);
                    cboType.SelectedItem = null;
                }
                else
                {
                    bsSchedule.DataSource = _schedule;
                    _schedule.PropertyChanged += new PropertyChangedEventHandler(Schedule_PropertyChanged);

                    cboType.SelectedItem = null;
                    foreach (ScheduleDisplayOption option in cboType.Items)
                    {
                        if (option.ScheduleType == _schedule.GetType())
                        {
                            cboType.SelectedItem = option;
                            break;
                        }
                    }

                    //SetEndDate(true);
                }

                this.Enabled = (_schedule != null);
                OnCurrentScheduleChanged();
                _typeComboCanCreate = true;

                RebuildUI(cboType.SelectedItem as ScheduleDisplayOption);
            }
        }

        void Schedule_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "EndDate")
            {
                //SetEndDate(true);
            }
        }

        #endregion

    }
}
