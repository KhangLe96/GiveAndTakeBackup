﻿using Microsoft.Extensions.Configuration;

namespace Giveaway.Service.Services
{
	public class ConfigurationService : IConfigurationService
	{
		public string ConnectionString => _configRoot.GetConnectionString("DefaultConnection");

		private readonly IConfiguration _configRoot;

		public ConfigurationService(IConfiguration configRoot)
		{
			_configRoot = configRoot;
		}
	}
}