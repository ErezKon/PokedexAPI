using Newtonsoft.Json;
using PokedexAPI.Models;
using static PokedexAPI.Logger;

namespace PokedexAPI.PokeDex
{
    public class Pokedex
    {
        private static readonly string pokedexUrl = @"https://www.pokemon.com/us/api/pokedex/kalos";
        private static readonly Dictionary<int, Pokemon> pokedex = new();
        private static readonly Dictionary<string, int> reverseDex = new();
        public Pokedex()
        {
            Task.Factory.StartNew(() =>
            {
                using var http = new HttpClient();
                Log("Pokedex", "Loading pokedex..");
                var getPokemon = http.GetAsync(pokedexUrl);
                getPokemon.Wait();
                var getPokemonResult = getPokemon.Result;
                var getPokemonTask = getPokemonResult.Content.ReadAsStringAsync();
                getPokemonTask.Wait();
                var pokemonRes = getPokemonTask.Result;

                //var pokemonRes = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Mocks", "pokedex.json"));

                var pokemons = JsonConvert.DeserializeObject<List<ParsedPokemon>>(pokemonRes);
                foreach (var pokemon in pokemons)
                {
                    if (pokedex.ContainsKey(pokemon.Id))
                    {
                        continue;
                    }
                    Log("Pokedex", $"Added pokemon {pokemon.Id}:{pokemon.Name}");
                    pokedex.Add(pokemon.Id, new Pokemon
                    {
                        ID = pokemon.Id,
                        Name = pokemon.Name,
                        Image = pokemon.Image,
                        Type = pokemon.Type.Select(tp =>
                        {
                            return $"{tp[0].ToString().ToUpper()}{tp.Substring(1, tp.Length - 1)}";
                        })
                        .ToList(),
                        Weakness = pokemon.Weakness
                    });
                    reverseDex.Add(pokemon.Name.ToLower(), pokemon.Id);
                }
            });
        }

        public Pokemon Get(int index)
        {
            if (pokedex.ContainsKey(index))
            {
                return pokedex[index];
            }
            return null;
        }

        public List<Pokemon> GetAll()
        {
            return pokedex.Values.ToList();
        }

        public int GetPokemonId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return -1;
            }
            var normalized = name.ToLower();
            if (!reverseDex.ContainsKey(normalized))
            {
                return -1;
            }
            return reverseDex[normalized];
        }
    }

}
