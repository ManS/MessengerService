using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerService.Model.DataContracts;

namespace MessengerService.Model
{
    public class UserCallback
    {

        #region Attributes
        private IMessengerCallback m_userCallbackHandler;
        private UserInfo m_userInfo;
        #endregion 

        #region Constructor
        public UserCallback(UserInfo p_userInfo, IMessengerCallback p_userCallback)
        {
            this.m_userInfo = p_userInfo;
            this.m_userCallbackHandler = p_userCallback;
        }
        #endregion

        #region Properties
        public UserInfo UserInfo
        {
            get { return m_userInfo; }
            set { m_userInfo = value; }
        }
        public IMessengerCallback UserCallbackHandler
        {
            get { return m_userCallbackHandler; }
            set { m_userCallbackHandler = value; }
        }
        #endregion
       
    }
}
