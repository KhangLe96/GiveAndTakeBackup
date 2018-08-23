using System;
using Microsoft.Extensions.DependencyInjection;

namespace CarPooling.API.Shared.Helpers
{
	public class ServiceProviderHelper
	{
		public static ServiceProviderHelper Current { get; private set; }

		private IServiceProvider ServiceProvider { get; set; }

		private static ServiceProviderHelper _resolver;

		public static void Init(IServiceProvider serviceProvider)
		{
			Current = new ServiceProviderHelper
			{
				ServiceProvider = serviceProvider
			};
		}


		public object GetService(Type serviceType)
			=> ServiceProvider.GetService(serviceType);

		public T GetService<T>()
			=> ServiceProvider.GetService<T>();
	}
}
