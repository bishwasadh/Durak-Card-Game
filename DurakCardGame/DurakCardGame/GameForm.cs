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
            TestGameStateClass();
        }

        private void TestGameStateClass()
        {
            // Create two players
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Computer");

            // Create game state
            GameState gameState = new GameState(player1, player2);

            string testResults = "GameState Test Results:\n\n";

            // Show initial game state
            testResults += "Game Started\n";
            testResults += $"Trump Suit: {gameState.TrumpSuit}\n";
            testResults += $"Attacker: {gameState.Attacker.Name} ({gameState.Attacker.CardCount} cards)\n";
            testResults += $"Defender: {gameState.Defender.Name} ({gameState.Defender.CardCount} cards)\n";
            testResults += $"Current Phase: {gameState.CurrentPhase}\n\n";

            testResults += "Attacker's hand:\n";
            foreach (Card card in gameState.Attacker.Hand)
            {
                testResults += $"- {card}\n";
            }

            // Simulate playing one card
            if (gameState.Attacker.CardCount > 0)
            {
                Card attackingCard = gameState.Attacker.Hand[0];
                testResults += $"\nAttacker plays: {attackingCard}\n";

                bool attackSuccessful = gameState.Attack(attackingCard);
                testResults += $"Attack successful: {attackSuccessful}\n";
                testResults += $"Current Phase: {gameState.CurrentPhase}\n\n";

                // Check if defender can defend
                Card defendingCard = null;
                foreach (Card card in gameState.Defender.Hand)
                {
                    if (card.IsStrongerThan(attackingCard, gameState.TrumpSuit))
                    {
                        defendingCard = card;
                        break;
                    }
                }

                if (defendingCard != null)
                {
                    testResults += $"Defender defends with: {defendingCard}\n";
                    bool defenseSuccessful = gameState.Defend(attackingCard, defendingCard);
                    testResults += $"Defense successful: {defenseSuccessful}\n";
                    testResults += $"Current Phase: {gameState.CurrentPhase}\n\n";
                }
                else
                {
                    testResults += "Defender cannot defend and takes the card\n";
                    bool takeCardsSuccessful = gameState.TakeCards();
                    testResults += $"Take cards successful: {takeCardsSuccessful}\n";
                    testResults += $"Current Phase: {gameState.CurrentPhase}\n";
                    testResults += $"Defender now has {gameState.Defender.CardCount} cards\n";
                }
            }

            // Display results
            MessageBox.Show(testResults, "GameState Test");
        }

        private void TestPlayerClass()
        {
            // Create a new deck and players
            Deck deck = new Deck();
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Computer");

            string testResults = "Player Class Test Results:\n\n";

            // Deal 6 cards to each player
            player1.AddCards(deck.DealCards(6));
            player2.AddCards(deck.DealCards(6));

            // Show players' hands
            testResults += $"{player1.Name}'s hand ({player1.CardCount} cards):\n";
            foreach (Card card in player1.Hand)
            {
                testResults += $"- {card}\n";
            }

            testResults += $"\n{player2.Name}'s hand ({player2.CardCount} cards):\n";
            foreach (Card card in player2.Hand)
            {
                testResults += $"- {card}\n";
            }

            testResults += $"\nTrump suit: {deck.TrumpSuit}\n\n";

            // Test playing a card
            if (player1.CardCount > 0)
            {
                Card playedCard = player1.PlayCard(0);
                testResults += $"{player1.Name} plays: {playedCard}\n";
                testResults += $"{player1.Name} now has {player1.CardCount} cards\n\n";

                // Test if player2 can defend against this card
                bool canDefend = player2.CanDefend(playedCard, deck.TrumpSuit);
                testResults += $"Can {player2.Name} defend? {canDefend}\n";

                if (canDefend)
                {
                    Card defendingCard = player2.FindDefendingCard(playedCard, deck.TrumpSuit);
                    testResults += $"Best defending card: {defendingCard}\n";

                    // Play the defending card
                    player2.PlayCard(defendingCard);
                    testResults += $"{player2.Name} plays {defendingCard} to defend\n";
                    testResults += $"{player2.Name} now has {player2.CardCount} cards\n";
                }
            }

            // Display results
            MessageBox.Show(testResults, "Player Class Test");
        }

        private void TestDeckClass()
        {
            // Create a new deck
            Deck deck = new Deck();

            string testResults = "Deck Class Test Results:\n\n";

            // Show trump suit
            testResults += $"Trump Suit: {deck.TrumpSuit}\n\n";

            // Deal some cards and show them
            testResults += "Dealing 6 cards:\n";
            List<Card> dealtCards = deck.DealCards(6);
            foreach (Card card in dealtCards)
            {
                testResults += $"- {card}\n";
            }

            // Show remaining cards
            testResults += $"\nRemaining cards in deck: {deck.RemainingCards}\n";

            // Show trump card
            Card trumpCard = deck.GetTrumpCard();
            testResults += $"Trump card (bottom of deck): {trumpCard}\n";

            // Display results
            MessageBox.Show(testResults, "Deck Class Test");
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