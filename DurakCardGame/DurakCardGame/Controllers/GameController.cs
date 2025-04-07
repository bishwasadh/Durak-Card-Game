//using System;
//using System.Collections.Generic;
//using DurakCardGame.Models;

//namespace DurakCardGame.Controllers
//{
//    public class GameController
//    {
//        // Game state and players
//        private GameState _gameState;
//        private Player _humanPlayer;
//        private ComputerPlayer _computerPlayer;

//        // Event for notifying UI of game state changes
//        public event EventHandler GameStateChanged;

//        // Public properties to access game information
//        public GameState GameState => _gameState;
//        public Player HumanPlayer => _humanPlayer;
//        public ComputerPlayer ComputerPlayer => _computerPlayer;

//        // Constructor
//        public GameController()
//        {
//            InitializeGame();
//        }

//        // Initialize a new game
//        public void InitializeGame()
//        {
//            // Create players
//            _humanPlayer = new Player("Player");
//            _computerPlayer = new ComputerPlayer("Computer");

//            // Create game state
//            _gameState = new GameState(_humanPlayer, _computerPlayer);

//            // Notify UI
//            OnGameStateChanged();

//            // If computer starts, perform its turn
//            if (_gameState.Attacker == _computerPlayer)
//            {
//                PerformComputerTurn();
//            }
//        }

//        // Player attacks with a card
//        public bool PlayerAttack(Card card)
//        {
//            if (_gameState.CurrentPhase == GamePhase.Attack && _gameState.Attacker == _humanPlayer)
//            {
//                bool result = _gameState.Attack(card);
//                if (result)
//                {
//                    OnGameStateChanged();

//                    // If the attack was successful, let the computer defend
//                    if (_gameState.Defender == _computerPlayer)
//                    {
//                        PerformComputerTurn();
//                    }
//                }
//                return result;
//            }
//            return false;
//        }

//        // Player defends with a card
//        public bool PlayerDefend(Card attackingCard, Card defendingCard)
//        {
//            if (_gameState.CurrentPhase == GamePhase.Defense && _gameState.Defender == _humanPlayer)
//            {
//                bool result = _gameState.Defend(attackingCard, defendingCard);
//                if (result)
//                {
//                    OnGameStateChanged();

//                    // If all cards are defended, computer's turn to attack
//                    if (_gameState.CurrentPhase == GamePhase.Attack && _gameState.Attacker == _computerPlayer)
//                    {
//                        PerformComputerTurn();
//                    }
//                }
//                return result;
//            }
//            return false;
//        }

//        // Player takes cards
//        public bool PlayerTakeCards()
//        {
//            if (_gameState.CurrentPhase == GamePhase.Defense && _gameState.Defender == _humanPlayer)
//            {
//                bool result = _gameState.TakeCards();
//                if (result)
//                {
//                    OnGameStateChanged();

//                    // After taking cards, it's computer's turn to attack
//                    if (_gameState.Attacker == _computerPlayer)
//                    {
//                        PerformComputerTurn();
//                    }
//                }
//                return result;
//            }
//            return false;
//        }

//        // Player ends turn
//        public bool PlayerEndTurn()
//        {
//            if (_gameState.CurrentPhase == GamePhase.Attack && _gameState.Attacker == _humanPlayer && _gameState.AttackingCards.Count > 0)
//            {
//                // End the round
//                EndRound();
//                return true;
//            }
//            return false;
//        }

//        // Computer's turn (attack or defend)
//        private void PerformComputerTurn()
//        {
//            // We would add a delay in a real UI, but for now, perform immediately

//            if (_gameState.CurrentPhase == GamePhase.Attack && _gameState.Attacker == _computerPlayer)
//            {
//                // Computer attacks
//                Card attackCard = _computerPlayer.ChooseAttackingCard();

//                if (attackCard != null)
//                {
//                    _gameState.Attack(attackCard);
//                    OnGameStateChanged();
//                }
//                else
//                {
//                    // No valid attack card, end turn
//                    EndRound();
//                }
//            }
//            else if (_gameState.CurrentPhase == GamePhase.Defense && _gameState.Defender == _computerPlayer)
//            {
//                // Computer defends
//                if (_gameState.AttackingCards.Count > _gameState.DefendingCards.Count)
//                {
//                    Card attackingCard = _gameState.AttackingCards[_gameState.DefendingCards.Count];

//                    Card defenseCard = _computerPlayer.ChooseDefendingCard(attackingCard, _gameState.TrumpSuit);

//                    if (defenseCard != null)
//                    {
//                        _gameState.Defend(attackingCard, defenseCard);
//                    }
//                    else
//                    {
//                        // Can't defend, take cards
//                        _gameState.TakeCards();
//                    }

//                    OnGameStateChanged();
//                }
//            }
//        }

//        // End the current round
//        private void EndRound()
//        {
//            // This should be implemented in GameState
//            // For now we'll simulate it

//            // Clear cards from table
//            _gameState.AttackingCards.Clear();
//            _gameState.DefendingCards.Clear();

//            // Switch attacker/defender if defender didn't take cards
//            if (_gameState.CurrentPhase == GamePhase.Attack)
//            {
//                Player temp = _gameState.Attacker;
//                // This is a simplification - you'd need to add this functionality to GameState
//                //_gameState.SetAttacker(_gameState.Defender);
//                //_gameState.SetDefender(temp);
//            }

//            // Deal cards to refill hands
//            while (_humanPlayer.CardCount < 6 && !_gameState.Deck.IsEmpty)
//            {
//                _humanPlayer.AddCard(_gameState.Deck.DealCard());
//            }

//            while (_computerPlayer.CardCount < 6 && !_gameState.Deck.IsEmpty)
//            {
//                _computerPlayer.AddCard(_gameState.Deck.DealCard());
//            }

//            // Check for game over
//            // This is a simplification - you'd need to add this functionality to GameState

//            OnGameStateChanged();

//            // If it's computer's turn next, perform its turn
//            if (_gameState.Attacker == _computerPlayer)
//            {
//                PerformComputerTurn();
//            }
//        }

//        // Raise event when game state changes
//        protected virtual void OnGameStateChanged()
//        {
//            GameStateChanged?.Invoke(this, EventArgs.Empty);
//        }
//    }
//}




using System;
using System.Linq;
using DurakCardGame.Models;

namespace DurakCardGame.Controllers
{
    /// <summary>
    /// Central game logic controller between the UI and game models.
    /// </summary>
    public class GameController
    {
        #region Fields
        private readonly GameState _gameState;
        private readonly Player _humanPlayer;
        private readonly ComputerPlayer _computerPlayer;
        #endregion

        #region Events
        public event EventHandler GameStateChanged;
        #endregion

        #region Properties
        public GameState GameState => _gameState;
        public Player HumanPlayer => _humanPlayer;
        public ComputerPlayer ComputerPlayer => _computerPlayer;
        #endregion

        #region Constructor
        public GameController()
        {
            _gameState = new GameState();
            _humanPlayer = new Player("You");
            _computerPlayer = new ComputerPlayer("Computer");

            Deck deck = new Deck();
            _humanPlayer.AddCards(deck.DrawCards(6));
            _computerPlayer.AddCards(deck.DrawCards(6));

            _gameState.StartNewRound(_humanPlayer, _computerPlayer, deck);
        }
        #endregion

        #region Methods
        public void HumanAttack(Card selectedCard)
        {
            if (_gameState.CurrentPhase != GamePhase.Attack) return;

            _humanPlayer.RemoveCard(selectedCard);
            _gameState.AddAttackCard(selectedCard);

            _gameState.SetPhase(GamePhase.Defense);
            GameStateChanged?.Invoke(this, EventArgs.Empty);

            ComputerDefend();   // IMMEDIATELY handle defense
            //ContinueRound();    // Automatically continue
        }

        private void ComputerDefend()
        {
            Card attackCard = _gameState.AttackingCards.Last();
            Card defense = _computerPlayer.ChooseDefendingCard(attackCard, _gameState.TrumpSuit);

            if (defense != null)
            {
                _computerPlayer.RemoveCard(defense);
                _gameState.AddDefenseCard(defense);
                _gameState.SetPhase(GamePhase.Resolve);
            }
            else
            {
                _gameState.DefenderTakesCards();
                _gameState.SetPhase(GamePhase.Resolve);
            }

            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void ContinueRound()
        {
            if (_gameState.IsGameOver)
            {
                _gameState.SetPhase(GamePhase.GameOver);
                GameStateChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            FillHands();

            _gameState.SwitchRoles();
            _gameState.StartNewRound(_gameState.Attacker, _gameState.Defender, _gameState.Deck);
            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }

        public void EndTurn()
        {
            if (_gameState.IsGameOver)
            {
                _gameState.SetPhase(GamePhase.GameOver);
                GameStateChanged?.Invoke(this, EventArgs.Empty);
                return;
            }

            FillHands();

            _gameState.SwitchRoles();
            _gameState.StartNewRound(_gameState.Attacker, _gameState.Defender, _gameState.Deck);
            GameStateChanged?.Invoke(this, EventArgs.Empty);
        }

        private void FillHands()
        {
            int maxHand = 6;

            _humanPlayer.AddCards(_gameState.Deck.DrawCards(maxHand - _humanPlayer.CardCount));
            _computerPlayer.AddCards(_gameState.Deck.DrawCards(maxHand - _computerPlayer.CardCount));
        }
        #endregion
    }
}
