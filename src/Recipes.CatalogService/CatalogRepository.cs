using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Data;
using Microsoft.ServiceFabric.Data.Collections;
using Recipes.CatalogService.Domain;

namespace Recipes.CatalogService
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IReliableStateManager _stateManager;
        private const string DictionaryName = "recipes";

        public CatalogRepository(IReliableStateManager stateManager)
        {
            _stateManager = stateManager;
        }

        public async Task SaveRecipe(Recipe recipe)
        {
            var recipes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Recipe>>(DictionaryName);

            using (var tx = _stateManager.CreateTransaction())
            {
                await recipes.AddOrUpdateAsync(tx, recipe.Id, recipe, (guid, value) => recipe = value);
                await tx.CommitAsync();
            }
        }

        public async Task<Recipe> GetRecipe(Guid id)
        {
            var recipes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Recipe>>(DictionaryName);

            using (var tx = _stateManager.CreateTransaction())
            {
                var recipe = await recipes.TryGetValueAsync(tx, id);
                return recipe.HasValue ? recipe.Value : null;
            }
        }

        public async Task<Recipe[]> GetRecipes()
        {
            var recipes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Recipe>>(DictionaryName);
            var result = new List<Recipe>();

            using (var tx = _stateManager.CreateTransaction())
            {
                var enumerable = await recipes.CreateEnumerableAsync(tx, EnumerationMode.Unordered);

                using (var enumerator = enumerable.GetAsyncEnumerator())
                {
                    while (await enumerator.MoveNextAsync(CancellationToken.None))
                    {
                        KeyValuePair<Guid, Recipe> current = enumerator.Current;
                        result.Add(current.Value);
                    }
                }
            }

            return result.ToArray();
        }

        public async Task DeleteRecipe(Guid id)
        {
            var recipes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Recipe>>(DictionaryName);

            using (var tx = _stateManager.CreateTransaction())
            {
                await recipes.TryRemoveAsync(tx, id);
                await tx.CommitAsync();
            }
        }
    }
}