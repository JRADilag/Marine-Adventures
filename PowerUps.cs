using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Marine_Adventures
{
    internal class PowerUps : Environment
    {
        private static readonly Random random = new Random();
        private readonly string[] types = { "Health", "Speed", "Attack" };
        private string type;

        public PowerUps()
        {
            InitializePowerUp();
        }

        private void InitializePowerUp()
        {
            x = random.Next((int)(WINDOW_WIDTH * 0.4), (int)WINDOW_WIDTH - 100);
            y = random.Next(0, (int)WINDOW_HEIGHT - 200);
            InitializeAppearance();

            type = types[random.Next(types.Length)];
            SetPowerUpImage();

            timer.Interval = 200;
            timer.Tick += EventTimer;
            timer.Start();
        }

        private void InitializeAppearance()
        {
            SizeMode = PictureBoxSizeMode.StretchImage;
            Location = new Point(x, y);
            BackColor = Color.Transparent;
            sizeHeight = 33 * 0.8;
            sizeWidth = 33 * 0.8;
            Size = new Size((int)sizeWidth, (int)sizeHeight);
            Tag = "PowerUp";
            MoveSpeed = 15;
        }

        private void SetPowerUpImage()
        {
            switch (type)
            {
                case "Health":
                    Name = "HealthPowerUp";
                    Image = Resources.PowerUp_Health;
                    break;

                case "Speed":
                    Name = "SpeedPowerUp";
                    Image = Resources.PowerUp_Speed;
                    break;

                case "Attack":
                    Name = "AttackPowerUp";
                    Image = Resources.PowerUp_Attack;
                    break;
            }
        }

        public string PowerUpType => type;

        public override void EventTimer(object sender, EventArgs e)
        {
            Left -= MoveSpeed;

            if (Left < -20)
            {
                timer.Stop();
                timer.Dispose();
                timer = null;
            }
        }
    }
}
