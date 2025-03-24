using DurakGame.Classes;
using System.Collections.Generic;

namespace DurakGame.Classes
{
    public class GameState
    {
        public List<Card> CardsOnTable { get; private set; }
        public int CurrentTurn { get; set; }
        public bool IsGameOver { get; set; }

        public GameState()
        {
            CardsOnTable = new List<Card>();
            CurrentTurn = 0;
            IsGameOver = false;
        }

        public void NextTurn()
        {
            CurrentTurn = (CurrentTurn + 1) % 2;
        }
    }
}
