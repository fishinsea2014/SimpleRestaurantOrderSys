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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void setLabelTxt()
        {
            label1.Text = "Form 2 clicked";
        }

        private void button1_Click(object sender, EventArgs e)
        { 
            Form2 f2 = new Form2();
            Form3 f3 = new Form3();

            f2.setForm1 += f3.setLabelTxt;
            f2.Show();
            f3.Show();
        }
    }
}
