using System;
using System.Collections.Generic;
using System.Linq;

namespace DurakCardGame.Models
{
    // ComputerPlayer extends the Player class (inheritance)
    public class ComputerPlayer : Player
    {
        private Random _random;

        public ComputerPlayer(string name) : base(name)
        {
            _random = new Random();
        }

        // AI logic for choosing an attacking card
        public Card ChooseAttackingCard(List<Card> validRanks = null)
        {
            // If no cards to play
            if (Hand.Count == 0)
                return null;

            List<Card> playableCards = Hand.ToList(); // Make a copy of the hand

            // If validRanks is provided (not first attack of the round), filter cards
            if (validRanks != null && validRanks.Count > 0)
            {
                // Get the ranks currently in play
                var validRankValues = validRanks.Select(c => c.CardRank).Distinct().ToList();

                // Filter cards to only those with matching ranks
                playableCards = playableCards.Where(c => validRankValues.Contains(c.CardRank)).ToList();

                // If no valid cards to play
                if (playableCards.Count == 0)
                    return null;
            }

            // Basic AI strategy: play the lowest non-trump card first
            return playableCards
                .OrderBy(c => c.CardSuit == base.Hand[0].CardSuit ? 100 : 0) // Non-trump first (using first card's suit as trump for now)
                .ThenBy(c => (int)c.CardRank)  // Lower ranks first
                .FirstOrDefault();
        }

        // AI logic for choosing a defending card
        public Card ChooseDefendingCard(Card attackingCard, Card.Suit trumpSuit)
        {
            // Find the lowest card that can beat the attacking card
            return FindDefendingCard(attackingCard, trumpSuit);
        }

        // AI logic for deciding whether to take cards or try to defend
        public bool ShouldDefend(List<Card> attackingCards, List<Card> defendingCards, Card.Suit trumpSuit)
        {
            // Count how many cards we can defend against
            int cardsToDefend = attackingCards.Count - defendingCards.Count;
            int defendableCount = 0;

            // Check each attacking card that hasn't been defended yet
            for (int i = defendingCards.Count; i < attackingCards.Count; i++)
            {
                if (CanDefend(attackingCards[i], trumpSuit))
                {
                    defendableCount++;
                }
            }

            // Simple strategy: defend if we can defend all cards
            return defendableCount >= cardsToDefend;
        }
    }
}