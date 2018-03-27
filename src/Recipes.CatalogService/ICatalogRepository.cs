using System.Threading.Tasks;
using Recipes.CatalogService.Domain;

namespace Recipes.CatalogService
{
    public interface ICatalogRepository
    {
        Task<Recipe[]> GetRecipes();
        Task AddRecipe(Recipe recipe);
    }
}