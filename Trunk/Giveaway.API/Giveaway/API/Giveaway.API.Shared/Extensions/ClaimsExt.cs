using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Security.Claims;

namespace Giveaway.API.Shared.Extensions
{
	public static class ClaimsExt
	{
		public static Guid GetUserId(this IEnumerable<Claim> claims)
			=> Guid.TryParse(GetClaimValue(claims, ClaimTypes.Sid), out Guid id)
				? id
				: throw new AuthenticationException($"Cannot read claim [{ClaimTypes.Sid}]");

		public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
			=> claimsPrincipal.Claims.GetUserId();

		public static string GetClient(this IEnumerable<Claim> claims)
			=> GetClaimValue(claims, ClaimTypes.System);

		public static string GetClient(this ClaimsPrincipal claimsPrincipal)
			=> claimsPrincipal.Claims.GetClient();

		private static string GetClaimValue(this IEnumerable<Claim> claims, string claimType)
		{
			var claim = claims.FirstOrDefault(x => x.Type == claimType);
			if (claim == null) throw new AuthenticationException($"Token does not contain claim [{claimType}]");
			return claim.Value;
		}
	}
}
