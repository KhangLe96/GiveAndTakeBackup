using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Giveaway.API.Shared.Services.Facebook
{
	public class FacebookClient : IFacebookClient
	{
		public const string FacebookApiUrl = "https://graph.facebook.com/v2.11/";

		private readonly HttpClient httpClient;

		public FacebookClient()
		{
			httpClient = new HttpClient
			{
				BaseAddress = new Uri(FacebookApiUrl)
			};

			httpClient.DefaultRequestHeaders
				.Accept
				.Add(new MediaTypeWithQualityHeaderValue("application/json"));
		}

		public async Task<T> GetAsync<T>(string accessToken, string endpoint, string args = null)
		{
			var response = await httpClient.GetAsync($"{endpoint}?access_token={accessToken}&{args}");

			if (!response.IsSuccessStatusCode)
			{
				return default;
			}

			var result = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(result);
		}

		public async Task PostAsync(string accessToken, string endpoint, object data, string args = null)
		{
			var payload = GetPayload(data);
			await httpClient.PostAsync($"{endpoint}?access_token={accessToken}&{args}", payload);
		}

		private static StringContent GetPayload(object data)
		{
			var json = JsonConvert.SerializeObject(data);
			return new StringContent(json, Encoding.UTF8, "application/json");
		}
	}
}
