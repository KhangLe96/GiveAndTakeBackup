using Microsoft.Extensions.Configuration;

namespace Giveaway.Service.Services
{
	public interface IConfigurationService
	{
		string ConnectionString { get; }
	}

    public class ConfigurationService : IConfigurationService
    {
        public string ConnectionString => _configRoot.GetConnectionString("DefaultConnection");

        private readonly IConfiguration _configRoot;

        public ConfigurationService(IConfiguration configRoot)
        {
            this._configRoot = configRoot;
        }
    }
}