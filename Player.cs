using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Player : Character
    {
        private string name;
        private int score = 0, currentLevel = 1, barrels = 1, damage;
        private SoundPlayer shootingSound = new SoundPlayer(Resources.Shoot);
        public Timer ShootInterval = new Timer();

        public Player()
        {
            InitializePlayer();
        }

        private void InitializePlayer()
        {
            health = 3; speed = 10; ShootDelay = 200; shootSpeed = 40;
            sizeHeight = 39; sizeWidth = 86; IsShooting = false; damage = 1;

            Size = new Size(sizeWidth, sizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            Image = Resources.Player;
            Location = new Point(100, (int)(Screen.PrimaryScreen.Bounds.Height / 1.2 / 2));
            BackColor = Color.Transparent;
            Tag = "Player";

            ShootInterval.Interval = shootDelay;
            ShootInterval.Tick += Shoot;
            ShootInterval.Start();
        }

        public override void Shoot(object sender, EventArgs e)
        {
            if (IsShooting)
            {
                shootingSound.Play();
                ShootInterval.Interval = shootDelay;

                for (int i = 0; i < barrels; i++)
                {
                    int topPosition = Location.Y - 10 * i;

                    var playerBullet = new Bullet(Location, "Player")
                    {
                        Shooter = "Player",
                        BulletSpeed = shootSpeed,
                        Top = topPosition
                    };
                    Parent.Controls.Add(playerBullet);
                }
            }
        }

        public List<PictureBox> HeartGUI()
        {
            var lifeGUI = new List<PictureBox>();
            for (int life = 0; life < health; life++)
            {
                var heart = new PictureBox
                {
                    Image = Resources.Heart_Full,
                    Location = new Point(30 + (20 * life), 30),
                    BackColor = Color.Transparent,
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Size = new Size(12, 12),
                    Tag = "Heart"
                };
                lifeGUI.Add(heart);
            }
            return lifeGUI;
        }

        public List<Label> TextGUI()
        {
            var textGUI = new List<Label>();

            void AddLabel(string labelText, Point location)
            {
                var label = new Label
                {
                    Location = location,
                    AutoSize = true,
                    ForeColor = Color.Transparent,
                    Text = labelText,
                    Font = new Font(Font.Name, 14)
                };
                textGUI.Add(label);
            }

            AddLabel("Score: " + score, new Point(30, 60));
            AddLabel("Level: " + currentLevel, new Point(30, 90));

            return textGUI;
        }

        public string PlayerName
        {
            get => name;
            set => name = value;
        }

        public int Score
        {
            get => score;
            set => score = value;
        }

        public int CurrentLevel
        {
            get => currentLevel;
            set => currentLevel = value;
        }

        public bool IsShooting { get; set; }

        public int Barrels
        {
            get => barrels;
            set => barrels = value;
        }

        public int Damage
        {
            get => damage;
            set => damage = value;
        }
    }
}
