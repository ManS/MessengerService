using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerService;
using System.ServiceModel;
using System.IO;
using System.Drawing;
using MessengerService.Model.DataContracts;

namespace MessengerUser.Controller
{
    public class VideoCapturer : ClientBase<IVideoStreamingService>, IVideoStreamingService
    {
        public VideoCapturer()
            : base("VideoStreamingService")
        { }

        public void SendFrame(VideoFrame p_videoFram)
        {
            base.Channel.SendFrame(p_videoFram);
        }

        public byte[] ReceiveFrame(KeyValuePair<int, int> p_sender_receiver)
        {
            return base.Channel.ReceiveFrame(p_sender_receiver);
        }

        public void StopVideoStreaming(KeyValuePair<int, int> p_sender_receiver)
        {
            base.Channel.StopVideoStreaming(p_sender_receiver);
        }
    }
}