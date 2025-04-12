//using DurakCardGame.Models;
//using DurakCardGame;
//using Microsoft.VisualBasic.ApplicationServices;
//using Microsoft.VisualBasic.Devices;
//using Microsoft.VisualBasic;
//using static System.Windows.Forms.AxHost;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;
//using System.Reflection.Metadata;
//using System.Windows.Forms;

//*GameForm.cs - Main game interface that handles the UI and player interactions
// * --------------------------------------------------------------------------
// * Course: Object Oriented Programming 3
// * Assignment: Durak Card Game Project
// * Date: April 2024
// * 
// * Contributing Team Members:
// *Bishwas Adhikari
//* Harsh Patel
// *  Aaryan Desai
// * 
// * Description:
// *This form implements the main user interface for the Durak card game.
// * It handles rendering of cards, game state visualization, and user
// * interactions including card selection, turns, and game flow.The UI
// * displays both player and computer hands, the playing area, deck information,
// *and provides controls for game actions.
// *
// *Features:
// *-Interactive card selection and gameplay
// * -Visual representation of game elements(hands, play area, trump)
// * -Game control buttons(Attack, Defense, Take Cards, End Turn)
// * -Game state information display
// * -New Game, Restart Game, Help, and Exit functionality
// * *****************************************************************************/



using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DurakCardGame.Controllers;
using DurakCardGame.Models;
using DurakCardGame.Views;
using System.Media;

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

            //lblStatus.Text = "New Game Started.";

            // ✅ Show updated stats in the status label
            lblStatus.Text = $"🟢 New Game Started\nWins: {GameStats.Wins} | Losses: {GameStats.Losses}";

            RenderAll(); // refresh full UI
        }

        private void GameController_GameStateChanged(object sender, EventArgs e)
        {
            RenderAll();
        }

        //private void RenderAll()
        //{
        //    var game = _gameController.GameState;

        //    lblTrumpSuit.Text = $"Trump Suit: {game.TrumpSuit}";
        //    lblTurnStatus.Text = $"Turn: {game.CurrentPhase}";

        //    lblStatus.Text = game.IsGameOver
        //        ? "🎉 Game Over!"
        //        : game.CurrentPhase switch
        //        {
        //            GamePhase.Attack => "Your move! Select a card to attack.",
        //            GamePhase.Defense => "Computer is defending...",
        //            GamePhase.Resolve => "Click 'End Turn' to continue.",
        //            _ => "Waiting..."
        //        };

        //    RenderHand(pnlPlayerHand, _gameController.HumanPlayer, true);
        //    RenderHand(pnlComputerHand, _gameController.ComputerPlayer, false);
        //    RenderPlayArea();
        //    RenderDeck();
        //}


        private void PlaySound(string path)
        {
            try
            {
                if (File.Exists(path))
                    new SoundPlayer(path).Play();
            }
            catch { }
        }

        private void RenderAll()
        {
            var game = _gameController.GameState;

            lblTrumpSuit.Text = $"Trump Suit: {game.Deck.TrumpCard}";
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

            // ✅ Game Over check: play win sound and show message
            if (game.IsGameOver)
            {
                bool playerWon = _gameController.HumanPlayer.CardCount == 0;
                PlaySound("win.wav");
                MessageBox.Show(playerWon ? "🎉 You Win!" : "😢 You Lost!",
                                "Game Over",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

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

            //var trumpCard = _gameController.GameState.Deck.DrawCards(1).FirstOrDefault();
            var trumpCard = _gameController.GameState.Deck.TrumpCard;
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

        private void btnViewStats_Click(object sender, EventArgs e)
        {

            MessageBox.Show($"📊 Stats:\nWins: {GameStats.Wins}\nLosses: {GameStats.Losses}",
                "Game Stats",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

        }

        private void btnViewLog_Click(object sender, EventArgs e)
        {

            //string log = System.IO.File.Exists("game_log.txt")
            //? System.IO.File.ReadAllText("game_log.txt")
            //: "No logs found yet.";

            //MessageBox.Show(log.Length > 3000 ? log.Substring(log.Length - 3000) : log,
            //                "Game Log (Recent)",
            //                MessageBoxButtons.OK,
            //                MessageBoxIcon.Information);

            if (!System.IO.File.Exists("game_log.txt"))
            {
                MessageBox.Show("No logs found yet.", "Game Log", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var allLines = System.IO.File.ReadAllLines("game_log.txt");
            var last50Lines = allLines.Reverse().Take(50).Reverse(); // ✅ Get last 50 lines in order

            //string logText = string.Join(Environment.NewLine, last50Lines);
            string logText = "📄 Last 50 Game Actions:\n\n" + string.Join(Environment.NewLine, last50Lines);


            MessageBox.Show(logText, "Game Log (Last 50 Moves)", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void btnResetStatsAndLogs_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to reset both stats and logs?\nThis cannot be undone.",
                                 "Confirm Full Reset",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    System.IO.File.WriteAllLines("stats.txt", new[]
                    {
                "Wins:0",
                "Losses:0",
                $"LastReset:{DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            });

                    GameStats.Load();

                    if (System.IO.File.Exists("game_log.txt"))
                        System.IO.File.Delete("game_log.txt");

                    MessageBox.Show("Stats and Logs have been reset.", "Reset Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lblStatus.Text = $"🟢 Stats Reset\nWins: {GameStats.Wins} | Losses: {GameStats.Losses}";
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Something went wrong:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
} 
