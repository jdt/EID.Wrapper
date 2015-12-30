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
        private IList<String> _warnings;

        public Card()
        {
            CardStatus = CardStatus.None;
            Error = null;
            _warnings = new List<String>();
        }

        public CardStatus CardStatus { get; set; }
        public Exception Error { get; set; }
        public string[] Warnings { get { return _warnings.ToArray(); } }

        public string CardSlot { get; set; }

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

        public void ReadDataFrom(IDictionary<string, byte[]> cardData)
        {
            BirthDate =TryGetUTF8(cardData, "date_of_birth");
            BirthPlace = TryGetUTF8(cardData, "location_of_birth");
            FirstNames = TryGetUTF8(cardData, "firstnames");
            Gender = TryGetUTF8(cardData, "gender");
            Municipality = TryGetUTF8(cardData, "address_municipality");
            Nationality = TryGetUTF8(cardData, "nationality");
            NationalNumber = TryGetUTF8(cardData, "national_number");
            StreetAndNumber = TryGetUTF8(cardData, "address_street_and_number");
            Surname = TryGetUTF8(cardData, "surname");
            ZipCode = TryGetUTF8(cardData, "address_zip");

            MemberOfFamily = TryGetUTF8(cardData, "member_of_family");
            SpecialOrganization = TryGetUTF8(cardData, "special_organization");
            Duplicata = TryGetUTF8(cardData, "duplicata");
            SpecialStatus = TryGetUTF8(cardData, "special_status");
            DocumentType = TryGetUTF8(cardData, "document_type");
            IssuingMunicipality = TryGetUTF8(cardData, "issuing_municipality");

            ValidityEndDate = TryGetUTF8(cardData, "validity_end_date");
            ValidityBeginDate = TryGetUTF8(cardData, "validity_begin_date");
            ChipNumber = TryGetBase64String(cardData, "chip_number"); //the docs don't specify this, but this is actually just an array of 16 bytes, so...
            CardNumber = TryGetUTF8(cardData, "card_number");

            PhotoData = TryGetByteArray(cardData, "photo_file");
        }
        
        private string TryGetUTF8(IDictionary<string, byte[]> data, string key)
        {
            var stringData = string.Empty;

            if (!data.ContainsKey(key))
            {
                _warnings.Add(string.Format("Data for '{0}' not found", key));
            }
            else
            {
                try
                {
                    stringData = Encoding.UTF8.GetString(data[key]);
                }
                catch (Exception ex)
                {
                    _warnings.Add(string.Format("Exception occurred trying to convert '{0}' to an UTF8 string: '{1}'", key, ex.Message));
                }

            }
            return stringData;
        }

        private string TryGetBase64String(IDictionary<string, byte[]> data, string key)
        {
            var stringData = string.Empty;

            if (!data.ContainsKey(key))
            {
                _warnings.Add(string.Format("Data for '{0}' not found", key));
            }
            else
            {
                try
                {
                    stringData = Convert.ToBase64String(data[key]);
                }
                catch (Exception ex)
                {
                    _warnings.Add(string.Format("Exception occurred trying to convert '{0}' to an UTF8 string: '{1}'", key, ex.Message));
                }
            }

            return stringData;
        }

        private byte[] TryGetByteArray(IDictionary<string, byte[]> data, string key)
        {
            if (!data.ContainsKey(key))
            {
                _warnings.Add(string.Format("Data for '{0}' not found", key));
            }

            return data[key];
        }
    }
}
