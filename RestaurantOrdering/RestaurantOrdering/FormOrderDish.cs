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
    public partial class FormOrderDish : Form
    {
        public FormOrderDish()
        {
            InitializeComponent();
        }
        OrderInfoBll oiBll = new OrderInfoBll();

        private void FormOrderDish_Load(object sender, EventArgs e)
        {
            LoadDishType();
            LoadDishInfo();

            LoadDetailList();
        }
            

        private void LoadDishInfo()
        {
            //throw new NotImplementedException();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (txtTitle.Text != "")
            {
                dic.Add("dchar", txtTitle.Text);
            }
            if (ddlType.SelectedValue.ToString() != "0")
            {
                dic.Add("dtypeId", ddlType.SelectedValue.ToString());
            }

            //Query cuisines and display them 
            DishInfoBll diBll = new DishInfoBll();
            dgvAllDish.AutoGenerateColumns = false;
            dgvAllDish.DataSource = diBll.GetList(dic);
        }

        private void LoadDishType()
        {
            //throw new NotImplementedException();
            DishTypeInfoBll dtiBll = new DishTypeInfoBll();
            var list = dtiBll.GetList();

            list.Insert(0, new DishTypeInfo()
            {
                DId = 0,
                DTitle = "All"
            });

            ddlType.ValueMember = "did";
            ddlType.DisplayMember = "dtitle";
            ddlType.DataSource = list;

        }

        private void txtTitle_TextChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDishInfo();
        }

        private void LoadDetailList()
        {
            
            int orderId = Convert.ToInt32(this.Tag);
            dgvOrderDetail.AutoGenerateColumns = false;
            dgvOrderDetail.DataSource = oiBll.GetDetailList(orderId);

        }

        private void dgvAllDish_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //Get order Id
            int orderId = Convert.ToInt32(this.Tag);

            //Get  a cuisine Id
            int dishId = Convert.ToInt32(dgvAllDish.Rows[e.RowIndex].Cells[0].Value);

            if (oiBll.TakeOrder(orderId, dishId))
            {
                LoadDetailList();
            }
        }

        /// <summary>
        /// Update an order when modify the quantity of  a cuisine.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvOrderDetail_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                
                //Get the updated row number
                var row = dgvOrderDetail.Rows[e.RowIndex];
                //Get the cuisine number
                int oid = Convert.ToInt32(row.Cells[0].Value);
                //Get the new quantity
                int count = Convert.ToInt32(row.Cells[2].Value);
                //Update the data in database
                oiBll.UpdateCountByOid(oid, count);

                //Recaculate the sum price.
                GetTotalMoneyByOrderId();
            }
        }

        private void GetTotalMoneyByOrderId()
        {
            int orderId = Convert.ToInt32(this.Tag);
            lblMoney.Text = oiBll.GetTotalMoneyByOrderId(orderId).ToString();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure to delete?", "tip", MessageBoxButtons.OKCancel);
            if (result == DialogResult.Cancel)
            {
                return;
            }
            //Get the number of cuisine to be deleted
            int oid = Convert.ToInt32(dgvOrderDetail.SelectedRows[0].Cells[0].Value);
            //Execeute the delete action
            if (oiBll.DeleteDetailById(oid))
            {
                LoadDetailList();
            }
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            //Get the number of the order
            int orderId = Convert.ToInt32(this.Tag);
            //Get the sum price
            decimal money = Convert.ToDecimal(lblMoney.Text);
            //Update the order
            if (oiBll.SetOrderMoney(orderId, money))
            {
                MessageBox.Show("Take order succeed");
            }
            else
            {
                MessageBox.Show("Take order failed");
            }
        }
    }
}
