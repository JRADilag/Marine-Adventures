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
        private readonly ListBox highScores = new ListBox();

        public HighscoreScreen()
        {
            InitializeForm();
            LoadHighScores();
        }

        private void InitializeForm()
        {
            this.Size = new System.Drawing.Size(500, 400);
            this.Text = "High Scores";
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Button backButton = new Button
            {
                Text = "Back",
                Size = new System.Drawing.Size(80, 30),
                Location = new System.Drawing.Point(10, 10)
            };
            backButton.Click += (sender, e) => this.Close();

            highScores.Location = new System.Drawing.Point(10, 50);
            highScores.Size = new System.Drawing.Size(this.ClientSize.Width - 20, this.ClientSize.Height - 70);

            this.Controls.Add(backButton);
            this.Controls.Add(highScores);

            this.Resize += (sender, e) => UpdateListBoxSize();
        }

        private void LoadHighScores()
        {
            string[] scores = File.ReadAllLines("highScores.txt")
                                  .OrderByDescending(score => int.Parse(score.Split(';')[1]))
                                  .ToArray();

            foreach (string score in scores)
            {
                string[] playerData = score.Split(';');
                string highScore = $"Name: {playerData[0]}\tScore: {playerData[1]}";
                highScores.Items.Add(highScore);
            }
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
