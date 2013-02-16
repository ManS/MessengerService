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

namespace MessengerUser.View
{
    public partial class ContactForm : Form
    {
        UserController m_controller;
        int senderID;

        public ContactForm(UserController p_controller, UserInfo p_contactInfo )
        {
            InitializeComponent();
            senderID = p_contactInfo.UserID;
            m_controller = p_controller;
            avatar_pb.Image = MessengerForm.m_avatarsList[p_contactInfo.UserPersonalInfo.AvatarID];
            username_lbl.Text += p_contactInfo.UserName;
            firstname_lbl.Text += p_contactInfo.UserPersonalInfo.FirstName;
            lastname_lbl.Text += p_contactInfo.UserPersonalInfo.LastName;
        }

        private void accept_btn_Click(object sender, EventArgs e)
        {
            m_controller.ReplyAddRequest(senderID,true);
            this.Close();
        }

        private void reject_btn_Click(object sender, EventArgs e)
        {
            m_controller.ReplyAddRequest(senderID, false);
            this.Close();
        }

        private void later_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
