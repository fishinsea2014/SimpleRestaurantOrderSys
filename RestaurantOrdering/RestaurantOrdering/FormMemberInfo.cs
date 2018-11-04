using CaterBll;
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
    }
}
