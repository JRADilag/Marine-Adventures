using Marine_Adventures;
using System.Drawing;
using System.Windows.Forms;
using System;

public class Bullet : PictureBox
{
    private int sizeWidth = 24;
    private int sizeHeight = 8;
    private int bulletSpeed;
    private string shooter;
    private double WINDOW_WIDTH = Screen.PrimaryScreen.Bounds.Width / 1.2;
    private Timer bulletTimer;

    public Bullet(Point location, string shooter)
    {
        this.Size = new Size(sizeWidth, sizeHeight);
        this.Location = location;
        this.BackColor = Color.Transparent;
        this.SizeMode = PictureBoxSizeMode.StretchImage;
        this.Tag = "Bullet";
        this.Top += 10;
        this.shooter = shooter;

        bulletTimer = new Timer();
        bulletTimer.Tick += BulletMove;

        //player and enemy shooting interval separated for easier adjustment
        if (shooter == "Player")
        {
            bulletSpeed = 10; 
            bulletTimer.Interval = 900 / 60; 
            this.Image = Resources.Player_Bullet;
        }
        else if (shooter == "Enemy")
        {
            bulletSpeed = 5;
            bulletTimer.Interval = 2000 / 60; 
            this.Image = Resources.Bullet;
        }

        bulletTimer.Start();
    }

    public string Shooter
    {
        get { return shooter; }
        set { shooter = value; }
    }

    public int BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }

    private void BulletMove(object sender, EventArgs e)
    {
        switch (shooter)
        {
            case "Player":
                this.Left += bulletSpeed;
                break;

            case "Enemy":
                this.Left -= bulletSpeed;
                break;
        }

        if (this.Left > (int)WINDOW_WIDTH || this.Left < -20)
        {
            bulletTimer.Stop();
            bulletTimer.Dispose();
            bulletTimer = null;
        }
    }
}
