using System;
using System.Drawing;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Enemy : PictureBox
    {
        private int health = 10;
        private int speed = 5;
        private int sizeHeight = 32, sizeWidth = 30;
        private int decision;
        private bool isShooting = true;
        private int shootDelay = 1000;
        private int shootSpeed = 20;
        private Timer shootInterval = new Timer();

        // enemy movement: Y level only, track player y level, shoot staight.

        public Enemy(int startPosX, int startPosY)
        {
            //startPosX = random.Next((int)(WINDOW_WIDTH * 0.4), (int)WINDOW_WIDTH);
            //startPosY = random.Next(100, (int)(WINDOW_HEIGHT));

            this.Size = new Size(sizeWidth, sizeHeight);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Resources.Enemy1;
            this.BackColor = Color.Transparent;
            this.Location = new System.Drawing.Point(startPosX, startPosY);
            this.Tag = "Enemy";
            Console.WriteLine("Enemy located in {0}, {1}", startPosX, startPosY);
            shootInterval.Interval = shootDelay / 60;
            shootInterval.Tick += Shoot;
            shootInterval.Start();
        }

        public void Shoot(object sender, EventArgs e)
        {
            if (isShooting)
            {
                shootInterval.Interval = shootDelay;
                Point loc = this.Location;
                Bullet bullet = new Bullet(loc, "Enemy");
                bullet.Shooter = "Enemy";
                bullet.Tag = "EnemyBullet";
                bullet.BulletSpeed = shootSpeed;

                if (this.Parent != null)
                {
                    this.Parent.Controls.Add(bullet);
                }
            }
        }

        public int Health
        {
            get { return health; }
            set { health = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int ShootDelay
        {
            get { return shootDelay; }
            set { shootDelay = value; }
        }

        public int ShootSpeed
        {
            get { return shootSpeed; }
            set { shootSpeed = value; }
        }

        public int Decision
        {
            get { return decision; }
            set { decision = value; }
        }
    }
}