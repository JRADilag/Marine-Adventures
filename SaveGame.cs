using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marine_Adventures
{
    internal class SaveGame
    {
        private string toSave;

        public SaveGame()
        {
        }

        public void SaveGameState(Player player)
        {
            toSave = player.CurrentLevel + ";" + player.Damage + ";" + player.Health + ";" + player.Score + ";" + player.Barrels + ";" + player.Speed;
            //player level, player damage, player health, player score, player barrels, player speed
            //FileStream saveGameState = new FileStream("gameSave.txt", FileMode.OpenOrCreate);
            File.WriteAllText("gameSave.txt", toSave);
        }

        public void SaveHighScore(string playerName, string playerScore)
        {
            //player level, player score
            toSave = playerName + ";" + playerScore;
            using (FileStream highscore = new FileStream("highScores.txt", FileMode.Append))
            using (StreamWriter writeData = new StreamWriter(highscore))
            {
                writeData.WriteLine(toSave);
            }
        }
    }
}