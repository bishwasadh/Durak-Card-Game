//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DurakCardGame.Models
//{
//    public enum GamePhase
//    {
//        Attack,
//        Defense,
//        GameOver
//    }

//    public class GameState
//    {
//        // Private fields
//        private List<Card> _attackingCards;
//        private List<Card> _defendingCards;
//        private Deck _deck;
//        private Player _attacker;
//        private Player _defender;
//        private Card.Suit _trumpSuit;
//        private GamePhase _currentPhase;
//        private bool _defenderTookCards;

//        // Public properties
//        public List<Card> AttackingCards => _attackingCards;
//        public List<Card> DefendingCards => _defendingCards;
//        public Deck Deck => _deck;
//        public Player Attacker => _attacker;
//        public Player Defender => _defender;
//        public Card.Suit TrumpSuit => _trumpSuit;
//        public GamePhase CurrentPhase => _currentPhase;
//        public bool IsGameOver => _currentPhase == GamePhase.GameOver;

//        // Constructor
//        public GameState(Player player1, Player player2)
//        {
//            _attackingCards = new List<Card>();
//            _defendingCards = new List<Card>();
//            _deck = new Deck();
//            _trumpSuit = _deck.TrumpSuit;

//            // Deal 6 cards to each player
//            player1.AddCards(_deck.DealCards(6));
//            player2.AddCards(_deck.DealCards(6));

//            // For simplicity in testing, player1 always starts as attacker
//            // In a full implementation, determine first attacker by lowest trump
//            _attacker = player1;
//            _defender = player2;

//            _currentPhase = GamePhase.Attack;
//            _defenderTookCards = false;
//        }

//        // Attack with a card
//        public bool Attack(Card card)
//        {
//            // Can only attack in attack phase
//            if (_currentPhase != GamePhase.Attack)
//                return false;

//            // Validate the card is in attacker's hand
//            Card playedCard = _attacker.PlayCard(card);
//            if (playedCard == null)
//                return false;

//            // Valid attack
//            _attackingCards.Add(playedCard);
//            _currentPhase = GamePhase.Defense;

//            return true;
//        }

//        // Defend with a card
//        public bool Defend(Card attackingCard, Card defendingCard)
//        {
//            // Can only defend in defense phase
//            if (_currentPhase != GamePhase.Defense)
//                return false;

//            // Validate the defending card is in defender's hand
//            Card playedCard = _defender.PlayCard(defendingCard);
//            if (playedCard == null)
//                return false;

//            // Check if defending card can beat attacking card
//            if (!playedCard.IsStrongerThan(attackingCard, _trumpSuit))
//            {
//                // Cannot defend, return card to hand
//                _defender.AddCard(playedCard);
//                return false;
//            }

//            // Valid defense
//            _defendingCards.Add(playedCard);

//            // If all attacks are defended, switch to attack phase
//            if (_defendingCards.Count == _attackingCards.Count)
//            {
//                // All cards are defended, end the round
//                EndRound();
//            }

//            return true;
//        }

//        // Defender takes all cards from the table
//        public bool TakeCards()
//        {
//            // Can only take cards in defense phase
//            if (_currentPhase != GamePhase.Defense)
//                return false;

//            // Add all cards on the table to defender's hand
//            _defender.AddCards(_attackingCards);
//            _defender.AddCards(_defendingCards);

//            _defenderTookCards = true;

//            // Clear table
//            _attackingCards.Clear();
//            _defendingCards.Clear();

//            // Deal cards to refill hands (attacker first, then defender)
//            DealCardsToRefill();

//            _currentPhase = GamePhase.Attack;

//            return true;
//        }

//        // End the round, clear table, and swap roles if needed
//        private void EndRound()
//        {
//            // Clear the table
//            _attackingCards.Clear();
//            _defendingCards.Clear();

//            // Deal cards to refill hands (attacker first, then defender)
//            DealCardsToRefill();

//            // If defender didn't take cards, swap roles
//            if (!_defenderTookCards)
//            {
//                Player temp = _attacker;
//                _attacker = _defender;
//                _defender = temp;
//            }

//            _defenderTookCards = false;
//            _currentPhase = GamePhase.Attack;

//            // Check for win conditions
//            CheckGameOver();
//        }

//        // Deal cards to refill hands (attacker first, then defender)
//        private void DealCardsToRefill()
//        {
//            // Deal cards until players have 6 cards or deck is empty
//            while (_attacker.CardCount < 6 && !_deck.IsEmpty)
//            {
//                _attacker.AddCard(_deck.DealCard());
//            }

//            while (_defender.CardCount < 6 && !_deck.IsEmpty)
//            {
//                _defender.AddCard(_deck.DealCard());
//            }
//        }

//        // Check if the game is over
//        private void CheckGameOver()
//        {
//            // Game is over when deck is empty and one player has no cards
//            if (_deck.IsEmpty && (_attacker.CardCount == 0 || _defender.CardCount == 0))
//            {
//                _currentPhase = GamePhase.GameOver;
//            }
//        }

//        // Get the winner (null if game is not over)
//        public Player GetWinner()
//        {
//            if (!IsGameOver)
//                return null;

//            // The player with no cards wins
//            if (_attacker.CardCount == 0)
//                return _attacker;
//            if (_defender.CardCount == 0)
//                return _defender;

//            // No winner yet
//            return null;
//        }
//    }
//}




using System.Collections.Generic;
using System.Linq;

namespace DurakCardGame.Models
{
    /// <summary>
    /// Represents the current state of the Durak game.
    /// </summary>
    public enum GamePhase
    {
        Attack,
        Defense,
        Resolve,
        GameOver
    }

    public class GameState
    {
        #region Fields
        private readonly List<Card> _attackingCards = new();
        private readonly List<Card> _defendingCards = new();
        private Deck _deck;
        private Player _attacker;
        private Player _defender;
        private GamePhase _currentPhase;
        private bool _defenderTookCards;
        #endregion

        #region Properties
        public List<Card> AttackingCards => _attackingCards;
        public List<Card> DefendingCards => _defendingCards;
        public Deck Deck => _deck;
        public Card.Suit TrumpSuit => _deck.TrumpSuit;
        public Player Attacker => _attacker;
        public Player Defender => _defender;
        public GamePhase CurrentPhase => _currentPhase;
        public bool DefenderTookCards => _defenderTookCards;
        public bool IsGameOver => _attacker.CardCount == 0 || _defender.CardCount == 0;
        #endregion

        #region Methods
        public void StartNewRound(Player attacker, Player defender, Deck deck)
        {
            _attacker = attacker;
            _defender = defender;
            _deck = deck;
            _currentPhase = GamePhase.Attack;

            _attackingCards.Clear();
            _defendingCards.Clear();
            _defenderTookCards = false;
        }

        public void AddAttackCard(Card card)
        {
            _attackingCards.Add(card);
        }

        public void AddDefenseCard(Card card)
        {
            _defendingCards.Add(card);
        }

        public void DefenderTakesCards()
        {
            _defenderTookCards = true;
            _defender.AddCards(_attackingCards);
            _defender.AddCards(_defendingCards);
            _attackingCards.Clear();
            _defendingCards.Clear();
        }

        public void ClearTable()
        {
            _attackingCards.Clear();
            _defendingCards.Clear();
        }

        public void SetPhase(GamePhase phase)
        {
            _currentPhase = phase;
        }

        public void SwitchRoles()
        {
            var temp = _attacker;
            _attacker = _defender;
            _defender = temp;
        }
        #endregion
    }
}
