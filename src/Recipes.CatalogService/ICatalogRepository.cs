using System;
using System.Threading.Tasks;
using Recipes.CatalogService.Domain;

namespace Recipes.CatalogService
{
    public interface ICatalogRepository
    {
        Task SaveRecipe(Recipe recipe);
        Task<Recipe> GetRecipe(Guid id);
        Task<Recipe[]> GetRecipes();
        Task DeleteRecipe(Guid id);
    }
}