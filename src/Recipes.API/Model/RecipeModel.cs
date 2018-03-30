using System;
using Newtonsoft.Json;

namespace Recipes.API.Model
{
    public class RecipeModel
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("servings")]
        public int Servings { get; set; }
    }
}
