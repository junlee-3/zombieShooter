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
        }

        txtAmmo.Text = "Ammunition: " + ammunition;
        txtScore.Text = "Kills: " + score;

        if (_goLeft == true && player.Left > 0)
        {
            player.Left -= speed; 
        }
        
        if (_goRight == true && player.Left + player.Width < this.ClientSize.Width)
        {
            player.Left += speed;
        }

        if (_goUp == true && player.Top > 0)
        {
            player.Top -= speed;
        }

        if (_goDown == true && player.Top + player.Height < this.ClientSize.Height)
        {
            player.Top += speed;
        }
        
    }

    private void KeyIsDown(object sender, KeyEventArgs e)
    {
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
                ShootBullet(facing);
                break;
        }
    }

    private void ShootBullet(string direction)
    {
        
    }

    private void MakeZombies()
    {
        
    }

    private void RestartGame()
    {
        
    }
}