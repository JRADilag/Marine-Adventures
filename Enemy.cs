using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Enemy : PictureBox
    {
        private int health;
        private int speed;
        private int sizeHeight = 60, sizeWidth = 60;
        private double shootDelay;
        private int startPosX, startPosY;
        private Random random = new Random();
        private double WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
        private double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;

        public Enemy()
        {
            startPosX = random.Next((int)(WINDOW_WIDTH * 0.4), (int)WINDOW_WIDTH);
            startPosY = random.Next(50, (int)(WINDOW_HEIGHT * 0.5));
            this.Size = new Size(sizeWidth, sizeHeight);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Resources.Enemy;
            this.Location = new System.Drawing.Point(startPosX, startPosY);
            this.Name = "Enemy";
        }
    }
}