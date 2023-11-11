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
        private int sizeHeight = 32, sizeWidth = 30;
        private double shootDelay;

        //private Random random = new Random();
        //private int startPosX, startPosY;
        private double WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;

        private double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;

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
        }
    }
}