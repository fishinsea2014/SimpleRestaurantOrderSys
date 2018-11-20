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
    public partial class FormOrderPay : Form
    {
        public FormOrderPay()
        {
            InitializeComponent();
        }

        private void FormOrderPay_Load(object sender, EventArgs e)
        {
            gbMember.Enabled = false;
        }
    }
}
