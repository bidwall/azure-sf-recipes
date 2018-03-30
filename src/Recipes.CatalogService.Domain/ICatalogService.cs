using System;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Remoting;

namespace Recipes.CatalogService.Domain
{
    public interface ICatalogService : IService
    {
        Task SaveRecipe(Recipe recipe);
        Task<Recipe> GetRecipe(Guid id);
        Task<Recipe[]> GetRecipes();
        Task DeleteRecipe(Guid id);
    }
}