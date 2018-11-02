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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            string name = textUsername.Text;
            string pwd = textPassword.Text;

            ManagerInfoBll miBll = new ManagerInfoBll();
            LoginState loginState = miBll.Login(name, pwd);
            switch (loginState)
            {
                case LoginState.Ok:
                    FormMain main = new FormMain();
                    main.Show();
                    this.Hide();
                    break;
                case LoginState.NameError:
                    MessageBox.Show("User name error");
                    break;
                case LoginState.PwdError:
                    MessageBox.Show("Password error");
                    break;
            }
        }
    }
}
