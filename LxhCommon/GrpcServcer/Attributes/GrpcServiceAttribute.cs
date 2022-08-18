using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxhCommon.GrpcServcer
{
    public class GrpcServiceAttribute: Attribute
    {
        public bool IsStart { get; set; }
    }
}
