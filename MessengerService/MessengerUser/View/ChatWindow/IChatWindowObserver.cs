using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengerUser.View
{
    public interface IChatWindowObserver
    {
        void OnChatWindowClosed(string p_formName);
    }
}
