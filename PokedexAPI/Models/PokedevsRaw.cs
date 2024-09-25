using Newtonsoft.Json;

namespace PokedexAPI.Models
{
    public class PokedevsRaw
    {
        [JsonProperty("number")]
        public int Number { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("gen")]
        public int Gen { get; set; }

        [JsonProperty("species")]
        public string Species { get; set; }

        [JsonProperty("types")]
        public List<string> Types { get; set; }

        [JsonProperty("abilities")]
        public List<Ability> Abilities { get; set; }

        [JsonProperty("height")]
        public string Height { get; set; }

        [JsonProperty("weight")]
        public string Weight { get; set; }

        [JsonProperty("mega")]
        public bool? Mega { get; set; }

        [JsonProperty("baseStats")]
        public BaseStats BaseStats { get; set; }

        [JsonProperty("training")]
        public Training Training { get; set; }

        [JsonProperty("breeding")]
        public Breeding Breeding { get; set; }

        [JsonProperty("sprite")]
        public string Sprite { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Ability
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("hidden")]
        public bool Hidden { get; set; }
    }

    public class BaseStats
    {
        [JsonProperty("hp")]
        public int Hp { get; set; }

        [JsonProperty("attack")]
        public int Attack { get; set; }

        [JsonProperty("defense")]
        public int Defense { get; set; }

        [JsonProperty("spAtk")]
        public int SpAtk { get; set; }

        [JsonProperty("spDef")]
        public int SpDef { get; set; }

        [JsonProperty("speed")]
        public int Speed { get; set; }
    }

    public class Breeding
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("eggGroups")]
        public List<string> EggGroups { get; set; }

        [JsonProperty("eggCycles")]
        public string EggCycles { get; set; }
    }

    public class Training
    {
        [JsonProperty("evYield")]
        public string EvYield { get; set; }

        [JsonProperty("catchRate")]
        public string CatchRate { get; set; }

        [JsonProperty("baseFriendship")]
        public string BaseFriendship { get; set; }

        [JsonProperty("baseExp")]
        public string BaseExp { get; set; }

        [JsonProperty("growthRate")]
        public string GrowthRate { get; set; }
    }


}
