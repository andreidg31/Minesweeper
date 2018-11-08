using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Minesweeper
{
    class Cell
    {
        public bool hasMine;
        public bool hasFlag;
        public bool isRevealed = false;
        private int posX, posY;
        private GameTable table;
        private Button btn;
        public int noMinesNearby = 0;
        public int noFlagsNearby = 0;

        public Cell(GameTable table,int posX = 0, int posY = 0, bool hasMine = false)
        {
            this.table   = table;
            this.hasMine = hasMine;
            this.posX    = posX;
            this.posY    = posY;
            this.btn = createButton();
        }

        public void destroy()
        {
            //MessageBox.Show(String.Format("Removing control at {0} and {1}",this.posX,this.posY));
            Program.gameForm.Controls.Remove(this.btn);
            this.btn.Parent = null;
            this.table = null;
        }

        private Button createButton()
        {
            Button button = new Button();
            button.Size = new System.Drawing.Size(25, 25);
            button.Location = new System.Drawing.Point(this.posX*25+1,this.posY*25+1 + GameTable.submenuHeight);
            button.FlatStyle = FlatStyle.Popup;
            button.Visible = true;
            button.TabStop = false;
            button.Tag = "cell";
            button.Font = new System.Drawing.Font("Franklin Gothic Medium", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            button.TextAlign = ContentAlignment.TopCenter;
            button.MouseDown += new System.Windows.Forms.MouseEventHandler(onBtnClick);
            return button;
        }

        private void onBtnClick(object sender, MouseEventArgs e)
        {

            if (this.isRevealed && e.Button == MouseButtons.Left && Control.ModifierKeys == Keys.Shift)
            {
                //MessageBox.Show(this.noFlagsNearby.ToString() + " " + this.noMinesNearby.ToString());
               if (this.noFlagsNearby == this.noMinesNearby)
                    this.table.revealNeightbours(this.posX, this.posY);
            }

            if (this.isRevealed || !this.table.ongoing)
                return;

            switch (e.Button)
            {
                case MouseButtons.Left:
                {
                    if (hasMine&&!this.hasFlag)
                    {
                        this.table.gameOver();
                    }
                    else
                    {
                        if (!hasFlag)
                            this.table.revealCells(this.posX, this.posY);
                    }
                    break;
                }
                case MouseButtons.Right:
                {
                    if (this.hasFlag){
                        this.btn.Image = null;
                        this.btn.Text = "";
                        this.table.noFlags--;
                        if (this.hasMine)
                            this.table.noCorrectFlags--;
                        this.table.addRemoveFlags(this.posX, this.posY, true);
                    }
                    else{
                        this.btn.Image = global::Minesweeper.Properties.Resources.flag;
                        this.table.noFlags++;
                        if (this.hasMine)
                            this.table.noCorrectFlags++;

                        if (this.table.noCorrectFlags == this.table.getMines() &&
                            this.table.noFlags == this.table.noCorrectFlags)
                        {
                            this.table.isWon = true;
                            this.table.gameOver();
                        }
                        this.table.addRemoveFlags(this.posX, this.posY,false);
                    }
                    this.hasFlag = !this.hasFlag;
                    Program.gameForm.lblMinesLeft.Text = (this.table.getMines() - this.table.noFlags).ToString();
                    break;
                }
            }
        }

        public void revealCell()
        {
            if (this.isRevealed)
                return;

            if (this.hasMine)
                this.table.gameOver();

            this.btn.FlatStyle = FlatStyle.Flat;
            this.btn.FlatAppearance.BorderSize = 1;
            this.btn.FlatAppearance.BorderColor = SystemColors.Control; 
            
            this.btn.BackColor = System.Drawing.Color.White;
            this.btn.Text = this.noMinesNearby.ToString();
            if (this.noMinesNearby == 0)
            {
                this.btn.Text = "";
                this.btn.Enabled = false;
            }
            else
                pickColor(noMinesNearby);
            //this.btn.Enabled = false;
        }
        private void pickColor(int num)
        {
            switch (num)
            {
                case 1:
                {
                    this.btn.ForeColor = System.Drawing.Color.Blue;
                    break;
                }
                case 2:
                {
                    this.btn.ForeColor = System.Drawing.Color.Green;
                    break;
                }
                case 3:
                {
                    this.btn.ForeColor = System.Drawing.Color.Red;
                    break;
                }
                case 4:
                {
                    this.btn.ForeColor = System.Drawing.Color.DarkBlue;
                    break;
                }
                case 5:
                {
                    this.btn.ForeColor = System.Drawing.Color.Maroon;
                    break;
                }
                case 6:
                {
                    this.btn.ForeColor = System.Drawing.Color.Turquoise;
                    break;
                }
                case 7:
                {
                    this.btn.ForeColor = System.Drawing.Color.Black;
                    break;
                }
                case 8:
                {
                    this.btn.ForeColor = System.Drawing.Color.Gray;
                    break;
                }
            }
        }
        public Button getButton()
        {
            return this.btn;
        }

        public int getPosX()
        {
            return this.posX;
        }

        public void setPosX(int posX)
        {
            this.posX = posX;
        }

        public int getPosY()
        {
            return this.posY;
        }

        public void setPosY(int posY)
        {
            this.posY = posY;
        }
    }
}
