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
    public partial class FormDishInfo : Form
    {
        public FormDishInfo()
        {
            InitializeComponent();
        }
        private DishInfoBll diBll = new DishInfoBll();

        private void FormDishInfo_Load(object sender, EventArgs e)
        {
            LoadTypeList();
            LoadList();

        }

        private void LoadList()
        {
            //throw new NotImplementedException();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtTitleSearch.Text != "")
            {
                dic.Add("dtitle", txtTitleSearch.Text);
            }
            if (ddlTypeSearch.SelectedValue.ToString() != "0")
            {
                dic.Add("DTypeId", ddlTypeSearch.SelectedValue.ToString());
            }


            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = diBll.GetList(dic);
        }

        private void LoadTypeList()
        {
            //throw new NotImplementedException();
            DishTypeInfoBll dtiBll = new DishTypeInfoBll();


            #region Bind the query dropdown list.
            List<DishTypeInfo> list = dtiBll.GetList();
            //
            list.Insert(0, new DishTypeInfo()
            {
                DId = 0,
                DTitle = "All"
            });

            ddlTypeSearch.DataSource = list;
            ddlTypeSearch.ValueMember = "did";
            ddlTypeSearch.DisplayMember = "dtitle"; 
            #endregion

            #region Bind the dropdown list when adding an item.

            ddlTypeAdd.DataSource = dtiBll.GetList();
            ddlTypeAdd.DisplayMember = "dtitle";
            ddlTypeAdd.ValueMember = "did";

            #endregion
        }

        private void txtTitleSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlTypeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            txtTitleSearch.Text = "";
            ddlTypeSearch.SelectedIndex = 0;
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //Createa a new cuision object according to the inputs
            DishInfo di = new DishInfo()
            {
                DTitle = txtTitleSave.Text,
                DChar = txtChar.Text,
                DPrice = Convert.ToDecimal(txtPrice.Text),
                DTypeId = Convert.ToInt32(ddlTypeAdd.SelectedValue)
            };

            if (txtId.Text == "No number")
            {
                #region Add

                if (diBll.Add(di))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Failed to add a cuision");
                }
                #endregion
            }
            else
            {
                #region Update

                di.DId = int.Parse(txtId.Text);
                if (diBll.Update(di))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Failed to add a cuision");
                }
                #endregion
            }

            #region Restore 

            txtId.Text = "No number";
            txtTitleSave.Text = "";
            txtPrice.Text = "";
            txtChar.Text = "";
            ddlTypeAdd.SelectedIndex = 0;

            #endregion
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitleSave.Text = row.Cells[1].Value.ToString();
            ddlTypeAdd.Text = row.Cells[2].Value.ToString();
            txtPrice.Text = row.Cells[3].Value.ToString();
            txtChar.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "Update";
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormDishTypeInfo formDti = new FormDishTypeInfo();
            DialogResult result = formDti.ShowDialog();
            if (result == DialogResult.OK)
            {
                LoadTypeList();
                LoadList();
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result = MessageBox.Show("Are you sure to delete?", "Tips", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (diBll.Remove(id))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Fail to delete");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "No number";
            txtTitleSave.Text = "";
            txtPrice.Text = "";
            txtChar.Text = "";
            ddlTypeAdd.SelectedIndex = 0;
        }
    }
}
