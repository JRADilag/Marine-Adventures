using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class MainWindow : Form
    {
        private const double WINDOW_SCALE = 1.2;
        private SoundPlayer BGM = new SoundPlayer(Resources.BGM);

        public MainWindow()
        {
            InitializeWindow();
            InitializeButtons();
        }

        private void InitializeWindow()
        {
            double WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / WINDOW_SCALE;
            double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / WINDOW_SCALE;

            Text = "Marine Adventures";
            Size = new Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            BackgroundImage = Resources.MARINE_ADVENTURES;
            BackgroundImageLayout = ImageLayout.Stretch;

            BGM.PlayLooping();
        }

        private void InitializeButtons()
        {
            var start = CreateButton("New Game");
            var load = CreateButton("Load");
            var highscore = CreateButton("High score");
            var exit = CreateButton("Exit");

            ArrangeButtons(start, load, highscore, exit);

            exit.Click += ExitButtonEvent;
            start.Click += StartGameButtonEvent;
            load.Click += StartGameButtonEventLoad;
            highscore.Click += HighScoreButtonEvent;

            Controls.AddRange(new Control[] { start, load, highscore, exit });
        }

        private Button CreateButton(string text)
        {
            return new Button
            {
                Text = text,
                Anchor = AnchorStyles.None
            };
        }

        private void ArrangeButtons(Button start, Button load, Button highscore, Button exit)
        {
            int topMargin = (ClientSize.Height - start.Height * 4 - 30) / 2;

            start.Left = (ClientSize.Width - start.Width) / 2;
            start.Top = topMargin;

            load.Left = start.Left;
            load.Top = start.Top + start.Height + 10;

            highscore.Left = load.Left;
            highscore.Top = load.Top + load.Height + 10;

            exit.Left = highscore.Left;
            exit.Top = highscore.Top + highscore.Height + 10;
        }

        private void HighScoreButtonEvent(object sender, EventArgs e)
        {
            string highScoresFile = "highScores.txt";

            if (File.Exists(highScoresFile))
            {
                Hide();
                var highScores = new HighscoreScreen();
                highScores.ShowDialog();
                Show();
            }
            else
            {
                MessageBox.Show("No Scores Yet!");
            }
        }

        private void StartGameButtonEvent(object sender, EventArgs e) => StartGame(false);

        private void StartGameButtonEventLoad(object sender, EventArgs e) => StartGame(true);

        private void StartGame(bool isLoad)
        {
            Hide();
            var gameWindow = new GameWindow(isLoad);
            gameWindow.ShowDialog();
            Show();
        }

        private void ExitButtonEvent(object sender, EventArgs e) => Close();
    }
}
