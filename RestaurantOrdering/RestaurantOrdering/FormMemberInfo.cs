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
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dgvList.AutoGenerateColumns = false;
            dgvList.DataSource = miBll.GetList(dic);
        }
    }
}
