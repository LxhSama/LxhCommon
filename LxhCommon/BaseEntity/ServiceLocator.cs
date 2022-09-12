using Microsoft.AspNetCore.Builder;

namespace LxhCommon.BaseEntity
{
    public static class ServiceLocator
    {
        public static IServiceProvider Instance
        {
            get; set;
        }
    }
}
