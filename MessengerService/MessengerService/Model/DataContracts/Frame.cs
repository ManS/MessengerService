using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace MessengerService.Model.DataContracts
{

    [DataContract]
    public class Frame
    {
        private int m_senderID;
        private int m_receiverID;
        private byte[] m_bitmap;

        [DataMember]
        public int ReceiverID
        {
            get { return m_receiverID; }
            set { m_receiverID = value; }
        }

        [DataMember]
        public int SenderID
        {
            get { return m_senderID; }
            set { m_senderID = value; }
        }

        [DataMember]
        public byte[] bitmap
        {
            get { return m_bitmap; }
            set { m_bitmap = value; }
        }
    }
}
