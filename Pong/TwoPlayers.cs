using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Media;

namespace Pong
{
    public partial class TwoPlayers : Form
    {
        int velX = 16; 
        int velY = 16;
        int vel = 16; //Velocity of racket 1
        int velX2 = 12;
        int velY2 = 12;
        int vel2 = 12; // Velocity of racket 2 
        Random random = new Random();
        bool goDown, goUp, negative;
        bool goDown2, goUp2, negative2;
        bool ScoreSound, CollisionSound;
        bool gameOver = false;
        int player2_Change = 200;
        int player1Score = 0;
        int player2Score = 0;
        int playerSpeed = 24;
        int player2Speed = 24;
        int x = 16;
        int y = 16;

        public TwoPlayers()
        {
            InitializeComponent();
        }

        private void GameTimerEvent(object sender, EventArgs e)
        {

            PongBall.Top -= velY;
            PongBall.Left-= velX;

            //If the ball hits the top or bottom of the window
            if(PongBall.Top < 0 || PongBall.Bottom > this.ClientSize.Height)
            {
                velY = -velY;
            }

            //If player 2 hits the ball behind player1's racket
            if(PongBall.Left < Player1.Right-135)
            {
                PongBall.Left = this.ClientSize.Width/2;
                player2Score++;
                ScoreSound = true;
                PlayMusic();
                velX = 16;
                velX = -velX;
                x = 16;
                velY = 16;
                y = 16;
                label2.Text = player2Score.ToString();

            }

            //If player 1 hits the ball behind player2's racket 
            if(PongBall.Right > this.ClientSize.Width)
            {
                PongBall.Left = this.ClientSize.Width / 2;
                player1Score++;
                ScoreSound = true;
                PlayMusic();
                velX = 16;
                x = 16;
                velY = 16;
                y = 16;
                label1.Text = player1Score.ToString();
            }

            if(Player2.Top <=1)
            {
                Player2.Top = 0;
            }

            else if(Player2.Bottom >= this.ClientSize.Height)
            {
                Player2.Top = this.ClientSize.Height - Player2.Height;
            }

            if(goUp2 == true)
            {
                Player2.Top -= vel2;
            }

            if(goDown2 == true)
            {
                Player2.Top += vel2;
            }

            //if(PongBall.Top < Player2.Top + (Player2.Height/2) && PongBall.Left > 300)
            //{
            //    Player2.Top -= vel;
            //}

            //if(PongBall.Top > Player2.Top + (Player2.Height/2) && PongBall.Left > 300)
            //{
            //    Player2.Top += vel;
            //}

           //player2_Change -= 1;

            //if (player2_Change < 0)
            //{
            //    vel = 24;
            //    player2_Change = 200;
            //}

            // Controls player1 up movement
            if(goDown && Player1.Top + Player1.Height < this.ClientSize.Height)
            {
                Player1.Top += playerSpeed;
            }

            // Controls player1 down moevement
            if(goUp && Player1.Top > 0)
            {
                Player1.Top -= playerSpeed;
            }

            // Controls player2 up movement
            if(goDown2 && Player2.Top + Player2.Height < this.ClientSize.Height)
            {
                Player2.Top += player2Speed;
            }

            // Controls player2 down movement
            if(goUp2 && Player2.Top > 0)
            {
                Player2.Top -= player2Speed;
            }

            CheckCollision(PongBall, Player1, Player1.Right + 5);
            CheckCollision(PongBall, Player2, Player2.Right - 285);

            //Game ending
            if(player1Score >= 10)
            { 
                GameEnd("Game Over, Player 1 Wins!");
            }
            else if(player2Score >= 10)
            {
                GameEnd("Game Over, Player 2 Wins!");
            }

        }

        private void KeyPressedDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = true;
            }

            if (e.KeyCode == Keys.Up)
            {
                goUp = true;
            }

            if(e.KeyCode == Keys.S)
            {
                goDown2 = true;
            }

            if (e.KeyCode == Keys.W)
            {
                goUp2 = true;
            }

        }

        private void KeyPressedUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                goDown = false;
            }

            if(e.KeyCode == Keys.Up)
            {
                goUp = false;
            }

            if (e.KeyCode == Keys.S)
            {
                goDown2 = false;
            }

            if (e.KeyCode == Keys.W)
            {
                goUp2 = false;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CheckCollision(PictureBox Racket, PictureBox Ball, int offset)
        {   
            if (Racket.Bounds.IntersectsWith(Ball.Bounds))
            {
                Racket.Left = offset;

                // Player 1 

                //X axis
                if(velX < 0)
                {
                    velX = x;
                }
                else
                {
                    velX = -x;
                }

                //Y Axis 
                if (velY < 0)
                {
                    velY = y;
                }
                else
                {
                    velY = -y;
                }             

                PlayMusic2();

                //if (velX < 0)
                //{
                //    velX = x;
                //}

                //else
                //{
                //    velX = -x;
                //}

                x = x + 4;
                y = y + 4;
            }
        }

        private void GameEnd(string message)
        {
            gameOver = true;
            GameTimer.Stop();
            MessageBox.Show(message, "Player Says: ");
            reset();
        }

        private void reset()
        {
            player1Score = 0;
            player2Score = 0;
            velX = 15;
            velY = 15;
            player2_Change = 200;
            GameTimer.Start();
            ScoreSound = false;
            CollisionSound = false;
            PongBall.Top = this.ClientSize.Height / 2;
            Player1.Top = 0;
            Player2.Top = 0;
            label1.Text = player1Score.ToString();
            label2.Text = player2Score.ToString();
        }

        public void PlayMusic()
        {
            var ScoreSoundEffect = new SoundPlayer(@"C:\Users\Matt\Downloads\ScoreSound.wav");
            ScoreSoundEffect.Play();
            ScoreSound = false;
        }
        public void PlayMusic2()
        {
            var RacketSoundEffect = new SoundPlayer(@"C:\Users\Matt\Downloads\ReflectSound.wav");
            RacketSoundEffect.Play();
            CollisionSound = false;
        }

    }
}
