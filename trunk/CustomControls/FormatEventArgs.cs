using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CustomControls
{
    public delegate void FormatEventHandler(object sender, FormatEventArgs e);

    /// <summary>
    /// An event class to format a source object to a string target.
    /// </summary>
    public class FormatEventArgs : EventArgs
    {
        public object Source { get; protected set; }
        public string Target { get; set; }

        public FormatEventArgs(object value)
        {
            Source = value;
            Target = null;
        }
    }
}
