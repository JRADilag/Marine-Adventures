using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class GameWindow : Form
    {
        private Player player = new Player();
        private Random random = new Random();
        private Timer gameTimer = new Timer();
        private Timer subTimer = new Timer();
        private double WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
        private double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
        private bool movingLeft, movingRight, movingDown, movingUp;
        private bool isLevelOver;
        private bool isGameOver = false;
        private int numberOfEnemies = 5;
        public List<PictureBox> enemies = new List<PictureBox>();
        public List<PictureBox> lifeGUI = new List<PictureBox>();
        public List<Label> textGUI;

        public GameWindow()
        {
            this.Size = new System.Drawing.Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.DoubleBuffered = true;
            this.BackColor = Color.LightSeaGreen;
            this.KeyDown += new KeyEventHandler(GameWindowControlsDown);
            this.KeyUp += new KeyEventHandler(GameWindowControlsUp);

            InitializeEntities(player.CurrentLevel);
            GameTimer();
            SubTimer();
        }

        private void InitializeEntities(int level)
        {
            //PLAYER
            this.Controls.Add(player);

            //GUI
            lifeGUI = player.HeartGUI();
            textGUI = player.TextGUI();
            foreach (PictureBox heart in lifeGUI)
            {
                this.Controls.Add(heart);
            }

            foreach (Label label in textGUI)
            {
                this.Controls.Add(label);
            }
            //ENEMIES
            int enemyX;
            int enemyY;
            for (int i = 0; i < numberOfEnemies; i++)
            {
                enemyX = random.Next((int)(WINDOW_WIDTH * 0.4), (int)WINDOW_WIDTH - 100);
                enemyY = random.Next(0, (int)WINDOW_HEIGHT - 200);
                enemies.Add(new Enemy(enemyX, enemyY));
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Speed += 2 * player.CurrentLevel;
                enemy.ShootDelay -= 2 * player.CurrentLevel;
                enemy.ShootSpeed += 2 * player.CurrentLevel;
                this.Controls.Add(enemy);
            }
;
        }

        private void GameTimer()
        {
            gameTimer.Interval = 1000 / 60;
            gameTimer.Tick += GameWindowLoop;
            gameTimer.Start();
        }

        private void SubTimer()
        {
            subTimer.Interval = 500;
            subTimer.Tick += SubTimer;
            subTimer.Start();
        }

        private void GameWindowLoop(object sender, EventArgs e)
        {
            if (movingLeft && player.Left > 0)
            {
                player.Left -= player.Speed;
            }
            if (movingRight && player.Left < (int)(WINDOW_WIDTH * 0.4))
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
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "EnemyBullet")
                {
                    if (control.Bounds.IntersectsWith(player.Bounds))
                    {
                        Collision(control, player);
                        player.Life -= 1;
                        if (lifeGUI.Count > 1)
                        {
                            this.Controls.Remove(lifeGUI.Last());
                            lifeGUI.Last().Dispose();
                            lifeGUI.Remove(lifeGUI.Last());
                        }
                        else
                        {
                            isGameOver = true;
                        }
                    }
                }

                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    Enemy enemy = (Enemy)enemies[i];
                    if (control is PictureBox && (string)control.Tag == "Bullet")
                    {
                        if (control.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            Collision(control, enemy);
                            enemy.Health -= 1;

                            // enemy defeated
                            if (enemy.Health == 0)
                            {
                                player.Score += 10;
                                textGUI[0].Text = "Score: " + player.Score.ToString();
                                textGUI[1].Text = "Level: " + player.CurrentLevel.ToString();
                                this.Controls.Remove(enemy);
                                enemy.Dispose();
                                enemies.Remove(enemies[i]);
                                if (enemies.Count == 0)
                                {
                                    isLevelOver = true;
                                }
                            }
                        }
                    }
                }
            }

            if (isLevelOver)
            {
                Console.WriteLine("Player Score: {0}", player.Score);
                player.CurrentLevel += 1;
                resetGame();
            }
            if (isGameOver)
            {
                gameTimer.Stop();
                subTimer.Stop();
                MessageBox.Show("Game Over.");
            }
            this.Invalidate();
        }

        private void SubTimer(object sender, EventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "Hit" ||
                        control is PictureBox && (string)control.Tag == "Bullet" && control.Left > WINDOW_WIDTH ||
                        control is PictureBox && (string)control.Tag == "EnemyBullet" && control.Left < 0)
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
            }
            foreach (Control control in this.Controls)
            {
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    Enemy enemy = (Enemy)enemies[i];

                    // enemy move to y level of player
                    // enemy movement logic
                    enemy.Decision = random.Next(4);
                    enemy.ShootDelay = random.Next(2000, 4000);
                    switch (enemy.Decision)
                    {
                        case 1:
                            if (enemy.Top > player.Top)
                            {
                                enemy.Top -= enemy.Speed;
                            }
                            else if (enemy.Top < player.Top)
                            {
                                enemy.Top += enemy.Speed;
                            }
                            break;

                        case 2:
                            if (enemy.Top > WINDOW_HEIGHT)
                            {
                                enemy.Top -= enemy.Speed * 3;
                                break;
                            }
                            break;

                        case 3:
                            if (enemy.Top < 0)
                            {
                                enemy.Top += enemy.Speed * 3;
                                break;
                            }
                            break;
                    }
                    //  If enemy intersects with player
                    if (control is PictureBox && (string)control.Tag == "Player")
                    {
                        if (control.Bounds.IntersectsWith(enemy.Bounds))
                        {
                            player.Life -= 1;
                            if (lifeGUI.Count > 1)
                            {
                                this.Controls.Remove(lifeGUI.Last());
                                lifeGUI.Last().Dispose();
                                lifeGUI.Remove(lifeGUI.Last());
                            }
                            else
                            {
                                isGameOver = true;
                            }
                        }
                    }
                }
            }
        }

        private void Collision(Control control, Control control2)
        {
            if ((string)control.Tag == "Bullet" || (string)control.Tag == "EnemyBullet")
            {
                this.Controls.Remove(control);
                control.Dispose();
            }

            PictureBox bulletHit = new PictureBox();
            bulletHit.Image = Resources.Hit;
            bulletHit.Location = control2.Location;
            bulletHit.Left -= 20;
            bulletHit.Top += 10;
            bulletHit.BackColor = Color.Transparent;
            bulletHit.SizeMode = PictureBoxSizeMode.StretchImage;
            bulletHit.Size = new Size(26, 24);
            bulletHit.Tag = "Hit";
            this.Controls.Add(bulletHit);
        }

        private void resetGame()
        {
            isLevelOver = false;

            //remove all controls in game
            foreach (Control control in this.Controls)
            {
                if (!((string)control.Tag == "Player"))
                {
                    this.Controls.Remove(control);
                    control.Dispose();
                }
            }
            foreach (PictureBox heart in lifeGUI)
            {
                this.Controls.Remove(heart);
                heart.Dispose();
            }
            foreach (Label label in textGUI)
            {
                this.Controls.Remove(label);
                label.Dispose();
            }
            //intialize entities again
            InitializeEntities(player.CurrentLevel);
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