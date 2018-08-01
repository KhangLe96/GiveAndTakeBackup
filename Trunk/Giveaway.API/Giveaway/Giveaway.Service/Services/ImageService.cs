using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
    public interface IImageService : IEntityService<Image>
    {
    }

    public class ImageService : EntityService<Image>, IImageService
    {

    }
}
