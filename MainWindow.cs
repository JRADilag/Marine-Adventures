using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public MainWindow()
        {
            WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
            WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
            this.Text = "Marine Adventures";
            this.Size = new Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);

            System.Windows.Forms.Button[] buttons = { start, load, highscore, exit };

            start.Text = "Start";
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

            foreach (System.Windows.Forms.Button button in buttons)
            {
                this.Controls.Add(button);
            }
        }

        private void StartGameButtonEvent(object sender, EventArgs e)
        {
            this.Hide();
            GameWindow gameWindow = new GameWindow();
            gameWindow.ShowDialog();
        }

        private void ExitButtonEvent(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}