using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class HighscoreScreen : Form
    {
        private ListBox highScores = new ListBox();
        private string[] scores;

        public HighscoreScreen()
        {
            string playerName, playerScore;
            string[] playerData = new string[2];
            string highScore;
            highScores.Size = new System.Drawing.Size(this.Width, this.Height);
            this.Controls.Add(highScores);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            scores = File.ReadAllLines("highScores.txt");

            highScores.BeginUpdate();

            foreach (string score in scores)
            {
                playerData = score.Split(';');

                highScore = "Name: " + playerData[0] + "\tScore: " + playerData[1];

                highScores.Items.Add(highScore);

                highScores.EndUpdate();
            }
        }
    }
}