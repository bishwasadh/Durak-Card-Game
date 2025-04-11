using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakCardGame.Models
{
    /// <summary>
    /// Computer-controlled player logic.
    /// </summary>
    public class ComputerPlayer : Player
    {
        private readonly Random _random;

        public ComputerPlayer(string name) : base(name)
        {
            _random = new Random();
        }

        public Card ChooseAttackingCard(List<Card> inPlayRanks = null)
        {
            var playable = inPlayRanks == null || inPlayRanks.Count == 0
                ? Hand
                : Hand.Where(card => inPlayRanks.Any(r => r.CardRank == card.CardRank)).ToList();

            return playable.OrderBy(card => card.CardRank).FirstOrDefault();
        }

        public Card ChooseDefendingCard(Card attackingCard, Card.Suit trump)
        {
            var valid = Hand.Where(c => c.CanDefendAgainst(attackingCard, trump)).ToList();
            return valid.OrderBy(c => c.IsTrump(trump)).ThenBy(c => c.CardRank).FirstOrDefault();
        }
    }
}
