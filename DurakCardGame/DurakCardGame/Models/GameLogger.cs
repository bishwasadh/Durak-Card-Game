using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakCardGame.Models
{

    public static class GameLogger 
    {
        private static readonly string LogPath = "game_log.txt";

        public static void Log(string message)
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}";
            File.AppendAllLines(LogPath, new[] { entry });
        }

        public static void LogCardAction(string playerName, Card card, string action)
        {
            Log($"{playerName} {action} {card}");
        }

        public static void LogGameResult(bool playerWon)
        {
            Log(playerWon ? "Player Wins 🎉" : "Player Lost 😢");
        }
    }

}
