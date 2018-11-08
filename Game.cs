using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
namespace Minesweeper
{
    public partial class Game : Form
    {
        private int width, height, mines;
        private GameTable table;
        private int seconds = 0;
        private bool newGame = false;

        public Game(int width = 6,int height = 6,int mines = 10)
        {
            InitializeComponent();
            this.width = width;
            this.height = height;
            this.mines = mines;
        }

        private void Game_Load(object sender, EventArgs e)
        {
            table = new GameTable(width, height, mines);
            resetTable();
            this.Size = new Size(table[height - 1, width - 1].getButton().Location.X + 2 * table[height - 1, width - 1].getButton().Width - 9,
                                 table[height - 1, width - 1].getButton().Location.Y + 3 * table[height - 1, width - 1].getButton().Height-11);
            this.btnReset.BringToFront();
            timer1.Start();
        }
        private void resetTable()
        {
            
            for (int i = 0; i < table.getHeight(); i++)
            {
                for (int j = 0; j < table.getWidth(); j++)
                {
                    Program.gameForm.Controls.Add(table[i, j].getButton());
                }
            }
            this.lblMinesLeft.Text = this.mines.ToString();
            this.lblTime.Text = "00:00";
            timer1.Stop();
            timer1 = new Timer();
            seconds = 0;
            timer1.Interval = 1000;
            timer1.Tick += new System.EventHandler(this.timer1_Tick); 
            timer1.Start();
        }
        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            timer1.Stop();
            if (newGame)
            {
                Program.mainMenu.Show();
            }
            else
                Application.Exit();
        }
        
        private void btnReset_Click(object sender, EventArgs e)
        {
            this.table.remakeTable();
            resetTable();
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string buf = Path.Combine(Application.StartupPath, "MinesweeperHelp.rtf");
            System.Diagnostics.Process.Start(buf);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!(seconds / 60 == 99 && seconds % 60 == 59))
                seconds += 1;
            lblTime.Text = String.Format("{0:00}:{1:00}", (int)(seconds/60),seconds%60);
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newGame = true;
            this.Close();
        }
    }
}
