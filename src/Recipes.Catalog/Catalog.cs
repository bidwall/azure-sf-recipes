﻿using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Recipes.Catalog.Domain;

namespace Recipes.Catalog
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class Catalog : StatefulService, ICatalogService
    {
        private readonly IRecipeRepository _repository;

        public Catalog(StatefulServiceContext context)
            : base(context)
        {
            _repository = new RecipeRepository(StateManager);
        }

        /// <summary>
        /// Optional override to create listeners (e.g., HTTP, Service Remoting, WCF, etc.) for this service replica to handle client or user requests.
        /// </summary>
        /// <remarks>
        /// For more information on service communication, see https://aka.ms/servicefabricservicecommunication
        /// </remarks>
        /// <returns>A collection of listeners.</returns>
        protected override IEnumerable<ServiceReplicaListener> CreateServiceReplicaListeners()
        {
            return this.CreateServiceRemotingReplicaListeners();
        }

        public async Task<IEnumerable<Recipe>> GetRecipies()
        {
            return await _repository.GetAllRecipes();
        }

        public async Task AddRecipe(Recipe recipe)
        {
            await _repository.AddRecipe(recipe);
        }
    }
}
