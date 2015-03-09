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
        /// Will return a result indication of a read on the card
        /// </summary>
        CardStatus CardStatus { get; }
        /// <summary>
        /// If the CardStatus returns Error, this field will contain the exception that was raised
        /// </summary>
        Exception Error { get; }

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

        /// <summary>
        /// The raw byte array of photo data on the card (jpg-image)
        /// </summary>
        byte[] PhotoData { get; }

        /// <summary>
        /// Saves the photo data to a file
        /// </summary>
        /// <param name="fileName">Full path of the file to save to</param>
        void SavePhoto(string fileName);
    }
}
