using MvvmCross;
using GiveAndTake.Core;
using GiveAndTake.Core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Plugin.Connectivity;

namespace GiveAndTake.Core.Services
{
	public class RestClient
	{
		private const string Bearer = "Bearer";
		private const string ContentType = "application/json";

/*		public RestClient()
		{
			_networkHelper = Mvx.Resolve<INetworkHelper>();
		}*/

		public async Task<BaseResponse> Get(string url, string token = null, Dictionary<string, string> parameters = null)
		{
			var request = parameters != null
				? $"?{string.Join("&", parameters.Select(kvp => $"{kvp.Key}={kvp.Value}"))}"
				: string.Empty;
			return await SendRequest(RequestMethod.Get, url + request, token).ConfigureAwait(false);
		}

		public async Task<BaseResponse> Post(string url, HttpContent content, string token = null)
		{
			return await SendRequest(RequestMethod.Post, url, token, content).ConfigureAwait(false);
		}

		public async Task<BaseResponse> Put(string url, HttpContent content, string token = null)
		{
			return await SendRequest(RequestMethod.Put, url, token, content).ConfigureAwait(false);
		}

		public async Task<BaseResponse> Delete(string url, string token = null)
		{
			return await SendRequest(RequestMethod.Delete, url, token).ConfigureAwait(false);
		}

		private async Task<BaseResponse> SendRequest(RequestMethod method, string url, string token = null, HttpContent content = null)
		{
			var response = new BaseResponse
			{
				NetworkStatus = NetworkStatus.Success
			};

			if (!CrossConnectivity.Current.IsConnected)
			{
				response.NetworkStatus = NetworkStatus.NoWifi;

				return response;
			}

			try
			{
				HttpResponseMessage httpResponse;

				using (HttpClient httpClient = CreateClient(token))
				{
					switch (method)
					{
						case RequestMethod.Get:
							httpResponse = await httpClient.GetAsync(url).ConfigureAwait(false);
							break;
						case RequestMethod.Post:
							httpResponse = await httpClient.PostAsync(url, content).ConfigureAwait(false);
							break;
						case RequestMethod.Put:
							httpResponse = await httpClient.PutAsync(url, content).ConfigureAwait(false);
							break;
						case RequestMethod.Delete:
							httpResponse = await httpClient.DeleteAsync(url).ConfigureAwait(false);
							break;
						default:
							throw new Exception("Request method " + method + " is not supported");
					}
				}

				response.RawContent = httpResponse.Content.ReadAsStringAsync().Result;
			}
			catch (OperationCanceledException)
			{
				Debug.WriteLine("Time out when sending request: " + url);
				response.NetworkStatus = NetworkStatus.Timeout;
			}
			catch (Exception e)
			{
				Debug.WriteLine("Error: \"{0}\" with request: {1}", e.Message, url);
				response.NetworkStatus = NetworkStatus.Exception;
			}

			return response;
		}

		private HttpClient CreateClient(string token = null)
		{
			var client = new HttpClient { BaseAddress = new Uri(AppConstants.ApiUrl) };
			client.DefaultRequestHeaders.Add("ContentType", ContentType);
			client.Timeout = TimeSpan.FromSeconds(AppConstants.ApiTimeout);

			if (token != null)
			{
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Bearer, token);
			}

			return client;
		}
	}
}
