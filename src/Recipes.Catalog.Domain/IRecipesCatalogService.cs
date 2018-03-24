using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Recipes.Catalog.Domain
{
    public interface IRecipesCatalogService : IService
    {
        Task<IEnumerable<Recipe>> GetAllRecipies();
        Task AddRecipe(Recipe recipe);
    }
}