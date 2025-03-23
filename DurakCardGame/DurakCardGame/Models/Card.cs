using System;

namespace DurakCardGame.Models
{
    public class Card
    {
        // Enums for card properties
        public enum Suit { Clubs, Diamonds, Hearts, Spades }
        public enum Rank { Six = 6, Seven = 7, Eight = 8, Nine = 9, Ten = 10, Jack = 11, Queen = 12, King = 13, Ace = 14 }

        // Private fields with public properties (encapsulation)
        private Suit _suit;
        private Rank _rank;

        public Suit CardSuit
        {
            get { return _suit; }
            private set { _suit = value; }
        }

        public Rank CardRank
        {
            get { return _rank; }
            private set { _rank = value; }
        }

        // Constructor
        public Card(Suit suit, Rank rank)
        {
            CardSuit = suit;
            CardRank = rank;
        }

        // Methods
        public override string ToString()
        {
            return $"{CardRank} of {CardSuit}";
        }

        // Compare cards - useful for game logic later
        public bool IsStrongerThan(Card otherCard, Suit trumpSuit)
        {
            // If this card is trump and other isn't
            if (CardSuit == trumpSuit && otherCard.CardSuit != trumpSuit)
                return true;

            // If other card is trump and this isn't
            if (CardSuit != trumpSuit && otherCard.CardSuit == trumpSuit)
                return false;

            // If both same suit, compare ranks
            if (CardSuit == otherCard.CardSuit)
                return CardRank > otherCard.CardRank;

            // If different suits and neither is trump, attacker can't beat defender
            return false;
        }
    }
}