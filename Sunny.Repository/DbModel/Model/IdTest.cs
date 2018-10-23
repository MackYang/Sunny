using Sunny.Common.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Repository.DbModel.Model
{
    public class IdTest
    {
        public long Id { get; set; }

        public Enums.RequestType requestType { get; set; }
    }
}
