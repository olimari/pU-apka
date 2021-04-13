using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace pUŁapka
{
    public partial class Form1 : Form
    {
        bool leftSide, rightSide, jump;
        int speedPlayer = 10;
        int speedJump = 10;
        int force = 8;
        int score = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            //ruch na boki
            if (e.KeyCode == Keys.Left)
            {
                leftSide = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                rightSide = true;
            }
            if (e.KeyCode == Keys.Space && jump == false)
            {
                jump = true;
            }
        }

        private void MainTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Wynik: " + score;
            player.Top += speedJump;

            if (leftSide == true && player.Left > 60)
            {
                player.Left -= speedPlayer;
            }
            if (rightSide == true && player.Left + (player.Width + 60) < this.ClientSize.Width)
            {
                player.Left += speedPlayer;
            }


            if (jump == true)
            {
                speedJump = -12;
                force -= 1;
               /* player.Top -= force;
                force -= 1;*/
            }
            else
            {
                speedJump = 12;
            }

            if (jump == true && force < 0)
            {
                jump = false;
            }
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (player.Bounds.IntersectsWith(x.Bounds) && jump == false)
                    {
                        force = 8;
                        player.Top = x.Top - player.Height;
                        speedJump = 0;
                    }
                    x.BringToFront();
                }
            }

            if (player.Top + player.Height > this.ClientSize.Height)
            {
                GameTimer.Stop();
                MessageBox.Show("Przegrałeś!" + Environment.NewLine + "Kliknij OK, aby zagrać ponownie!");
                RestartGame();
            }


        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                leftSide = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                rightSide = false;
            }
            if (jump == true)
            {
                jump = false;
            }
        }
        private void RestartGame()
        {
            Form1 window = new Form1();
            window.Show();
            this.Hide();

        }

        private void CloseGame(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
   }
}
