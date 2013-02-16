using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerService.Model.DataContracts;

namespace MessengerUser.Model
{
    interface IUserSubject
    {
        void Register(IUserObserver observer);
        void UnRegister(IUserObserver observer);
        void NotfiyLogIn(int p_friendID, Status p_loginStatus);
        void NotifyLoggedOut(int p_friendID);
        void NotfiyStatusChanged(int p_friendID, Status p_newStatus);
        void NotfiyPMChanaged(int p_friendID, string p_newPM);
        void NotfiyReceiveIsTyping(int p_friendID);
        void NotfiyAvatarChanaged(int p_friendID, int p_newAvatarID);
        void NotfiyAddNewFriend(UserInfo p_friendInfo);
        void NotfiyUpdateContactList(List<UserInfo> p_updatedFriendsInfo);

        void NotifyError(string p_errorMsg);
        void NotifyReceiveIM(InstantMessage p_instantMessage);
        void NotifyReceiveAddRequest(UserInfo p_senderInfo);


        #region Video Conversation
        void NotifyStartVideoConv(int p_senderID);
        void NotifyReceiveVideoRequest(int p_senderID);
        void NotifyEndVideoConv(int p_senderID);
        #endregion

        #region File Transfer
        void NotifyFileTransferSuccessed(string p_fileName, int p_receiverID);
        void NotifyFileTransferFailed(string p_fileName, int p_receiverID);
        void NotifyReceiveFileRequest(string p_fileName, int p_sender);
        #endregion
    }
}
    