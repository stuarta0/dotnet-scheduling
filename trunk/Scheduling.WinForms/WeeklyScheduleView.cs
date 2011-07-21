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
    public partial class WeeklyScheduleView : UserControl, IScheduleView 
    {
        public WeeklyScheduleView()
        {
            InitializeComponent();
        }

        #region IScheduleView Members

        private WeeklySchedule _schedule = null;

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
                if (value != null && !(value is WeeklySchedule))
                    throw new ArgumentException("WeeklyScheduleView.CurrentSchedule must be of type WeeklySchedule.");

                _schedule = value as WeeklySchedule;
                if (_schedule == null)
                    bsWeeklySchedule.DataSource = typeof(WeeklySchedule);
                else
                    bsWeeklySchedule.DataSource = _schedule;

                this.Enabled = (_schedule != null);
                OnCurrentScheduleChanged();
            }
        }

        #endregion
    }
}
