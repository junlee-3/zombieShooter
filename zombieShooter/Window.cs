namespace zombieShooter;

public partial class Window : Form
{
    private bool _goLeft, _goRight, _goUp, _goDown, gameOver;
    private string facing = "up";
    private int playerHealth = 100;
    private int speed = 10;
    private int ammunition = 10;
    private int zombieSpeed = 3;
    private int score = 0;
    private Random randNum = new Random();

    private List<PictureBox> zombiesList = new List<PictureBox>(); 
    
    public Window()
    {
        InitializeComponent();
        RestartGame();
    }

    private void MainTimerEvent(object sender, EventArgs e)
    {
        if (playerHealth > 1)
        {
            healthBar.Value = playerHealth;
        }
        else
        {
            gameOver = true;
            player.Image = Properties.Resources.dead;
            gameTimer.Stop();
        }

        txtAmmo.Text = "Ammunition: " + ammunition;
        txtScore.Text = "Kills: " + score;

        if (_goLeft && player.Left > 0)
        {
            player.Left -= speed; 
        }
        
        if (_goRight && player.Left + player.Width < ClientSize.Width)
        {
            player.Left += speed;
        }

        if (_goUp && player.Top > 0)
        {
            player.Top -= speed;
        }

        if (_goDown && player.Top + player.Height < ClientSize.Height)
        {
            player.Top += speed;
        }

        foreach (Control x in this.Controls)
        {
            if (x is PictureBox && (string)x.Tag == "ammo")
            {
                if (player.Bounds.IntersectsWith(x.Bounds))
                {
                    Controls.Remove(x);
                    ((PictureBox)x).Dispose();
                    ammunition += 5;
                }
            }
            
             //Basic "AI" That tells zombie to go towards player
             if (x is PictureBox && (string)x.Tag == "zombie")
             {

                 if (player.Bounds.IntersectsWith(x.Bounds))
                 {
                     playerHealth -= 1;
                 }
                 
                 if (x.Left > player.Left)
                 {
                     x.Left -= zombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zleft;
                 }
                 
                 if (x.Left < player.Left)
                 {
                     x.Left += zombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zright;
                 } 
                 
                 if (x.Top > player.Top)
                 {
                     x.Top -= zombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zup;
                 } 
                 
                 if (x.Top < player.Top)
                 {
                     x.Top += zombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zdown;
                 }
             }

             foreach (Control j in this.Controls)
             {
                 if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                 {
                     if (x.Bounds.IntersectsWith(j.Bounds))
                     {
                         score++;
                         
                         Controls.Remove(j);
                         ((PictureBox)j).Dispose();
                         Controls.Remove(x);
                         zombiesList.Remove(((PictureBox)x));
                         MakeZombies();
                     }
                 }
             }
        }
        
    }

    private void KeyIsDown(object sender, KeyEventArgs e)
    {
        if (gameOver)
        {
            return;
        }
        
        switch (e.KeyCode)  
        {
            case Keys.A:
                _goLeft = true;
                facing = "left";
                player.Image = Properties.Resources.left;
                break;
            case Keys.D:
                _goRight = true;
                facing = "right";
                player.Image = Properties.Resources.right;
                break;
            case Keys.W:
                _goUp = true;
                facing = "up";
                player.Image = Properties.Resources.up;
                break;
            case Keys.S:
                _goDown = true;
                facing = "down";    
                player.Image = Properties.Resources.down;
                break;
        }
    }

    private void KeyIsUp(object sender, KeyEventArgs e)
    {
        switch (e.KeyCode)
        {
            case Keys.A:
                _goLeft = false;
                break;
            case Keys.D:
                _goRight = false;
                break;
            case Keys.W:
                _goUp = false;
                break;
            case Keys.S:
                _goDown = false;
                break;
            case Keys.Space:
                if (ammunition > 0 && gameOver == false)
                {
                    ammunition--;
                    ShootBullet(facing);
                    if (ammunition < 1)
                    {
                        DropAmmoBox();
                    }
                }
                break;
            case Keys.Enter:
                if (gameOver)
                {
                    RestartGame();
                }

                break;
        }
        
    }

    private void ShootBullet(string direction)
    {
        Bullet shootBullet = new Bullet
        {
            direction = direction,
            bulletLeft = player.Left + (player.Width / 2),
            bulletTop = player.Top + (player.Height / 2)
        };
        shootBullet.MakeBullet(this);
    }

    private void MakeZombies()
    {
        PictureBox zombie = new PictureBox();
        zombie.Tag = "zombie";
        zombie.Image = Properties.Resources.zdown;
        zombie.Left = randNum.Next(0, 900);
        zombie.Top = randNum.Next(0, 800);
        zombie.SizeMode = PictureBoxSizeMode.AutoSize;
        zombiesList.Add(zombie);
        Controls.Add(zombie);
        player.BringToFront();
    }

    private void DropAmmoBox()
    {
        PictureBox ammo = new PictureBox();
        ammo.Image = Properties.Resources.ammo_Image;
        ammo.SizeMode = PictureBoxSizeMode.AutoSize;    
        ammo.Left = randNum.Next(10, this.ClientSize.Width - ammo.Width);
        ammo.Top = randNum.Next(60, this.ClientSize.Height - ammo.Height);
        ammo.Tag = "ammo";
        Controls.Add(ammo);
        ammo.BringToFront();
        player.BringToFront();
    }

    private void DropMedKit()
    {
        PictureBox MedKit = new PictureBox();
    }

    private void RestartGame()
    {
        player.Image = Properties.Resources.up;

        foreach (PictureBox i in zombiesList)
        {
            Controls.Remove(i);
        }
        
        zombiesList.Clear();

        for (var i = 0; i < 3; i++)
        {
            MakeZombies();
        }

        _goUp = false;
        _goDown = false;
        _goLeft = false;
        _goRight = false;
        gameOver = false;

        playerHealth = 100;
        score = 0;
        ammunition = 10;
        
        gameTimer.Start();
    }
}