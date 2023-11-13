using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class MainWindow : Form
    {
        private double WINDOW_HEIGHT, WINDOW_WIDTH;
        private Button start = new Button();
        private Button load = new Button();
        private Button highscore = new Button();
        private Button exit = new Button();
        private SoundPlayer BGM = new SoundPlayer(Resources.BGM);

        public MainWindow()
        {
            BGM.PlayLooping();
            WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
            WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
            this.Text = "Marine Adventures";
            this.Size = new Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackgroundImage = Resources.MARINE_ADVENTURES;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            System.Windows.Forms.Button[] buttons = { start, load, highscore, exit };

            start.Text = "New Game";
            load.Text = "Load";
            highscore.Text = "High score";
            exit.Text = "Exit";

            start.Left = (this.ClientSize.Width - start.Width) / 2;
            start.Top = (this.ClientSize.Height - start.Height) / 2;
            start.Anchor = AnchorStyles.None;

            // TODO: MAKE INTO FOR LOOP
            load.Location = new Point(start.Left, start.Height + start.Top + 10);
            load.Anchor = AnchorStyles.None;
            highscore.Location = new Point(load.Left, load.Height + load.Top + 10);
            highscore.Anchor = AnchorStyles.None;
            exit.Location = new Point(highscore.Left, highscore.Height + highscore.Top + 10);
            exit.Anchor = AnchorStyles.None;

            exit.Click += new EventHandler(ExitButtonEvent);
            start.Click += new EventHandler(StartGameButtonEvent);
            load.Click += new EventHandler(StartGameButtonEventLoad);
            highscore.Click += new EventHandler(HighScoreButtonEvent);

            foreach (System.Windows.Forms.Button button in buttons)
            {
                this.Controls.Add(button);
            }
        }

        private void HighScoreButtonEvent(object sender, EventArgs e)
        {
            if (File.Exists("highScores.txt"))
            {
                this.Hide();
                HighscoreScreen highScores = new HighscoreScreen();
                highScores.ShowDialog();
                this.Show();
            }
            else
            {
                MessageBox.Show("No Scores Yet!");
            }
        }

        private void StartGameButtonEvent(object sender, EventArgs e)
        {
            this.Hide();
            GameWindow gameWindow = new GameWindow(false);
            gameWindow.ShowDialog();
            this.Show();
        }

        private void StartGameButtonEventLoad(object sender, EventArgs e)
        {
            this.Hide();
            GameWindow gameWindow = new GameWindow(true);
            gameWindow.ShowDialog();
            this.Show();
        }

        private void ExitButtonEvent(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}