using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MessengerService.Model.DataContracts
{
    //For registration
    [DataContract]
    public class UserInfo
    {
        #region Attributes
        private UserPersonalInfo m_userPersonalInfo;
        private string m_userName;
        private string m_userPM;
        private Status m_userStatus;
        private int m_userID;
        #endregion

        #region Constructor
        public UserInfo(int p_userID,string p_userName, string p_firstName, string p_lastName, int p_avatarID,Status p_userStatus, string p_userPM)
        {
            this.m_userPersonalInfo = new UserPersonalInfo(p_firstName, p_lastName, p_avatarID);
            this.m_userName = p_userName;
            this.m_userPM = p_userPM;
            this.m_userStatus = p_userStatus;
            this.m_userID = p_userID;
        }
        public UserInfo(UserPersonalInfo p_userPersonalInfo, int p_userID, string p_userName, string p_userPM, Status p_userStatus)
        {
            this.m_userPersonalInfo = p_userPersonalInfo;
            this.m_userName = p_userName;
            this.m_userPM = p_userPM;
            this.m_userStatus = p_userStatus;
            this.m_userID = p_userID;
        }
        #endregion

        #region Properties
        [DataMember]
        public UserPersonalInfo UserPersonalInfo
        {
            get { return m_userPersonalInfo; }
            set { m_userPersonalInfo = value; }
        }
        [DataMember]
        public int UserID
        {
            get { return m_userID; }
            set { m_userID = value; }
        }
        [DataMember]
        public Status UserStatus
        {
            get { return m_userStatus; }
            set { m_userStatus = value; }
        }
        [DataMember]
        public string UserPM
        {
            get { return m_userPM; }
            set { m_userPM = value; }
        }
        [DataMember]
        public string UserName
        {
            get { return m_userName; }
            set { m_userName = value; }
        }
        #endregion
    }
}