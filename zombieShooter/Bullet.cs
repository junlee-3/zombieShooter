using System.Diagnostics.Tracing;
using System.Drawing;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace zombieShooter;

public class Bullet
{
    public string direction;
    public int bulletLeft;
    public int bulletTop;

    private int speed = 20;
    private PictureBox bullet = new PictureBox();
    private Timer bulletTimer = new Timer();

    public void MakeBullet(Form form)
    {
        bullet.BackColor = Color.White;
        bullet.Size = new Size(5, 5);
        bullet.Tag = "bullet";
        bullet.Left = bulletLeft;
        bullet.Top = bulletTop;
        bullet.BringToFront();

        form.Controls.Add(bullet);

        bulletTimer.Interval = speed;
        bulletTimer.Tick += new EventHandler(BulletTimerEvent);
        bulletTimer.Start();
    }

    private void BulletTimerEvent(object sender, EventArgs e)
    {
        switch (direction)
        {
            case "left":
                bullet.Left -= speed;
                break;
            case "right":
                bullet.Left += speed;
                break;
            case "up":
                bullet.Top -= speed;
                break;
            case "down":
                bullet.Top += speed;
                break;
        }

        if (bullet.Left < 10 || bullet.Left > 860 || bullet.Top < 10 || bullet.Top > 600)
        {
            bulletTimer.Stop();
            bulletTimer.Dispose();
            bullet.Dispose();
            bulletTimer = null;
            bullet = null;
        }
        
        //30:47
    }
}