using CaterDal;
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
    public partial class FormDishTypeInfo : Form
    {
        public FormDishTypeInfo()
        {
            InitializeComponent();
        }
        private DishTypeInfoDal dtiBll = new DishTypeInfoDal();
        //This variable is use to keep the selected row
        private int rowIndex = -1;
        private DialogResult result = DialogResult.Cancel;

        private void FormDishTypeInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            //throw new NotImplementedException();
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = dtiBll.GetList();
            if (rowIndex >= 0)
            {
                dgvList.Rows[rowIndex].Selected = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
