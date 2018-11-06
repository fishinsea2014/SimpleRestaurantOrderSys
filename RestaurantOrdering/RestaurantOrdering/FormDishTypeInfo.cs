using CaterDal;
using CaterModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CaterBll;

namespace RestaurantOrdering
{
    public partial class FormDishTypeInfo : Form
    {
        public FormDishTypeInfo()
        {
            InitializeComponent();
        }
        private DishTypeInfoBll dtiBll = new DishTypeInfoBll();
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
            DishTypeInfo dti = new DishTypeInfo()
            {
                DTitle = txtTitle.Text
            };

            if (txtId.Text == "No number")
            {
                //Add
                if (dtiBll.Add(dti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Failed to add a dish type");
                }
            }
            else
            {
                //Update
                dti.DId = int.Parse(txtId.Text);
                if (dtiBll.Edit(dti))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Failed to add a dish type");
                }
            }
            ReinitiateCombo();

            this.result = DialogResult.OK;
        }

        private void ReinitiateCombo()
        {
            //Reinitiate the add combo
            txtId.Text = "No number";
            txtTitle.Text = "";
            btnSave.Text = "Add";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ReinitiateCombo();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];

            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            btnSave.Text = "Update";

            //Keep the selected row, recall this row when refresh the page
            rowIndex = e.RowIndex;

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var row = dgvList.SelectedRows[0];
            int id = Convert.ToInt32(row.Cells[0].Value);

            DialogResult result = MessageBox.Show("Are you sure to delete?", "Tips", MessageBoxButtons.OKCancel);

            if (result == DialogResult.Cancel)
            {
                return;
            }

            if (dtiBll.Delete(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("Fail to delete");
            }

            this.result = DialogResult.OK;
        }

        private void FormDishTypeInfo_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = this.result;
        }
    }
}
