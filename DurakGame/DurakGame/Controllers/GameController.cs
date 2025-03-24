using System;
using DurakGame.Classes;


namespace DurakGame.Controllers
{
    public class GameController
    {
        private Deck deck;
        private Player player;
        private ComputerPlayer ai;
        private GameState gameState;

        public GameController()
        {
            deck = new Deck();
            player = new Player("Human");
            ai = new ComputerPlayer("AI");
            gameState = new GameState();
            StartGame();
        }

        public void StartGame()
        {
            for (int i = 0; i < 6; i++)
            {
                player.ReceiveCard(deck.DealCard());
                ai.ReceiveCard(deck.DealCard());
            }
        }

        public void PlayerMove(Card card)
        {
            if (!player.Hand.Contains(card)) return;
            player.PlayCard(card);
            gameState.CardsOnTable.Add(card);
            gameState.NextTurn();
            AIMove();
        }

        public void AIMove()
        {
            Card aiCard = ai.MakeMove();
            if (aiCard != null)
            {
                ai.PlayCard(aiCard);
                gameState.CardsOnTable.Add(aiCard);
            }
            gameState.NextTurn();
        }
    }
}
