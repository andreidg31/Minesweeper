using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
namespace Minesweeper
{
    class GameTable
    {
        private int[,] neightbours = new int[8,2]{{1,0},{1,1},{1,-1},{0,1},{0,-1},{-1,0},{-1,1},{-1,-1}};
        
        private int width, height;
        private int noMines;
        private Cell[,] cells = null;

        public bool isWon = false;
        public bool ongoing = false;
        public int noFlags = 0;
        public int noCorrectFlags = 0;

        public GameTable(int width = 0, int height = 0, int mines = 0)
        {
            this.width = width;
            this.height = height;
            this.noMines = mines;
            this.cells = new Cell[height, width];
            this.ongoing = true;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    cells[i, j] = new Cell(this,i, j, false);
                }
            }
            placeMines();
        }

        private void placeMines()
        {
            int[] buff = new int[width * height];
            Random rnd = new Random();
            for (int i = 0; i < width * height; i++)
            {
                buff[i] = i;
            }
            buff = buff.OrderBy(x => rnd.Next()).ToArray();

            for (int i = 0; i < noMines; i++)
            {
                int x = buff[i] % width, y = buff[i] / width;
                cells[y, x].hasMine = true;
                for (int j = 0; j < 8; j++)
                {
                    int px = x+neightbours[j,0],
                        py = y+neightbours[j,1];

                    if (px >= 0 && py >= 0 && px < width && py < height)
                    {
                        cells[py, px].noMinesNearby++;
                    }
                }
            }
        }

        public void gameOver()
        {
            this.ongoing = false;
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if(cells[i,j].hasMine) {
                        Button btn = cells[i, j].getButton();
                        btn.Text = "";
                        btn.FlatStyle = FlatStyle.Flat;
                        btn.FlatAppearance.BorderSize = 1;
                        btn.FlatAppearance.BorderColor = SystemColors.Control;
                        btn.BackgroundImage = global::Minesweeper.Properties.Resources.mine;
                        btn.BackgroundImageLayout = ImageLayout.Stretch;
                        continue;
                    }

                    if (cells[i, j].hasFlag && !cells[i, j].hasMine)
                    {
                        cells[i,j].getButton().Image = global::Minesweeper.Properties.Resources.flagCrossed;
                        continue;
                    }

                    if (!cells[i, j].isRevealed&&!cells[i,j].hasFlag) {
                        cells[i, j].revealCell();
                        continue;
                    }
                }
            }
            if (isWon)
                MessageBox.Show("CONGRATULATIONS!\n      YOU WON!");
            else
                MessageBox.Show("GAME OVER");
        }

        public void revealCells(int x, int y)
        {
            cells[y, x].revealCell();
            cells[y, x].isRevealed = true;
            if (cells[y, x].noMinesNearby == 0)
            {
                for (int ct = 0; ct < 8; ct++)
                {
                    int px = x + neightbours[ct, 0],
                        py = y + neightbours[ct, 1];
                    if (px >= 0 && py >= 0 && px < width && py < height
                        && !(cells[py,px].isRevealed||cells[py,px].hasFlag||cells[py,px].hasMine))
                    {
                        revealCells(px, py);
                    }
                }
            }
        }


        public int getHeight()
        {
            return this.height;
        }

        public int getWidth()
        {
            return this.width;
        }

        public int getMines()
        {
            return this.noMines;
        }

        public Cell this[int i, int j]
        {
            get { return cells[i, j]; }
            set { cells[i, j] = value; }
        }
    }
}
