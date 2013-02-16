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
using MessengerService.Model.DataContracts;
using MessengerUser.Model;
using System.Threading;
using System.Media;
using MessengerUser.View;
using MessengerService.Model;
using Emgu.CV;
using Emgu.CV.Structure;
using System.Drawing.Imaging;


namespace MessengerUser.Controller
{
    public partial class ChatForm : Form, IChatWindowSubject, IChatWindow
    {
        #region Attributes
        private UserController m_userControler;
        private int m_friendID;
        private string m_friendName;
        private int m_userID;
        private string m_userName;
        private int m_userAvatarID;
        private int m_friendAvatarID;
        private Status m_friendStatus;
        private Status m_userStatus;
        private List<IChatWindowObserver> m_observers = new List<IChatWindowObserver>();
        private Dictionary<string, Image> emoticons;
        private bool InVideoConvMode = false;
        private Thread SendVideoStreamThread;
        private Thread ReceiveVideoStreamThread;
        private Capture _Camera;
        private System.Timers.Timer ReceiverTimer = new System.Timers.Timer(100);
        private System.Timers.Timer SenderTimer = new System.Timers.Timer(100);

        #endregion
        
        #region Delegets
        delegate void ReceiveMessage(InstantMessage p_instantMessage);
        delegate void IsWritingMessage();
        delegate void StartVideoConversation();
        #endregion

        #region Constructors
        public ChatForm(UserController p_userController, InstantMessage p_intialMessage, int p_friendAvatarID)
        {
            
            InitializeComponent();
            

            LoadEmoticons();
            Control.CheckForIllegalCrossThreadCalls = false;
            m_userControler = p_userController;

            m_friendName = p_intialMessage.SenderName;
            m_friendID = p_intialMessage.SenderID;
            m_userID = p_intialMessage.ReceiverID;
            m_userName = p_intialMessage.ReceiverName;

            m_userAvatarID = m_userControler.User.UserInfo.UserID;
            m_friendAvatarID = p_friendAvatarID;

            m_friendStatus = this.m_userControler.User.FriendList[m_friendID].UserStatus;
            m_userStatus = this.m_userControler.User.UserInfo.UserStatus;

            this.CheckFileTransferAvailablity();
            friendAvatar_pb.Image = MessengerForm.m_avatarsList[p_friendAvatarID];
            userAvatar_pb.Image = MessengerForm.m_avatarsList[this.m_userControler.User.UserInfo.UserPersonalInfo.AvatarID];
            this.Text = m_friendName;
            this.OnReceiveNewMessage(p_intialMessage);

           // ReceiverTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReceiveVideoStream);
           // SenderTimer.Elapsed += new System.Timers.ElapsedEventHandler(SendVideoStream);

        }
        public ChatForm(UserController p_userController, int p_friendID, int p_friendAvatarID, string p_friendName)
        {
            InitializeComponent();
            
            LoadEmoticons();
            Control.CheckForIllegalCrossThreadCalls = false;
            m_userControler = p_userController;
            m_friendName = p_friendName;
            m_friendID = p_friendID;
            m_userID = this.m_userControler.UserID;
            m_userName = this.m_userControler.User.UserInfo.UserName;

            m_userAvatarID = m_userControler.User.UserInfo.UserID;
            m_friendAvatarID = p_friendAvatarID;

            m_friendStatus = this.m_userControler.User.FriendList[m_friendID].UserStatus;
            m_userStatus = this.m_userControler.User.UserInfo.UserStatus;
            this.CheckFileTransferAvailablity();

            this.Text = m_friendName;
            friendAvatar_pb.Image = MessengerForm.m_avatarsList[p_friendAvatarID];
            userAvatar_pb.Image = MessengerForm.m_avatarsList[this.m_userControler.User.UserInfo.UserPersonalInfo.AvatarID];

            //ReceiverTimer.Elapsed += new System.Timers.ElapsedEventHandler(ReceiveVideoStream);
            //SenderTimer.Elapsed += new System.Timers.ElapsedEventHandler(SendVideoStream);

        }
        #endregion

        #region IChatWindowSubject Members
        public void Register(IChatWindowObserver p_observer)
        {
            this.m_observers.Add(p_observer);
        }
        public void UnRegister(IChatWindowObserver p_observer)
        {
            this.m_observers.Remove(p_observer);
        }
        public void NotifyClosed(string p_windowName)
        {
            if (this.InVideoConvMode)
            {
                this.m_userControler.SendEndVideoConversation(this.m_friendID);
            }
            foreach (IChatWindowObserver observer in this.m_observers)
            {
                observer.OnChatWindowClosed(this.Text);
            }
        }
        #endregion

        #region IChatWindow Members
        public void OnReceiveNewMessage(InstantMessage p_message)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new ReceiveMessage(OnReceiveNewMessage), p_message);
            }
            else
            {
                chat_txtbox.AppendText(p_message.SenderName + " says: " + p_message.MessageContent + "\r\n");
                chat_txtbox.ScrollToCaret();
                if (!this.Focused)
                {
                    SystemSounds.Beep.Play();
                }

            }
        }
        public void OnFriendAvatarChanged(int p_newAvatar)
        {
            this.friendAvatar_pb.Image = MessengerForm.m_avatarsList[p_newAvatar];
            this.m_friendAvatarID = p_newAvatar;
        }
        public void OnUserAvatarChanged(int p_newAvatar)
        {
            this.userAvatar_pb.Image = MessengerForm.m_avatarsList[p_newAvatar];
            this.m_userAvatarID = p_newAvatar;
            this.CheckFileTransferAvailablity();
        }
        public void OnUserStatusChanged(Status p_newStatus)
        {
            this.m_userStatus = p_newStatus;
            this.CheckFileTransferAvailablity();
        }
        public void OnFriendStatusChanged(Status p_newStatus)
        {
            this.m_friendStatus = p_newStatus;
        }
        public void OnTypingMessage()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new IsWritingMessage(OnTypingMessage));
            }
            else
            {
                Thread TypingMessageThread = new Thread(this.TyptingMessageHandler);
                TypingMessageThread.Start();
            }
        }
        #endregion

        #region Methods
        private void LoadEmoticons()
        {
            emoticons = new Dictionary<string, Image>();
            emoticons.Add("AngelSmile1", global::MessengerUser.Properties.Resources.AngelSmile1);
            emoticons.Add("AngrySmile1", global::MessengerUser.Properties.Resources.AngrySmile1);
            emoticons.Add("Beer1", global::MessengerUser.Properties.Resources.Beer1);
            emoticons.Add("BrokenHeart1", global::MessengerUser.Properties.Resources.BrokenHeart1);
            emoticons.Add("ConfusedSmile1", global::MessengerUser.Properties.Resources.ConfusedSmile1);
            emoticons.Add("CrySmile1", global::MessengerUser.Properties.Resources.CrySmile1);
            emoticons.Add("DevilSmile1", global::MessengerUser.Properties.Resources.DevilSmile1);
            emoticons.Add("EmbarassedSmile1", global::MessengerUser.Properties.Resources.EmbarassedSmile1);
            emoticons.Add("ThumbsUp1", global::MessengerUser.Properties.Resources.ThumbsUp1);
        }
        private void SendMessage()
        {
            if (this.message_txtbox.Text != string.Empty)
            {
                InstantMessage newIM = new InstantMessage(message_txtbox.Text);
                newIM.SenderID = this.m_userID;
                newIM.ReceiverID = this.m_friendID;
                newIM.SenderName = this.m_userName;
                newIM.ReceiverName = this.m_friendName;
                newIM.SendingTime = DateTime.Now;
                this.m_userControler.SendIM(newIM);
                this.message_txtbox.Text = "";
                chat_txtbox.AppendText(this.m_userName + " says: " + newIM.MessageContent + "\r\n");
                chat_txtbox.ScrollToCaret();
            }
        }
        private void CheckFileTransferAvailablity()
        {
            if (this.m_friendStatus == Status.Offline || this.m_userStatus == Status.Offline)
                this.sendFile_btn.Enabled = false;
        }
        object syncTyping = new object();
        public void TyptingMessageHandler()
        {
            lock (syncTyping)
            {
                iswriting_lbl.Text = this.m_friendName + " is typing a message...";
                for (int i = 0; i < 3; i++)
                {
                    //iswriting_lbl.Text += ".";
                    Thread.Sleep(40);
                }
                iswriting_lbl.Text = "";
            }
        }
        #endregion

        #region Controls Event Handlers
        private void textformat_btn_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();
            if (fontDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.message_txtbox.Font = fontDialog.Font;
            }
        }
        private void send_btn_Click(object sender, EventArgs e)
        {
            this.SendMessage();
        }
        private void message_txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
                this.SendMessage();
        }
        private void ChatForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _Camera.Dispose();
            if(this.SendVideoStreamThread.IsAlive)
            this.SendVideoStreamThread.Abort();
            if (this.ReceiveVideoStreamThread.IsAlive)
            this.ReceiveVideoStreamThread.Abort();

            NotifyClosed(this.Text);
            
        }
        private void message_txtbox_TextChanged(object sender, EventArgs e)
        {
            if (this.chat_txtbox.Text.Trim() != string.Empty)
                this.m_userControler.SendIsTypingMessage(this.m_friendID);
        }
        private void message_txtbox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                message_txtbox.Clear();
        }
        private void sendFile_btn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string filePath = dlg.FileName;
                m_userControler.SendFileRequest(filePath, m_friendID);
            }
        }
        private void smileybtn_Click(object sender, EventArgs e)
        {
            message_txtbox.InsertImage(this.emoticons[((Button)sender).Name]);
        }
        private void video_btn_Click(object sender, EventArgs e)
        {
            m_userControler.SendVideoConversationRequest(m_friendID);
        }
        #endregion

        public void OnStartVideoConversation()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StartVideoConversation(OnStartVideoConversation));
            }
            else
            {
                this.ConfigVideoConv(true);
                

                ThreadStart SendVideoFrames = delegate { SendVideoStream(); };
                SendVideoStreamThread = new Thread(SendVideoFrames);
                SendVideoStreamThread.Start();

                //Start Receive
                ThreadStart ReceiveVideoFrames = delegate { ReceiveVideoStream(); };
                ReceiveVideoStreamThread = new Thread(ReceiveVideoFrames);
                ReceiveVideoStreamThread.Start();
            }
        }

        private void ReceiveVideoStream()
        {
            while (true)
            {
                byte[] frameStream = this.m_userControler.ReceiveFrame(this.m_friendID);
                if (frameStream != null)
                {
                    ImageConverter ic = new ImageConverter();
                    Image img = (Image)ic.ConvertFrom(frameStream);
                    Bitmap bitmap1 = new Bitmap(img);
                    this.friendVideoScreen.Image = bitmap1;
                }
                
                Thread.SpinWait(2);
            }
        }

        private void SendVideoStream()
        {
            if (_Camera == null)
                _Camera = new Capture(0);
            while (true)
            {
                Image<Bgr, byte> frame = _Camera.QuerySmallFrame();
                Bitmap currentframe = (Bitmap)frame.ToBitmap().GetThumbnailImage(300, 300, null, IntPtr.Zero); // -2- //

                byte[] data = (byte[])System.ComponentModel.TypeDescriptor.GetConverter(currentframe).ConvertTo(currentframe, typeof(byte[]));

                this.m_userControler.SendFrame(data, this.m_friendID);
               
                this.userVideoScreen.Image = currentframe;
                Thread.SpinWait(2);
            }
        }

        private void ConfigVideoConv(bool p_config)
        {
            if (p_config)
                this.Size = new Size(1019, 535);
            else
                this.Size = new Size(633, 535);
            this.InVideoConvMode = p_config;
        }

        public void OnEndVideoConversation()
        {
            this.ConfigVideoConv(false);

            SendVideoStreamThread.Abort();
            ReceiveVideoStreamThread.Abort();
            _Camera.Dispose();
        }

        private void stopVideo_btn_Click(object sender, EventArgs e)
        {
            this.ConfigVideoConv(false);

            SendVideoStreamThread.Abort();
            ReceiveVideoStreamThread.Abort();
            _Camera.Dispose();
            this.m_userControler.SendEndVideoConversation(this.m_friendID);
        }

    }
}