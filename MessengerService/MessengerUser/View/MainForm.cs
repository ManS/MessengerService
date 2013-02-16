using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MessengerService;
using MessengerUser.Controller;
using System.Threading;
using MessengerUser.Model;
using MessengerService.Model.DataContracts;
using System.ServiceModel;
namespace MessengerUser
{
    public partial class IMessenger : Form, IMainObserver
    {

        #region Attributes
        private UserController m_userController;
        NetTcpBinding nettcp = new NetTcpBinding(SecurityMode.None);
        EndpointAddress address = new EndpointAddress("net.tcp://localhost:8082/test");

        #endregion

        #region Delegats
        delegate void RegistrationSucceed();
        delegate void SignInSucceed();
        delegate void ErrorHandler(string p_errorMsg);
        #endregion

        #region Constructor
        public IMessenger()
        {
            InitializeComponent();
            this.status_cb.SelectedIndex = 0;
            m_userController = new UserController();
            m_userController.RegisterMainObserver(this);
        }

        #endregion
      
        #region Controls Event Handlers
        private void signup_lbl_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.m_userController.ConnectToServer(nettcp, address);
            if (m_userController.IsConnected)
            {
                RegisterationForm form = new RegisterationForm(this.m_userController);
                form.ShowDialog();
            }
        }
        private void signin_btn_Click(object sender, EventArgs e)
        {
            string userName = username_txtbox.Text;
            string password = password_txtbox.Text;
            Status loginStatus = this.GetSelectedStatus();
            AccountInfo accountInfo = new AccountInfo(userName, password);
            if (!m_userController.IsConnected)
                this.m_userController.ConnectToServer(nettcp, address);
            if (m_userController.IsConnected)
                this.m_userController.SignIn(accountInfo, loginStatus);
        }
        #endregion

        #region Methods
        private Status GetSelectedStatus()
        {
            if (this.status_cb.SelectedIndex == 0)
            {
                return Status.Offline;
            }
            else if (this.status_cb.SelectedIndex == 1)
            {
                return Status.Online;
            }
            else if (this.status_cb.SelectedIndex == 2)
            {
                return Status.Busy;
            }
            else
            {
                return Status.Away;
            }
        }
        #endregion
       
        #region IMainObserver Members
        public void OnRegistrationSucceed()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new RegistrationSucceed(OnRegistrationSucceed));
            }
            else
            {
                MessageBoxIcon icon = MessageBoxIcon.Asterisk;
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxOptions options = MessageBoxOptions.ServiceNotification;
                MessageBox.Show("Registration Succeed!", "Congratulations..", button, icon, MessageBoxDefaultButton.Button1, options);
            }
        }
        public void OnSignInSucceed()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SignInSucceed(OnSignInSucceed));
            }
            else
            {
                //MessageBox.Show("You signned in successfully!");
                MessengerForm m_messengerForm = new MessengerForm(this.m_userController);
                m_messengerForm.Show();
                //this.m_userController.UnRegisterMainObserver(this);
                this.m_userController.StartUp();
                this.Hide();
            }
        }
        public void OnError(string p_errorMsg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ErrorHandler(OnError), p_errorMsg);
            }
            else
            {
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                MessageBox.Show(p_errorMsg, "Error", button, icon);
            }
        }
        #endregion
    }
}