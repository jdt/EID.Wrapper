using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace EID.Wrapper
{
    [InterfaceType(ComInterfaceType.InterfaceIsDual)]
    [Guid("ee8f42af-4b88-4b3c-8bf8-fc1ed64807e1")]
    [ComVisible(true)]
    public interface ICard
    {
        CardStatus CardStatus { get; }
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
        string MemberOfFamily { get; }
        string SpecialOrganization { get; }
        string Duplicata { get; }
        string SpecialStatus { get; }
        string DocumentType { get; }
        string IssuingMunicipality { get; }
        string ValidityEndDate { get; }
        string ValidityBeginDate { get; }
        /// <summary>
        /// Chip Number is not in a 'readable' format as it is just an array of 16 bytes. This displays the value base64-encoded
        /// </summary>
        string ChipNumber { get; }
        string CardNumber { get; }

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
