using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal abstract class Environment : PictureBox
    {
        protected double WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
        protected double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
        protected int health, speed;
        protected int x, y;
        protected Timer timer = new Timer();
        protected double sizeHeight, sizeWidth;

        public abstract void EventTimer(object sender, EventArgs e);

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public int MoveSpeed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}