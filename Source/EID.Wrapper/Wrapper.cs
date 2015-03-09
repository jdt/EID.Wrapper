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
                //this is a very expensive call, hence the ICardData-result object has properties indicating weather or not this call worked
                Slot[] slotlist = m.GetSlotList(true);
                if (slotlist.Length > 0)
                {
                    Slot slot = slotlist[0];

                    // Search for objects
                    // First, define a search template

                    Session session = slot.Token.OpenSession(true);

                    IDictionary<string, byte[]> cardData = new Dictionary<string, byte[]>();

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
                            var value = data.Value.Value;

                            if (!cardData.ContainsKey(label))
                                cardData.Add(label, value);
                            else
                                cardData[label] = value;
                        }
                        counter--;
                    }

                    session.FindObjectsFinal();
                    
                    result.BirthDate = Encoding.UTF8.GetString(cardData["date_of_birth"]);
                    result.BirthPlace = Encoding.UTF8.GetString(cardData["location_of_birth"]);
                    result.FirstNames = Encoding.UTF8.GetString(cardData["firstnames"]);
                    result.Gender = Encoding.UTF8.GetString(cardData["gender"]);
                    result.Municipality = Encoding.UTF8.GetString(cardData["address_municipality"]);
                    result.Nationality = Encoding.UTF8.GetString(cardData["nationality"]);
                    result.NationalNumber = Encoding.UTF8.GetString(cardData["national_number"]);
                    result.StreetAndNumber = Encoding.UTF8.GetString(cardData["address_street_and_number"]);
                    result.Surname = Encoding.UTF8.GetString(cardData["surname"]);
                    result.ZipCode = Encoding.UTF8.GetString(cardData["address_zip"]);
                    result.PhotoData = cardData["PHOTO_FILE"];
                    
                    result.CardStatus = CardStatus.Read;
                }
                else
                {
                    result.CardStatus = CardStatus.NoCardFound;
                }
            }
            catch(Exception ex)
            {
                result.CardStatus = CardStatus.Error;
                result.Error = ex;
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
