using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MessengerService
{
    [DataContract]
    abstract public class Message
    {

        #region Attributes
        private int m_senderID;
        private int m_receiverID;
        private string m_senderName;
        private string m_receiverName;
        private DateTime m_sendingTime;
        #endregion


        #region Properties

        [DataMember]
        public int SenderID
        {
            get { return m_senderID; }
            set { m_senderID = value; }
        }


       [DataMember]
        public string SenderName
        {
            get { return m_senderName; }
            set { m_senderName = value; }
        }


        [DataMember]
        public int ReceiverID
        {
            get { return m_receiverID; }
            set { m_receiverID = value; }
        }

        [DataMember]
        public string ReceiverName
        {
            get { return m_receiverName; }
            set { m_receiverName = value; }
        }

        [DataMember]
        public DateTime SendingTime
        {
            get { return m_sendingTime; }
            set { m_sendingTime = value; }
        }
        #endregion

    }
}
