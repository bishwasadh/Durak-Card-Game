//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DurakCardGame.Models
//{
//    public class Deck
//    {
//        // Private fields
//        private List<Card> _cards;
//        private Random _random;
//        private Card.Suit _trumpSuit;

//        // Public properties
//        public Card.Suit TrumpSuit => _trumpSuit;
//        public int RemainingCards => _cards.Count;
//        public bool IsEmpty => _cards.Count == 0;

//        // Constructor
//        public Deck()
//        {
//            _random = new Random();
//            InitializeDeck();
//        }

//        // Initialize a fresh Durak deck with 36 cards (6-Ace)
//        private void InitializeDeck()
//        {
//            _cards = new List<Card>();

//            // For each suit
//            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
//            {
//                // For each rank (6-Ace)
//                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
//                {
//                    _cards.Add(new Card(suit, rank));
//                }
//            }

//            // Shuffle the deck
//            Shuffle();

//            // Set the trump suit (bottom card)
//            if (_cards.Any())
//                _trumpSuit = _cards.Last().CardSuit;
//        }

//        // Shuffle the deck
//        public void Shuffle()
//        {
//            // Fisher-Yates shuffle algorithm
//            int n = _cards.Count;
//            while (n > 1)
//            {
//                n--;
//                int k = _random.Next(n + 1);
//                Card temp = _cards[k];
//                _cards[k] = _cards[n];
//                _cards[n] = temp;
//            }
//        }

//        // Deal a single card from the top of the deck
//        public Card DealCard()
//        {
//            if (_cards.Count > 0)
//            {
//                Card card = _cards[0];
//                _cards.RemoveAt(0);
//                return card;
//            }
//            return null;  // No cards left
//        }

//        // Deal multiple cards
//        public List<Card> DealCards(int count)
//        {
//            List<Card> dealtCards = new List<Card>();
//            for (int i = 0; i < count && _cards.Count > 0; i++)
//            {
//                dealtCards.Add(DealCard());
//            }
//            return dealtCards;
//        }

//        // Get the trump card (bottom card, but don't remove it)
//        public Card GetTrumpCard()
//        {
//            if (_cards.Any())
//                return _cards.Last();
//            return null;
//        }
//    }
//}



using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakCardGame.Models
{
    /// <summary>
    /// Represents a Durak deck (36 cards, with a trump suit).
    /// </summary>
    public class Deck
    {
        #region Fields
        private List<Card> _cards;
        private Random _random;
        private Card.Suit _trumpSuit;
        #endregion

        #region Properties
        public Card.Suit TrumpSuit => _trumpSuit;
        public int RemainingCards => _cards.Count;
        public bool IsEmpty => !_cards.Any();
        #endregion

        #region Constructor
        public Deck()
        {
            _random = new Random();
            InitializeDeck();
        }
        #endregion

        #region Methods
        private void InitializeDeck()
        {
            _cards = new List<Card>();

            foreach (Card.Suit suit in Enum.GetValues(typeof(Card.Suit)))
            {
                foreach (Card.Rank rank in Enum.GetValues(typeof(Card.Rank)))
                {
                    _cards.Add(new Card(suit, rank));
                }
            }

            Shuffle();
            SetTrump();
        }

        public void Shuffle()
        {
            _cards = _cards.OrderBy(x => _random.Next()).ToList();
        }

        private void SetTrump()
        {
            if (_cards.Count > 0)
            {
                _trumpSuit = _cards.Last().CardSuit;
            }
        }

        public Card DrawCard()
        {
            if (IsEmpty) return null;

            Card topCard = _cards[0];
            _cards.RemoveAt(0);
            return topCard;
        }

        public List<Card> DrawCards(int count)
        {
            var drawn = new List<Card>();
            for (int i = 0; i < count && !IsEmpty; i++)
            {
                drawn.Add(DrawCard());
            }
            return drawn;
        }
        #endregion
    }
}
