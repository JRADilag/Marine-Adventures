using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class GameWindow : Form
    {
        private Player player = new Player();
        private Random random = new Random();
        private Timer gameTimer = new Timer();
        private Timer subTimer = new Timer();
        private Timer environmentTimer = new Timer();
        private double WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
        private double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
        private bool movingLeft, movingRight, movingDown, movingUp;
        private bool isLevelOver;
        private bool isGameOver = false;
        private bool isLoad;
        private int numberOfEnemies = 3;
        public List<PictureBox> enemies = new List<PictureBox>();
        public List<PictureBox> lifeGUI = new List<PictureBox>();
        public List<PowerUps> powerUps = new List<PowerUps>();
        public List<Obstacles> obstacles = new List<Obstacles>();
        public List<Label> textGUI;

        public GameWindow(bool isLoad)
        {
            this.Size = new System.Drawing.Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.DoubleBuffered = true;
            this.BackColor = Color.LightSeaGreen;
            this.Text = "Marine Adventures";
            this.KeyDown += new KeyEventHandler(GameWindowControlsDown);
            this.KeyUp += new KeyEventHandler(GameWindowControlsUp);
            this.isLoad = isLoad;

            InitializeEntities(player.CurrentLevel);
            GameTimer();
            SubTimer();
            EnvironmentTimer();
        }

        private void InitializeEntities(int level)
        {
            //PLAYER
            this.Controls.Add(player);

            // load
            if (File.Exists("gameSave.txt") && this.isLoad == true)
            {
                string playerData = File.ReadAllText("gameSave.txt");
                Console.Write(playerData);
                string[] playerDataLoaded = playerData.Split(';');
                Console.Write(playerDataLoaded.ToString());
                player.CurrentLevel = Int32.Parse(playerDataLoaded[0]);
                player.Damage = Int32.Parse(playerDataLoaded[1]);
                player.Health = Int32.Parse(playerDataLoaded[2]);
                player.Score = Int32.Parse(playerDataLoaded[3]);
                player.Barrels = Int32.Parse(playerDataLoaded[4]);
                player.Speed = Int32.Parse(playerDataLoaded[5]);
            }

            //GUI
            resetGUI();
            //ENEMIES
            SpawnEnemies();
        }

        private int enemyX;
        private int enemyY;

        private void SpawnEnemies()
        {
            for (int i = 0; i < numberOfEnemies; i++)
            {
                enemyX = random.Next((int)(WINDOW_WIDTH * 0.4), (int)WINDOW_WIDTH - 100);
                enemyY = random.Next(0, (int)WINDOW_HEIGHT - 200);
                enemies.Add(new Enemy(enemyX, enemyY, "Normal"));
            }
            foreach (Enemy enemy in enemies)
            {
                enemy.Speed += 2 * player.CurrentLevel;
                enemy.ShootDelay -= 2 * player.CurrentLevel;
                enemy.ShootSpeed += 2 * player.CurrentLevel;
                enemy.Health += (int)(player.CurrentLevel * 1.8);
                this.Controls.Add(enemy);
            }
;
        }

        private void SpawnBoss()
        {
            enemyX = (int)WINDOW_WIDTH - 200;
            enemyY = (int)WINDOW_HEIGHT / 2;
            enemies.Add(new Enemy(enemyX, enemyY, "Boss"));
            foreach (Enemy enemy in enemies)
            {
                enemy.Speed += 2 * player.CurrentLevel;
                enemy.ShootDelay -= 2 * player.CurrentLevel;
                enemy.ShootSpeed += 2 * player.CurrentLevel;
                enemy.Health += (int)(player.CurrentLevel * 1.8);
                this.Controls.Add(enemy);
            }
            Enemy a = (Enemy)enemies.Find(x => x.Name == "Boss");
            Console.WriteLine("boss health {0}", a.Health);
            Console.WriteLine("player damage {0}", player.Damage);
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

        private void EnvironmentTimer()
        {
            environmentTimer.Interval = 5000;
            environmentTimer.Tick += EnvironmentTimer_Tick;
            environmentTimer.Start();
        }

        private void EnvironmentTimer_Tick(object sender, EventArgs e)
        {
            if (enemies.Count < 6 && player.CurrentLevel % 5 != 0)
            {
                SpawnEnemies();
            }
            else if (!(enemies.Exists(x => x.Name == "Boss")) && player.CurrentLevel % 5 == 0)
            {
                SpawnBoss();
            }
            PowerUps powerUp = new PowerUps();
            this.powerUps.Add(powerUp);
            this.Controls.Add(powerUp);

            Obstacles obstacle = new Obstacles();
            this.obstacles.Add(obstacle);
            this.Controls.Add(obstacle);
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
                        player.Health -= 1;
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

                for (int i = powerUps.Count - 1; i >= 0; i--)
                {
                    PowerUps powerUp = (PowerUps)powerUps[i];

                    if (control is PictureBox && (string)control.Tag == "PowerUp")
                    {
                        if (control.Bounds.IntersectsWith(player.Bounds))
                        {
                            switch (powerUp.Name)
                            {
                                case "HealthPowerUp":
                                    if (player.Health < 3)
                                    {
                                        player.Health += 1;
                                        resetGUI();
                                    }
                                    Collision(control, powerUp);
                                    this.Controls.Remove(powerUp);
                                    powerUp.Dispose();
                                    powerUps.Remove(powerUp); break;

                                case "SpeedPowerUp":
                                    if (player.Speed < 30)
                                    {
                                        player.Speed += 1;
                                    }
                                    Collision(control, powerUp);
                                    this.Controls.Remove(powerUp);
                                    powerUp.Dispose();
                                    powerUps.Remove(powerUp); break;

                                case "AttackPowerUp":
                                    if (player.Barrels < 2)
                                    {
                                        player.Barrels += 1;
                                    }
                                    else
                                    {
                                        player.Damage += 1;
                                    }
                                    Collision(control, powerUp);
                                    this.Controls.Remove(powerUp);
                                    powerUp.Dispose();
                                    powerUps.Remove(powerUp); break;
                            }
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
                            enemy.Health -= player.Damage;

                            // enemy defeated
                            if (enemy.Health <= 0)
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
                // save game state
                player.CurrentLevel += 1;
                SaveGame save = new SaveGame();
                save.SaveGameState(player);
                resetGame();
            }
            if (isGameOver)
            {
                // close form and open a new diaglogue to save player name and score

                gameTimer.Stop();
                subTimer.Stop();
                environmentTimer.Stop();
                player.ShootInterval.Stop();

                foreach (Enemy enemy in enemies)
                {
                    enemy.ShootInterval.Stop();
                }

                this.ForeColor = Color.DarkSlateGray;
                this.BackColor = Color.DarkSlateGray;

                this.Close();

                EndScreen endDialogue = new EndScreen(player.Score);
                endDialogue.ShowDialog();
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
                            player.Health -= 1;
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

                for (int i = obstacles.Count - 1; i >= 0; i--)
                {
                    Obstacles obstacle = (Obstacles)obstacles[i];

                    if (control is PictureBox && (string)control.Tag == "Player")
                    {
                        if (control.Bounds.IntersectsWith(obstacle.Bounds))
                        {
                            player.Health -= 1;
                            Collision(control, obstacle);
                            this.Controls.Remove(obstacle);
                            obstacle.Dispose();
                            obstacles.Remove(obstacle);

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
            if ((string)control.Tag == "Bullet" ||
                (string)control.Tag == "EnemyBullet" ||
                (string)control.Name == "PowerUpHealth" ||
                (string)control.Name == "PowerUpSpeed" ||
                (string)control.Name == "PowerUpAttack")
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
            resetGUI();

            //intialize entities again
            InitializeEntities(player.CurrentLevel);
        }

        private void resetGUI()
        {
            if (lifeGUI != null && textGUI != null)
            {
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
            }

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

                case Keys.Escape:
                    isGameOver = true;
                    break;
            }
        }
    }
}