using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using MessengerService.Model;
using FileServer.Services;
using System.Threading;
using MessengerService;
namespace MessengerHost
{
    public partial class Form1 : Form
    {
        ServiceHost MessengerServiceHost = new ServiceHost(typeof(MessengerService.Model.MessengerService));
        NetTcpBinding MessengernetTCP = new NetTcpBinding(SecurityMode.None);

        static ServiceHost FileRepositoryServiceHost = null;
        static FileRepositoryService FileRepositoryservice = null;
        static ServiceHost VideoStreamingServiceHost = null;
        static VideoStreamingService videoStreamingService = null;

        static Thread fileTransferThread;
        static Thread videoStreamingThread;

        delegate void Initiator();

        public Form1()
        {
            InitializeComponent();
        }

        private void Initiat()
        {

            fileTransferThread = new Thread(StartFileTransferService);
            fileTransferThread.Start();

            videoStreamingThread = new Thread(StartVideoStreamingService);
            videoStreamingThread.Start();

            MessengernetTCP.ReceiveTimeout = TimeSpan.MaxValue;
            MessengernetTCP.SendTimeout = TimeSpan.MaxValue;
            //nettcp1.TransferMode = TransferMode.Streamed;
            
            MessengerServiceHost.AddServiceEndpoint(typeof(IMessengerService),
                MessengernetTCP, "net.tcp://localhost:8082/test");
            

            MessengerServiceHost.Open();
            MessengerServiceHost.Closed += new EventHandler(OnServiceClosed);

            MessageBox.Show("Service Is running now, press ok to exit...");
            MessengerServiceHost.Close();
        }


        private void StartFileTransferService()
        {
            #region File Server
            FileRepositoryservice = new FileRepositoryService();
            FileRepositoryservice.RepositoryDirectory = "storage";

            FileRepositoryservice.FileRequested += new FileEventHandler(Service_FileRequested);
            FileRepositoryservice.FileUploaded += new FileEventHandler(Service_FileUploaded);
            FileRepositoryservice.FileDeleted += new FileEventHandler(Service_FileDeleted);

            FileRepositoryServiceHost = new ServiceHost(FileRepositoryservice);
            FileRepositoryServiceHost.Faulted += new EventHandler(FileTransferHost_Faulted);

            try
            {
                FileRepositoryServiceHost.Open();
            }
            catch
            {
                FileRepositoryServiceHost.Close();
            }
            MessageBox.Show("File Service Start.. press any key to close..");
            #endregion
        }
        
        private void StartVideoStreamingService()
        {
            #region Video Streaming
            videoStreamingService = new VideoStreamingService();

            VideoStreamingServiceHost = new ServiceHost(videoStreamingService);
            VideoStreamingServiceHost.Faulted += new EventHandler(VideoStreamingHost_Faulted);

            try
            {
                VideoStreamingServiceHost.Open();
            }
            catch
            {
                VideoStreamingServiceHost.Close();
            }
            MessageBox.Show("Video Streaming Service Start.. press any key to close..");
            #endregion
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Initiat();
        }

        private void OnFileServiceClosed(object sender, EventArgs e)
        {
            FileRepositoryservice.DeleteFiles();
            fileTransferThread.Abort();
            FileRepositoryServiceHost.Abort();
        }
        private void OnServiceClosed(object sender, EventArgs e)
        {
            fileTransferThread.Abort();
            MessengerServiceHost.Abort();
        }

        static void FileTransferHost_Faulted(object sender, EventArgs e)
        {
            FileRepositoryservice.DeleteFiles();
            fileTransferThread.Abort();
            FileRepositoryServiceHost.Abort();
        }

        static void VideoStreamingHost_Faulted(object sender, EventArgs e)
        {
            MessageBox.Show("Video Service fault!");
            videoStreamingThread.Abort();
            VideoStreamingServiceHost.Abort();
        }

        static void Service_FileRequested(object sender, FileEventArgs e)
        {
           // MessageBox.Show(string.Format("File upload\t{0}\t{1}", e.VirtualPath, DateTime.Now));
           // Console.WriteLine(string.Format("File access\t{0}\t{1}", e.VirtualPath, DateTime.Now));
        }

        static void Service_FileUploaded(object sender, FileEventArgs e)
        {
            //MessageBox.Show(string.Format("File upload\t{0}\t{1}", e.VirtualPath, DateTime.Now));
        }

        static void Service_FileDeleted(object sender, FileEventArgs e)
        {
            Console.WriteLine(string.Format("File deleted\t{0}\t{1}", e.VirtualPath, DateTime.Now));
        }

    }
}
