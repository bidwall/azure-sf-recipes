﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Recipes.Catalog.Domain
{
    public interface ICatalogService : IService
    {
        Task<IEnumerable<Recipe>> GetRecipes();
        Task AddRecipe(Recipe recipe);
    }
}