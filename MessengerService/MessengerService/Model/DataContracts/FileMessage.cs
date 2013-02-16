using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.IO;
using System.ServiceModel;

namespace MessengerService.Model.DataContracts
{

    [DataContract]
    [KnownType(typeof(FileStream))]
    public class FileMessage : Message
    {
        #region Attributes
        private string m_fileName;
        private Stream m_fileData;
        private double m_fileSize;
        
        #endregion

        public FileMessage(int senderID, int receiverID, string p_filePath)
        {
            this.SenderID = senderID;
            this.ReceiverID = receiverID;
            this.FileName = Path.GetFileName(p_filePath);
            this.FileData = File.Open(p_filePath, FileMode.Open);
            this.FileSize = this.FileData.Length / 1024;
        }

        #region Properties
        [MessageHeader]
        public string FileName
        {
            get { return m_fileName; }
            set { m_fileName = value; }
        }
        [DataMember]
        public Stream FileData
        {
            get { return m_fileData; }
            set { m_fileData = value; }
        }
        [DataMember]
        public double FileSize
        {
            get { return m_fileSize; }
            set { m_fileSize = value; }
        }
        #endregion

        #region Methods

        static public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {

                // Open file for reading

                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader

                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);
                // get total byte length of the file

                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;
                // read entire file into buffer

                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);
                // close file reader

                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();

            }

            catch (Exception _Exception)
            {
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());

            }
            return _Buffer;

        }
  
        #endregion
    }
}