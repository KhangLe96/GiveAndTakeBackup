using System.Threading.Tasks;

namespace Giveaway.API.Shared.Services.Facebook
{
	public interface IFacebookClient
	{
		Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null);
		Task PostAsync(string accessToken, string endpoint, object data, string args = null);
	}
}
