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
    public partial class Menu : Form
    {
        int width, height, mines;

        public Menu()
        {
            InitializeComponent();
        }

        private void Menu_Load(object sender, EventArgs e)
        {
            width = 10;
            height = 10;
            mines = 20;
            txtHeight.Text = height.ToString();
            txtWidth.Text  = width.ToString();
            txtMines.Text  = mines.ToString();
            //MessageBox.Show(Application.StartupPath.ToString());
        }

        private void onWriteCharacter(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        const int minWidth = 8, maxWidth = 48;
        const int minHeight = 8, maxHeight = 24;
        const int minMines = 10;
        
        public void resetTable()
        {
            Program.gameForm = new Game(width, height, mines);
            Program.gameForm.Show();
            this.Hide();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            mines = width = height = 0;
            if (txtWidth.Text != "")
                width = Convert.ToInt32(txtWidth.Text);
            if (txtHeight.Text != "")
                height = Convert.ToInt32(txtHeight.Text);
            if (txtMines.Text != "")
                mines = Convert.ToInt32(txtMines.Text);

            bool canStart = true;
            string errorMessage = "";

            if (width < minWidth || width > maxWidth){
                canStart = false;
                errorMessage = errorMessage + String.Format("The width must be between {0} and {1}!\n",minWidth,maxWidth);
            }
            if (height < minHeight || height > maxHeight){
                canStart = false;
                errorMessage = errorMessage + String.Format("The height must be between {0} and {1}!\n",minHeight,maxHeight);
            }
            if (mines < minMines || mines > (int)((3 * width * height) / 4))
            {
                canStart = false;
                errorMessage = errorMessage + String.Format("The number of mines must be between {0} and {1}!\n",minMines,(int)((3*width * height)/4));
            }

            if (canStart){
                resetTable();
            }
            else{
                MessageBox.Show(errorMessage);
            }
        }

        
    }
}
