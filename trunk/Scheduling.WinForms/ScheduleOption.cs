using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduling.WinForms
{
    // NOTE: This class could be changed to have a list of Label/Control pairs which could be inserted into the table layout in ScheduleView
    // so the labels and controls all line up in the interface (instead of the ControlType property).  The labels would have to be instantiated 
    // by the consumer and the controls must have some way of binding to a Schedule object.  This is OK for a UserControl  as a property could 
    // by provided to bind to, but for a standard type like TextBox or CheckBox it may require some thought.  
    // These controls could either be dynamically instantiated or provided by the consumer.

    // TODO: Provide some way of controlling what is displayed in ScheduleView.  e.g. StartDateVisible, FrequencyVisible, etc...

    /// <remarks>
    /// A class used to instantiate a schedule and optionally a control for the particular schedule details at runtime.  To be used in ScheduleView.
    /// </remarks>
    public class ScheduleDisplayOption
    {
        /// <summary>
        /// Gets the name to display which identifies this schedule/control combination to the user.
        /// </summary>
        public virtual string Name { get; protected set; }

        /// <summary>
        /// Gets the type of schedule to instantiate for this option.  This type must represent a class that inherits from Schedule.
        /// </summary>
        public virtual Type ScheduleType { get; protected set; }
        
        /// <summary>
        /// Gets the type of control to instantiate for this option.  This type must represent a class that inherits from Control and implements IScheduleView.
        /// </summary>
        public virtual Type ControlType { get; protected set; }

        public ScheduleDisplayOption(string name, Type schedule, Type control)
        {
            Name = name;
            ScheduleType = schedule;
            ControlType = control;
        }

        public override string ToString()
        {
            return Name;
        }

        internal virtual Schedule CreateSchedule()
        {
            try
            {
                return (Schedule)Activator.CreateInstance(ScheduleType);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not create the Schedule instance.", ex);
            }
        }

        internal virtual Control CreateControl()
        {
            try
            {
                object instance = Activator.CreateInstance(ControlType);
                if (!(instance is IScheduleView))
                    throw new ArgumentException("Control must implement IScheduleView interface.");
                return (Control)instance;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Could not create the Control instance.", ex);
            }

        }
    }
}
