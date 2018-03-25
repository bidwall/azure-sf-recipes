using System.Collections.Generic;
using System.Threading.Tasks;
using Recipes.CatalogService.Domain;

namespace Recipes.CatalogService
{
    public interface ICatalogRepository
    {
        Task<IEnumerable<Recipe>> GetRecipes();
        Task AddRecipe(Recipe recipe);
    }
}