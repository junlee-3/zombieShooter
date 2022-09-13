namespace zombieShooter;

public partial class Window : Form
{
    private bool _goLeft, _goRight, _goUp, _goDown, gameOver;
    private string facing = "up";
    private int playerHealth = 100;
    private int speed = 10;
    private int ammunition = 10;
    private int zombieSpeed = 3;
    private Random randNum = new Random();

    private List<PictureBox> zombiesList = new List<PictureBox>(); 
    
    public Window()
    {
        InitializeComponent();
    }

    private void MainTimerEvent(object sender, EventArgs e)
    {

    }

    private void KeyIsDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.A)
        {
            _goLeft = true;
            facing = "left";
            player.Image = Properties.Resources.left;
        }
        
        if (e.KeyCode == Keys.D)
        {
            _goRight = true;
            facing = "right";
            player.Image = Properties.Resources.right;
        }
        
        if (e.KeyCode == Keys.W)
        {
            _goUp = true;
            facing = "up";
            player.Image = Properties.Resources.up;
        }

        if (e.KeyCode == Keys.S)
        {
            _goDown = true;
            facing = "down";
            player.Image = Properties.Resources.down;
        }

    }

    private void KeyIsUp(object sender, KeyEventArgs e)
    {

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