using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Bullet : PictureBox
    {
        private int sizeWidth = 24;
        private int sizeHeight = 8;
        private int bulletSpeed = 10;
        private string shooter;
        private double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;

        private Timer BulletTimer = new Timer();

        public Bullet(Point location, string shooter)
        {
            //starting location
            this.Size = new Size(sizeWidth, sizeHeight);
            this.Location = location;
            this.BackColor = Color.Transparent;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Tag = "Bullet";
            this.Top += 10;

            if (shooter == "Player")
            {
                this.Image = Resources.Player_Bullet;
            }
            else if (shooter == "Enemy")
            {
                this.Image = Resources.Bullet;
            }

            BulletTimer.Interval = 1000 / 60;
            BulletTimer.Tick += BulletMove;
            BulletTimer.Start();
        }

        public string Shooter
        {
            get { return shooter; }
            set { shooter = value; }
        }

        public int BulletSpeed
        {
            get { return bulletSpeed; }
            set { bulletSpeed = value; }
        }

        public void BulletMove(object sender, EventArgs e)
        {
            switch (shooter)
            {
                case "Player":
                    this.Left += bulletSpeed;
                    break;

                case "Enemy":
                    this.Left -= bulletSpeed;
                    break;
            }
            if (this.Left > (int)WINDOW_WIDTH || this.Left < -20)
            {
                BulletTimer.Stop();
                BulletTimer.Dispose();
                BulletTimer = null;
            }
        }
    }
}