﻿using System;
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

        public async Task<Recipe[]> GetRecipes()
        {
            var recipes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Recipe>>(DictionaryName);
            var result = new List<Recipe>();

            using (var tx = _stateManager.CreateTransaction())
            {
                var allRecipes = await recipes.CreateEnumerableAsync(tx, EnumerationMode.Unordered);

                using (var enumerator = allRecipes.GetAsyncEnumerator())
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

        public async Task AddRecipe(Recipe recipe)
        {
            var recipes = await _stateManager.GetOrAddAsync<IReliableDictionary<Guid, Recipe>>(DictionaryName);

            using (var tx = _stateManager.CreateTransaction())
            {
                await recipes.AddOrUpdateAsync(tx, recipe.Id, recipe, (guid, value) => recipe = value);
                await tx.CommitAsync();
            }
        }
    }
}