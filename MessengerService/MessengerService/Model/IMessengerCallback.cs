using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MessengerService.Model.DataContracts;

namespace MessengerService.Model
{
    [ServiceContract]
    public interface IMessengerCallback
    {
        [OperationContract(IsOneWay = true)]
        void NotifyLoggedIn(int p_friendID, Status p_newStatus);

        [OperationContract(IsOneWay = true)]
        void NotifyStatusChanged(int p_friendID, Status p_newStatus);

        [OperationContract(IsOneWay = true)]
        void NotifyAvatarChanged(int p_friendID, int p_newAvatarID);

        [OperationContract(IsOneWay = true)]
        void UpdateContactList(List<UserInfo> p_updatedFriendsInfo);

        [OperationContract(IsOneWay = true)]
        void ReceiveIM(InstantMessage p_instantMessage);

        [OperationContract(IsOneWay = true)]
        void NotifyPMChanged(string p_newPM, int p_friendID);

        [OperationContract(IsOneWay = true)]
        void ReceiveAddRequest(UserInfo p_senderInfo);

        [OperationContract(IsOneWay = true)]
        void AddNewFriend(UserInfo p_newFriendInfo);

        [OperationContract(IsOneWay = true)]
        void ReceiveIsTypingMessage(int p_friendID);

        [OperationContract(IsOneWay = true)]
        void NotifyLoggedOff(int p_friendID);

       
        [OperationContract]
        void ErrorNotification(string p_errorMsg);

        #region Video Conversation

        [OperationContract(IsOneWay = true)]
        void ReceiveVideoRequest(int p_senderID);

        [OperationContract(IsOneWay = true)]
        void StartVideoConv(int p_senderID);

        [OperationContract(IsOneWay = true)]
        void EndVideoConv(int p_senderID);

        #endregion

        #region File Transfer
        [OperationContract(IsOneWay = true)]
        void ReceiveFile(string p_fileName, int p_sender);

        [OperationContract(IsOneWay = true)]
        void StartFileTransfer(string p_fileName, int p_sender);

        [OperationContract(IsOneWay = true)]
        void ReceiveFileRequest(string p_fileName, int p_sender);

        [OperationContract(IsOneWay = true)]
        void TransferFailed(string p_fileName, int p_receiverID);

        [OperationContract(IsOneWay = true)]
        void TransferCompleted(string p_fileName, int p_receiverID);
        #endregion

    }
}
