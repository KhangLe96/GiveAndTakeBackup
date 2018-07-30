﻿using Giveaway.Data.Models.Database;

namespace Giveaway.Service.Services
{
    public interface ICategoryService : IEntityService<Category>
    {
        
    }

    public class CategoryService : EntityService<Category>, ICategoryService
    {
        
    }
}