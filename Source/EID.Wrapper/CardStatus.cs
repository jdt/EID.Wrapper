using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EID.Wrapper
{
    [ComVisible(true)]
    [GuidAttribute("744b768f-62a4-4cd0-9f92-727658f55a9e")]
    public enum CardStatus
    {
        /// <summary>
        /// No operation was performed
        /// </summary>
        None,
        /// <summary>
        /// An exception occurred when trying to read card data
        /// </summary>
        Error,
        /// <summary>
        /// A read attempt was made but there was no cardreader with an available card found
        /// </summary>
        NoCardFound,
        /// <summary>
        /// A card was read and the data fetched
        /// </summary>
        Read
    }
}
