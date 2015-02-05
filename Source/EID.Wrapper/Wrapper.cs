using System;
using System.Collections.Generic;

using System.Text;

using System.Runtime.InteropServices;

using Net.Sf.Pkcs11;
using Net.Sf.Pkcs11.Objects;
using Net.Sf.Pkcs11.Wrapper;

using System.Security.Cryptography.X509Certificates;

namespace EID.Wrapper
{
    [ClassInterface(ClassInterfaceType.None)]
    [Guid("19ff9b41-8652-4012-ac55-c1a1d8f18cbb")]
    [ComVisible(true)]
    [ProgId("EID.Wrapper")]
    public class Wrapper : IWrapper
    {
        private String mFileName;

        /// <summary>
        /// Default constructor. Will instantiate the beidpkcs11.dll pkcs11 module
        /// </summary>
        public Wrapper()
        {
            mFileName = "beidpkcs11.dll";
        }

        public ICardData GetCardData()
        {
            var result = new CardData();

            var m = Module.GetInstance(mFileName);
            // pkcs11 module init
            //m.Initialize();
            try
            {
                // Get the first slot (cardreader) with a token
                //this is a very expensive call!
                Slot[] slotlist = m.GetSlotList(true);
                if (slotlist.Length > 0)
                {
                    Slot slot = slotlist[0];

                    // Search for objects
                    // First, define a search template

                    Session session = slot.Token.OpenSession(true);

                    IDictionary<string, string> cardData = new Dictionary<string, string>();

                    // "The label attribute of the objects should equal ..."
                    ByteArrayAttribute classAttribute = new ByteArrayAttribute(CKA.CLASS);
                    classAttribute.Value = BitConverter.GetBytes((uint)Net.Sf.Pkcs11.Wrapper.CKO.DATA);
                    
                    session.FindObjectsInit(new P11Attribute[] { classAttribute });

                    P11Object[] foundObjects = session.FindObjects(50);
                    int counter = foundObjects.Length;
                    Data data;
                    while (counter > 0)
                    {
                        //foundObjects[counter-1].ReadAttributes(session);
                        //public static BooleanAttribute ReadAttribute(Session session, uint hObj, BooleanAttribute attr)
                        data = foundObjects[counter - 1] as Data;
                        //label = data.Label.ToString();
                        if (data.Value.Value != null)
                        {
                            var label = new string(data.Label.Value);
                            var value = System.Text.Encoding.UTF8.GetString(data.Value.Value);

                            if (!cardData.ContainsKey(label))
                                cardData.Add(label, value);
                            else
                                cardData[label] = value;
                        }
                        counter--;
                    }

                    session.FindObjectsFinal();
                    
                    result.BirthDate = cardData["date_of_birth"];
                    result.BirthPlace = cardData["location_of_birth"];
                    result.FirstNames = cardData["firstnames"];
                    result.Gender = cardData["gender"];
                    result.Municipality = cardData["address_municipality"];
                    result.Nationality = cardData["nationality"];
                    result.NationalNumber = cardData["national_number"];
                    result.StreetAndNumber = cardData["address_street_and_number"];
                    result.Surname = cardData["surname"];
                    result.ZipCode = cardData["address_zip"];
                }
                else
                {
                    throw new Exception("No card found\n");
                }
            }
            finally
            {
                // pkcs11 finalize
                m.Dispose();//m.Finalize_();
            }
            return result;
        }
    }
}
