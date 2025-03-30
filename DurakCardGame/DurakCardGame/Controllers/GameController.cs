using System;
using System.Collections.Generic;
using DurakCardGame.Models;

namespace DurakCardGame.Controllers
{
    public class GameController
    {
        // Game state and players
        private GameState _gameState;
        private Player _humanPlayer;
        private ComputerPlayer _computerPlayer;

        // Event for notifying UI of game state changes
        public event EventHandler GameStateChanged;

        // Public properties to access game information
        public GameState GameState => _gameState;
        public Player HumanPlayer => _humanPlayer;
        public ComputerPlayer ComputerPlayer => _computerPlayer;

        // Constructor
        public GameController()
        {
            InitializeGame();
        }

        // Initialize a new game
        public void InitializeGame()
        {
            // Create players
            _humanPlayer = new Player("Player");
            _computerPlayer = new ComputerPlayer("Computer");

            // Create game state
            _gameState = new GameState(_humanPlayer, _computerPlayer);

            // Notify UI
            OnGameStateChanged();

            // If computer starts, perform its turn
            if (_gameState.Attacker == _computerPlayer)
            {
                PerformComputerTurn();
            }
        }

        // Player attacks with a card
        public bool PlayerAttack(Card card)
        {
            if (_gameState.CurrentPhase == GamePhase.Attack && _gameState.Attacker == _humanPlayer)
            {
                bool result = _gameState.Attack(card);
                if (result)
                {
                    OnGameStateChanged();

                    // If the attack was successful, let the computer defend
                    if (_gameState.Defender == _computerPlayer)
                    {
                        PerformComputerTurn();
                    }
                }
                return result;
            }
            return false;
        }

        // Player defends with a card
        public bool PlayerDefend(Card attackingCard, Card defendingCard)
        {
            if (_gameState.CurrentPhase == GamePhase.Defense && _gameState.Defender == _humanPlayer)
            {
                bool result = _gameState.Defend(attackingCard, defendingCard);
                if (result)
                {
                    OnGameStateChanged();

                    // If all cards are defended, computer's turn to attack
                    if (_gameState.CurrentPhase == GamePhase.Attack && _gameState.Attacker == _computerPlayer)
                    {
                        PerformComputerTurn();
                    }
                }
                return result;
            }
            return false;
        }

        // Player takes cards
        public bool PlayerTakeCards()
        {
            if (_gameState.CurrentPhase == GamePhase.Defense && _gameState.Defender == _humanPlayer)
            {
                bool result = _gameState.TakeCards();
                if (result)
                {
                    OnGameStateChanged();

                    // After taking cards, it's computer's turn to attack
                    if (_gameState.Attacker == _computerPlayer)
                    {
                        PerformComputerTurn();
                    }
                }
                return result;
            }
            return false;
        }

        
    }
}