﻿using CaterBll;
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
    public partial class FormTable : Form
    {
        private TableInfoBll tiBll = new TableInfoBll();
        public FormTable()
        {
            InitializeComponent();
        }

        private void FormTable_Load(object sender, EventArgs e)
        {
            LoadSearchList();
            LoadList();
        }

        private void LoadSearchList()
        {
            //throw new NotImplementedException();
            HallInfoBll hiBll = new HallInfoBll();
            var list = hiBll.GetList();

            list.Insert(0, new HallInfo()
            {
                HId = 0,
                HTitle = "All"
            });
            ddlHallSearch.DataSource = list;
            ddlHallSearch.ValueMember = "hid";
            ddlHallSearch.DisplayMember = "htitle";

            ddlHallAdd.DataSource = hiBll.GetList();
            ddlHallAdd.ValueMember = "hid";
            ddlHallAdd.DisplayMember = "htitle";

            List<DdlModel> listDdl = new List<DdlModel>()
            {
                new DdlModel("-1","All"),
                new DdlModel("1","Available"),
                new DdlModel("0","Occupied")
            };
            ddlFreeSearch.DataSource = listDdl;
            ddlFreeSearch.ValueMember = "id";
            ddlFreeSearch.DisplayMember = "title";
        }

        private void LoadList()
        {
            //throw new NotImplementedException();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (ddlHallSearch.SelectedIndex > 0)
            {
                dic.Add("tHallId", ddlHallSearch.SelectedValue.ToString());
            }
            if (ddlFreeSearch.SelectedIndex > 0)
            {
                dic.Add("tIsFree", ddlFreeSearch.SelectedValue.ToString());
            }

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = tiBll.GetList(dic);
        }

        private void dgvList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = Convert.ToBoolean(e.Value) ? "√" : "×";
            }
        }

        private void ddlHallSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void ddlFreeSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadList();
        }

        private void btnSearchAll_Click(object sender, EventArgs e)
        {
            ddlHallSearch.SelectedIndex = 0;
            ddlFreeSearch.SelectedIndex = 0;
            LoadList();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            TableInfo ti = new TableInfo()
            {
                TTitle = txtTitle.Text,
                THallId = Convert.ToInt32(ddlHallAdd.SelectedValue),
                TIsFree = rbFree.Checked
            };

            if (txtId.Text == "No number")
            {
                #region Add

                if (tiBll.Add(ti))
                {
                    LoadList();
                }
                #endregion
            }
            else
            {
                #region Update

                ti.TId = int.Parse(txtId.Text);
                if (tiBll.Edit(ti))
                {
                    LoadList();
                }

                #endregion
            }

            
            txtId.Text = "No number";
            txtTitle.Text = "";
            ddlHallAdd.SelectedIndex = 0;
            rbFree.Checked = true;
            btnSave.Text = "Add";

            //Refresh();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtId.Text = "No number";
            txtTitle.Text = "";
            ddlHallAdd.SelectedIndex = 0;
            rbFree.Checked = true;
            btnSave.Text = "Add";
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgvList.Rows[e.RowIndex];
            txtId.Text = row.Cells[0].Value.ToString();
            txtTitle.Text = row.Cells[1].Value.ToString();
            ddlHallAdd.Text = row.Cells[2].Value.ToString();
            if (Convert.ToBoolean(row.Cells[3].Value))
            {
                rbFree.Checked = true;
            }
            else
            {
                rbUnFree.Checked = true;
            }
            btnSave.Text = "Update";
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvList.SelectedRows[0].Cells[0].Value);
            DialogResult result = MessageBox.Show("Are you sure to delete?", "Tip", MessageBoxButtons.OKCancel);
            if (result == DialogResult.OK)
            {
                if (tiBll.Remove(id))
                {
                    LoadList();
                }
            }

            //Refresh();
        }

        private void btnAddHall_Click(object sender, EventArgs e)
        {
            FormHall formHallInfo = new FormHall();
            formHallInfo.MyUpdateForm += LoadSearchList;
            formHallInfo.MyUpdateForm += LoadList;
            formHallInfo.Show();
        }
    }

}
