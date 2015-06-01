using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EID.Wrapper
{
    [ComVisible(true)]
    [GuidAttribute("ab266039-c8f4-402a-bc50-b0824bccbf55")]
    public enum CardDataStatus
    {
        /// <summary>
        /// No operation was performed
        /// </summary>
        None,
        /// <summary>
        /// An exception occurred when trying to find cards to read
        /// </summary>
        Error,
        /// <summary>
        /// Any available card slots were read and data returned
        /// </summary>
        Ready
    }
}
