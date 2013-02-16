using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MessengerService.Model.DatabaseManipulator;
using MessengerService.Model.DataContracts;
using System.Threading;
using System.IO;
namespace MessengerService.Model
{
    struct AddRequest
    {
        public int m_senderID;
        public int m_receiverID;

        public AddRequest(int p_senderID, int p_receiverID)
        {
            m_senderID = p_senderID;
            m_receiverID = p_receiverID;
        }
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MessengerService : IMessengerService
    {

        #region Attributes
        private Dictionary<int, UserCallback> m_usersAccounts = new Dictionary<int, UserCallback>();
        private Dictionary<int, List<int>> m_userObservers = new Dictionary<int, List<int>>();
        private Dictionary<int, Dictionary<int, UserInfo>> m_userFriends = new Dictionary<int, Dictionary<int, UserInfo>>();
        private DBManipulator m_databaseManipulator = new DBManipulator("user id=Ahmad-PC\\Ahmad;Data Source=localhost;Initial Catalog=ChatApplication;Integrated Security=SSPI");
        #endregion

        #region Properties
        public Dictionary<int, Dictionary<int, UserInfo>> UserFriends
        {
            get { return m_userFriends; }
            set { m_userFriends = value; }
        }
        internal DBManipulator DatabaseManipulator
        {
            get { return m_databaseManipulator; }
            set { m_databaseManipulator = value; }
        }
        public Dictionary<int, UserCallback> UsersAccounts
        {
            get { return m_usersAccounts; }
            set { m_usersAccounts = value; }
        }
        public Dictionary<int, List<int>> UserObservers
        {
            get { return m_userObservers; }
            set { m_userObservers = value; }
        }

        #endregion

        #region Methods
        private void AddObservers(int p_userID, List<UserInfo> AllFriendList)
        {
            Dictionary<int, UserInfo> AllFriends = new Dictionary<int, UserInfo>();
            List<int> Observers = new List<int>();
            for (int i = 0; i < AllFriendList.Count; i++)
            {
                if (this.UsersAccounts.ContainsKey(AllFriendList[i].UserID))
                {
                    int friendID = AllFriendList[i].UserID;
                    AllFriendList[i].UserStatus = this.UsersAccounts[friendID].UserInfo.UserStatus;
                    Observers.Add(friendID);
                    this.UserObservers[friendID].Add(p_userID);
                }
                AllFriends.Add(AllFriendList[i].UserID, AllFriendList[i]);
            }
            this.UserObservers.Add(p_userID, Observers);
            this.UserFriends.Add(p_userID, AllFriends);
        }
        #endregion
   
        #region IMessengerService Members

        public int RegisterNewUser(AccountInfo p_userAccountInfo, UserPersonalInfo p_userPersonalInfo)
        {
            if (p_userAccountInfo.UserName.Trim() == "" || p_userAccountInfo.UserPassword.Length < 4)
            {
                return -1;//Invalid Data
            }
            try
            {
                return this.DatabaseManipulator.RegisterNewUser(p_userAccountInfo, p_userPersonalInfo);
            }
            catch
            {
                return -2;//means an error happend
            }
        }

        public int LogIn(AccountInfo p_accountInfo, Status p_loginStatus)
        {
            try
            {
                int UserID = this.DatabaseManipulator.LogIn(p_accountInfo);
                if (UserID != 0 && UserID != -1)//means user logged in successfully and Database Respond contains user ID
                {
                    if (this.UsersAccounts.ContainsKey(UserID))
                    {
                        return -2;//means that this account is already logged in from another pc !
                    }
                    UserInfo userInfo = this.DatabaseManipulator.GetUserInfo(UserID);
                    userInfo.UserStatus = p_loginStatus;
                    UserCallback userCallBack = new UserCallback(userInfo, OperationContext.Current.GetCallbackChannel<IMessengerCallback>());
                    this.UsersAccounts.Add(UserID, userCallBack);
                    List<UserInfo> AllFriendList = m_databaseManipulator.GetUserFriendList(UserID);
                    this.AddObservers(UserID, AllFriendList);
                    //Thread workerThread = new Thread(new ParameterizedThreadStart(UserStartUp));
                    //workerThread.Start(UserID);
                    //this.UserStartUp(UserID);
                }
                return UserID;

            }
            catch
            {
                throw;
            }
        }

        public void UserStartUp(object p_userID)
        {
            int userID = (int)p_userID;
            if (this.UsersAccounts.ContainsKey(userID))
            {
                try
                {
                    List<InstantMessage> OfflineMessages = this.DatabaseManipulator.GetOfflineMessages(userID);
                    for (int i = 0; i < OfflineMessages.Count; i++)
                    {
                        OfflineMessages[i].SenderName = this.DatabaseManipulator.GetUserInfo(OfflineMessages[i].SenderID).UserName;
                        OfflineMessages[i].ReceiverName = this.UsersAccounts[userID].UserInfo.UserName;
                        this.UsersAccounts[userID].UserCallbackHandler.ReceiveIM(OfflineMessages[i]);
                    }
                    this.DatabaseManipulator.RemoveOfflineMessages(userID);
                    List<AddRequest> OfflineAddRequests = this.DatabaseManipulator.GetOfflineAddRequests(userID);
                    for (int i = 0; i < OfflineAddRequests.Count; i++)
                    {
                        int senderID = OfflineAddRequests[i].m_senderID;
                        UserInfo senderInfo;
                        if (this.UsersAccounts.ContainsKey(senderID))
                            senderInfo = UsersAccounts[senderID].UserInfo;
                        else
                            senderInfo = this.DatabaseManipulator.GetUserInfo(senderID);
                        this.UsersAccounts[userID].UserCallbackHandler.ReceiveAddRequest(senderInfo);
                    }

                }
                catch 
                {
                    this.UsersAccounts[userID].UserCallbackHandler.ErrorNotification("Error during start up..");
                }
                
            }
        }
       
        public void NotifyILoggedIn(int p_userID, Status p_loginStatus)
        {
            try
            {
                if (p_loginStatus == Status.Offline)
                    return;
                if (this.UsersAccounts.ContainsKey(p_userID))
                {
                    List<int> UObservers = this.UserObservers[p_userID];
                    for (int friend = 0; friend < UObservers.Count; friend++)
                    {
                        this.UsersAccounts[UObservers[friend]].UserCallbackHandler.NotifyLoggedIn(p_userID, p_loginStatus);
                        this.UserFriends[UObservers[friend]][p_userID].UserStatus = p_loginStatus;
                        this.UsersAccounts[p_userID].UserInfo.UserStatus = p_loginStatus;
                    }
                }
            }
            catch 
            {
                this.UsersAccounts[p_userID].UserCallbackHandler.ErrorNotification("Error during notifing user logged in..");
            }
            
        }

        public List<UserInfo> GetContactListUpdates(int p_userID)
        {
            if (this.UsersAccounts.ContainsKey(p_userID))
            {
                return this.UserFriends[p_userID].Values.ToList<UserInfo>();
            }
            else
                return new List<UserInfo>();
        }

        public void ChangeStatus(Status p_newStatus, int p_userID)
        {
            try
            {
                if (this.UsersAccounts.ContainsKey(p_userID))
                {
                    List<int> UserObserversList = this.UserObservers[p_userID];
                    this.UsersAccounts[p_userID].UserInfo.UserStatus = p_newStatus;
                    if (p_newStatus != Status.Offline)
                    {
                        for (int i = 0; i < UserObserversList.Count; i++)
                        {
                            int friendID = UserObserversList[i];
                            this.UsersAccounts[friendID].UserCallbackHandler.NotifyStatusChanged(p_userID, p_newStatus);
                            this.UserFriends[friendID][p_userID].UserStatus = p_newStatus;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < UserObserversList.Count; i++)
                        {
                            int friendID = UserObserversList[i];
                            this.UsersAccounts[friendID].UserCallbackHandler.NotifyLoggedOff(p_userID);
                            this.UserFriends[friendID][p_userID].UserStatus = p_newStatus;
                        }
                    }
                }
            }
            catch
            {
                this.UsersAccounts[p_userID].UserCallbackHandler.ErrorNotification("Error during changing status..");
            }
            
        }
        
        public void SendIM(InstantMessage p_instantMessage)
        {
            try
            {
                int senderID = p_instantMessage.SenderID;
                int receiverID = p_instantMessage.ReceiverID;
                p_instantMessage.SendingTime = DateTime.Now;
                if (this.UsersAccounts.ContainsKey(p_instantMessage.SenderID))
                {
                    if (this.UsersAccounts.ContainsKey(p_instantMessage.ReceiverID))
                    {
                        this.UsersAccounts[receiverID].UserCallbackHandler.ReceiveIM(p_instantMessage);
                    }
                    else
                    {

                        this.DatabaseManipulator.AddOfflineMessage(p_instantMessage);
                    }
                }
            }
            catch 
            {
                this.UsersAccounts[p_instantMessage.SenderID].UserCallbackHandler.ErrorNotification("Error during sending IM..");
            }
            

        }

        public void GetOfflineMessages(int p_userID)
        {

            if (this.UsersAccounts.ContainsKey(p_userID))
            {
                List<InstantMessage> offlineMessages = this.DatabaseManipulator.GetOfflineMessages(p_userID);
                for (int i = 0; i < offlineMessages.Count; i++)
                {
                    this.UsersAccounts[p_userID].UserCallbackHandler.ReceiveIM(offlineMessages[i]);
                }
                this.DatabaseManipulator.RemoveOfflineMessages(p_userID);
            }
        }

        public void ChangePersonalMessage(string p_newPM, int p_userID)
        {
            try
            {
                if (this.UsersAccounts.ContainsKey(p_userID))
                {
                    if (this.UsersAccounts[p_userID].UserInfo.UserStatus != Status.Offline)
                    {
                        List<int> UObservers = this.UserObservers[p_userID];
                        Status currentUserStatus = this.UsersAccounts[p_userID].UserInfo.UserStatus;
                        if (currentUserStatus != Status.Offline)
                            for (int i = 0; i < UObservers.Count; i++)
                            {
                                int friendID = UObservers[i];
                                this.UsersAccounts[friendID].UserCallbackHandler.NotifyPMChanged(p_newPM, p_userID);
                                this.UserFriends[friendID][p_userID].UserPM = p_newPM;
                            }
                    }
                    this.UsersAccounts[p_userID].UserInfo.UserPM = p_newPM;
                    this.DatabaseManipulator.ChangePM(p_userID, p_newPM);
                }
            }
            catch 
            {
                this.UsersAccounts[p_userID].UserCallbackHandler.ErrorNotification("Error during chanaging your status..");
            }
            
        }

        public void SendAddRequest(string p_friendName, int p_userID)
        {
            int requestedFriendID = this.DatabaseManipulator.GetUserID(p_friendName);
            if (requestedFriendID == -1)//means not found
                return;

            if (this.UserFriends[p_userID].ContainsKey(requestedFriendID))
                return;
            UserInfo senderInfo;
            if (this.UsersAccounts.ContainsKey(p_userID))
                senderInfo = this.UsersAccounts[p_userID].UserInfo;
            else
                return;
            
            if (this.UsersAccounts.ContainsKey(requestedFriendID))//means this friend is logged in now
            {
                this.UsersAccounts[requestedFriendID].UserCallbackHandler.ReceiveAddRequest(senderInfo);
            }
            this.DatabaseManipulator.AddNewAddRequest(p_userID, requestedFriendID);
        }

        public void ReplyAddRequest(int p_receiverID, int p_senderID, bool p_reply)
        {
            if (p_reply)
            {
                UserInfo receiver = this.DatabaseManipulator.GetUserInfo(p_receiverID);
                UserInfo sender = this.DatabaseManipulator.GetUserInfo(p_senderID);

                this.DatabaseManipulator.AddNewFriendship(p_senderID, p_receiverID);//receiver && sender and sender && receiver

                bool check = false;
                if (this.UsersAccounts.ContainsKey(p_senderID))
                {
                    check = true;

                    if (this.UsersAccounts.ContainsKey(p_receiverID))
                        receiver = this.UsersAccounts[p_receiverID].UserInfo;

                    this.UserFriends[p_senderID].Add(receiver.UserID, receiver);
                    this.UsersAccounts[p_senderID].UserCallbackHandler.AddNewFriend(receiver);
                }

                if (this.UsersAccounts.ContainsKey(p_receiverID))
                {
                    if (check)
                    {
                        sender = this.UsersAccounts[p_senderID].UserInfo;
                        this.UserObservers[p_senderID].Add(p_receiverID);
                        this.UserObservers[p_receiverID].Add(p_senderID);
                    }
                    this.UsersAccounts[p_receiverID].UserCallbackHandler.AddNewFriend(sender);
                    this.UserFriends[p_receiverID].Add(sender.UserID, sender);
                }
            }

            this.DatabaseManipulator.RemoveAddRequest(p_senderID, p_receiverID);
        }
       
        public void ChangeAvatar(int p_avatarID, int p_userID)
        {
            if (this.UsersAccounts.ContainsKey(p_userID))
            {
                List<int> UserObserver = this.UserObservers[p_userID];
                Status currentUserStatus = this.UsersAccounts[p_userID].UserInfo.UserStatus;
                
                for (int i = 0; i < UserObserver.Count; i++)
                {
                    int friendID = UserObserver[i];
                    this.UsersAccounts[friendID].UserCallbackHandler.NotifyAvatarChanged(p_userID,p_avatarID);
                    this.UserFriends[friendID][p_userID].UserPersonalInfo.AvatarID = p_avatarID;
                }
                this.UsersAccounts[p_userID].UserInfo.UserPersonalInfo.AvatarID = p_avatarID;
                this.DatabaseManipulator.ChanageAvatarID(p_userID, p_avatarID);
            }

        }
        
        public void SendIsTypingMessage(int p_senderID, int p_receiverID)
        {
            if (this.UsersAccounts.ContainsKey(p_senderID) && this.UsersAccounts.ContainsKey(p_receiverID))
            {
                this.UsersAccounts[p_receiverID].UserCallbackHandler.ReceiveIsTypingMessage(p_senderID);
            }
        }

        public void LogOut(int p_userID)
        {
            if (this.UsersAccounts.ContainsKey(p_userID))
            {
                this.UsersAccounts.Remove(p_userID);
                List<int> observers = this.UserObservers[p_userID];
                for (int i = 0; i < observers.Count; i++)
                {
                    if (this.UsersAccounts.ContainsKey(observers[i]))
                        this.UsersAccounts[observers[i]].UserCallbackHandler.NotifyLoggedOff(p_userID);
                    if (this.UserObservers.ContainsKey(observers[i]))
                        this.UserObservers[observers[i]].Remove(p_userID);
                    this.UserFriends[observers[i]][p_userID].UserStatus = Status.Offline;
                }
                this.UserObservers.Remove(p_userID);
                this.UserFriends.Remove(p_userID);
            }
        }
        
        public UserInfo GetUserInfo(int p_userID)
        {
            if (this.UsersAccounts.ContainsKey(p_userID))
                return this.UsersAccounts[p_userID].UserInfo;
            return this.DatabaseManipulator.GetUserInfo(p_userID);
        }

        public int TestConnection()
        {
            return 1;
        }


        #region Video Conversation
        
        public void ReplyVideoRequest(bool p_reply, int p_senderID, int p_receiverID)
        {
            if (p_reply && this.UsersAccounts.ContainsKey(p_receiverID) && this.UsersAccounts.ContainsKey(p_senderID))
            {
                UsersAccounts[p_receiverID].UserCallbackHandler.StartVideoConv(p_senderID);
                UsersAccounts[p_senderID].UserCallbackHandler.StartVideoConv(p_receiverID);
            }
            else
            {
                InstantMessage IM = new InstantMessage("I reject your video conversation request");
                IM.SenderID = p_receiverID;
                IM.ReceiverID = p_senderID;

                IM.SendingTime = DateTime.Now;
                if (UsersAccounts.ContainsKey(p_receiverID))
                    this.UsersAccounts[p_senderID].UserCallbackHandler.ReceiveIM(IM);
            }
        }

        public void SendVideoRequest(int p_senderID, int p_receiverID)
        {
            if (this.UsersAccounts.ContainsKey(p_receiverID))
            {
                UsersAccounts[p_receiverID].UserCallbackHandler.ReceiveVideoRequest(p_senderID);    
            }
        }
        
        public void EndVideoConversation(int p_senderID, int p_receiverID)
        {
            if (this.UsersAccounts.ContainsKey(p_receiverID))
            {
                this.UsersAccounts[p_receiverID].UserCallbackHandler.EndVideoConv(p_senderID);
            }
        }
        #endregion


        #region File Transfer

        public void RequestSendFile(string p_filePath, int p_senderID, int p_receiverID)
        {
            if (this.UsersAccounts.ContainsKey(p_receiverID))
            {
                UsersAccounts[p_receiverID].UserCallbackHandler.ReceiveFileRequest(p_filePath, p_senderID);
            }
            else
            {
                UsersAccounts[p_senderID].UserCallbackHandler.ErrorNotification("Error, cann't send a file to an offline contact");
            }
        }

        public void ReplySendFileRequest(string p_filePath, bool p_reply, int p_senderID, int p_receiverID)
        {
            if (p_reply && this.UsersAccounts.ContainsKey(p_receiverID) && this.UsersAccounts.ContainsKey(p_senderID))
            {
                UsersAccounts[p_senderID].UserCallbackHandler.StartFileTransfer(p_filePath, p_receiverID);
            }
            else
            {
                InstantMessage IM = new InstantMessage("I reject your File Transfer request");
                IM.SenderID = p_receiverID;
                IM.ReceiverID = p_senderID;

                IM.SendingTime = DateTime.Now;
                if (UsersAccounts.ContainsKey(p_receiverID))
                    this.UsersAccounts[p_senderID].UserCallbackHandler.ReceiveIM(IM);
            }
        }

        public void SentFileAcknowledge(string p_fileName, int p_senderID, int p_receiverID)
        {
            if (this.UsersAccounts.ContainsKey(p_receiverID))
            {
                this.UsersAccounts[p_receiverID].UserCallbackHandler.ReceiveFile(p_fileName, p_senderID);
            }
            else // delete file
            {
                this.UsersAccounts[p_senderID].UserCallbackHandler.TransferFailed(p_fileName, p_receiverID);
            }
        }

        public void ReceiveFileAcknowledge(string p_fileName, int p_senderID, int p_receiverID)
        {
            if (this.UsersAccounts.ContainsKey(p_senderID))
            {
                this.UsersAccounts[p_senderID].UserCallbackHandler.TransferCompleted(p_fileName, p_receiverID);
            }
        }

        #endregion

        #endregion



        
    }
}