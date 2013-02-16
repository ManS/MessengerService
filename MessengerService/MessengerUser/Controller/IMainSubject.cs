using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MessengerService.Model.DataContracts;
using System.ServiceModel;

namespace MessengerUser.Controller
{
    public interface IMainSubject
    {
        void RegisterMainObserver(IMainObserver p_observer);
        void Subscribe(AccountInfo p_userAccountInfo, UserPersonalInfo p_userPersonalInfo);
        void UnRegisterMainObserver(IMainObserver p_observer);
        void ConnectToServer(NetTcpBinding nettcp, EndpointAddress address);
        void SignIn(AccountInfo p_accountInfo, Status p_loginStatus);
        void NotifyError(string p_errorMsg);
        void NotifyRegistrationSucceed();
        void NotifySignInSucceed();
    }
}
