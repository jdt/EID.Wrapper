using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace EID.Wrapper
{
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("d9bbe1d0-086a-4aa6-81b1-46c042d27c7c")]
    [ComVisible(true)]
    public interface ICardData
    {
        /// <summary>
        /// Will return a result indication of the read operation from one or more slots
        /// </summary>
        CardDataStatus CardDataStatus { get; }
        /// <summary>
        /// If the CardStatus returns Error, this field will contain the exception that was raised
        /// </summary>
        Exception Error { get; }
        /// <summary>
        /// A list containing data from every card read
        /// </summary>
        ICard[] Cards { get; }
    }
}
