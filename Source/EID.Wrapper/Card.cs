using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace EID.Wrapper
{
    public class Card : ICard
    {
        public Card()
        {
            CardStatus = CardStatus.None;
            Error = null;
        }

        public CardStatus CardStatus { get; set; }
        public Exception Error { get; set; }

        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string FirstNames { get; set; }
        public string Gender { get; set; }
        public string Municipality { get; set; }
        public string Nationality { get; set; }
        public string NationalNumber { get; set; }
        public string StreetAndNumber { get; set; }
        public string Surname { get; set; }
        public string ZipCode { get; set; }
        public string MemberOfFamily { get; set; }
        public string SpecialOrganization { get; set; }
        public string Duplicata { get; set; }
        public string SpecialStatus { get; set; }
        public string DocumentType { get; set; }
        public string IssuingMunicipality { get; set; }
        public string ValidityEndDate { get; set; }
        public string ValidityBeginDate { get; set; }
        public string ChipNumber { get; set; }
        public string CardNumber { get; set; }

        public byte[] PhotoData { get; set; }

        public void SavePhoto(string fileName)
        {
            using (MemoryStream ms = new MemoryStream(PhotoData))
            using (Image img = Image.FromStream(ms))
            {
                img.Save(fileName);
            }
        }
    }
}
