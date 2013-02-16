using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using MessengerService.Model.DataContracts;

namespace MessengerService.Model.DatabaseManipulator
{
    class DBManipulator
    {
        static public SqlConnection myConnection { get; set; }
        static private string m_myConnectionString;

       
        /// <summary>
        /// Initializes a new instance of the <see cref="DBManipulator"/> class.
        /// </summary>
        public DBManipulator(string ConnectionString)
        {
            m_myConnectionString = ConnectionString;
            myConnection = new SqlConnection(ConnectionString);
            myConnection.Open();
        }
        public string MyConnectionString
        {
            get { return m_myConnectionString; }
            set { m_myConnectionString = value; }
        }

        /// <summary>
        /// Registers the new user.
        /// </summary>
        /// <param name="p_userInfo">The user info.[UserName, Password, First Name, Last Name, Avatar ID]</param>
        /// <returns>User Id if Registeration Success, -1 if invalid data or 0 if the username already exist</returns>
        internal int RegisterNewUser(AccountInfo p_userAccountInfo, UserPersonalInfo p_userPersonalInfo)
        {
            SqlCommand AddUserCommand = new SqlCommand("dbo.AddUser", myConnection);
            AddUserCommand.CommandType = CommandType.StoredProcedure;
            
            AddUserCommand.Parameters.AddWithValue("@Username", p_userAccountInfo.UserName);
            AddUserCommand.Parameters.AddWithValue("@PassWord", p_userAccountInfo.UserPassword);
            AddUserCommand.Parameters.AddWithValue("@FN", p_userPersonalInfo.FirstName);
            AddUserCommand.Parameters.AddWithValue("@LN", p_userPersonalInfo.LastName);
            AddUserCommand.Parameters.AddWithValue("@PicId", p_userPersonalInfo.AvatarID);
            AddUserCommand.Parameters.AddWithValue("@Pm", DBNull.Value);
            AddUserCommand.Parameters.AddWithValue("@Out", DBNull.Value);
            
            int id=0;

            SqlDataReader reader = AddUserCommand.ExecuteReader();
            try
            {
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                reader.Close();
            }
            catch
            {
                id = 0;
                reader.Close();
            }

            AddUserCommand.Cancel();
            
            return id;
        }

        /// <summary>
        /// Logs the in.
        /// </summary>
        /// <param name="p_accountInfo">The user account info.[Username, Password]</param>
        /// <returns>User ID in the best case, 0 if username exist but wrong password or -1 if data is invalid</returns>
        internal int LogIn(AccountInfo p_accountInfo)
        {
            SqlCommand LoginCommand = new SqlCommand("select dbo.GetID(@Username,@PassWord)", myConnection);
            LoginCommand.CommandType = CommandType.Text;
            LoginCommand.Parameters.AddWithValue("@Username",p_accountInfo.UserName);
            LoginCommand.Parameters.AddWithValue("@PassWord",p_accountInfo.UserPassword);
            int id;
            var result = LoginCommand.ExecuteScalar();
            id = (int)result;

            LoginCommand.Cancel();
            return id;
        }

        internal List<UserInfo> GetUserFriendList(int UserID)
        {
            List<UserInfo> Friends=new List<UserInfo>();
            SqlCommand GetUserInfoCommand = new SqlCommand("select * from dbo.GetContacts(@ID)", myConnection);
            GetUserInfoCommand.CommandType = CommandType.Text;
            GetUserInfoCommand.Parameters.AddWithValue("@ID", UserID);
            int id = 0;
            
            SqlDataReader reader = GetUserInfoCommand.ExecuteReader();
            List<int> FriendsIDs = new List<int>();
            while (reader.Read())
            {
                id = reader.GetInt32(0);
                FriendsIDs.Add(id);
            }
            reader.Close();
            for (int i = 0; i < FriendsIDs.Count; i++)
                Friends.Add(GetUserInfo(FriendsIDs[i]));
            GetUserInfoCommand.Cancel();
            return Friends;
        }

        internal void AddOfflineMessage(InstantMessage p_instantMessage)
        {
            SqlCommand AddOfflineMessageCommand = new SqlCommand("AddOfflineMsg", myConnection);
            AddOfflineMessageCommand.CommandType = CommandType.StoredProcedure;
            AddOfflineMessageCommand.Parameters.AddWithValue("@SenderID", p_instantMessage.SenderID);
            AddOfflineMessageCommand.Parameters.AddWithValue("@RecieverID", p_instantMessage.ReceiverID);
            AddOfflineMessageCommand.Parameters.AddWithValue("@Content", p_instantMessage.MessageContent);
            AddOfflineMessageCommand.Parameters.AddWithValue("@Date", p_instantMessage.SendingTime);
            AddOfflineMessageCommand.ExecuteScalar();
            AddOfflineMessageCommand.Cancel();
        }

        internal List<InstantMessage> GetOfflineMessages(int p_userID)
        {
            List<InstantMessage> OfflineMsgs = new List<InstantMessage>();
            SqlCommand GetOfflineMessagesCommand = new SqlCommand("select * from dbo.GetOfflineMsg(@RecieverID)", myConnection);
            GetOfflineMessagesCommand.CommandType = CommandType.Text;
            GetOfflineMessagesCommand.Parameters.AddWithValue("@RecieverID", p_userID);
            
            SqlDataReader reader = GetOfflineMessagesCommand.ExecuteReader();
            
            while (reader.Read())
            {
                InstantMessage OfflineMsg = new InstantMessage();
                
                OfflineMsg.SenderID= reader.GetInt32(1);
               OfflineMsg.ReceiverID= reader.GetInt32(2);
               OfflineMsg.MessageContent = reader.GetString(3);
                OfflineMsg.SendingTime = reader.GetDateTime(4);
                OfflineMsgs.Add(OfflineMsg);
            }
            reader.Close();
            GetOfflineMessagesCommand.Cancel();
            
            return OfflineMsgs;
        }

        internal void RemoveOfflineMessages(int p_userID)
        {
            SqlCommand com = new SqlCommand("dbo.RemoveOfflineMsg", myConnection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@RecieverID", p_userID);
            com.ExecuteScalar();
            com.Cancel();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="p_friendName">The user name of friend to be added</param>
        /// <returns>User ID in the best case and -1 if data is invalid</returns>
        internal int GetUserID(string p_friendName)
        {
            SqlCommand GetUserIDCommand = new SqlCommand("select dbo.GetIdUsingName(@UserName)", myConnection);
            GetUserIDCommand.CommandType = CommandType.Text;
            GetUserIDCommand.Parameters.AddWithValue("@UserName", p_friendName);
            var x = GetUserIDCommand.ExecuteScalar();
           
            int id = (int)x;
            GetUserIDCommand.Cancel();
            return id;
        }

        internal UserInfo GetUserInfo(int UserID)
        {
            SqlCommand GetUserInfoCommand = new SqlCommand("select * from dbo.GetInfo(@ID)", myConnection);
            GetUserInfoCommand.CommandType = CommandType.Text;
            GetUserInfoCommand.Parameters.AddWithValue("@ID", UserID);
            SqlDataReader reader = GetUserInfoCommand.ExecuteReader();

            int id=0;
            string username="";
            string FN="";
            string LN="";
            int avatarID=0;
            string Pm="";

            while (reader.Read())
            {
                id = reader.GetInt32(0);

                username = reader.GetString(1);

                var x = reader.GetValue(3);
                if (x != DBNull.Value)
                    FN = (string)x;

                x = reader.GetValue(4);
                if (x != DBNull.Value)
                    LN = (string)x;

                x = reader.GetValue(5);
                if (x != DBNull.Value)
                    avatarID = (int)x;

                x = reader.GetValue(6);
                if (x != DBNull.Value)
                    Pm = (string)x;
            }
            reader.Close();
            GetUserInfoCommand.Cancel();
            UserInfo user = new UserInfo(id, username, FN, LN, avatarID, Status.Offline, Pm);
            return user;
        }

        internal void ChangePM(int p_userID, string p_newPM)
        {
            SqlCommand UpdatePMCommand = new SqlCommand("UpdatePersonalMsg", myConnection);
            UpdatePMCommand.CommandType = CommandType.StoredProcedure;
            UpdatePMCommand.Parameters.AddWithValue("@NewPm", p_newPM);
            UpdatePMCommand.Parameters.AddWithValue("@UserID", p_userID);
            UpdatePMCommand.ExecuteScalar();
            UpdatePMCommand.Cancel();
        }

        internal void AddNewAddRequest(int p_userID, int p_requestedFriendID)
        {
            SqlCommand AddNewAddRequestCommand = new SqlCommand("dbo.AddFriendRequest", myConnection);
            AddNewAddRequestCommand.CommandType = CommandType.StoredProcedure;
            AddNewAddRequestCommand.Parameters.AddWithValue("@SenderID", p_userID);
            AddNewAddRequestCommand.Parameters.AddWithValue("@RecieverID", p_requestedFriendID);
            AddNewAddRequestCommand.ExecuteScalar();
            AddNewAddRequestCommand.Cancel();
        }

        internal void RemoveAddRequest(int p_senderID, int p_requestedFriendID)
        {
            SqlCommand com = new SqlCommand("dbo.RemoveFriendRequest", myConnection);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@SenderID", p_senderID);
            com.Parameters.AddWithValue("@RecieverID", p_requestedFriendID);
            com.ExecuteScalar();
            com.Cancel();
        }

        internal void AddNewFriendship(int p_senderID, int p_receiverID)
        {
            SqlCommand AddContactCommand = new SqlCommand("AddContact", myConnection);
            AddContactCommand.CommandType = CommandType.StoredProcedure;
            AddContactCommand.Parameters.AddWithValue("@UserName", p_senderID);
            AddContactCommand.Parameters.AddWithValue("@FriendName", p_receiverID);
            AddContactCommand.ExecuteScalar();
            AddContactCommand.Cancel();
        }

        internal void ChanageAvatarID(int p_userID, int p_avatarID)
        {
            SqlCommand AddContactCommand = new SqlCommand("UpdateAvatarID", myConnection);
            AddContactCommand.CommandType = CommandType.StoredProcedure;
            AddContactCommand.Parameters.AddWithValue("@ID", p_userID);
            AddContactCommand.Parameters.AddWithValue("@newAvatarID", p_avatarID);
            AddContactCommand.ExecuteScalar();
            AddContactCommand.Cancel();
        }

        internal List<AddRequest> GetOfflineAddRequests(int userID)
        {
            SqlCommand GetUserInfoCommand = new SqlCommand("select * from dbo.GetOfflineAddRequests(@ID)", myConnection);
            GetUserInfoCommand.CommandType = CommandType.Text;
            GetUserInfoCommand.Parameters.AddWithValue("@ID", userID);
            List<AddRequest> AddRequests = new List<AddRequest>();
            SqlDataReader reader = GetUserInfoCommand.ExecuteReader();

            int RecieverID;
            int SenderID;
            
            while (reader.Read())
            {
                SenderID = reader.GetInt32(1);
                RecieverID=reader.GetInt32(2);
                AddRequests.Add(new AddRequest(SenderID,RecieverID));               
            }
            reader.Close();
            return AddRequests;
        }

        internal void RemoveAllFriendRequests(int recieverID)
        {
            SqlCommand AddContactCommand = new SqlCommand("RemoveOfflineAddRequest", myConnection);
            AddContactCommand.CommandType = CommandType.StoredProcedure;
            AddContactCommand.Parameters.AddWithValue("@ID", recieverID);
          
            AddContactCommand.ExecuteScalar();
            AddContactCommand.Cancel();
        }
        
    }
}
