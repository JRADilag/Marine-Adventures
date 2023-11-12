using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Player : PictureBox
    {
        private string name;
        private int life = 10;
        private int speed = 10;
        private int shootDelay = 200;
        private int shootSpeed = 50;
        private int score = 0;
        private int sizeHeight = 39, sizeWidth = 86;
        private int playerX = 100, playerY = 100;
        private bool isShooting = false;
        private int currentLevel = 1;
        private Timer playerShootInterval = new Timer();
        private SoundPlayer shootingSound = new SoundPlayer(Resources.Shoot);

        public Player()
        {
            this.Size = new Size(sizeWidth, sizeHeight);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Resources.Player;
            this.Location = new System.Drawing.Point(playerX, playerY);
            this.BackColor = Color.Transparent;
            this.Tag = "Player";
            playerShootInterval.Interval = shootDelay;
            playerShootInterval.Tick += Shoot;
            playerShootInterval.Start();
        }

        public void Shoot(object sender, EventArgs e)
        {
            if (isShooting)
            {
                shootingSound.Play();
                playerShootInterval.Interval = shootDelay;
                Bullet playerBullet = new Bullet(this.Location, "Player");
                playerBullet.Shooter = "Player";
                playerBullet.BulletSpeed = shootSpeed;
                this.Parent.Controls.Add(playerBullet);
            }
        }

        public List<PictureBox> HeartGUI()
        {
            List<PictureBox> lifeGUI = new List<PictureBox>();
            for (int life = 0; life < this.life; life++)
            {
                PictureBox heart = new PictureBox();
                heart.Image = Resources.Heart_Full;
                heart.Location = new Point(30 + (20 * life), 30);
                heart.BackColor = Color.Transparent;
                heart.SizeMode = PictureBoxSizeMode.StretchImage;
                heart.Size = new Size(12, 12);
                heart.Tag = "Heart";
                lifeGUI.Add(heart);
            }
            return lifeGUI;
        }

        public List<Label> TextGUI()
        {
            List<Label> textGUI = new List<Label>();
            Label scoreGUI = new Label();
            scoreGUI.Location = new Point(30, 60);
            scoreGUI.AutoSize = true;
            scoreGUI.Name = "Score";
            scoreGUI.ForeColor = Color.Transparent;
            scoreGUI.Text = "Score: " + score.ToString();
            scoreGUI.Font = new Font(scoreGUI.Font.Name, 14);
            textGUI.Add(scoreGUI);

            Label levelGUI = new Label();
            levelGUI.Location = new Point(30, 90);
            levelGUI.AutoSize = true;
            levelGUI.Name = "Level";
            levelGUI.ForeColor = Color.Transparent;
            levelGUI.Text = "Level: " + currentLevel.ToString();
            levelGUI.Font = new Font(levelGUI.Font.Name, 14);
            textGUI.Add(levelGUI);

            return textGUI;
        }

        public string PlayerName

        {
            get { return name; }
            set { name = value; }
        }

        public int Life
        {
            get { return life; }
            set { life = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public int ShootDelay
        {
            get { return shootDelay; }
            set { shootDelay = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int CurrentLevel
        {
            get { return currentLevel; }
            set { currentLevel = value; }
        }

        public bool IsShooting
        {
            get { return isShooting; }
            set { isShooting = value; }
        }
    }
}