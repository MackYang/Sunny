using System;
using System.Collections.Generic;
using System.Text;

namespace Sunny.Common.ConfigOption
{
    public class RedisOption
    {
        public string ConnectionString { get; set; }
        public string InstanceName { get; set; }

        public int DefaultSlidingExpiration { get; set; }
    }
}
