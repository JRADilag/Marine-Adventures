using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class GameWindow : Form
    {
        private double WINDOW_HEIGHT, WINDOW_WIDTH;

        public GameWindow()
        {
            WINDOW_HEIGHT = Screen.PrimaryScreen.Bounds.Height / 1.2;
            WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
            this.Size = new System.Drawing.Size((int)WINDOW_WIDTH, (int)WINDOW_HEIGHT);
        }
    }
}