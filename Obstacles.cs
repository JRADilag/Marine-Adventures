using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Marine_Adventures
{
    internal class Obstacles : Environment
    {
        private Random random = new Random();

        public Obstacles()
        {
            this.x = random.Next((int)(WINDOW_WIDTH * 0.6), (int)WINDOW_WIDTH - 100);
            this.y = random.Next(0, (int)WINDOW_HEIGHT - 200);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Location = new System.Drawing.Point(x, y);
            this.BackColor = Color.Transparent;
            this.sizeHeight = 23;
            this.sizeWidth = 21;
            this.Size = new Size((int)sizeWidth, (int)sizeHeight);
            this.Tag = "Mine";

            this.MoveSpeed = 10;

            this.Image = Resources.Mine;

            timer.Interval = 200;
            timer.Tick += EventTimer;
            timer.Start();
        }

        public override void EventTimer(object sender, EventArgs e)
        {
            this.Left -= this.MoveSpeed;

            if (this.Left < -20)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }
    }
}