using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal abstract class Character : PictureBox
    {
        protected int health, speed;
        protected int sizeHeight, sizeWidth;
        protected int shootDelay, shootSpeed;
        protected bool isShooting;
        protected int x, y;
        protected Timer ShootInterval = new Timer();

        public abstract void Shoot(object sender, EventArgs e);

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
    }
}