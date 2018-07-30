using Microsoft.Extensions.Configuration;

namespace Giveaway.Service.Services
{
	public class ConfigurationService : IConfigurationService
	{
		public string ConnectionString => configRoot.GetConnectionString("DefaultConnection");

		private readonly IConfiguration configRoot;

		public ConfigurationService(IConfiguration configRoot)
		{
			this.configRoot = configRoot;
		}
	}
}