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
        private string gameState;

        public SaveGame()
        {
        }

        public void SaveGameState(Player player)
        {
            gameState = player.CurrentLevel + ";" + player.Damage + ";" + player.Health + ";" + player.Score + ";" + player.Barrels + ";" + player.Speed;
            Console.Write(gameState);

            //player level, player damage, player health, player score, player barrels, player speed
            //FileStream saveGameState = new FileStream("gameSave.txt", FileMode.OpenOrCreate);
            File.WriteAllText("gameSave.txt", gameState);
        }

        public void SaveHighScore()
        {
            //player level, player score
        }
    }
}