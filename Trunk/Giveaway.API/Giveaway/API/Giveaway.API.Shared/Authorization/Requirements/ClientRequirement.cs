using Microsoft.AspNetCore.Authorization;

namespace Giveaway.API.Shared.Authorization.Requirements
{
	public class ClientRequirement : IAuthorizationRequirement
	{
		public string Client { get; }

		public ClientRequirement(string client)
		{
			Client = client;
		}
	}
}
