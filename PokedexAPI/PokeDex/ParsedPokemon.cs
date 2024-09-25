using Newtonsoft.Json;

namespace PokedexAPI.PokeDex
{
    internal class ParsedPokemon
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("ThumbnailImage")]
        public string Image { get; set; }

        [JsonProperty("weakness")]
        public List<string> Weakness { get; set; }

        [JsonProperty("type")]
        public List<string> Type { get; set; }

        [JsonProperty("height")]
        public double Height { get; set; }

        [JsonProperty("weight")]
        public double Weight { get; set; }
    }
}
