
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurakCardGame.Models
{

    public static class GameStats
    {
        private static readonly string FilePath = "stats.txt";
        public static int Wins { get; private set; }
        public static int Losses { get; private set; }

        public static string LastReset { get; private set; } = "Never";

        static GameStats()
        {
            Load();
        }

        public static void Load()
        {
            if (!File.Exists(FilePath))
            {
                Wins = 0;
                Losses = 0;
                LastReset = "Never";
                return;
            }

            string[] lines = File.ReadAllLines(FilePath);
            foreach (var line in lines)
            {
                if (line.StartsWith("Wins")) Wins = int.Parse(line.Split(':')[1]);
                if (line.StartsWith("Losses")) Losses = int.Parse(line.Split(':')[1]);
                if (line.StartsWith("LastReset")) LastReset = line.Split(':')[1];
            }
        }

        public static void Save()
        {
            File.WriteAllLines(FilePath, new[]
            {
                $"Wins:{Wins}",
                $"Losses:{Losses}"
            });
        }

        public static void RecordWin()
        {
            Wins++;
            Save();
        }

        public static void RecordLoss()
        {
            Losses++;
            Save();
        }
    }

}
