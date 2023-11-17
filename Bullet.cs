using Marine_Adventures;
using System.Drawing;
using System.Windows.Forms;
using System;

public class Bullet : PictureBox
{
    private int bulletSpeed;
    private string shooter;

    public Bullet(Point location, string shooter)
    {
        Size = new Size(24, 8);
        Location = location;
        BackColor = Color.Transparent;
        SizeMode = PictureBoxSizeMode.StretchImage;
        Tag = "Bullet";
        Top += 10;
        this.shooter = shooter;

        var bulletTimer = new Timer();
        bulletTimer.Tick += BulletMove;

        if (shooter == "Player")
        {
            bulletSpeed = 10;
            bulletTimer.Interval = 900 / 60;
            Image = Resources.Player_Bullet;
        }
        else if (shooter == "Enemy")
        {
            bulletSpeed = 5;
            bulletTimer.Interval = 2000 / 60;
            Image = Resources.Bullet;
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
        Left += shooter == "Player" ? bulletSpeed : -bulletSpeed;

        if (Left > Screen.PrimaryScreen.Bounds.Width / 1.2 || Left < -20)
        {
            ((Timer)sender).Stop();
            ((Timer)sender).Dispose();
        }
    }
}
