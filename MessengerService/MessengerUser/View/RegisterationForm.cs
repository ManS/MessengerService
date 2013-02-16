using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MessengerUser.Controller;
using MessengerService.Model.DataContracts;

namespace MessengerUser.Controller
{
    public partial class RegisterationForm : Form
    {
        UserController m_userController;
        
        public RegisterationForm()
        {
            InitializeComponent();
        }
        
        public RegisterationForm(UserController p_userController)
        {
            InitializeComponent();
            m_userController = p_userController;
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string fName = firstname_txtbox.Text;
                string lName = lastname_txtbox.Text;
                string uName = username_txtbox.Text;
                string password = password_txtbox.Text;
                int avatarID = int.Parse(avatar_txtbox.Text);
                AccountInfo accountInfo = new AccountInfo(uName, password);
                UserPersonalInfo userPersonalInfo = new UserPersonalInfo(fName, lName, avatarID);
                m_userController.Subscribe(accountInfo, userPersonalInfo);
                this.Close();
            }
            catch
            {
                throw new NotSupportedException("Error during Registeration!");
            }
            
        }
    }
}
