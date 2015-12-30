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
                                    var label = new string(data.Label.Value).ToLower();
                                    var value = data.Value.Value;

                                    if (!cardData.ContainsKey(label))
                                        cardData.Add(label, value);
                                    else
                                        cardData[label] = value;
                                }
                                counter--;
                            }

                            session.FindObjectsFinal();

                            card.ReadDataFrom(cardData);
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
