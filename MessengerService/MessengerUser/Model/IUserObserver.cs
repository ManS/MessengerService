using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerService;
using System.Drawing;

using MessengerService.Model.DataContracts;

namespace MessengerUser
{

    public interface IUserObserver
    {
        void OnUserLogIn(string p_userName, Status p_userStatus);

        void OnUserLogOut(string p_userName);

        void OnPMChanaged(string p_userName, string p_newPM);

        void OnStatusChanaged(string p_userName, Status p_status);

        void OnAvatarChanaged(string p_friendName, int p_newAvatar);

        void OnReceiveIM(InstantMessage p_instantMessage);

        void OnReceieveAddRequest(UserInfo p_senderInfo);

        void OnError(string p_errorMsg);

        void OnTypingMessage(string p_friendName);

        void OnAddNewFriend(UserInfo p_friendInfo);

        void OnReceiveFileTransferRequest(string p_fileName, string p_senderName);

        void OnReceiveVideoRequest(string p_senderName);

        void OnReceiveAudioRequest(string p_senderName);

        void OnStartVideoConv(string p_friendName);

        void OnEndVideoConv(string p_friendName);

        //------------
        /*
         * 
         * void OnReceieveAudioConvRequest(string p_senderName);
        void OnSendingaFrame(Bitmap frame, string p_senderName);

        void OnReceivingFile(bool errorHasOccuredWhileSaving, string filePath, int p_sender);
        
        void OnReceivingVideoReplyRequest(int p_SenderID, bool reply);
        void OnReceiveVideoFrame(MessengerService.Model.DataContracts.Frame videoFrame);
        */
    }
}
