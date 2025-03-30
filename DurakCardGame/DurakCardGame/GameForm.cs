using System;
using System.Windows.Forms;
using DurakCardGame.Controllers;
using DurakCardGame.Models;
using DurakCardGame.Views;

namespace DurakCardGame
{
    public partial class GameForm : Form
    {
        // GameController manages the game logic
        private GameController? _gameController;

        public GameForm()
        {
            InitializeComponent();

        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            InitializeGame();
        }

        // Initializes a new game and sets up the controller
        private void InitializeGame()
        {
            // Create a new game controller
            _gameController = new GameController();

            // Subscribe to game state changes to update UI
            _gameController.GameStateChanged += GameController_GameStateChanged;

            // Update the UI with initial game state
            UpdateUI();
        }

        // Event handler for game state changes
        private void GameController_GameStateChanged(object? sender, EventArgs e)
        {
            // Update the UI whenever game state changes
            UpdateUI();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            InitializeGame();

        }
        private void UpdateUI()
        {
            // Don't continue if game controller isn't initialized
            if (_gameController == null || _gameController.GameState == null)
                return;

            // Create more visible game info
            string gameInfo = $"Game Status: {_gameController.GameState.CurrentPhase}";
            gameInfo += $"\nAttacker: {_gameController.GameState.Attacker.Name}";
            gameInfo += $"\nDefender: {_gameController.GameState.Defender.Name}";
            gameInfo += $"\nTrump: {_gameController.GameState.TrumpSuit}";

            // Update the label with clear text
            lblStatus.Text = gameInfo;



            // Force UI to refresh
            lblStatus.Update();
            Application.DoEvents();
        }








    }
}