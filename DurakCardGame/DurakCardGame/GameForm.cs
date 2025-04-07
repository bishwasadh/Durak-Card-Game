//using System;
//using System.Windows.Forms;
//using DurakCardGame.Controllers;
//using DurakCardGame.Models;
//using DurakCardGame.Views;

//namespace DurakCardGame
//{
//    public partial class GameForm : Form
//    {
//        // GameController manages the game logic
//        private GameController? _gameController;

//        public GameForm()
//        {
//            InitializeComponent();

//        }

//        private void GameForm_Load(object sender, EventArgs e)
//        {
//            InitializeGame();
//        }

//        // Initializes a new game and sets up the controller
//        private void InitializeGame()
//        {
//            // Create a new game controller
//            _gameController = new GameController();

//            // Subscribe to game state changes to update UI
//            _gameController.GameStateChanged += GameController_GameStateChanged;

//            // Update the UI with initial game state
//            UpdateUI();
//        }

//        // Event handler for game state changes
//        private void GameController_GameStateChanged(object? sender, EventArgs e)
//        {
//            // Update the UI whenever game state changes
//            UpdateUI();
//        }

//        private void btnNewGame_Click(object sender, EventArgs e)
//        {
//            InitializeGame();

//        }
//        // Updates the UI based on current game state
//        private void UpdateUI()
//        {
//            // Don't continue if game controller isn't initialized
//            if (_gameController == null || _gameController.GameState == null)
//                return;


//            // Create more compact game info with separators
//            string gameInfo = $"STATUS: {_gameController.GameState.CurrentPhase} | ";
//            gameInfo += $"\nAttacker: {_gameController.GameState.Attacker.Name}";
//            gameInfo += $"\nDefender: {_gameController.GameState.Defender.Name}";
//            gameInfo += $"\nTrump: {_gameController.GameState.TrumpSuit}";
//            gameInfo += $"Deck: {_gameController.GameState.Deck.RemainingCards}";

//            // Update the label with clear text
//            lblStatus.Text = gameInfo;

//            // Turn off auto-size for single line
//            lblStatus.AutoSize = false;

//            // Make the label more visible
//            lblStatus.BackColor = Color.White;
//            lblStatus.ForeColor = Color.Black;
//            lblStatus.Font = new Font(lblStatus.Font, FontStyle.Bold);

//            // Display player's hand
//            DisplayPlayerHand();

//            // Force UI to refresh
//            Application.DoEvents();
//        }

//        // Displays the player's hand of cards
//        private void DisplayPlayerHand()
//        {
//            // Clear the panel first
//            pnlPlayerHand.Controls.Clear();

//            // Exit if controller isn't initialized
//            if (_gameController == null)
//                return;

//            // Calculate total width needed for all cards
//            int cardWidth = 80;
//            int cardGap = 5;
//            int cardSpacing = cardWidth + cardGap;
//            int totalCardsWidth = _gameController.HumanPlayer.Hand.Count * cardSpacing - cardGap;

//            // Calculate starting position to center cards
//            int startX = (pnlPlayerHand.Width - totalCardsWidth) / 2;
//            if (startX < 10) startX = 10; // Minimum margin

//            // Position for first card
//            int xPosition = startX;

//            // Create a card control for each card in player's hand
//            foreach (Card card in _gameController.HumanPlayer.Hand)
//            {
//                CardControl cardControl = new CardControl();
//                cardControl.Card = card;
//                cardControl.Location = new Point(xPosition, 10);
//                cardControl.Click += PlayerCard_Click;

//                // Add to panel
//                pnlPlayerHand.Controls.Add(cardControl);

//                // Move position for next card
//                xPosition += cardSpacing;
//            }
//        }


//        // Handles player clicking on a card
//        private void PlayerCard_Click(object? sender, EventArgs e)
//        {
//            // Check if sender is null
//            if (sender == null)
//                return;

//            // Cast to CardControl
//            CardControl clickedCard = (CardControl)sender;

//            // Check if the card is set
//            if (clickedCard.Card == null)
//                return;

//            // Deselect all other cards first
//            foreach (Control control in pnlPlayerHand.Controls)
//            {
//                if (control is CardControl cardControl && cardControl != clickedCard)
//                {
//                    cardControl.IsSelected = false;
//                }
//            }

//            // Toggle selection on clicked card
//            clickedCard.IsSelected = !clickedCard.IsSelected;

//            // Update status text with selection info
//            UpdateStatusWithSelection(clickedCard.IsSelected ? clickedCard.Card : null);
//        }

//        // Helper method to update status with selection info
//        private void UpdateStatusWithSelection(Card? selectedCard)
//        {
//            // Don't continue if game controller isn't initialized
//            if (_gameController == null || _gameController.GameState == null)
//                return;

//            // Create compact game info with separators
//            string gameInfo = $"STATUS: {_gameController.GameState.CurrentPhase} | ";
//            gameInfo += $"\nAttacker: {_gameController.GameState.Attacker.Name}";
//            gameInfo += $"\nDefender: {_gameController.GameState.Defender.Name}";
//            gameInfo += $"\nTrump: {_gameController.GameState.TrumpSuit}";
//            gameInfo += $"Deck: {_gameController.GameState.Deck.RemainingCards}";

//            // Add selected card info if there is one
//            if (selectedCard != null)
//            {
//                gameInfo += $"\nSelected: {selectedCard}";
//            }

//            // Update the label text
//            lblStatus.Text = gameInfo;
//        }


//    }
//}



using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DurakCardGame.Controllers;
using DurakCardGame.Models;
using DurakCardGame.Views;

namespace DurakCardGame
{
    public partial class GameForm : Form
    {
        private GameController _gameController;

        public GameForm()
        {
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            StartNewGame();
        }

        private void StartNewGame()
        {
            _gameController = new GameController();
            _gameController.GameStateChanged += GameController_GameStateChanged;

            lblStatus.Text = "New Game Started.";
            RenderAll();
        }

        private void GameController_GameStateChanged(object sender, EventArgs e)
        {
            RenderAll();
        }

        private void RenderAll()
        {
            var game = _gameController.GameState;

            lblTrumpSuit.Text = $"Trump Suit: {game.TrumpSuit}";
            lblTurnStatus.Text = $"Turn: {game.CurrentPhase}";

            lblStatus.Text = game.IsGameOver
                ? "🎉 Game Over!"
                : game.CurrentPhase switch
                {
                    GamePhase.Attack => "Your move! Select a card to attack.",
                    GamePhase.Defense => "Computer is defending...",
                    GamePhase.Resolve => "Click 'End Turn' to continue.",
                    _ => "Waiting..."
                };

            RenderHand(pnlPlayerHand, _gameController.HumanPlayer, true);
            RenderHand(pnlComputerHand, _gameController.ComputerPlayer, false);
            RenderPlayArea();
            RenderDeck();
        }

        private void RenderHand(Panel panel, Player player, bool faceUp)
        {
            panel.Controls.Clear();
            int x = 5;

            foreach (var card in player.Hand)
            {
                var cardControl = new CardControl
                {
                    Card = card,
                    FaceUp = faceUp,
                    Size = new Size(70, 100),
                    Location = new Point(x, 10),
                    BackColor = Color.White,
                    Cursor = faceUp ? Cursors.Hand : Cursors.Default
                };

                if (faceUp && _gameController.GameState.CurrentPhase == GamePhase.Attack)
                    cardControl.Click += (s, e) => _gameController.HumanAttack(card);

                panel.Controls.Add(cardControl);
                x += 80;
            }
        }

        private void RenderPlayArea()
        {
            pnlPlayArea.Controls.Clear();
            var game = _gameController.GameState;

            int x = 10;
            for (int i = 0; i < game.AttackingCards.Count; i++)
            {
                var attack = game.AttackingCards[i];
                var defense = i < game.DefendingCards.Count ? game.DefendingCards[i] : null;

                var attackCard = new CardControl
                {
                    Card = attack,
                    FaceUp = true,
                    Size = new Size(70, 100),
                    Location = new Point(x, 10)
                };
                pnlPlayArea.Controls.Add(attackCard);

                if (defense != null)
                {
                    var defendCard = new CardControl
                    {
                        Card = defense,
                        FaceUp = true,
                        Size = new Size(70, 100),
                        Location = new Point(x + 20, 120)
                    };
                    pnlPlayArea.Controls.Add(defendCard);
                }

                x += 90;
            }
        }

        private void RenderDeck()
        {
            pnlDeck.Controls.Clear();

            var trumpCard = _gameController.GameState.Deck.DrawCards(1).FirstOrDefault();
            if (trumpCard == null) return;

            var cardControl = new CardControl
            {
                Card = trumpCard,
                FaceUp = true,
                Size = new Size(60, 90),
                Location = new Point(10, 10)
            };

            pnlDeck.Controls.Add(cardControl);
        }



        private void btnEndTurn_Click_1(object sender, EventArgs e)
        {
              
            if (_gameController.GameState.CurrentPhase == GamePhase.Resolve)
            {
                _gameController.EndTurn();
                lblStatus.Text = "Turn ended. Roles switched.";
            }

        }

        private void btnRestartGame_Click_1(object sender, EventArgs e)
        {

            // Restart the game
            StartNewGame();
            lblStatus.Text = "Game restarted.";

        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            StartNewGame();
        }
        private void btnTakeCards_Click_1(object sender, EventArgs e)
        {

            if (_gameController.GameState.CurrentPhase == GamePhase.Defense)
            {
                _gameController.GameState.DefenderTakesCards();
                _gameController.GameState.SetPhase(GamePhase.Resolve);
                lblStatus.Text = "You took the cards. Click End Turn.";
                RenderAll();
            }

        }
    }
}
