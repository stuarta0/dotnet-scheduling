using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CustomControls
{
    public delegate void RadioSelectionChangedEventHandler(object sender, RadioSelectionChangedEventArgs e);

    /// <summary>
    /// An EventArgs class for radio button selection changed events. 
    /// </summary>
    public class RadioSelectionChangedEventArgs : EventArgs
    {
        private RadioButton m_rbButtonClicked;

        public RadioSelectionChangedEventArgs(RadioButton rb)
        {
            m_rbButtonClicked = rb;
        }

        public RadioButton RadioButtonClicked
        {
            get
            {
                return m_rbButtonClicked;
            }
        }
    }
}
