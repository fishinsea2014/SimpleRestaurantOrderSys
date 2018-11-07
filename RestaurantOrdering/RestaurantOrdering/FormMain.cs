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
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            //Identify the role of the user, and set the buttons accordingly.
            int type = Convert.ToInt32(this.Tag);
            if (type == 1)
            {

            }
            else
            {
                MenuManagerInfo.Visible = false;
            }

            LoadHallInfo();

        }

        private void LoadHallInfo()
        {
            //throw new NotImplementedException();
            HallInfoBll hiBll = new HallInfoBll();
            var list = hiBll.GetList();
            //Clear previous tab pages
            tcHallInfo.TabPages.Clear();
            TableInfoBll tiBll = new TableInfoBll();
            foreach (var hi in list)
            {
                //Create a tab each area. 
                TabPage tp = new TabPage(hi.HTitle);
                //Get the tables of the area
                //Set query condition
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("thallid", hi.HId.ToString());
                var listTableInfo = tiBll.GetList(dic);
                //Create a ListView for each area tap
                ListView lvTableInfo = new ListView();
                lvTableInfo.LargeImageList = imageList1;
                lvTableInfo.Dock = DockStyle.Fill;
                tp.Controls.Add(lvTableInfo);
                foreach (var ti in listTableInfo)
                {
                    var lvi = new ListViewItem(ti.TTitle, ti.TIsFree ? 0 : 1);
                    lvi.Tag = ti.TId;
                    lvTableInfo.Items.Add(lvi);
                }

                //Add tab page into a tab container
                tcHallInfo.TabPages.Add(tp);

                //tcHallInfo.TabPages.Add(new TabPage(hi.HTitle));
            }
        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void MenuManagerInfo_Click(object sender, EventArgs e)
        {
            FormManagerInfo formManagerInfo = FormManagerInfo.Create();
            formManagerInfo.Show();
            formManagerInfo.Focus();
            formManagerInfo.WindowState = FormWindowState.Normal;
        }

        private void MenuMemberInfo_Click(object sender, EventArgs e)
        {
            FormMemberTypeInfo formMemberInfo = new FormMemberTypeInfo();
            formMemberInfo.Show();
        }

        private void MenuQuit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MenuTableInfo_Click(object sender, EventArgs e)
        {
            FormTable formTable = new FormTable();
            formTable.Refresh += LoadHallInfo;
            formTable.Show();
        }

        private void MenuDishInfo_Click(object sender, EventArgs e)
        {
            FormDishInfo formDishInfo = new FormDishInfo();
            formDishInfo.Show();
        }
    }
}
