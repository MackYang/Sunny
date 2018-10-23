using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api.DTO.Request
{
    public class Enummm
    {

        public enum IntEnum {

           Wifi,
           [Description("这是一个很NB的GPS哦")]
            GPS
        }
    }
}
