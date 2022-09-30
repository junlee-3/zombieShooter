namespace zombieShooter;

public partial class Window : Form
{
    private bool _goLeft, _goRight, _goUp, _goDown, _gameOver;
    private string _facing = "up";
    private int _playerHealth = 100;
    private const int Speed = 10;
    private int _ammunition = 10;
    private const int ZombieSpeed = 3;
    private int _score = 0;
    private readonly Random _randNum = new Random();

    private readonly List<PictureBox> _zombiesList = new List<PictureBox>(); 
    
    public Window()
    {
        InitializeComponent();
        RestartGame();
    }

    private void MainTimerEvent(object sender, EventArgs e)
    {
        if (_playerHealth > 1)
        {
            healthBar.Value = _playerHealth;
        }
        else
        {
            _gameOver = true;
            player.Image = Properties.Resources.dead;
            gameTimer.Stop();
        }

        txtAmmo.Text = "Ammunition: " + _ammunition;
        txtScore.Text = "Kills: " + _score;

        if (_goLeft && player.Left > 0)
        {
            player.Left -= Speed; 
        }
        
        if (_goRight && player.Left + player.Width < ClientSize.Width)
        {
            player.Left += Speed;
        }

        if (_goUp && player.Top > 0)
        {
            player.Top -= Speed;
        }

        if (_goDown && player.Top + player.Height < ClientSize.Height)
        {
            player.Top += Speed;
        }

        foreach (Control x in this.Controls)
        {
            if (x is PictureBox && (string)x.Tag == "ammo")
            {
                if (player.Bounds.IntersectsWith(x.Bounds))
                {
                    Controls.Remove(x);
                    ((PictureBox)x).Dispose();
                    _ammunition += 5;
                }
            }
            
             //Basic "AI" That tells zombie to go towards player
             if (x is PictureBox && (string)x.Tag == "zombie")
             {

                 if (player.Bounds.IntersectsWith(x.Bounds))
                 {
                     _playerHealth -= 1;
                 }
                 
                 if (x.Left > player.Left)
                 {
                     x.Left -= ZombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zleft;
                 }
                 
                 if (x.Left < player.Left)
                 {
                     x.Left += ZombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zright;
                 } 
                 
                 if (x.Top > player.Top)
                 {
                     x.Top -= ZombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zup;
                 } 
                 
                 if (x.Top < player.Top)
                 {
                     x.Top += ZombieSpeed;
                     ((PictureBox)x).Image = Properties.Resources.zdown;
                 }
             }

             foreach (Control j in this.Controls)
             {
                 if (j is PictureBox && (string)j.Tag == "bullet" && x is PictureBox && (string)x.Tag == "zombie")
                 {
                     if (x.Bounds.IntersectsWith(j.Bounds))
                     {
                         _score++;
                         
                         Controls.Remove(j);
                         ((PictureBox)j).Dispose();
                         Controls.Remove(x);
                         _zombiesList.Remove(((PictureBox)x));
                         MakeZombies();
                     }
                 }
             }
        }
        
    }

    private void KeyIsDown(object sender, KeyEventArgs e)
    {
        if (_gameOver)
        {
            return;
        }
        
        switch (e.KeyCode)  
        {
            case Keys.A:
                _goLeft = true;
                _facing = "left";
                player.Image = Properties.Resources.left;
                break;
            case Keys.D:
                _goRight = true;
                _facing = "right";
                player.Image = Properties.Resources.right;
                break;
            case Keys.W:
                _goUp = true;
                _facing = "up";
                player.Image = Properties.Resources.up;
                break;
            case Keys.S:
                _goDown = true;
                _facing = "down";    
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
                if (_ammunition > 0 && _gameOver == false)
                {
                    _ammunition--;
                    ShootBullet(_facing);
                    if (_ammunition < 1)
                    {
                        DropAmmoBox();
                    }
                }
                break;
            case Keys.Enter:
                if (_gameOver)
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
        zombie.Left = _randNum.Next(0, 900);
        zombie.Top = _randNum.Next(0, 800);
        zombie.SizeMode = PictureBoxSizeMode.AutoSize;
        _zombiesList.Add(zombie);
        Controls.Add(zombie);
        player.BringToFront();
    }

    private void DropAmmoBox()
    {
        PictureBox ammo = new PictureBox();
        ammo.Image = Properties.Resources.ammo_Image;
        ammo.SizeMode = PictureBoxSizeMode.AutoSize;    
        ammo.Left = _randNum.Next(10, this.ClientSize.Width - ammo.Width);
        ammo.Top = _randNum.Next(60, this.ClientSize.Height - ammo.Height);
        ammo.Tag = "ammo";
        Controls.Add(ammo);
        ammo.BringToFront();
        player.BringToFront();
    }

    private void DropMedKit()
    {
        PictureBox medKit = new PictureBox();
        medKit.SizeMode = PictureBoxSizeMode.AutoSize;
        medKit.Left = _randNum.Next(10, this.ClientSize.Width - medKit.Width);
        medKit.Top = _randNum.Next(60, this.ClientSize.Height - medKit.Height);
        medKit.Tag = "Medkit";
        Controls.Add(medKit);
        medKit.BringToFront();
        player.BringToFront();
    }

    private void RestartGame()
    {
        player.Image = Properties.Resources.up;

        foreach (PictureBox i in _zombiesList)
        {
            Controls.Remove(i);
        }
        
        _zombiesList.Clear();

        for (var i = 0; i < 3; i++)
        {
            MakeZombies();
        }

        _goUp = false;
        _goDown = false;
        _goLeft = false;
        _goRight = false;
        _gameOver = false;
        

        _playerHealth = 100;
        _score = 0;
        _ammunition = 10;
        
        gameTimer.Start();
    }
}