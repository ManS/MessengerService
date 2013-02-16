using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengerUser.View
{
    interface IChatWindowSubject
    {
        void Register(IChatWindowObserver p_observer);
        void UnRegister(IChatWindowObserver p_observer);
        void NotifyClosed(string p_windowName);
    }
}
