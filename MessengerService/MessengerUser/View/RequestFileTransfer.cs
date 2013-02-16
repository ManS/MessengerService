using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MessengerUser.Controller;
using System.IO;

namespace MessengerUser.View
{
    public partial class RequestFileTransfer : Form
    {
        UserController m_controller;
        string m_path;
        string m_filePath;
        int m_friendID;

        public RequestFileTransfer(UserController p_controller,int p_senderID, string filepath)
        {
            InitializeComponent();
            m_controller = p_controller;
            m_path = m_controller.ReceivedFilePath;
            m_filePath = filepath;
            m_friendID = p_senderID;
            string senderName = this.m_controller.User.FriendList[p_senderID].UserName;
            this.friendName_lbl.Text = senderName + " request to send to you " + Path.GetFileName(filepath);
        }
        private void accept_btn_Click(object sender, EventArgs e)
        {
            if (this.m_controller.ReceivedFilePath != m_path)
                this.m_controller.ChangeSaveFilePath(m_path);
            this.m_controller.ReplySendFile(m_filePath, m_friendID, true);
            this.Close();
        }
        private void reject_btn_Click(object sender, EventArgs e)
        {
            this.m_controller.ReplySendFile(m_filePath, m_friendID, false);
            this.Close();
        }
        private void browse_btn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_path = saveDialog.FileName;
                path_txtbox.Text = m_path;
            }
        }
    }
}