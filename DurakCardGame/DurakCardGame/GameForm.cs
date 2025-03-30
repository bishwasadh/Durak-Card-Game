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
        // Updates the UI based on current game state
        private void UpdateUI()
        {
            // Don't continue if game controller isn't initialized
            if (_gameController == null || _gameController.GameState == null)
                return;

            // Create more visible game info
            string gameInfo = $"GAME STATUS: {_gameController.GameState.CurrentPhase}";
            gameInfo += $"\nAttacker: {_gameController.GameState.Attacker.Name}";
            gameInfo += $"\nDefender: {_gameController.GameState.Defender.Name}";
            gameInfo += $"\nTrump: {_gameController.GameState.TrumpSuit}";

            // Update the label with clear text
            lblStatus.Text = gameInfo;

            // Make the label more visible
            lblStatus.BackColor = Color.White;
            lblStatus.ForeColor = Color.Black;
            lblStatus.Font = new Font(lblStatus.Font, FontStyle.Bold);

            // Display player's hand
            DisplayPlayerHand();

            // Force UI to refresh
            Application.DoEvents();
        }

        // Displays the player's hand of cards
        private void DisplayPlayerHand()
        {
            // Clear the panel first
            pnlPlayerHand.Controls.Clear();

            // Exit if controller isn't initialized
            if (_gameController == null)
                return;

            // Position for first card
            int xPosition = 10;

            // Create a card control for each card in player's hand
            foreach (Card card in _gameController.HumanPlayer.Hand)
            {
                CardControl cardControl = new CardControl();
                cardControl.Card = card;
                cardControl.Location = new Point(xPosition, 10);
                cardControl.Click += PlayerCard_Click;

                // Add to panel
                pnlPlayerHand.Controls.Add(cardControl);

                // Move position for next card
                xPosition += 85; // Space between cards
            }
        }

        // Handles player clicking on a card
        private void PlayerCard_Click(object? sender, EventArgs e)
        {
            // Check if sender is null
            if (sender == null)
                return;

            // Cast to CardControl
            CardControl clickedCard = (CardControl)sender;

            // Check if the card is set
            if (clickedCard.Card == null)
                return;

            // Card is now selected/deselected by the CardControl itself
            // No need for a message box
        }








    }
}