using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileServer.Services;
using System.ServiceModel;
using System.IO;
using System.Security.Cryptography;

namespace MessengerUser.Controller
{
   public class FileTransferer : ClientBase<IFileRepositoryService>, IFileRepositoryService, IDisposable
    {
       
        public FileTransferer()
           : base("FileRepositoryService")
		{

		}

		#region IFileRepositoryService Members

		public Stream GetFile(string virtualPath)
		{
			return base.Channel.GetFile(virtualPath);
		}

		public void PutFile(FileUploadMessage msg)
		{

			base.Channel.PutFile(msg);
		}

		public void DeleteFile(string virtualPath)
		{
			base.Channel.DeleteFile(virtualPath);
		}
        
		public StorageFileInfo[] List(string virtualPath)
		{
			return base.Channel.List(virtualPath);
		}

		#endregion

		#region IDisposable Members

		void IDisposable.Dispose()
		{
			if (this.State == CommunicationState.Opened)
				this.Close();
		}

		#endregion

        public void FileDownloadedAcknowledge(string virtualPath)
        {
            base.Channel.FileDownloadedAcknowledge(virtualPath);
        }

        public void FileTransferFailed(string virtaualPath)
        {
            base.Channel.FileTransferFailed(virtaualPath);
        }

        static public string GetMD5String(string _PATH)
        {

            FileStream file = new FileStream(_PATH, FileMode.Open);
            MD5 md5 = MD5.Create();
            byte[] checksum = md5.ComputeHash(file);
            file.Close();
            return (BitConverter.ToString(checksum).Replace("-", string.Empty));

        }
    }
}
