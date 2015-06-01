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
            CardData result = new CardData();

            using(var m = Module.GetInstance(mFileName))
            {
                Slot[] slotlist = null;
                try
                {
                    //get cardreaders with a token
                    //this is a very expensive call, hence the ICardData-result object has properties indicating weather or not this call worked
                    slotlist = m.GetSlotList(true);
                    result.CardDataStatus = CardDataStatus.Ready;
                }
                catch(Exception ex)
                {
                    result.CardDataStatus = CardDataStatus.Error;
                    result.Error = ex;
                }

                if(slotlist != null)
                {
                    foreach (var slot in slotlist)
                    {
                        var card = new Card();
                        card.CardSlot = slot.SlotInfo.SlotDescription;

                        try
                        {
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


                            card.BirthDate = Encoding.UTF8.GetString(cardData["date_of_birth"]);
                            card.BirthPlace = Encoding.UTF8.GetString(cardData["location_of_birth"]);
                            card.FirstNames = Encoding.UTF8.GetString(cardData["firstnames"]);
                            card.Gender = Encoding.UTF8.GetString(cardData["gender"]);
                            card.Municipality = Encoding.UTF8.GetString(cardData["address_municipality"]);
                            card.Nationality = Encoding.UTF8.GetString(cardData["nationality"]);
                            card.NationalNumber = Encoding.UTF8.GetString(cardData["national_number"]);
                            card.StreetAndNumber = Encoding.UTF8.GetString(cardData["address_street_and_number"]);
                            card.Surname = Encoding.UTF8.GetString(cardData["surname"]);
                            card.ZipCode = Encoding.UTF8.GetString(cardData["address_zip"]);
                            
                            card.MemberOfFamily = Encoding.UTF8.GetString(cardData["member_of_family"]);
                            card.SpecialOrganization = Encoding.UTF8.GetString(cardData["special_organization"]);
                            card.Duplicata = Encoding.UTF8.GetString(cardData["duplicata"]);
                            card.SpecialStatus = Encoding.UTF8.GetString(cardData["special_status"]);
                            card.DocumentType = Encoding.UTF8.GetString(cardData["document_type"]);
                            card.IssuingMunicipality = Encoding.UTF8.GetString(cardData["issuing_municipality"]);
                            
                            card.ValidityEndDate = Encoding.UTF8.GetString(cardData["validity_end_date"]);
                            card.ValidityBeginDate = Encoding.UTF8.GetString(cardData["validity_begin_date"]);
                            card.ChipNumber = Convert.ToBase64String(cardData["chip_number"]); //the docs don't specify this, but this is actually just an array of 16 bytes, so...
                            card.CardNumber = Encoding.UTF8.GetString(cardData["card_number"]);
                            
                            card.PhotoData = cardData["PHOTO_FILE"];

                            card.CardStatus = CardStatus.Available;
                        }
                        catch (Exception ex)
                        {
                            card.CardStatus = CardStatus.Error;
                            card.Error = ex;
                        }

                        result.AddCard(card);
                    }
                }
            }

            return result;
        }
    }
}
