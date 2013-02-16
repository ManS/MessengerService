using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MessengerService.Model.DataContracts
{
    [DataContract]
   public class UserPersonalInfo
    {
        #region Attributes
        private string m_lastName;
        private string m_firstName;
        private int m_avatarID;
        #endregion

        #region Constructor
        public UserPersonalInfo(string p_firstName, string p_lastName, int p_avatarID)
        {
            this.m_avatarID = p_avatarID;
            this.m_firstName = p_firstName;
            this.m_lastName = p_lastName;
        }
        #endregion

        #region Properties
        [DataMember]
        public string FirstName
        {
            get { return m_firstName; }
            set { m_firstName = value; }
        }
        [DataMember]
        public string LastName
        {
            get { return m_lastName; }
            set { m_lastName = value; }
        }
        [DataMember]
        public int AvatarID
        {
            get { return m_avatarID; }
            set { m_avatarID = value; }
        }
        #endregion
    }
}