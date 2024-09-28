using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PokedexAPI.Models;
using System.Text.RegularExpressions;
using static PokedexAPI.Logger;

namespace PokedexAPI.PokeDex
{
    public class Pokedex
    {
        private static readonly string pokedexUrl = @"https://www.pokemon.com/us/api/pokedex/kalos";
        private static readonly Func<int, string> pokeapiSpeciesUrl = (int id) => @$"https://pokeapi.co/api/v2/pokemon-species/{id}";
        private static readonly Func<int, string> pokedevsSpeciesUrl = (int id) => @$"https://ex.traction.one/pokedex/pokemon/{id}";
        private static readonly Dictionary<int, Pokemon> pokedex = new();
        private static readonly Dictionary<string, int> reverseDex = new();
        public Pokedex()
        {
            var dexPath = Path.Combine(Directory.GetCurrentDirectory(), "Mocks", "detailed-pokedex.json");
            var detailedPokedexJson = File.ReadAllText(dexPath);

            var dex = JsonConvert.DeserializeObject<Dictionary<int, Pokemon>>(detailedPokedexJson);
            foreach (var entry in dex)
            {
                pokedex.Add(entry.Key, entry.Value);
                reverseDex.Add(entry.Value.Name, entry.Key);
            }
        }

        public Pokemon Get(int index)
        {
            if (pokedex.ContainsKey(index))
            {
                return pokedex[index];
            }
            return null;
        }

        public Pokemon GetDetailedPokemon(int id, bool loadPrevious = true, bool loadNext = true)
        {
            var pokemon = pokedex[id];
            if(loadPrevious && pokemon.Details.EvolvesFrom.HasValue)
            {
                pokemon.Details.PrevEvolution = GetDetailedPokemon(pokemon.Details.EvolvesFrom.Value, loadPrevious, false);
            }
            if (loadNext && pokemon.Details.EvolvesTo.HasValue)
            {
                pokemon.Details.NextEvolution = GetDetailedPokemon(pokemon.Details.EvolvesTo.Value, false, loadNext);
            }

            return pokemon;

        }

        public List<Pokemon> GetAll()
        {
            return pokedex.Values
                .Select(p => new Pokemon
                {
                    ID = p.ID,
                    Name = p.Name,
                    Weakness = p.Weakness,
                    Type = p.Type,
                    Image = p.Image,
                }).ToList();
        }

        public int GetPokemonId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return -1;
            }
            if(name == "nidoran-f")
            {
                return 29;
            }
            if (name == "nidoran-m")
            {
                return 32;
            }
            var normalized = name.ToLower();
            if (!reverseDex.ContainsKey(normalized))
            {
                return -1;
            }
            return reverseDex[normalized];
        }
        public Pokemon GetPokemonByName(string name) => pokedex[GetPokemonId(name)];
    }

}
