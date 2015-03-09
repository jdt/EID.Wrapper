using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EID.Wrapper
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("8866371b-a88a-48e2-b811-31153db14b4e")]
    [ComVisible(true)]
    [ProgId("EID.Wrapper")]
    public class CardData : ICardData
    {
        public CardData()
        {
            CardStatus = CardStatus.None;
        }

        public CardStatus CardStatus { get; set; }
        public Exception Error { get; set; }

        public string BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string FirstNames { get; set; }
        public string Gender { get;set; }
        public string Municipality { get; set; }
        public string Nationality { get; set; }
        public string NationalNumber { get; set; }
        public string StreetAndNumber { get; set; }
        public string Surname { get; set; }
        public string ZipCode { get; set; }

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
