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
        private double WINDOW_HEIGHT, WINDOW_WIDTH;
        private Player player = new Player();
        private Random random = new Random();

        public GameWindow()
        {
            WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
            WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
            this.Size = new System.Drawing.Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
            this.KeyDown += new KeyEventHandler(GameWindowEvents);
            InitializeEntities();
            GameTimer();
        }

        private void InitializeEntities()
        {
            this.Controls.Add(player);
        }

        private void GameTimer()
        {
            Timer gameTimer = new Timer();
            gameTimer.Interval = 1000 / 60;
            gameTimer.Tick += Update;
            gameTimer.Start();
        }

        private void Update(object sender, EventArgs e)
        {
            this.Invalidate();
        }

        private void GameWindowEvents(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Right)
            {
                player.Left += player.Speed;
            }
            if (e.KeyCode == Keys.Left)
            {
                player.Left -= player.Speed;
            }
            if (e.KeyCode == Keys.Down)
            {
                player.Top += player.Speed;
            }
            if (e.KeyCode == Keys.Up)
            {
                player.Top -= player.Speed;
            }
        }
    }
}