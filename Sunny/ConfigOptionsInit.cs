using Microsoft.Extensions.Options;
using Sunny.Common.ConfigOption;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny.Api
{
    public class ConfigOptionsInit
    {
        TestOption options { get; set; }

        public ConfigOptionsInit(IOptions<TestOption> op)
        {

            options = op.Value;
        }
    }
}
