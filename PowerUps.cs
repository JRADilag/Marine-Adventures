using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Marine_Adventures
{
    internal class PowerUps : Environment
    {
        private Random random = new Random();
        private string[] types = new string[3];
        private string type;

        public PowerUps()
        {
            this.x = random.Next((int)(WINDOW_WIDTH * 0.4), (int)WINDOW_WIDTH - 100);
            this.y = random.Next(0, (int)WINDOW_HEIGHT - 200);
            this.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Location = new System.Drawing.Point(x, y);
            this.BackColor = Color.Transparent;
            this.sizeHeight = 33 * 0.8;
            this.sizeWidth = 33 * 0.8;
            this.Size = new Size((int)sizeWidth, (int)sizeHeight);
            this.Tag = "PowerUp";

            this.MoveSpeed = 15;

            types[0] = "Health";
            types[1] = "Speed";
            types[2] = "Attack";

            this.type = types[random.Next(3)];

            switch (type)
            {
                case "Health":
                    this.Name = "HealthPowerUp";
                    this.Image = Resources.PowerUp_Health;
                    break;

                case "Speed":
                    this.Name = "SpeedPowerUp";
                    this.Image = Resources.PowerUp_Speed;
                    break;

                case "Attack":
                    this.Name = "AttackPowerUp";
                    this.Image = Resources.PowerUp_Attack;
                    break;
            }

            timer.Interval = 200;
            timer.Tick += EventTimer;
            timer.Start();
        }

        public string PowerUpType
        {
            get { return type; }
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