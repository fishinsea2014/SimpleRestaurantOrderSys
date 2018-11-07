using CaterBll;
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

namespace RestaurantOrdering
{
    public partial class FormHall : Form
    {
        private HallInfoBll hiBll;
        public event Action MyUpdateForm;
        public FormHall()
        {
            InitializeComponent();
            hiBll = new HallInfoBll();
        }

        private void FormHall_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            //throw new NotImplementedException();
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = hiBll.GetList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HallInfo hi = new HallInfo()
            {
                HTitle = txtTitle.Text
            };

            if (txtId.Text == "No number")
            {
                
                if (hiBll.Add(hi))
                {
                    LoadList();
                }
            }
            else
            {
                
                hi.HId = int.Parse(txtId.Text);
                if (hiBll.Edit(hi))
                {
                    LoadList();
                }
            }

            txtId.Text = "No number";
            txtTitle.Text = "";
            btnSave.Text = "Add";

            //MyUpdateForm();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "No number";
            txtTitle.Text = "";
            btnSave.Text = "Add";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            btnSave.Text = "Update";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result = MessageBox.Show("Are you sure to delete?", "Tip", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }

            if (hiBll.Remove(id))
            {
                LoadList();
            }

            
            //MyUpdateForm();
        }
    }
}
