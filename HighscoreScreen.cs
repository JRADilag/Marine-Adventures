using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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

            this.Size = new Size(500, 400);
            this.Text = "High Scores";

            Button backButton = new Button();
            backButton.Text = "Back";
            backButton.Size = new Size(80, 30);
            backButton.Location = new Point(10, 10);
            backButton.Click += BackButton_Click;
            this.Controls.Add(backButton);

            highScores.Location = new Point(10, 50);
            UpdateListBoxSize(); 

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
            }

            highScores.EndUpdate();

            this.Resize += HighscoreScreen_Resize;
        }

        private void HighscoreScreen_Resize(object sender, EventArgs e)
        {
            UpdateListBoxSize();
        }

        private void UpdateListBoxSize()
        {
            highScores.Size = new Size(this.ClientSize.Width - 20, this.ClientSize.Height - 70);
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
