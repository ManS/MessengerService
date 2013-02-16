using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace MessengerService.Model.DataContracts
{
    [DataContract]
    public class InstantMessage : Message
    {
        private string m_messageContent;

        public InstantMessage()
        {
            this.SenderID = 0;
            this.ReceiverID = 0;
            this.SenderName = "";
            this.ReceiverName = "";
            this.m_messageContent = "";
        }
        public InstantMessage(string p_messageContent)
        {
            this.SenderID = 0;
            this.ReceiverID = 0;
            this.SenderName = "";
            this.ReceiverName = "";
            this.m_messageContent = p_messageContent;
        }

        [DataMember]
        public string MessageContent
        {
            get { return m_messageContent; }
            set { m_messageContent = value; }
        }
    }
}
