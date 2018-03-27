using System;

namespace Recipes.CatalogService.Domain
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Servings { get; set; }
    }
}
