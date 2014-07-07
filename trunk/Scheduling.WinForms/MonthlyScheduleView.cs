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
    /// <remarks>
    /// Control to edit WeeklySchedule specific information.  Does not contain general Schedule information.
    /// </remarks>
    public partial class MonthlyScheduleView : UserControl, IScheduleView 
    {
        public MonthlyScheduleView()
        {
            InitializeComponent();
            radioPanel.EnumType = typeof(MonthlySchedule.IntervalType);
        }

        #region IScheduleView Members

        private MonthlySchedule _schedule = null;

        public event EventHandler CurrentScheduleChanged;
        protected virtual void OnCurrentScheduleChanged()
        {
            if (CurrentScheduleChanged != null)
                CurrentScheduleChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Gets or sets the schedule object.  Null will disable the control.  Must be of type WeeklySchedule.
        /// </summary>
        public virtual Schedule CurrentSchedule
        {
            get
            {
                return _schedule;
            }
            set
            {
                if (value != null && !(value is MonthlySchedule))
                    throw new ArgumentException("MonthlyScheduleView.CurrentSchedule must be of type MonthlySchedule.");

                _schedule = value as MonthlySchedule;
                if (_schedule == null)
                    bsMonthlySchedule.DataSource = typeof(MonthlySchedule);
                else
                {
                    bsMonthlySchedule.DataSource = _schedule;
                    radioPanel.DataSource = _schedule;
                }

                this.Enabled = (_schedule != null);
                OnCurrentScheduleChanged();
            }
        }

        #endregion

        private void radioPanel_FormatEnum(object sender, CustomControls.FormatEventArgs e)
        {
            if (e.Source is MonthlySchedule.IntervalType)
            {
                switch ((MonthlySchedule.IntervalType)e.Source)
                {
                    case MonthlySchedule.IntervalType.DayOfMonth:
                        e.Target = "Day of month";
                        break;
                    case MonthlySchedule.IntervalType.DayOfWeek:
                        e.Target = "Day of week";
                        break;
                    default:
                        break;
                }
            }

        }
    }
}
