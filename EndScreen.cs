using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class EndScreen : Form
    {
        private readonly SaveGame highScore = new SaveGame();
        private readonly int playerScore;

        public EndScreen(int playerScore)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.Beige;
            this.Text = "Game Over!";
            this.playerScore = playerScore;

            InitializeControls();
        }

        private void InitializeControls()
        {
            Label score = new Label();
            score.Font = new Font(Font.Name, 14);
            score.BackColor = Color.Transparent;
            score.Text = "Score: " + playerScore.ToString();
            score.AutoSize = true;
            score.Left = (this.ClientSize.Width - score.Width) / 2;
            score.Top = (this.ClientSize.Height - score.Height) / 2;
            score.Anchor = AnchorStyles.None;

            TextBox nameEntry = new TextBox();
            nameEntry.Location = new Point(score.Left, score.Height + score.Top + 10);
            nameEntry.Anchor = AnchorStyles.None;
            nameEntry.Text = "";

            Button accept = new Button();
            accept.Location = new Point(nameEntry.Left, nameEntry.Height + nameEntry.Top + 10);
            accept.Anchor = AnchorStyles.None;
            accept.Text = "Done";
            accept.BackColor = Color.Gray;
            accept.Click += SaveHighScore;

            Controls.AddRange(new Control[] { accept, score, nameEntry });
        }

        private void SaveHighScore(object sender, EventArgs e)
        {
            string playerName = Controls[2].Text;
            highScore.SaveHighScore(playerName, playerScore.ToString());

            MessageBox.Show("Highscore saved. Thank you for playing!");
            Close();
        }
    }
}
