﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Marine_Adventures
{
    internal class Player : PictureBox
    {
        private string name;
        private int life = 3;
        private int speed = 30;
        private double shootDelay;
        private int score = 0;
        private int sizeHeight = 50, sizeWidth = 50;
        private int playerX = 100, playerY = 100;

        public Player()
        {
            this.Size = new Size(sizeWidth, sizeHeight);
            this.SizeMode = PictureBoxSizeMode.StretchImage;
            this.Image = Resources.player;
            this.Location = new System.Drawing.Point(playerX, playerY);
        }

        public string PlayerName

        {
            get { return name; }
            set { name = value; }
        }

        public int Life
        {
            get { return life; }
            set { life = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public double ShootDelay
        {
            get { return shootDelay; }
            set { shootDelay = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }
    }
}