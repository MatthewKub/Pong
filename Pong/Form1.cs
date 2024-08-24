using System.Media;

namespace Pong
{
    public partial class Form1 : Form
    {
        public bool playMainTheme = true;

        public Form1()
        {
            InitializeComponent();
            if(playMainTheme == true)
            {
                PlayMusic();
            }

        }

        private void LoadHelp(object sender, EventArgs e)
        {
            var helpLoaded = new HelpWindow();
            helpLoaded.Show();
        }

        private void SinglePressed(object sender, EventArgs e)
        {
            var singleGame = new SinglePlayer();
            singleGame.Show();
            playMainTheme = false;
            PlayMusic();
        }

        private void DoublePressed(object sender, EventArgs e)
        {
            var doubleGame = new TwoPlayers();
            doubleGame.Show();
            playMainTheme = false;
            PlayMusic();
        }

        public void PlayMusic()
        {
            var musicPlayer = new SoundPlayer(@"C:\Users\Matt\Downloads\PongMainTheme.wav");
            if (playMainTheme == false)
            {
                musicPlayer.Stop();
            }
            else
            {
                musicPlayer.PlayLooping();
            } 
                
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void MutePressed(object sender, EventArgs e)
        {
            playMainTheme = false;
            PlayMusic();
        }
    }
}