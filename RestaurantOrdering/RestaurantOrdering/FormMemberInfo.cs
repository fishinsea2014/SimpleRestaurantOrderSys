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
    public partial class FormMemberInfo : Form
    {
        public FormMemberInfo()
        {
            InitializeComponent();
        }

        MemberInfoBll miBll = new MemberInfoBll();

        private void FormMemberInfo_Load(object sender, EventArgs e)
        {
            LoadList();
            //Load the list of type in the type combo box.
            LoadTypeList();

        }

        private void LoadTypeList()
        {
            //throw new NotImplementedException();

            MemberTypeInfoBll mtiBll = new MemberTypeInfoBll();
            List<MemberTypeInfo> list = mtiBll.GetList();

            ddlType.DataSource = list;
            ddlType.DisplayMember = "mtitle";
            ddlType.ValueMember = "mid";
        }

        private void LoadList()
        {
            //throw new NotImplementedException();
            //Create an dictionary to store the query string
            Dictionary<string, string> dic = new Dictionary<string, string>();

            //Create user name query string
            if (txtNameSearch.Text != "")
            {
                dic.Add("mname", txtNameSearch.Text);
            }

            //Create mobile query string
            if (txtPhoneSearch.Text != "")
            {
                dic.Add("MPhone", txtPhoneSearch.Text);
            }


            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = miBll.GetList(dic);


            //Return to the selected row after update.
            if (dgvSelectedIndex > -1)
            {
                dgvList.Rows[dgvSelectedIndex].Selected = true;
            }
        }
        /// <summary>
        /// Query when text changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNameSearch_TextChanged(object sender, EventArgs e)
        {
            LoadList();
        }


        /// <summary>
        /// Query when lost focus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPhoneSearch_Leave(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtNameAdd.Text == "")
            {
                MessageBox.Show("Please input the VIP name");
                txtNameAdd.Focus();
                return;
            }

            MemberInfo mi = new MemberInfo()
            {
                MName = txtNameAdd.Text,
                MPhone = txtPhoneAdd.Text,
                MMoney = Convert.ToDecimal(txtMoney.Text),
                MTypeId = Convert.ToInt32(ddlType.SelectedValue)
            };

            if (txtId.Text.Equals("No number"))
            {
                #region Add

                if (miBll.Add(mi))
                {
                    LoadList();
                }
                else
                {
                    MessageBox.Show("Fail to add a VIP.");
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
                else
                {
                    MessageBox.Show("Fail to update the VIP");
                }

                #endregion
            }

            InitiateAddCombo();

        }

        private void InitiateAddCombo()
        {
            //Initiate the adding combo
            txtId.Text = "No number";
            txtNameAdd.Text = "";
            txtPhoneAdd.Text = "";
            txtMoney.Text = "";
            ddlType.SelectedIndex = 0;
            btnSave.Text = "Add";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            InitiateAddCombo();
        }

        /// <summary>
        /// This action do not effect.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvList_DoubleClick(object sender, EventArgs e)
        {

        }

        private int dgvSelectedIndex = -1;

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            
            dgvSelectedIndex = e.RowIndex;
            //Get the clicked row.
            var row = dgvList.Rows[e.RowIndex];

            //Display the selected VIP to updating tools
            txtId.Text = row.Cells[0].Value.ToString();
            txtNameAdd.Text = row.Cells[1].Value.ToString();
            ddlType.Text = row.Cells[2].Value.ToString();
            txtPhoneAdd.Text = row.Cells[3].Value.ToString();
            txtMoney.Text = row.Cells[4].Value.ToString();
            btnSave.Text = "Update";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            //Get the number of the selected item
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);

            //Tips of confirmation
            DialogResult result = MessageBox.Show("Are you sure to delete?", "Tips: ", MessageBoxButtons.OKCancel);

            if (result == DialogResult.Cancel)
            {
                return;
            }
            if (miBll.Remove(id))
            {
                LoadList();
            }
            else
            {
                MessageBox.Show("Fail to delete a VIP");
            }
        }

        private void btnAddType_Click(object sender, EventArgs e)
        {
            FormMemberTypeInfo forMti = new FormMemberTypeInfo();
            //Utilise modal dialog
            //Get the results of FormMemberTypeInfo, which are ok or cancel
            DialogResult result = forMti.ShowDialog(); 
            if (result == DialogResult.OK)
            {
                LoadTypeList();
                LoadList();
            }

        }
    }
}
