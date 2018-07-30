using System.Threading.Tasks;
using Giveaway.API.Shared.Exceptions;
//using Giveaway.DataLayers.Models.IntermediateModels;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Services.Facebook
{
	//public class FacebookService : IFacebookService
	//{
	//	private readonly IFacebookClient _facebookClient;

	//	public FacebookService(IFacebookClient facebookClient)
	//	{
	//		_facebookClient = facebookClient;
	//	}

	//	public async Task<FacebookAccount> GetAccountAsync(string accessToken)
	//	{
	//		if (string.IsNullOrEmpty(accessToken))
	//		{
	//			throw new BadRequestException("Empty access token");
	//		}

	//		var result = await _facebookClient.GetAsync<dynamic>(
	//			accessToken, "me", "fields=id,name,email,first_name,last_name,birthday,gender,picture");

	//		if (result == null)
	//		{
	//			throw new BadRequestException("Invalid access token");
	//		}

	//		return JsonConvert.DeserializeObject<FacebookAccount>(JsonConvert.SerializeObject(result));
	//	}

	//	public async Task PostOnWallAsync(string accessToken, string message)
	//		=> await _facebookClient.PostAsync(accessToken, "me/feed", new { message });
	//}
}
