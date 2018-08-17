using GiveAndTake.Core.Helpers;
using GiveAndTake.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GiveAndTake.Core.Services
{
    public class ManagementService : IManagementService
    {
        private readonly RestClient _apiHelper;

        public ManagementService()
        {
            _apiHelper = new RestClient();
        }

        public void GetCategories()
        {
            Task.Run(async () =>
            {
                var response = await _apiHelper.Get(AppConstants.GetCategories);
                if (response != null && response.NetworkStatus == NetworkStatus.Success)
                {
                    var categories = JsonHelper.Deserialize<CategoryList>(response.RawContent);
                }
            });
        }
    }


}
