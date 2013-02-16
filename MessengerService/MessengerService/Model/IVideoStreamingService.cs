using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.Drawing;
using MessengerService.Model.DataContracts;

namespace MessengerService
{
    [ServiceContract]
    public interface IVideoStreamingService
    {
        [OperationContract]
        void SendFrame(VideoFrame p_videoFrame);

        [OperationContract]
        byte[] ReceiveFrame(KeyValuePair<int, int> p_sender_receiver);

        [OperationContract]
        void StopVideoStreaming(KeyValuePair<int, int> p_sender_receiver);
    }
}