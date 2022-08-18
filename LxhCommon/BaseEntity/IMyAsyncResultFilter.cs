using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxhCommon.BaseEntity
{
    internal interface IMyAsyncResultFilter : IAsyncResultFilter, IFilter
    {
        public int Order { get;}
    }
}
