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
        string BirthDate { get; }
        string BirthPlace { get; }
        string FirstNames { get; }
        string Gender { get; }
        string Municipality { get; }
        string Nationality { get; }
        string NationalNumber { get; }
        string StreetAndNumber { get; }
        string Surname { get; }
        string ZipCode { get; }
    }
}
