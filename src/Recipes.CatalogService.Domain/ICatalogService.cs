using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Recipes.CatalogService.Domain
{
    public interface ICatalogService : IService
    {
        Task<Recipe[]> GetRecipes();
        Task AddRecipe(Recipe recipe);
    }
}