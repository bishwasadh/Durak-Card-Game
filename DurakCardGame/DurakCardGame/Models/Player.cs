//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DurakCardGame.Models
//{
//    public class Player
//    {
//        // Private fields
//        private List<Card> _hand;
//        private string _name;

//        // Public properties
//        public string Name => _name;
//        public int CardCount => _hand.Count;
//        public List<Card> Hand => _hand;  // Normally we would return a copy for encapsulation,
//                                          // but for testing we'll allow direct access

//        // Constructor
//        public Player(string name)
//        {
//            _name = name;
//            _hand = new List<Card>();
//        }

//        // Add a card to the player's hand
//        public void AddCard(Card card)
//        {
//            if (card != null)
//            {
//                _hand.Add(card);
//            }
//        }

//        // Add multiple cards to the player's hand
//        public void AddCards(List<Card> cards)
//        {
//            if (cards != null)
//            {
//                _hand.AddRange(cards);
//            }
//        }

//        // Play a card (remove from hand and return it)
//        public Card PlayCard(int index)
//        {
//            if (index >= 0 && index < _hand.Count)
//            {
//                Card card = _hand[index];
//                _hand.RemoveAt(index);
//                return card;
//            }
//            return null;
//        }

//        // Play a specific card if it exists in the hand
//        public Card PlayCard(Card card)
//        {
//            int index = _hand.FindIndex(c =>
//                c.CardSuit == card.CardSuit &&
//                c.CardRank == card.CardRank);

//            return (index >= 0) ? PlayCard(index) : null;
//        }

//        // Check if player has any cards that can beat the attackingCard
//        public bool CanDefend(Card attackingCard, Card.Suit trumpSuit)
//        {
//            return _hand.Any(card => card.IsStrongerThan(attackingCard, trumpSuit));
//        }

//        // Find the lowest card that can beat the attacking card
//        public Card FindDefendingCard(Card attackingCard, Card.Suit trumpSuit)
//        {
//            // Get all cards that can beat the attacking card
//            var validDefenses = _hand.Where(card => card.IsStrongerThan(attackingCard, trumpSuit)).ToList();

//            if (!validDefenses.Any())
//                return null;

//            // Find the lowest valid card to play (strategic decision to save stronger cards)
//            return validDefenses.OrderBy(card =>
//                // Non-trump cards first
//                (card.CardSuit == trumpSuit ? 100 : 0) +
//                // Then by rank
//                (int)card.CardRank).FirstOrDefault();
//        }
//    }
//}



using System.Collections.Generic;
using System.Linq;

namespace DurakCardGame.Models
{
    /// <summary>
    /// Base class for a player in the Durak game.
    /// </summary>
    public class Player
    {
        #region Fields
        protected List<Card> _hand;
        protected string _name;
        #endregion

        #region Properties
        public string Name => _name;
        public int CardCount => _hand.Count;
        public virtual List<Card> Hand => _hand.ToList(); // Return a copy for safety
        #endregion

        #region Constructor
        public Player(string name)
        {
            _name = name;
            _hand = new List<Card>();
        }
        #endregion

        #region Methods
        public void AddCard(Card card)
        {
            if (card != null)
                _hand.Add(card);
        }

        public void AddCards(IEnumerable<Card> cards)
        {
            _hand.AddRange(cards.Where(c => c != null));
        }

        public void RemoveCard(Card card)
        {
            _hand.Remove(card);
        }

        public void ClearHand()
        {
            _hand.Clear();
        }
        #endregion
    }
}
