using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MessengerService.Model.DataContracts;
using System.IO;

namespace MessengerService.Model
{
    [ServiceContract(CallbackContract = typeof(IMessengerCallback))]
    public interface IMessengerService 
    {
        [OperationContract]
        int RegisterNewUser(AccountInfo p_userAccountInfo, UserPersonalInfo p_userPersonalInfo);

        [OperationContract]
        int LogIn(AccountInfo p_accountInfo, Status p_loginStatus);
         
        [OperationContract(IsOneWay = true)]
        void UserStartUp(object userID);

        [OperationContract(IsOneWay = true)]
        void NotifyILoggedIn(int p_userID, Status p_loginStatus);

        [OperationContract]
        List<UserInfo> GetContactListUpdates(int p_userID);

        [OperationContract(IsOneWay = true)]
        void ChangeStatus(Status p_newStatus, int p_userID);

        [OperationContract]
        UserInfo GetUserInfo(int p_userID);

        [OperationContract(IsOneWay = true)]
        void SendIM(InstantMessage p_instantMessage);

        [OperationContract(IsOneWay = true)]
        void ChangePersonalMessage(string p_newPM, int p_userID);

        [OperationContract(IsOneWay = true)]
        void ChangeAvatar(int p_avatarID, int p_userID);

        [OperationContract(IsOneWay = true)]
        void GetOfflineMessages(int p_userID);

        [OperationContract(IsOneWay = true)]
        void SendAddRequest(string p_friendName, int p_userID);

        [OperationContract(IsOneWay = true)]
        void ReplyAddRequest(int p_receiverID, int p_senderID, bool p_reply);

        [OperationContract(IsOneWay = true)]
        void LogOut(int p_userID);

        [OperationContract(IsOneWay = true)]
        void SendIsTypingMessage(int p_senderID, int p_receiverID);

        [OperationContract]
        int TestConnection();

        #region File Transfer Request
        
        [OperationContract(IsOneWay = true)]
        void ReceiveFileAcknowledge(string p_fileName, int p_senderID, int p_receiverID);

        [OperationContract(IsOneWay = true)]
        void RequestSendFile(string p_filePath, int p_senderID, int p_receiverID);

        [OperationContract(IsOneWay = true)]
        void ReplySendFileRequest(string p_filePath, bool p_reply, int p_senderID, int p_receiverID);

        [OperationContract(IsOneWay = true)]
        void SentFileAcknowledge(string p_fileName, int p_senderID,int p_receiverID);
        
        #endregion


        #region Video Conversation
        [OperationContract(IsOneWay = true)]
        void SendVideoRequest(int p_senderID, int p_receiverID);

        [OperationContract(IsOneWay = true)]
        void ReplyVideoRequest(bool p_reply, int p_senderID, int p_receiverID);

        [OperationContract(IsOneWay = true)]
        void EndVideoConversation(int p_senderID, int p_receiverID); 
        #endregion
    }
}
