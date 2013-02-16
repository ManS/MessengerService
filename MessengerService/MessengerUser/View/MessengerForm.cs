using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MessengerService.Model.DataContracts;
using MessengerService;
using MessengerUser.Controller;
using MessengerUser.View;

namespace MessengerUser.Controller
{
    public partial class MessengerForm : Form, IUserObserver, IChatWindowObserver
    {

        #region Attributes
        Dictionary<string, UserInfo> m_Contacts = new Dictionary<string, UserInfo>();
        Dictionary<string, ChatForm> m_ChatForms = new Dictionary<string, ChatForm>();
        public static Dictionary<int, Image> m_avatarsList = new Dictionary<int, Image>();
        Dictionary<string, TreeNode> m_friendsTree = new Dictionary<string, TreeNode>();
        UserController m_controller;
        UserInfo m_userInfo;
        #endregion

        #region Constructor
        public MessengerForm(UserController p_userController)
        {
            InitializeComponent();
            m_controller = p_userController;
            this.m_controller.Register(this);
            m_userInfo = m_controller.User.UserInfo;
            this.InitiateAvatars();
            this.profilePic_picbox.Image = m_avatarsList[m_userInfo.UserPersonalInfo.AvatarID];
            this.pm_txtbox.Text = m_userInfo.UserPM;
            this.SetStatus();
            this.SetContactsList(this.m_controller.User.FriendList);
            this.Text = p_userController.User.UserInfo.UserName;
            //this.m_controller.StartUp();
        }
        #endregion

        #region Methods
        private void SetContactsList(Dictionary<int, UserInfo> friendList)
        {
            foreach (KeyValuePair<int, UserInfo> Friend in friendList)
            {
                this.m_Contacts.Add(Friend.Value.UserName, Friend.Value);
                TreeNode friend = new TreeNode(Friend.Value.UserName);
                friend.Name = Friend.Value.UserName;
                if (Friend.Value.UserStatus != Status.Offline)
                {
                    this.contactsTree.SelectedNode = this.contactsTree.Nodes[0];
                    this.contactsTree.SelectedNode.Nodes.Add(friend);
                }
                else
                {
                     this.contactsTree.SelectedNode = this.contactsTree.Nodes[1];
                     this.contactsTree.SelectedNode.Nodes.Add(friend);
                }
                this.m_friendsTree.Add(Friend.Value.UserName, friend);
            }
        }
        private void SetStatus()
        {
            switch (this.m_userInfo.UserStatus)
            {
                case Status.Online:
                    this.status_cb.SelectedIndex = 1;
                    break;
                case Status.Offline:
                    this.status_cb.SelectedIndex = 0;
                    break;
                case Status.Busy:
                    this.status_cb.SelectedIndex = 2;
                    break;
                case Status.Away:
                    this.status_cb.SelectedIndex = 3;
                    break;
                default:
                    break;
            }

        }
        private void InitiateAvatars()
        {
            m_avatarsList.Add(1, global::MessengerUser.Properties.Resources._1);
            m_avatarsList.Add(2, global::MessengerUser.Properties.Resources._2);
            m_avatarsList.Add(3, global::MessengerUser.Properties.Resources._3);
            m_avatarsList.Add(4, global::MessengerUser.Properties.Resources._4);
            m_avatarsList.Add(5, global::MessengerUser.Properties.Resources._5);
            m_avatarsList.Add(6, global::MessengerUser.Properties.Resources._6);
            m_avatarsList.Add(7, global::MessengerUser.Properties.Resources._7);
            m_avatarsList.Add(8, global::MessengerUser.Properties.Resources._8);
            m_avatarsList.Add(9, global::MessengerUser.Properties.Resources._9);
            m_avatarsList.Add(10, global::MessengerUser.Properties.Resources._10);
        }
        #endregion
        
        #region Delegats
        delegate void UserLogIn(string p_userName, Status p_userStatus);
        delegate void UserLogOut(string p_userName);
        delegate void PMChanaged(string p_userName, string p_newPM);
        delegate void StatusChanaged(string p_userName, Status p_status);
        delegate void AddNewFriend(UserInfo p_friendInfo);
        delegate void ReceiveIM(InstantMessage p_instantMessage);
        delegate void ReceiveAddRequest(UserInfo p_senderInfo);
        delegate void ReceiveVideoConvRequest(string p_senderName);
        delegate void StartVideoConversation(string p_senderName);
        delegate void ReceiveAudioConvRequest(string p_senderName);
        delegate void ReceiveFileTransferRequest(string p_fileName, string p_senderName);
        delegate void NotifyError(string p_errorMsg);
        delegate void AvatarChanaged(string p_userName, int p_newAvatarID);
        delegate void IsTypingMessage(string p_friendName);
        #endregion

        #region IUserObserver Members

        public void OnUserLogIn(string p_friendName, Status p_userStatus)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new UserLogIn(OnUserLogIn), p_friendName);
            }
            else
            {
                this.contactsTree.Nodes.Remove(this.m_friendsTree[p_friendName]);
                this.contactsTree.SelectedNode = this.contactsTree.Nodes[0];
                this.contactsTree.SelectedNode.Nodes.Add(this.m_friendsTree[p_friendName]);
            }
        }

        public void OnUserLogOut(string p_friendName)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new UserLogOut(OnUserLogOut), p_friendName);
            }
            else
            {
                this.contactsTree.Nodes.Remove(this.m_friendsTree[p_friendName]);
                this.contactsTree.SelectedNode = this.contactsTree.Nodes[1];
                this.contactsTree.SelectedNode.Nodes.Add(this.m_friendsTree[p_friendName]);
            }
        }

        public void OnPMChanaged(string p_friendName, string p_newPM)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new PMChanaged(OnPMChanaged), p_friendName, p_newPM);
            }
            else
            {
                this.m_Contacts[p_friendName].UserPM = p_newPM;
                MessageBox.Show(p_friendName + " changed his Pm to " + p_newPM);
            }
        }

        public void OnStatusChanaged(string p_friendName, Status p_status)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new StatusChanaged(OnStatusChanaged), p_friendName, p_status);
            }
            else
            {
                this.m_Contacts[p_friendName].UserStatus = p_status;
            }
        }

        public void OnAvatarChanaged(string p_friendName, int p_newAvatar)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new AvatarChanaged(OnAvatarChanaged), p_friendName, p_newAvatar);
            }
            {
                this.m_Contacts[p_friendName].UserPersonalInfo.AvatarID = p_newAvatar;
                if (this.m_ChatForms.ContainsKey(p_friendName))
                {
                    this.m_ChatForms[p_friendName].OnFriendAvatarChanged(p_newAvatar);
                }
            }
        }

        public void OnReceiveIM(InstantMessage p_instantMessage)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new ReceiveIM(OnReceiveIM), p_instantMessage);
            }
            else
            {
                if (this.m_ChatForms.ContainsKey(p_instantMessage.SenderName))
                {
                    this.m_ChatForms[p_instantMessage.SenderName].OnReceiveNewMessage(p_instantMessage);
                }
                else
                {
                    ChatForm newChatForm = new ChatForm(this.m_controller, p_instantMessage,m_Contacts[p_instantMessage.SenderName].UserPersonalInfo.AvatarID);
                    newChatForm.Register(this);
                    this.m_ChatForms.Add(p_instantMessage.SenderName, newChatForm);
                    newChatForm.Show();
                }
            }
        }

        public void OnReceieveAddRequest(UserInfo p_senderInfo)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new ReceiveAddRequest(OnReceieveAddRequest), p_senderInfo);
            }
            else
            {
                ContactForm contactForm = new ContactForm(this.m_controller,p_senderInfo);
                contactForm.Show();
            }
        }
     
        public void OnReceiveAudioRequest(string p_senderName)
        {
            if (this.InvokeRequired)
            {
                BeginInvoke(new ReceiveAudioConvRequest(OnReceiveAudioRequest), p_senderName);
            }
            else
            {
                throw new NotImplementedException();
            }
        }

        public void OnError(string p_errorMsg)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new NotifyError(OnError), p_errorMsg);
            }
            else
            {
                MessageBoxButtons button = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Error;
                MessageBox.Show(p_errorMsg, "Error", button, icon);
            }
        }

        public void OnAddNewFriend(UserInfo p_friendInfo)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new AddNewFriend(OnAddNewFriend), p_friendInfo);
            }
            else
            {
                this.m_Contacts.Add(p_friendInfo.UserName, p_friendInfo);
                TreeNode friend = new TreeNode(p_friendInfo.UserName);
                friend.Name = p_friendInfo.UserName;
                if (p_friendInfo.UserStatus != Status.Offline)
                {
                    this.contactsTree.SelectedNode = this.contactsTree.Nodes[0];
                    this.contactsTree.SelectedNode.Nodes.Add(friend);
                }
                else
                {
                    this.contactsTree.SelectedNode = this.contactsTree.Nodes[1];
                    this.contactsTree.SelectedNode.Nodes.Add(friend);
                }
            }
        }

        public void OnTypingMessage(string p_friendName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new IsTypingMessage(OnTypingMessage), p_friendName);
            }
            else
            {
                if (this.m_ChatForms.ContainsKey(p_friendName))
                {
                    m_ChatForms[p_friendName].OnTypingMessage();
                }
            }
        }
  
        #region File Transfer

        public void OnReceiveFileTransferRequest(string p_fileName, string p_senderName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ReceiveFileTransferRequest(OnReceiveFileTransferRequest), p_fileName, p_senderName);
            }
            else
            {
                int friendID = this.m_Contacts[p_senderName].UserID;
                RequestFileTransfer fileTransferRequest = new RequestFileTransfer(m_controller, friendID, p_fileName);
                fileTransferRequest.Show();
            }
        }

        #endregion

        #region Video Conversation
        public void OnStartVideoConv(string p_friendName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StartVideoConversation(OnStartVideoConv), p_friendName);
            }
            else
            {
                if (this.m_ChatForms.ContainsKey(p_friendName))
                {
                    this.m_ChatForms[p_friendName].OnStartVideoConversation();
                }
                else
                {
                    ChatForm newChatForm = new ChatForm(this.m_controller, m_Contacts[p_friendName].UserID, m_Contacts[p_friendName].UserPersonalInfo.AvatarID,p_friendName);
                    newChatForm.Register(this);
                    this.m_ChatForms.Add(p_friendName, newChatForm);
                    newChatForm.Show();
                    newChatForm.OnStartVideoConversation();
                }
            }
        }
        public void OnReceiveVideoRequest(string p_senderName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ReceiveVideoConvRequest(OnReceiveVideoRequest), p_senderName);
            }
            else
            {
                DialogResult result = MessageBox.Show(p_senderName + " wants to start a video conversation with you", "Video Conversation Request", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (result == DialogResult.OK)
                    this.m_controller.ReplyVideoConversationRequest(true, this.m_Contacts[p_senderName].UserID);
                else
                    this.m_controller.ReplyVideoConversationRequest(false, this.m_Contacts[p_senderName].UserID);
            }
        }
        public void OnEndVideoConv(string p_friendName)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StartVideoConversation(OnStartVideoConv), p_friendName);
            }
            else
            {
                if (this.m_ChatForms.ContainsKey(p_friendName))
                {
                    this.m_ChatForms[p_friendName].OnEndVideoConversation();
                }
            }
        }
        #endregion

        #endregion

        #region IChatWindowObserver Members
        public void OnChatWindowClosed(string p_windowName)
        {
            this.m_ChatForms.Remove(p_windowName);
        }
        #endregion

        #region Controls Event Handlers
        private void addFriend_btn_Click(object sender, EventArgs e)
        {
            AddContactForm contactForm = new AddContactForm();
            contactForm.ShowDialog();
            if (contactForm.addAccept)
            {
                this.m_controller.SendAddRequest(contactForm.contactName);
            }
        }
        private void status_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            Status newStatus = this.GetSelectedStatus();
            if (newStatus != this.m_controller.User.UserInfo.UserStatus)
            {
                this.m_controller.ChangeStatus(newStatus);
            }
        }
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
        private void profilePic_picbox_Click(object sender, EventArgs e)
        {
            int currentAvatar = this.m_userInfo.UserPersonalInfo.AvatarID;
            AvatarsForm avatarForm = new AvatarsForm(m_avatarsList[currentAvatar], currentAvatar);
            avatarForm.ShowDialog();
            if (avatarForm.selectedavatarID != currentAvatar)
            {
                currentAvatar = avatarForm.selectedavatarID;
                this.profilePic_picbox.Image = m_avatarsList[currentAvatar];
                this.m_controller.ChanageAvatar(currentAvatar);
                foreach (KeyValuePair<string,ChatForm> ChatWindow in this.m_ChatForms)
                {
                    ChatWindow.Value.OnUserAvatarChanged(currentAvatar);
                }
            }
        }
        private void contactsTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (!this.contactsTree.Nodes[0].IsSelected && !this.contactsTree.Nodes[1].IsSelected)
            {
                string selectedFriend = this.contactsTree.SelectedNode.Name;
                int friendID = this.m_Contacts[selectedFriend].UserID;
                if (!this.m_ChatForms.ContainsKey(selectedFriend))
                {
                    ChatForm newChatForm = new ChatForm(m_controller, friendID, this.m_Contacts[selectedFriend].UserPersonalInfo.AvatarID, selectedFriend);
                    this.m_ChatForms.Add(selectedFriend, newChatForm);
                    newChatForm.Register(this);
                    newChatForm.Show();
                }
                else
                {
                    this.m_ChatForms[selectedFriend].Show();
                }
            }
        }
        private void MessengerForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.m_controller.SignOut();
            Application.Restart();
        }
        private void pm_txtbox_Leave(object sender, EventArgs e)
        {
            if (this.m_controller.User.UserInfo.UserPM != this.pm_txtbox.Text)
                this.m_controller.ChanagePM(this.pm_txtbox.Text);
        }
        #endregion

    }
}