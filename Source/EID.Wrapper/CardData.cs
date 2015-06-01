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
        private IList<ICard> _cards;

        public CardData()
        {
            CardDataStatus = CardDataStatus.None;
            Error = null;

            _cards = new List<ICard>();
        }

        public void AddCard(ICard card)
        {
            _cards.Add(card);
        }

        public CardDataStatus CardDataStatus { get; set; }
        public Exception Error { get; set; }

        public ICard FirstCard
        {
            get
            {
                return _cards.FirstOrDefault(x => x.CardStatus == CardStatus.Available);
            }
        }

        public object Cards
        {
            get
            {
                return _cards.ToArray();
            }
        }
    }
}
