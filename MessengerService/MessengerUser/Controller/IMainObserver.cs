using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MessengerUser.Controller
{
    public interface IMainObserver
    {
        void OnRegistrationSucceed();
        void OnSignInSucceed();
        void OnError(string p_errorMsg);
    }
}
