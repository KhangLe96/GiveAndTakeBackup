using System.Net;
using Giveaway.Data.Models;
using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
    public interface ICategoryService : IEntityService<Category>
    {
        ResponseMessage GetAllCategories();
    }

    public class CategoryService : EntityService<Category>, ICategoryService
    {
        public ResponseMessage GetAllCategories()
        {
            var categories = All();
            return new ResponseMessage(HttpStatusCode.OK, data: categories);
        }
    }
}