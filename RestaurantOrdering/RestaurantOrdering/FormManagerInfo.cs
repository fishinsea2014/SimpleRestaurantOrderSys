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
    /// <summary>
    /// User management module.
    /// </summary>
    public partial class FormManagerInfo : Form
    {
        public FormManagerInfo()
        {
            InitializeComponent();
        }

        ManagerInfoBll miBll = new ManagerInfoBll();

        
        private void LoadList()
        {
            
            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = miBll.GetList();
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Save 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtName.Text == "" && txtPwd.Text == "")
            {
                label6.Text = "Tips: User name and password cannot be empty.";
                return;
            }
            //Create an new manager by input
            ManagerInfo mi = new ManagerInfo()
            {
                MName = txtName.Text,
                MPwd = txtPwd.Text,
                MType = rb1.Checked ? 1 : 0 //1 for Manager, 0 for Clerk
            };
            if (txtId.Text.Equals("The number is empty."))
            {
                #region Add
                //Call add method in Dal 
                if (miBll.Add(mi))
                {
                    
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Failed to add an item");
                }

                #endregion
            }
            else
            {
                #region Update

                mi.MId = int.Parse(txtId.Text);
                if (miBll.Edit(mi))
                {
                    LoadList();
                }

                #endregion
            }

            //Clear the value in from 
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            btnSave.Text = "Add";
            txtId.Text = "The number is empty.";
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "The number is empty.";
            txtName.Text = "";
            txtPwd.Text = "";
            rb2.Checked = true;
            btnSave.Text = "Add";
        }
        /// <summary>
        /// Initiate the display on column of Type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            
            if (e.ColumnIndex == 2)
            {                
                e.Value = Convert.ToInt32(e.Value) == 1 ? "Manager" : "Clerk";
            }
        }

        /// <summary>
        /// When click a row, show the user info in right, and update the user info if click Add.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value.ToString().Equals("1"))
            {
                rb1.Checked = true; 
            }
            else
            {
                rb2.Checked = true;
            }
            
            txtPwd.Text = "Is this original password?";

            btnSave.Text = " ";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            var rows = dgvList.SelectedRows;
            if (rows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure to delete?", "Tip: ", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    return;
                }

                int id = int.Parse(rows[0].Cells[0].Value.ToString());
                if (miBll.Remove(id))
                {
                    LoadList();
                }
            }
            else
            {
                MessageBox.Show("Please select a user to delete.");
            }
        }

        private void FormManagerInfo_Load_1(object sender, EventArgs e)
        {
            LoadList();
        }
    }
}
