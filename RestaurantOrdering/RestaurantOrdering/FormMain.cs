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

        OrderInfoBll oiBll = new OrderInfoBll();
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
            //2.1 Get all the area object
            HallInfoBll hiBll = new HallInfoBll();
            var list = hiBll.GetList();
            //Clear previous tab pages
            //2.2 Go through the set, add info to tag pages.
            tcHallInfo.TabPages.Clear();
            TableInfoBll tiBll = new TableInfoBll();
            foreach (var hi in list)
            {
                //Create a tab each area. 
                TabPage tp = new TabPage(hi.HTitle);
                //3.1 Create a ListView for each area tap
                ListView lvTableInfo = new ListView();

                //Double click to create a new order
                lvTableInfo.DoubleClick += lvTableInfo_DoubleClick;

                //3.2 Combine the imageList to listview 
                lvTableInfo.LargeImageList = imageList1;
                lvTableInfo.Dock = DockStyle.Fill;
                tp.Controls.Add(lvTableInfo);

                //4.1 Get the tables of the area
                //Set query condition
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("thallid", hi.HId.ToString());
                var listTableInfo = tiBll.GetList(dic);
                
                //Double click the ListView, add a creating new order event
                lvTableInfo.DoubleClick += LvTableInfo_DoubleClick_New_Order;
                
                //4.2 Add table info to the list
                foreach (var ti in listTableInfo)
                {
                    var lvi = new ListViewItem(ti.TTitle, ti.TIsFree ? 0 : 1);

                    //Keep the table number in lvi.Tag, which could be used when creating a new order
                    lvi.Tag = ti.TId;

                    lvTableInfo.Items.Add(lvi);
                }

                //2.3 Add tab page into a tab container
                tcHallInfo.TabPages.Add(tp);

                //tcHallInfo.TabPages.Add(new TabPage(hi.HTitle));
            }
        }

        private void lvTableInfo_DoubleClick(object sender, EventArgs e)
        {
            //Get the selected table
            var lv1 = sender as ListView;
            var lvi = lv1.SelectedItems[0];

            //Get the table number
            int tableId = Convert.ToInt32(lvi.Tag);

            if (lvi.ImageIndex == 0)
            {
                //The table is available, create a new order
                
                int orderId = oiBll.NewOrder(tableId);
                //Mark the table as occupied
                lv1.SelectedItems[0].ImageIndex = 1;
            }
            else
            {
                //Taking orders
                //lvi.Tag = oiBll
            }

            FormOrderDish formOrderDish = new FormOrderDish();
            formOrderDish.Tag = lvi.Tag;
            formOrderDish.Show();
        }

        private void LvTableInfo_DoubleClick_New_Order(object sender, EventArgs e)
        {


            //Get the number of table
            var lvl = sender as ListView;
            int tableId = Convert.ToInt32(lvl.SelectedItems[0].Tag);

            //Insert a new order item in orderinfo table
            //Update the status of table as occupied
            OrderInfoBll oiBll = new OrderInfoBll();
            oiBll.NewOrder(tableId);

            //Update the table image
            lvl.SelectedItems[0].ImageIndex = 1;

            
            //Insert order info to OrderInfo table

            //Update the status of the table
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
