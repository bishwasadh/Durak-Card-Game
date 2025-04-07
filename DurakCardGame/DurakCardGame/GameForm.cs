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

        // Player and attack card selections
        private Card? _selectedPlayerCard = null;
        private Card? _selectedAttackCard = null;

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

            // Add game-over status if applicable
            if (_gameController.GameState.IsGameOver)
            {
                Player? winner = _gameController.GameState.GetWinner();
                gameInfo += $"\n\nGAME OVER - {(winner != null ? winner.Name + " WINS!" : "DRAW")}";
            }

            // Update the label with clear text
            lblStatus.Text = gameInfo;

            // Turn off auto-size for single line
            lblStatus.AutoSize = false;

            // Make the label more visible
            lblStatus.BackColor = Color.White;
            lblStatus.ForeColor = Color.Black;
            lblStatus.Font = new Font(lblStatus.Font, FontStyle.Bold);

            // Display all game elements
            DisplayPlayerHand();
            DisplayComputerHand();
            DisplayPlayArea();
            DisplayDeck();

            // Enable/disable buttons based on game state
            UpdateButtonStates();

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

        private void DisplayPlayArea()
        {
            // Clear the panel first
            pnlPlayArea.Controls.Clear();

            // Exit if controller isn't initialized
            if (_gameController == null || _gameController.GameState == null)
                return;

            int cardWidth = 80;
            int cardGap = 10;
            int yOffset = 40; // Vertical offset for defending cards

            // Display attacking cards in top row
            for (int i = 0; i < _gameController.GameState.AttackingCards.Count; i++)
            {
                CardControl cardControl = new CardControl();
                cardControl.Card = _gameController.GameState.AttackingCards[i];
                cardControl.FaceUp = true;
                cardControl.Location = new Point(10 + (cardWidth + cardGap) * i, 10);
                cardControl.Tag = i; // Store index for reference
                cardControl.Click += AttackCard_Click; // Add click handler for selecting attack cards

                pnlPlayArea.Controls.Add(cardControl);
            }

            // Display defending cards in bottom row, slightly offset
            for (int i = 0; i < _gameController.GameState.DefendingCards.Count; i++)
            {
                CardControl cardControl = new CardControl();
                cardControl.Card = _gameController.GameState.DefendingCards[i];
                cardControl.FaceUp = true;
                cardControl.Location = new Point(10 + (cardWidth + cardGap) * i, 10 + yOffset);

                pnlPlayArea.Controls.Add(cardControl);
            }
        }

        // Displays the deck and trump card
        private void DisplayDeck()
        {
            // Clear the panel first
            pnlDeck.Controls.Clear();

            // Exit if controller isn't initialized
            if (_gameController == null || _gameController.GameState == null)
                return;

            // If deck is empty, just return
            if (_gameController.GameState.Deck.RemainingCards == 0)
                return;

            // Add deck of cards (face down stack)
            CardControl deckCardControl = new CardControl();
            deckCardControl.FaceUp = false;
            deckCardControl.Location = new Point(10, 10);
            deckCardControl.Size = new Size(80, 120);
            pnlDeck.Controls.Add(deckCardControl);

            // Add trump card (sideways, partially visible)
            Card? trumpCard = _gameController.GameState.Deck.GetTrumpCard();
            if (trumpCard != null)
            {
                CardControl trumpCardControl = new CardControl();
                trumpCardControl.Card = trumpCard;
                trumpCardControl.FaceUp = true;
                trumpCardControl.Location = new Point(30, 50);
                trumpCardControl.Size = new Size(120, 80); // Rotated dimensions

                // Add a label showing "TRUMP"
                Label lblTrump = new Label();
                lblTrump.Text = "TRUMP";
                lblTrump.Font = new Font(lblTrump.Font, FontStyle.Bold);
                lblTrump.Location = new Point(10, 80);
                lblTrump.AutoSize = true;
                lblTrump.BackColor = Color.White;

                pnlDeck.Controls.Add(trumpCardControl);
                pnlDeck.Controls.Add(lblTrump);
            }

            // Add label showing number of cards left
            Label lblDeckCount = new Label();
            lblDeckCount.Text = $"Cards: {_gameController.GameState.Deck.RemainingCards}";
            lblDeckCount.Location = new Point(10, 140);
            lblDeckCount.AutoSize = true;
            lblDeckCount.BackColor = Color.White;
            pnlDeck.Controls.Add(lblDeckCount);
        }

        // Updates the enabled state of buttons based on game state
        private void UpdateButtonStates()
        {
            // Don't continue if game controller isn't initialized
            if (_gameController == null || _gameController.GameState == null)
                return;

            bool isPlayerAttacker = _gameController.GameState.Attacker == _gameController.HumanPlayer;
            bool isPlayerDefender = _gameController.GameState.Defender == _gameController.HumanPlayer;
            bool isAttackPhase = _gameController.GameState.CurrentPhase == GamePhase.Attack;
            bool isDefensePhase = _gameController.GameState.CurrentPhase == GamePhase.Defense;
            bool isGameOver = _gameController.GameState.IsGameOver;

            // Take Cards button only enabled when player is defender
            btnTakeCards.Enabled = isPlayerDefender && isDefensePhase && !isGameOver;

            // End Turn button only enabled when player is attacker and has played at least one card
            btnEndTurn.Enabled = isPlayerAttacker && isAttackPhase &&
                _gameController.GameState.AttackingCards.Count > 0 && !isGameOver;

            // New Game button always enabled
            btnNewGame.Enabled = true;
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

            // Store the selected card or null if deselected
            _selectedPlayerCard = clickedCard.IsSelected ? clickedCard.Card : null;

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

      
        

        // Add this handler method alongside your other event handlers
        private void AttackCard_Click(object sender, EventArgs e)
        {
            // Ensure we're in defense phase and player is defender
            if (_gameController.GameState.CurrentPhase != GamePhase.Defense ||
                _gameController.GameState.Defender != _gameController.HumanPlayer)
                return;

            CardControl clickedCard = sender as CardControl;
            if (clickedCard == null || clickedCard.Card == null)
                return;

            // Reset selection of all attack cards
            foreach (Control control in pnlPlayArea.Controls)
            {
                if (control is CardControl cardControl)
                {
                    cardControl.IsSelected = false;
                }
            }

            // Select this attack card
            clickedCard.IsSelected = true;
            _selectedAttackCard = clickedCard.Card;

            // Optional: Update status to inform user
            UpdateStatusWithSelection();
        }

        private void UpdateStatusWithSelection()
        {
            // Add this method to update status bar/label with selection info
            if (_selectedAttackCard != null)
            {
                lblStatus.Text = $"{lblStatus.Text}\nSelected attack card: {_selectedAttackCard}";
            }

            if (_selectedPlayerCard != null)
            {
                lblStatus.Text = $"{lblStatus.Text}\nSelected player card: {_selectedPlayerCard}";
            }
        }

    }
}