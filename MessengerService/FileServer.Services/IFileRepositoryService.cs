using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;

namespace FileServer.Services
{
	
	[ServiceContract]
	public interface IFileRepositoryService
	{
        [OperationContract]
		Stream GetFile(string virtualPath);

        [OperationContract]
		void PutFile(FileUploadMessage msg);

		[OperationContract(IsOneWay = true)]
		void DeleteFile(string virtualPath);

		[OperationContract]
		StorageFileInfo[] List(string virtualPath);

        [OperationContract(IsOneWay = true)]
        void FileDownloadedAcknowledge(string virtualPath);

        [OperationContract(IsOneWay = true)]
        void FileTransferFailed(string virtaualPath);
	}
}
