using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace MessengerService.Model.DataContracts
{
    [MessageContract]
    public class VideoFrame
    {
        [MessageBodyMember(Order = 1)]
        public byte[] FrameStream { get;  set; }

        [MessageHeader(MustUnderstand=true)]
        public KeyValuePair<int,int> Contract { get;  set; }

    }
}
