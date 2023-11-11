using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class GameWindow : Form
    {
        private Player player = new Player();
        private Random random = new Random();
        private double WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
        private double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
        private bool movingLeft, movingRight, movingDown, movingUp;

        public GameWindow()
        {
            this.Size = new System.Drawing.Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            this.KeyDown += new KeyEventHandler(GameWindowControlsDown);
            this.KeyUp += new KeyEventHandler(GameWindowControlsUp);
            InitializeEntities();
            GameTimer();
        }

        private void InitializeEntities()
        {
            this.Controls.Add(player);
            for (int i = 0; i < 1; i++)
            {
                Enemy enemy = new Enemy();
                this.Controls.Add(enemy);
            }
        }

        private void GameTimer()
        {
            Timer gameTimer = new Timer();
            gameTimer.Interval = 1000 / 60;
            gameTimer.Tick += GameWindowLoop;
            gameTimer.Start();
        }

        private void GameWindowLoop(object sender, EventArgs e)
        {
            if (movingLeft)
            {
                player.Left -= player.Speed;
            }
            if (movingRight)
            {
                player.Left += player.Speed;
            }
            if (movingUp)
            {
                player.Top -= player.Speed;
            }
            if (movingDown)
            {
                player.Top += player.Speed;
            }
            if (player.IsShooting)
            {
                player.Shoot();
            }

            this.Invalidate();
        }

        private void GameWindowControlsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    movingRight = true;
                    break;

                case Keys.Left:
                    movingLeft = true;
                    break;

                case Keys.Up:
                    movingUp = true;
                    break;

                case Keys.Down:
                    movingDown = true;
                    break;

                case Keys.Space:
                    player.IsShooting = true;
                    break;
            }
        }

        private void GameWindowControlsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    movingRight = false;
                    break;

                case Keys.Left:
                    movingLeft = false;
                    break;

                case Keys.Up:
                    movingUp = false;
                    break;

                case Keys.Down:
                    movingDown = false;
                    break;

                case Keys.Space:
                    player.IsShooting = false;
                    break;
            }
        }
    }
}