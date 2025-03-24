using DurakGame.Classes;
using System.Collections.Generic;

namespace DurakGame.Classes
{
    public class Player
    {
        public string Name { get; set; }
        public List<Card> Hand { get; private set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public Player(string name)
        {
            Name = name;
            Hand = new List<Card>();
        }

        public void ReceiveCard(Card card)
        {
            if (card != null) Hand.Add(card);
        }

        public void PlayCard(Card card)
        {
            Hand.Remove(card);
        }
    }
}
