using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace MessengerService.Model.DataContracts
{
    [DataContract]
    public enum Status
    {
        [EnumMember]
        Online,
        [EnumMember]
        Offline,
        [EnumMember]
        Busy,
        [EnumMember]
        Away,
    }
}
