using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using MessengerService.Model;
using System.Drawing;
using MessengerService.Model.DataContracts;

namespace MessengerService
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true,
        InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class VideoStreamingService : IVideoStreamingService
    {
        public static Dictionary<KeyValuePair<int, int>, Queue<byte[]>> FramesQueues = new Dictionary<KeyValuePair<int, int>, Queue<byte[]>>();

        public void SendFrame(VideoFrame p_videoFrame)
        {
            byte[] p_videoStream = p_videoFrame.FrameStream;
            KeyValuePair<int, int> p_sender_receiver = p_videoFrame.Contract;
            if (!FramesQueues.ContainsKey(p_sender_receiver))
            {
                FramesQueues.Add(p_sender_receiver, new Queue<byte[]>());
            }
            FramesQueues[p_sender_receiver].Enqueue(p_videoStream);
        }

        public byte[] ReceiveFrame(KeyValuePair<int, int> p_sender_receiver)
        {
            if (FramesQueues.ContainsKey(p_sender_receiver))
            {
                if (FramesQueues[p_sender_receiver].Count == 0)
                {
                    return null;
                }
                return FramesQueues[p_sender_receiver].Dequeue();
            }
            return null;
        }

        public void StopVideoStreaming(KeyValuePair<int, int> p_sender_receiver)
        {
            if (FramesQueues.ContainsKey(p_sender_receiver))
            {
                FramesQueues[p_sender_receiver].Clear();
                FramesQueues.Remove(p_sender_receiver);
            }
        }

    }
}