using System;
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
        public Game(int width = 6,int height = 6,int mines = 10)
        {
            InitializeComponent();
            table = new GameTable(width, height, mines);
            this.width = width;
            this.height = height;
            this.mines = mines;
        }

        private void Game_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < table.getHeight(); i++)
            {
                for (int j = 0; j < table.getWidth(); j++)
                {
                    Program.gameForm.Controls.Add(table[i, j].getButton());
                }
            }
            this.Size = new Size(table[height - 1, width - 1].getButton().Location.X + 2 * table[height - 1, width - 1].getButton().Width - 9,
                                 table[height - 1, width - 1].getButton().Location.Y + 3 * table[height - 1, width - 1].getButton().Height-11);
        }
        
        private void Game_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();

        }
    }
}
