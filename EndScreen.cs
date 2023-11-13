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
        private List<Control> screenControls = new List<Control>();
        private SaveGame highScore = new SaveGame();
        private int playerScore;

        public EndScreen(int playerScore)
        {
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.Beige;
            this.Text = "Game Over!";
            this.playerScore = playerScore;

            Label score = new Label();
            Label gameOver = new Label();
            TextBox nameEntry = new TextBox();
            Button accept = new Button();

            score.Left = (this.ClientSize.Width - score.Width) / 2;
            score.Top = (this.ClientSize.Height - score.Height) / 2;
            score.Anchor = AnchorStyles.None;
            score.Text = "Score: " + playerScore.ToString();
            score.Font = new Font(score.Font.Name, 14);
            score.BackColor = Color.Transparent;

            nameEntry.Location = new Point(score.Left, score.Height + score.Top + 10);
            nameEntry.Anchor = AnchorStyles.None;
            nameEntry.Text = "Enter your name";

            accept.Location = new Point(nameEntry.Left, nameEntry.Height + nameEntry.Top + 10);
            accept.Anchor = AnchorStyles.None;
            accept.Text = "Done";
            accept.BackColor = Color.Gray;

            accept.Click += new EventHandler(SaveHighScore);

            screenControls.Add(accept);
            screenControls.Add(score);
            screenControls.Add(nameEntry);

            foreach (Control control in screenControls)
            {
                this.Controls.Add(control);
            }
        }

        private void SaveHighScore(object sender, EventArgs e)
        {
            string playerScore = this.playerScore.ToString();
            string playerName = screenControls[2].Text;

            highScore.SaveHighScore(playerName, playerScore);
            MessageBox.Show("Highscore saved. Thank you for playing!");
            this.Close();
            // save highscore
        }
    }
}