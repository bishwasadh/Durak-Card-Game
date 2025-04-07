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


            // Create more compact game info with separators
            string gameInfo = $"STATUS: {_gameController.GameState.CurrentPhase} | ";
            gameInfo += $"\nAttacker: {_gameController.GameState.Attacker.Name}";
            gameInfo += $"\nDefender: {_gameController.GameState.Defender.Name}";
            gameInfo += $"\nTrump: {_gameController.GameState.TrumpSuit}";
            gameInfo += $"Deck: {_gameController.GameState.Deck.RemainingCards}";

            // Update the label with clear text
            lblStatus.Text = gameInfo;

            // Turn off auto-size for single line
            lblStatus.AutoSize = false;

            // Make the label more visible
            lblStatus.BackColor = Color.White;
            lblStatus.ForeColor = Color.Black;
            lblStatus.Font = new Font(lblStatus.Font, FontStyle.Bold);

            // Display player's hand
            DisplayPlayerHand();

            // Display computer's hand (face down)
            DisplayComputerHand();



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

            // Calculate total width needed for all cards
            int cardWidth = 80;
            int cardGap = 5;
            int cardSpacing = cardWidth + cardGap;
            int totalCardsWidth = _gameController.HumanPlayer.Hand.Count * cardSpacing - cardGap;

            // Calculate starting position to center cards
            int startX = (pnlPlayerHand.Width - totalCardsWidth) / 2;
            if (startX < 10) startX = 10; // Minimum margin

            // Position for first card
            int xPosition = startX;

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
                xPosition += cardSpacing;
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

            // Deselect all other cards first
            foreach (Control control in pnlPlayerHand.Controls)
            {
                if (control is CardControl cardControl && cardControl != clickedCard)
                {
                    cardControl.IsSelected = false;
                }
            }

            // Toggle selection on clicked card
            clickedCard.IsSelected = !clickedCard.IsSelected;

            // Update status text with selection info
            UpdateStatusWithSelection(clickedCard.IsSelected ? clickedCard.Card : null);
        }

        // Helper method to update status with selection info
        private void UpdateStatusWithSelection(Card? selectedCard)
        {
            // Don't continue if game controller isn't initialized
            if (_gameController == null || _gameController.GameState == null)
                return;

            // Create compact game info with separators
            string gameInfo = $"STATUS: {_gameController.GameState.CurrentPhase} | ";
            gameInfo += $"\nAttacker: {_gameController.GameState.Attacker.Name}";
            gameInfo += $"\nDefender: {_gameController.GameState.Defender.Name}";
            gameInfo += $"\nTrump: {_gameController.GameState.TrumpSuit}";
            gameInfo += $"Deck: {_gameController.GameState.Deck.RemainingCards}";

            // Add selected card info if there is one
            if (selectedCard != null)
            {
                gameInfo += $"\nSelected: {selectedCard}";
            }

            // Update the label text
            lblStatus.Text = gameInfo;

        }

        // Handles the form closing event
        private void DisplayComputerHand()
        {
            // Clear the panel first
            pnlComputerHand.Controls.Clear();

            // Exit if controller isn't initialized
            if (_gameController == null)
                return;

            // Calculate total width needed for all cards
            int cardWidth = 80;
            int cardGap = 5;
            int cardSpacing = cardWidth + cardGap;
            int totalCardsWidth = _gameController.ComputerPlayer.CardCount * cardSpacing - cardGap;

            // Calculate starting position to center cards
            int startX = (pnlComputerHand.Width - totalCardsWidth) / 2;
            if (startX < 10) startX = 10; // Minimum margin

            // Position for first card
            int xPosition = startX;

            // Create a card control for each card in computer's hand (face down)
            for (int i = 0; i < _gameController.ComputerPlayer.CardCount; i++)
            {
                CardControl cardControl = new CardControl();
                cardControl.FaceUp = false; // Card is face down
                cardControl.Location = new Point(xPosition, 10);

                // Add to panel
                pnlComputerHand.Controls.Add(cardControl);

                // Move position for next card
                xPosition += cardSpacing;
            }
        }

    }
}