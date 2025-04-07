//using System;

//namespace DurakCardGame.Models
//{
//    public class Card
//    {
//        // Enums for card properties
//        public enum Suit { Clubs, Diamonds, Hearts, Spades }
//        public enum Rank { Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14 }

//        // Private fields with public properties (encapsulation)
//        private Suit _suit;
//        private Rank _rank;

//        public Suit CardSuit
//        {
//            get { return _suit; }
//            private set { _suit = value; }
//        }

//        public Rank CardRank
//        {
//            get { return _rank; }
//            private set { _rank = value; }
//        }

//        // Constructor
//        public Card(Suit suit, Rank rank)
//        {
//            CardSuit = suit;
//            CardRank = rank;
//        }

//        // Methods
//        public override string ToString()
//        {
//            return $"{CardRank} of {CardSuit}";
//        }

//        // Compare cards - useful for game logic later
//        public bool IsStrongerThan(Card otherCard, Suit trumpSuit)
//        {
//            // If this card is trump and other isn't
//            if (CardSuit == trumpSuit && otherCard.CardSuit != trumpSuit)
//                return true;

//            // If other card is trump and this isn't
//            if (CardSuit != trumpSuit && otherCard.CardSuit == trumpSuit)
//                return false;

//            // If both same suit, compare ranks
//            if (CardSuit == otherCard.CardSuit)
//                return CardRank > otherCard.CardRank;

//            // If different suits and neither is trump, attacker can't beat defender
//            return false;
//        }
//    }
//}


using System;

namespace DurakCardGame.Models
{
    /// <summary>
    /// Represents a playing card with a suit and rank.
    /// </summary>
    public class Card
    {
        #region Enums
        public enum Suit { Clubs, Diamonds, Hearts, Spades }
        public enum Rank { Six = 6, Seven, Eight, Nine, Ten, Jack, Queen, King, Ace }
        #endregion

        #region Properties
        public Suit CardSuit { get; private set; }
        public Rank CardRank { get; private set; }
        #endregion

        #region Constructor
        public Card(Suit suit, Rank rank)
        {
            CardSuit = suit;
            CardRank = rank;
        }
        #endregion

        #region Methods
        public override string ToString() => $"{CardRank} of {CardSuit}";

        public bool CanDefendAgainst(Card attackCard, Suit trump)
        {
            if (CardSuit == attackCard.CardSuit && CardRank > attackCard.CardRank)
                return true;
            if (CardSuit == trump && attackCard.CardSuit != trump)
                return true;
            return false;
        }

        public bool IsTrump(Suit trump) => CardSuit == trump;
        #endregion
    }
}

