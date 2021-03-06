﻿using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Recipes.CatalogService.Domain;
using System;
using System.Collections.Generic;
using System.Fabric;
using System.Threading.Tasks;

namespace Recipes.CatalogService
{
    /// <summary>
    /// An instance of this class is created for each service replica by the Service Fabric runtime.
    /// </summary>
    internal sealed class CatalogService : StatefulService, ICatalogService
    {
        private readonly ICatalogRepository _repository;

        public CatalogService(StatefulServiceContext context)
            : base(context)
        {
            _repository = new CatalogRepository(StateManager);
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
            return new[]
            {
                new ServiceReplicaListener(context => new FabricTransportServiceRemotingListener(context, this))
            };
        }

        public async Task SaveRecipe(Recipe recipe)
        {
            await _repository.SaveRecipe(recipe);
        }

        public async Task<Recipe> GetRecipe(Guid id)
        {
            return await _repository.GetRecipe(id);
        }

        public async Task<Recipe[]> GetRecipes()
        {
            return await _repository.GetRecipes();
        }

        public async Task DeleteRecipe(Guid id)
        {
            await _repository.DeleteRecipe(id);
        }
    }
}
