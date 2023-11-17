using System;
using System.Drawing;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Enemy : Character
    {
        private int decision;
        public Timer ShootInterval = new Timer();

        public Enemy(int startPosX, int startPosY, string type)
        {
            InitializeEnemy(type, startPosX, startPosY);
            SetupShootInterval();
        }

        private void InitializeEnemy(string type, int startPosX, int startPosY)
        {
            switch (type)
            {
                case "Normal":
                    InitializeNormalEnemy();
                    break;

                case "Boss":
                    InitializeBossEnemy();
                    break;
            }

            SetupCommonProperties(startPosX, startPosY);
        }

        private void InitializeNormalEnemy()
        {
            health = 2; speed = 5;
            sizeHeight = 32; sizeWidth = 30;
            isShooting = true; ShootDelay = 1000; shootSpeed = 20;
            Image = Resources.Enemy1;
            Name = "Normal";
        }

        private void InitializeBossEnemy()
        {
            health = 400; speed = 5;
            sizeHeight = 144; sizeWidth = 192;
            isShooting = true; shootDelay = 700; shootSpeed = 10;
            Image = Resources.Boss;
            Name = "Boss";
        }

        private void SetupCommonProperties(int startPosX, int startPosY)
        {
            Size = new Size(sizeWidth, sizeHeight);
            SizeMode = PictureBoxSizeMode.StretchImage;
            BackColor = Color.Transparent;
            Location = new Point(startPosX, startPosY);
            Tag = "Enemy";
        }

        private void SetupShootInterval()
        {
            ShootInterval.Interval = shootDelay / 60;
            ShootInterval.Tick += Shoot;
            ShootInterval.Start();
        }

        public override void Shoot(object sender, EventArgs e)
        {
            if (!isShooting || Parent == null) return;

            ShootInterval.Interval = shootDelay;
            Point loc = Location;
            Bullet bullet = new Bullet(loc, "Enemy")
            {
                Shooter = "Enemy",
                Tag = "EnemyBullet",
                BulletSpeed = shootSpeed
            };

            Parent.Controls.Add(bullet);
        }

        public int Decision
        {
            get => decision;
            set => decision = value;
        }
    }
}
