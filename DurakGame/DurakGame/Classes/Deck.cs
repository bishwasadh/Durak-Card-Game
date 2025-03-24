using DurakGame.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakGame.Classes
{
    public class Deck
    {
        private static readonly string[] Suits = { "Hearts", "Diamonds", "Clubs", "Spades" };
        private static readonly string[] Ranks = { "6", "7", "8", "9", "10", "J", "Q", "K", "A" };
        public List<Card> Cards { get; private set; }

        public Deck()
        {
            Cards = new List<Card>();
            foreach (var suit in Suits)
                foreach (var rank in Ranks)
                    Cards.Add(new Card(suit, rank));
            Shuffle();
        }

        public void Shuffle()
        {
            Random rng = new Random();
            Cards = Cards.OrderBy(_ => rng.Next()).ToList();
        }

        public Card DealCard()
        {
            if (Cards.Count == 0) return null;
            var card = Cards[0];
            Cards.RemoveAt(0);
            return card;
        }
    }
}
