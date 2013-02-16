using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace MessengerService.Model.DataContracts
{
    [DataContract]
    public class AccountInfo
    {
        #region Attributes
        private string m_userName;
        private string m_userPassword;
        #endregion

        #region Constructor
        public AccountInfo(string p_userName, string p_userPassword)
        {
            this.m_userName = p_userName;
            this.m_userPassword = p_userPassword;
        }

        #endregion
        
        #region Properties
        [DataMember]
        public string UserName
        {
            get { return m_userName; }
            private set { m_userName = value; }
        }

        [DataMember]
        public string UserPassword
        {
            get { return m_userPassword; }
            private set { m_userPassword = value; }
        }
        #endregion
    }
}
