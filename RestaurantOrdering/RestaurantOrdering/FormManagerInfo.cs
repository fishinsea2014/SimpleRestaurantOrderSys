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
    public partial class FormManagerInfo : Form
    {
        public FormManagerInfo()
        {
            InitializeComponent();
        }

        ManagerInfoBll miBll = new ManagerInfoBll();

        private void FormManagerInfo_Load(object sender, EventArgs e)
        {
            LoadList();
        }

        private void LoadList()
        {
            //throw new NotImplementedException();
            //禁用列表的自动生成
            dgvList.AutoGenerateColumns = false;
            //调用方法获取数据，绑定到列表的数据源上
            dgvList.DataSource = miBll.GetList();
        }

        private void dgvList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
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
            //根据当前点击的单元格，找到行与列，进行赋值
            //根据索引找到行
            DataGridViewRow row = dgvList.Rows[e.RowIndex];
            //找到对应的列
            txtId.Text = row.Cells[0].Value.ToString();
            txtName.Text = row.Cells[1].Value.ToString();
            if (row.Cells[2].Value.ToString().Equals("1"))
            {
                rb1.Checked = true; //值为1，则经理选中
            }
            else
            {
                rb2.Checked = true;//如果为0，则店员选中
            }
            //指定密码的值
            txtPwd.Text = "这是原来的密码吗";

            btnSave.Text = "修改";
        }
    }
}
