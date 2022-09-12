using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LxhCommon.BaseEntity
{
    public interface IMyAsyncAuthorizationFilter: IAsyncAuthorizationFilter,IFilter
    {
        public int Order { get; set; }
    }
}
