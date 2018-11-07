using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RestaurantOrdering.test
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        public  void setLabelTxt()
        {
            label1.Text = "Form 2 clicked";
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }
    }
}
