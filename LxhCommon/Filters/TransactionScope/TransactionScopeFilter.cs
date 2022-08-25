using LxhCommon.BaseEntity;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;
using System.Transactions;

namespace LxhCommon.Filters.TransactionScopes
{
    public class TransactionScopeFilter : IMyAsyncActionFilter
	{
		public int Order { get => 0; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
			bool hasNotTransactionalAttribute = false;
			if (context.ActionDescriptor is ControllerActionDescriptor)
			{
				var actionDesc = (ControllerActionDescriptor)context.ActionDescriptor;
				hasNotTransactionalAttribute = actionDesc.MethodInfo
					.IsDefined(typeof(NotTransactionalAttribute));
			}
			if (hasNotTransactionalAttribute)
			{
				await next();
				return;
			}
			using var txScope =new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
			var result = await next();
			if (result.Exception == null)
			{
				txScope.Complete();
			}
		}

    }
}
