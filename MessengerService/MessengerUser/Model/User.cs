using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MessengerService.Model;
using MessengerService;
using System.IO;
using MessengerService.Model.DataContracts;

namespace MessengerUser.Model
{
    public class User : IUserSubject
    {
        #region Attributes
        private List<IUserObserver> m_observers = new List<IUserObserver>();
        private UserInfo m_userInfo;
        private Dictionary<int, UserInfo> m_friendList = new Dictionary<int, UserInfo>();
        private bool m_isLoggedIn = false;
        #endregion
        
        #region Properties
        public UserInfo UserInfo
        {
            get { return m_userInfo; }
            set { m_userInfo = value; }
        }
        public List<IUserObserver> Observers
        {
            get { return m_observers; }
            set { m_observers = value; }
        }
        public Dictionary<int, UserInfo> FriendList
        {
            get { return m_friendList; }
            set { m_friendList = value; }
        }
        public bool IsLoggedIn
        {
            get { return m_isLoggedIn; }
            set { m_isLoggedIn = value; }
        }
        #endregion

        #region Constructor
        public User()
        {
            this.m_friendList = new Dictionary<int, UserInfo>();
            this.m_observers = new List<IUserObserver>();
        }
        #endregion

        #region IUserSubject Members
        public void Register(IUserObserver observer)
        {
            m_observers.Add(observer);
        }
        public void UnRegister(IUserObserver observer)
        {
            m_observers.Remove(observer);
        }
        public void NotfiyUpdateContactList(List<UserInfo> p_updatedFriendsInfo)
        {
            throw new NotImplementedException();
        }
        public void NotfiyAddNewFriend(UserInfo p_friendInfo)
        {
            this.FriendList.Add(p_friendInfo.UserID, p_friendInfo);
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnAddNewFriend(p_friendInfo);
            }
        }
        public void NotifyError(string p_errorMsg)
        {
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnError(p_errorMsg);
            }
        }
        public void NotfiyLogIn(int p_friendID, Status p_loginStatus)
        {
            this.FriendList[p_friendID].UserStatus = p_loginStatus;
            string friendName = this.FriendList[p_friendID].UserName;
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnUserLogIn(friendName, p_loginStatus);
            }
        }
        public void NotfiyStatusChanged(int p_friendID, Status p_newStatus)
        {
            this.FriendList[p_friendID].UserStatus = p_newStatus;
            string friendName = this.FriendList[p_friendID].UserName;
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnStatusChanaged(friendName, p_newStatus);
            }
        }
        public void NotifyReceiveIM(InstantMessage p_instantMessage)
        {
            p_instantMessage.SenderName = this.FriendList[p_instantMessage.SenderID].UserName;
            p_instantMessage.ReceiverName = this.UserInfo.UserName;
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnReceiveIM(p_instantMessage);
            }
        }
        public void NotfiyPMChanaged(int p_friendID, string p_newPM)
        {
            string friendName = this.FriendList[p_friendID].UserName;
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnPMChanaged(friendName, p_newPM);
            }
        }
        public void NotifyReceiveAddRequest(UserInfo p_senderInfo)
        {
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnReceieveAddRequest(p_senderInfo);
            }
        }
        public void NotifyLoggedOut(int p_friendID)
        {
            string friendName = this.FriendList[p_friendID].UserName;
            foreach (IUserObserver Observer in this.Observers)
            {
                Observer.OnUserLogOut(friendName);
            }
        }   
        public void NotfiyAvatarChanaged(int p_friendID, int p_newAvatarID)
        {
            this.FriendList[p_friendID].UserPersonalInfo.AvatarID = p_newAvatarID;
            string friendName = this.FriendList[p_friendID].UserName;
            foreach (IUserObserver observer in this.Observers)
            {
                observer.OnAvatarChanaged(friendName, p_newAvatarID);
            }
        }
        public void NotfiyReceiveIsTyping(int p_friendID)
        {
            string friendName = this.FriendList[p_friendID].UserName;
            
            foreach (IUserObserver observer in this.Observers)
            {
                observer.OnTypingMessage(friendName);
            }
        }


        #region Video Conversation
        public void NotifyReceiveVideoRequest(int p_senderID)
        {
            string friendName = this.FriendList[p_senderID].UserName;

            foreach (IUserObserver observer in this.m_observers)
            {
                observer.OnReceiveVideoRequest(friendName);
            }
        }
        public void NotifyStartVideoConv(int p_senderID)
        {
            string friendName = this.FriendList[p_senderID].UserName;

            foreach (IUserObserver observer in this.m_observers)
            {
                observer.OnStartVideoConv(friendName);
            }
        }
        public void NotifyEndVideoConv(int p_senderID)
        {
            string friendName = this.FriendList[p_senderID].UserName;

            foreach (IUserObserver observer in this.m_observers)
            {
                observer.OnEndVideoConv(friendName);
            }
        }
        #endregion

        #region File Transfer
        public void NotifyReceiveFileRequest(string p_fileName, int p_sender)
        {
            string friendName = this.FriendList[p_sender].UserName;
            foreach (IUserObserver observer in this.m_observers)
            {
                observer.OnReceiveFileTransferRequest(p_fileName, friendName);
            }
        }
        public void NotifyFileTransferSuccessed(string p_fileName, int p_receiverID)
        {
            string friendName = this.FriendList[p_receiverID].UserName;
            InstantMessage instantMessage = new InstantMessage();
            //instantMessage.ReceiverID = p_re
            foreach (IUserObserver observer in this.Observers)
            {
                // observer.OnReceiveIM(friendName);
            }

        }
        public void NotifyFileTransferFailed(string p_fileName, int p_receiverID)
        {
            throw new NotImplementedException();
        }
        #endregion

        #endregion

        #region Methods
        private void ConstructFriendList(List<UserInfo> p_friendList)
        {
            for (int i = 0; i < p_friendList.Count; i++)
            {
                this.m_friendList.Add(p_friendList[i].UserID, p_friendList[i]);
            }
        }
        public void SignIn(UserInfo p_userInfo, List<UserInfo> p_friendList)
        {
            this.UserInfo = p_userInfo;
            this.IsLoggedIn = true;
            this.ConstructFriendList(p_friendList);
        }
        public bool SaveFile(string path, byte[] bytes)
        {
            try
            {
                File.WriteAllBytes(path, bytes);

            }
            catch
            {
                return false;
            }
            return true;

        }
        public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {

                // Open file for reading

                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader

                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                // get total byte length of the file

                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                // read entire file into buffer

                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                // close file reader

                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();

            }

            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());

            }
            return _Buffer;

        }
        #endregion
    }
}