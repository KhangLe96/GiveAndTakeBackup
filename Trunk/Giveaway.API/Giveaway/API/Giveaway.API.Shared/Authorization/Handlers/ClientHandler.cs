using System;
using System.Threading.Tasks;
using Giveaway.API.Shared.Authorization.Requirements;
using Giveaway.API.Shared.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace Giveaway.API.Shared.Authorization.Handlers
{
	public class ClientHandler : AuthorizationHandler<ClientRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientRequirement requirement)
		{
			try
			{
				if (String.Equals(context.User.GetClient(), requirement.Client, StringComparison.InvariantCultureIgnoreCase))
				{
					context.Succeed(requirement);
				}

				return Task.CompletedTask;
			}
			catch
			{
				context.Fail();
				return Task.CompletedTask;
			}
		}
	}
}
