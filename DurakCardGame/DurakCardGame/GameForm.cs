using System;
using System.Windows.Forms;
using DurakCardGame.Models; // Add this to reference the Card class

namespace DurakCardGame
{
    public partial class GameForm : Form
    {
        public GameForm()
        {
            InitializeComponent();

            // Test the Card class
            TestCardClass();
        }

        private void TestCardClass()
        {
            // Create some test cards
            Card aceOfHearts = new Card(Card.Suit.Hearts, Card.Rank.Ace);
            Card kingOfHearts = new Card(Card.Suit.Hearts, Card.Rank.King);
            Card sevenOfSpades = new Card(Card.Suit.Spades, Card.Rank.Seven);

            // Test toString method
            string testResults = "Card Class Test Results:\n\n";
            testResults += $"Card 1: {aceOfHearts}\n";
            testResults += $"Card 2: {kingOfHearts}\n";
            testResults += $"Card 3: {sevenOfSpades}\n\n";

            // Test card comparison with Hearts as trump
            Card.Suit trumpSuit = Card.Suit.Hearts;
            testResults += $"Trump Suit: {trumpSuit}\n\n";

            testResults += $"Is {aceOfHearts} stronger than {kingOfHearts}? {aceOfHearts.IsStrongerThan(kingOfHearts, trumpSuit)}\n";
            testResults += $"Is {kingOfHearts} stronger than {sevenOfSpades}? {kingOfHearts.IsStrongerThan(sevenOfSpades, trumpSuit)}\n";
            testResults += $"Is {sevenOfSpades} stronger than {aceOfHearts}? {sevenOfSpades.IsStrongerThan(aceOfHearts, trumpSuit)}\n";

            // Display results in a message box
            MessageBox.Show(testResults, "Card Class Test");
        }
    }
}