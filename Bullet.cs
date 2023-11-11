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

        public Bullet(Point location)
        {
            //starting location
            this.Size = new Size(sizeWidth, sizeHeight);
            this.Location = location;
            this.Image = Resources.Bullet;
            this.BackColor = Color.Transparent;
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Tag = "Bullet";
            this.Top += 10;

            Timer BulletTimer = new Timer();
            BulletTimer.Interval = 1000 / 60;
            BulletTimer.Tick += BulletMove;
            BulletTimer.Start();
        }

        public int BulletSpeed
        {
            get { return bulletSpeed; }
            set { bulletSpeed = value; }
        }

        public void BulletMove(object sender, EventArgs e)
        {
            this.Left += bulletSpeed;
        }
    }
}