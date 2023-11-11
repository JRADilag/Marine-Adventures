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
        private int currentLevel;
        private bool isLevelOver;
        private List<PictureBox> enemies = new List<PictureBox>();
        private int numberOfEnemies = 5;

        public GameWindow()
        {
            this.Size = new System.Drawing.Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.BackgroundImage = Resources.background;
            this.BackColor = Color.LightSeaGreen;
            this.KeyDown += new KeyEventHandler(GameWindowControlsDown);
            this.KeyUp += new KeyEventHandler(GameWindowControlsUp);

            InitializeEntities();
            GameTimer();
        }

        private void InitializeEntities()
        {
            this.Controls.Add(player);
            int enemyX;
            int enemyY;

            for (int i = 0; i < numberOfEnemies; i++)
            {
                enemyX = random.Next((int)(WINDOW_WIDTH * 0.4), (int)WINDOW_WIDTH);
                enemyY = random.Next(0, (int)WINDOW_HEIGHT);
                enemies.Add(new Enemy(enemyX, enemyY));
            }
            foreach (Enemy enemy in enemies)
            {
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
            if (movingLeft && player.Left > 0)
            {
                player.Left -= player.Speed;
            }
            if (movingRight && player.Left < 500)
            {
                player.Left += player.Speed;
            }
            if (movingUp && player.Top > 0)
            {
                player.Top -= player.Speed;
            }
            if (movingDown && player.Top < WINDOW_HEIGHT - (player.Height * 2))
            {
                player.Top += player.Speed;
            }
            if (player.IsShooting)
            {
                player.Shoot();
            }
            foreach (Control control in this.Controls)
            {
                foreach (Enemy enemy in enemies)
                {
                    if (control is PictureBox && control.Tag == "Bullet")
                    {
                        if (control.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            Collision(control, enemy);
                            Console.WriteLine("Hit");
                            enemy.Health -= 1;

                            if (enemy.Health == 0)
                            {
                                this.Controls.Remove(enemy);
                                enemy.Dispose();
                            }
                        }
                    }
                }
                if (control is PictureBox && control.Tag == "Hit")
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
            }

            if (isLevelOver)
            {
                currentLevel += 1;
            }
            this.Invalidate();
        }

        private void Collision(Control control, Control control2)
        {
            this.Controls.Remove(control);
            control.Dispose();

            PictureBox bulletHit = new PictureBox();
            bulletHit.Image = Resources.Hit;
            bulletHit.Location = control2.Location;
            bulletHit.Left -= 40;
            bulletHit.BackColor = Color.Transparent;
            bulletHit.SizeMode = PictureBoxSizeMode.StretchImage;
            bulletHit.Size = new Size(26, 24);
            bulletHit.Tag = "Hit";
            this.Controls.Add(bulletHit);
        }

        private void GameWindowControlsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                case Keys.D:
                    movingRight = true;
                    break;

                case Keys.Left:
                case Keys.A:
                    movingLeft = true;
                    break;

                case Keys.Up:
                case Keys.W:
                    movingUp = true;
                    break;

                case Keys.Down:
                case Keys.S:
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
                case Keys.D:
                    movingRight = false;
                    break;

                case Keys.Left:
                case Keys.A:
                    movingLeft = false;
                    break;

                case Keys.Up:
                case Keys.W:
                    movingUp = false;
                    break;

                case Keys.Down:
                case Keys.S:
                    movingDown = false;
                    break;

                case Keys.Space:
                    player.IsShooting = false;
                    break;
            }
        }
    }
}