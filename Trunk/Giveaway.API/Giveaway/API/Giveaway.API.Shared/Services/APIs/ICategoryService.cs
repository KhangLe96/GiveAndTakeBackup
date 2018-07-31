﻿using System;
using System.Linq;
using Giveaway.API.Shared.Responses;
using Giveaway.Data.EF.DTOs.Requests;

namespace Giveaway.API.Shared.Services.APIs
{
    public interface ICategoryService
    {
        IQueryable<CategoryResponse> GetAllCategories();
        bool Delete(Guid id);
        CategoryResponse Create(CategoryRequest request);
    }
}