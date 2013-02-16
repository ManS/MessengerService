using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerService.Model.DataContracts;

namespace MessengerUser.View
{
    public interface IChatWindow
    {
        void OnFriendStatusChanged(Status p_newStatus);
        void OnUserStatusChanged(Status p_newStatus);
        void OnFriendAvatarChanged(int p_newAvatar);
        void OnUserAvatarChanged(int p_newAvatar);
        void OnStartVideoConversation();
        void OnTypingMessage();
        void OnEndVideoConversation();
    }
}
