using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerUser.Model;
using System.ServiceModel;
using MessengerService.Model;
using MessengerService.Model.DataContracts;
using System.IO;
using System.Threading;
using FileServer.Services;
using System.Diagnostics;
using System.Drawing;

namespace MessengerUser.Controller
{
   
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class UserController : IMessengerCallback, IMainSubject
    {
        #region Attribues
        private User m_user;
        private int m_userID;
        private IMessengerService m_proxy;
        private bool m_isConnected = false;
        private string m_receivedFilePath = "D:\\";
        private List<IMainObserver> m_observers;
        private NetTcpBinding m_netTCP;
        private EndpointAddress m_endpoint;
        private bool inVideoConv = false;
        private int numOfVideoReceivers = 0;
        #endregion

        #region Properties
        public bool InVideoConv
        {
            get { return inVideoConv; }
            set { inVideoConv = value; }
        }
        public NetTcpBinding NetTCP
        {
            get { return m_netTCP; }
            set { m_netTCP = value; }
        }
        public EndpointAddress Endpoint
        {
            get { return m_endpoint; }
            set { m_endpoint = value; }
        }
        public string ReceivedFilePath
        {
            get { return m_receivedFilePath; }
            set { m_receivedFilePath = value; }
        }
        public int UserID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        public User User
        {
            get { return m_user; }
            set { m_user = value; }
        }
        public IMessengerService Proxy
        {
            get { return m_proxy; }
            set { m_proxy = value; }
        }
        public bool IsConnected
        {
            get { return m_isConnected; }
            set { m_isConnected = value; }
        }
        public List<IMainObserver> Observers
        {
            get { return m_observers; }
            set { m_observers = value; }
        }
        #endregion
        
        #region Constructor
        public UserController()
        {
            this.m_user = new User();
            this.m_observers = new List<IMainObserver>();
        }
        #endregion

        #region IMessengerCallback Members

        #region File Transfer
          
        public void OnReceiveFile(string p_fileName, int p_senderID)
        {
            string filePath = Path.Combine(this.ReceivedFilePath, p_fileName);
            using (FileStream output = new FileStream(filePath, FileMode.Create))
            {
                Stream downloadStream;

                using (FileTransferer client = new FileTransferer())
                {
                    downloadStream = client.GetFile(p_fileName);
                }
                StreamExtensions.CopyTo(downloadStream, output);
            }
            this.Proxy.ReceiveFileAcknowledge(p_fileName, p_senderID, this.UserID);
        }
        
        public void StartFileTransfer(string p_fileName, int p_sender)
        {
            ThreadStart FileTransferThreadStart = delegate { OnSendFile(p_fileName,p_sender);};
            Thread FileTransferThread = new Thread(FileTransferThreadStart);
            FileTransferThread.Start();
        }
        
        public void OnSendFile(string p_filePath, int p_fileReceiver)
        {
            string FileName = Path.GetFileName(p_filePath);
            using (Stream uploadStream = new FileStream(p_filePath, FileMode.Open))
            {
                using (FileTransferer client = new FileTransferer())
                {
                    client.PutFile(new FileUploadMessage() { VirtualPath =FileName , DataStream = uploadStream });
                }
            }
            this.Proxy.SentFileAcknowledge(FileName,this.UserID, p_fileReceiver);
        }
        
        public void ReceiveFileRequest(string p_fileName, int p_sender)
        {
            User.NotifyReceiveFileRequest(p_fileName, p_sender);
        }

        public void ReceiveFile(string p_fileName, int p_sender)
        {
            ThreadStart FileTransferThreadStart = delegate { OnReceiveFile(p_fileName, p_sender); };
            Thread FileTransferThread = new Thread(FileTransferThreadStart);
            FileTransferThread.Start();
        }

        public void TransferFailed(string p_fileName, int p_receiverID)
        {
            string filePath = Path.Combine(this.ReceivedFilePath, p_fileName);
        
            using (FileTransferer client = new FileTransferer())
            {
                client.FileTransferFailed(p_fileName);
            }
            
            this.User.NotifyFileTransferFailed(p_fileName, p_receiverID);
        }
        
        public void TransferCompleted(string p_fileName, int p_receiverID)
        {
            using (FileTransferer client = new FileTransferer())
            {
                client.FileDownloadedAcknowledge(p_fileName);
            }
            
            this.User.NotifyFileTransferSuccessed(p_fileName, p_receiverID);
        }

        #endregion

        #region Video Conversation

        public void ReceiveVideoRequest(int p_senderId)
        {
            User.NotifyReceiveVideoRequest(p_senderId);
        }
 
        public void EndVideoConv(int p_senderID)
        {
            this.User.NotifyEndVideoConv(p_senderID);
            this.inVideoConv = false;
            //this.StopVideoStreaming(p_senderID);
        }

        public void StartVideoConv(int p_senderID)
        {
            this.User.NotifyStartVideoConv(p_senderID);
        }
        #endregion

        #region Notifications
        public void NotifyPMChanged(string p_newPM, int p_friendID)
        {
            this.User.NotfiyPMChanaged(p_friendID, p_newPM);
        }
        public void NotifyLoggedIn(int p_friendID, Status p_newStatus)
        {
            this.User.NotfiyLogIn(p_friendID, p_newStatus);
        }
        public void NotifyStatusChanged(int p_friendID, Status p_newStatus)
        {
            this.User.NotfiyStatusChanged(p_friendID, p_newStatus);
        }
        public void NotifyLoggedOff(int p_friendID)
        {
            this.User.NotifyLoggedOut(p_friendID);
        }
        public void ErrorNotification(string p_errorMsg)
        {
            this.User.NotifyError(p_errorMsg);
        }
        #endregion

        public void UpdateContactList(List<UserInfo> p_updatedFriendsInfo)
        {
            this.m_user.NotfiyUpdateContactList(p_updatedFriendsInfo);
        }
        public void ReceiveIM(InstantMessage p_instantMessage)
        {
            this.User.NotifyReceiveIM(p_instantMessage);
        }
        public void ReceiveAddRequest(UserInfo p_senderInfo)
        {
            this.User.NotifyReceiveAddRequest(p_senderInfo);
        }
        public void AddNewFriend(UserInfo p_newFriendInfo)
        {
            this.User.NotfiyAddNewFriend(p_newFriendInfo);
        }
        public void ReceiveIsTypingMessage(int p_friendID)
        {
            this.User.NotfiyReceiveIsTyping(p_friendID);
        }
     
        #endregion

        #region IMainSubject Members

        public void SignIn(AccountInfo p_accountInfo, Status p_loginStatus)
        {
            int ServerRespond = -1;
            try
            {
                ServerRespond = this.Proxy.LogIn(p_accountInfo, p_loginStatus);
            }
            catch
            {
                this.NotifyError("Error in the connection with server");
                return;
            }

            if (ServerRespond == -1)
            {
                this.NotifyError("Invalid Data");
            }
            else if (ServerRespond == 0)
            {
                this.NotifyError("Wrong Password!");
            }
            else if (ServerRespond == -2)
            {
                this.NotifyError("This account is signed in from another place..");
            }
            else
            {
                UserInfo userInfo = this.Proxy.GetUserInfo(ServerRespond);
                this.m_userID = ServerRespond;
                if (p_loginStatus != Status.Offline)
                    this.Proxy.NotifyILoggedIn(ServerRespond, p_loginStatus);
                List<UserInfo> friendList = this.Proxy.GetContactListUpdates(ServerRespond);
                this.User.SignIn(userInfo, friendList);
                this.NotifySignInSucceed();
            }
        }
        public void ConnectToServer(NetTcpBinding nettcp, EndpointAddress address)
        {
            try
            {
                nettcp.ReceiveTimeout = TimeSpan.MaxValue;
                nettcp.SendTimeout = TimeSpan.MaxValue;
                //nettcp.TransferMode = TransferMode.Streamed;
                //nettcp.MaxBufferSize = int.MaxValue;
                //nettcp.MaxConnections = int.MaxValue;
                //nettcp.MaxReceivedMessageSize = long.MaxValue;


                this.NetTCP = nettcp;
                this.Endpoint = address;

                //nettcp.TransferMode = TransferMode.Buffered;
                //nettcp.SendTimeout = TimeSpan.MaxValue;

                this.m_proxy = DuplexChannelFactory<IMessengerService>.CreateChannel(this, nettcp, address);
                int test = this.m_proxy.TestConnection();
                if (test != 1)
                {
                    this.NotifyError("Error, can't connect to the server!");
                    return;
                }
                this.m_isConnected = true;
            }
            catch
            {
                this.NotifyError("Error, can't connect to the server!");
            }

        }
        public void Subscribe(AccountInfo p_userAccountInfo, UserPersonalInfo p_userPersonalInfo)
        {
            if (!this.IsConnected)
            {
                this.NotifyError("Error, try to re-connect..");
                return;
            }
            int ServerRespond = this.Proxy.RegisterNewUser(p_userAccountInfo, p_userPersonalInfo);
            if (ServerRespond == 0)
            {
                this.NotifyError("Username already exist..");
            }
            else if (ServerRespond == -1)
            {
                this.NotifyError("Invalid data..");
            }
            else if (ServerRespond == -2)
            {
                this.NotifyError("An error happend during registeration");
            }
            else
                this.NotifyRegistrationSucceed();
        }
        public void RegisterMainObserver(IMainObserver p_observer)
        {
            this.Observers.Add(p_observer);
        }
        public void UnRegisterMainObserver(IMainObserver p_observer)
        {
            this.Observers.Remove(p_observer);
        }
        public void NotifyError(string p_errorMsg)
        {
            foreach (IMainObserver observer in this.Observers)
            {
                observer.OnError(p_errorMsg);
            }
        }
        public void NotifyRegistrationSucceed()
        {
            foreach (IMainObserver observer in this.Observers)
            {
                observer.OnRegistrationSucceed();
            }
        }
        public void NotifySignInSucceed()
        {
            foreach (IMainObserver observer in this.Observers)
            {
                observer.OnSignInSucceed();
            }
        }

        #endregion

        #region Methods
        public void UpdateContractList()
        {
            if (this.User.IsLoggedIn)
            {
                List<UserInfo> FriendList = this.Proxy.GetContactListUpdates(this.UserID);
                this.User.NotfiyUpdateContactList(FriendList);
            }
            else
            {
                this.User.NotifyError("I haven't logged in yet");
            }
        }
        public void ChangeStatus(Status p_newStatus)
        {
            if (this.User.IsLoggedIn)
            {
                if (this.User.UserInfo.UserStatus == Status.Offline)
                    this.Proxy.NotifyILoggedIn(this.UserID, p_newStatus);
                else
                {
                    this.Proxy.ChangeStatus(p_newStatus, this.UserID);
                }
                this.User.UserInfo.UserStatus = p_newStatus;
            }
            else
            {
                this.User.NotifyError("You haven't logged in yet");
            }
        }
        public void ChanagePM(string p_newPM)
        {
            if (this.IsConnected)
            {
                this.Proxy.ChangePersonalMessage(p_newPM, this.UserID);
                this.User.UserInfo.UserPM = p_newPM;
            }
            else
            {
                this.User.NotifyError("You haven't sign in yet!");
            }
        }
        public void AddNewFriend(string p_friendName)
        {
            if (this.User.IsLoggedIn)
            {
                this.Proxy.SendAddRequest(p_friendName, this.UserID);
            }
            else
            {
                this.User.NotifyError("You haven't sign in yet!");
            }
        }
        public void ReplyAddRequest(int p_senderID, bool p_reply)
        {
            if (this.User.IsLoggedIn)
            {
                this.Proxy.ReplyAddRequest(this.UserID, p_senderID,p_reply);
            }
            else
            {
                this.User.NotifyError("You haven't sign in yet!");
            }
        }
        public void SendIM(InstantMessage p_IM)
        {
            if (this.User.IsLoggedIn)
            {
                this.Proxy.SendIM(p_IM);
            }
            else
            {
                this.User.NotifyError("You haven't sign in yet!");
            }
        }
        public void Register(IUserObserver p_userObserver)
        {
            this.m_user.Register(p_userObserver);
        }
        public void ChanageAvatar(int currentAvatar)
        {
            if (this.IsConnected)
            {
                this.m_proxy.ChangeAvatar(currentAvatar, this.UserID);
                this.User.UserInfo.UserPersonalInfo.AvatarID = currentAvatar;
            }
        }
        public void NotifyAvatarChanged(int p_friendID, int p_newAvatarID)
        {
            this.User.NotfiyAvatarChanaged(p_friendID, p_newAvatarID);
        }
        public void ChangeSaveFilePath(string p_newPath)
        {
            this.m_receivedFilePath = p_newPath;
        }
        public void SignOut()
        {
            this.Proxy.LogOut(this.UserID);
            this.IsConnected = false;
        }
        public void StartUp()
        {
            this.Proxy.UserStartUp(this.UserID);
        }
        public void SendIsTypingMessage(int p_friendID)
        {
            this.Proxy.SendIsTypingMessage(this.m_userID, p_friendID);
        }
        
        #region File Transfer
        public void SendFileRequest(string p_filePath, int p_receiverID)
        {
            if (this.IsConnected)
            {
                this.Proxy.RequestSendFile(p_filePath, this.UserID, p_receiverID);
            }

        }
        public void SendAddRequest(string p_contactName)
        {
            if (this.IsConnected)
            {
                this.Proxy.SendAddRequest(p_contactName, this.UserID);
            }
        }
        public void ReplySendFile(string p_filePath, int p_senderID, bool p_reply)
        {
            this.Proxy.ReplySendFileRequest(p_filePath, p_reply, p_senderID, this.UserID);
        }
        #endregion

        #region Video Conversation
        public void SendVideoConversationRequest(int m_friendID)
        {
            if (this.IsConnected)
            {
                this.Proxy.SendVideoRequest(this.UserID, m_friendID);
            }
        }
        public void ReplyVideoConversationRequest(bool p_reply, int p_senderID)
        {
            this.Proxy.ReplyVideoRequest(p_reply, p_senderID, this.UserID);
        }
        #endregion

        #endregion

        public void SendEndVideoConversation(int p_friendID)
        {
            //this.StopVideoStreaming(p_friendID);
            this.Proxy.EndVideoConversation(this.UserID, p_friendID);
        }

        private void StopVideoStreaming(int p_friendID)
        {
          using (VideoCapturer client = new VideoCapturer())
          {
              client.StopVideoStreaming(new KeyValuePair<int, int>(this.UserID, p_friendID));
          }
        }
        public byte[] ReceiveFrame(int p_friendID)
        {
            byte[] frame;
            using (VideoCapturer client = new VideoCapturer())
            {
                KeyValuePair<int, int> sender_receiver = new KeyValuePair<int, int>(p_friendID, this.UserID);
                frame = client.ReceiveFrame(sender_receiver);
            }
            return frame;
        }
        public void SendFrame(byte[] p_frame, int p_friendID)
        {
            using (VideoCapturer client = new VideoCapturer())
            {
                KeyValuePair<int, int> sender_receiver = new KeyValuePair<int, int>(this.UserID, p_friendID);
                client.SendFrame(new VideoFrame() { Contract = sender_receiver, FrameStream = p_frame });
            }
        }
    }
}