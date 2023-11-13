using System;
using System.Drawing;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Enemy : Character
    {
        private int decision;
        //private PictureBox enemy = new PictureBox();

        // enemy movement: Y level only, track player y level, shoot staight.

        public Enemy(int startPosX, int startPosY, string type)
        {
            switch (type)
            {
                case "Normal":
                    this.health = 2; this.speed = 5;
                    this.sizeHeight = 32; this.sizeWidth = 30;
                    this.isShooting = true; this.ShootDelay = 1000; this.shootSpeed = 20;
                    this.Image = Resources.Enemy1;
                    this.Name = "Normal";
                    break;

                case "Boss":
                    this.health = 400; this.speed = 5;
                    this.sizeHeight = 144; this.sizeWidth = 192;
                    this.isShooting = true; this.shootDelay = 700; this.shootSpeed = 10;
                    this.Image = Resources.Boss;
                    this.Name = "Boss";
                    break;
            }
            this.Size = new Size(sizeWidth, sizeHeight);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.BackColor = Color.Transparent;
            this.Location = new System.Drawing.Point(startPosX, startPosY);
            this.Tag = "Enemy";
            this.ShootInterval.Interval = shootDelay / 60;
            this.ShootInterval.Tick += Shoot;
            this.ShootInterval.Start();
        }

        public override void Shoot(object sender, EventArgs e)
        {
            if (isShooting)
            {
                this.ShootInterval.Interval = shootDelay;
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

        public int Decision
        {
            get { return decision; }
            set { decision = value; }
        }
    }
}