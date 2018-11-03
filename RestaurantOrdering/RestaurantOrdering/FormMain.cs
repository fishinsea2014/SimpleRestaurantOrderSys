using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantOrdering
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Identify the role of the user, and set the buttons accordingly.
            int type = Convert.ToInt32(this.Tag);
            if (type == 1)
            {

            }
            else
            {
                MenuManagerInfo.Visible = false;
            }

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MenuManagerInfo_Click(object sender, EventArgs e)
        {

        }
    }
}
