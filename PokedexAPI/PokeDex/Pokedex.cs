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
        private int alreadyLoaded = 0;
        private int newlyLoaded = 0;
        public Pokedex()
        {
            Task.Factory.StartNew(() =>
            {
                //using var http = new HttpClient();
                //Log("Pokedex", "Loading pokedex..");
                //var getPokemon = http.GetAsync(pokedexUrl);
                //getPokemon.Wait();
                //var getPokemonResult = getPokemon.Result;
                //var getPokemonTask = getPokemonResult.Content.ReadAsStringAsync();
                //getPokemonTask.Wait();
                //var pokemonRes = getPokemonTask.Result;

                var pokemonRes = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Mocks", "pokedex.json"));

                var pokemons = JsonConvert.DeserializeObject<List<ParsedPokemon>>(pokemonRes)
                .GroupBy(p => p.Id)
                .ToDictionary(p => p.Key, p => p.First());

                var dexPath = Path.Combine(Directory.GetCurrentDirectory(), "Mocks", "detailed-pokedex.json");
                var detailedPokedexJson = File.ReadAllText(dexPath);

                var dex = JsonConvert.DeserializeObject<Dictionary<int, Pokemon>>(detailedPokedexJson);
                var pokedex = dex;
                foreach (var pokemon in pokedex)
                {
                    reverseDex.Add(pokemon.Value.Name, pokemon.Key);
                }
                //foreach (var pokemon in dex)
                //{
                //    pokemon.Value.Details.Stats ??= new();
                //    pokemon.Value.Details.Stats.Height = pokemons[pokemon.Key].Height;
                //    pokemon.Value.Details.Stats.Weight = pokemons[pokemon.Key].Weight;
                //}
                /*
                foreach (var pokemon in pokemons)
                {
                    if (pokedex.ContainsKey(pokemon.Id))
                    {
                        continue;
                    }
                    Log("Pokedex", $"Added pokemon {pokemon.Id}:{pokemon.Name}");
                    var parsedPokemon = new Pokemon
                    {
                        ID = pokemon.Id,
                        Name = pokemon.Name,
                        Image = pokemon.Image,
                        Type = pokemon.Type.Select(tp =>
                        {
                            return $"{tp[0].ToString().ToUpper()}{tp.Substring(1, tp.Length - 1)}";
                        })
                        .ToList(),
                        Weakness = pokemon.Weakness,
                        Details = null
                    };
                    pokedex.Add(pokemon.Id, parsedPokemon);
                    reverseDex.Add(pokemon.Name.ToLower(), pokemon.Id);
                }
                
                */

                //foreach (var pokemon in dex)
                //{
                //    try
                //    {
                //        if(LoadPokemonDetails(pokemon.Value))
                //        {
                //            Thread.Sleep(5_000);
                //        }
                //    }
                //    catch (ArgumentOutOfRangeException aex)
                //    {
                //        break;
                //    }
                //    catch (Exception ex)
                //    {
                //        //Thread.Sleep(2000);
                //        continue;
                //    }
                //}

                //Parallel.ForEach(dex, (pokemon) =>
                //{
                //    LoadPokemonDetails(pokemon.Value);
                //});
                //var detailedJson = JsonConvert.SerializeObject(dex, Formatting.Indented);
                //System.IO.File.WriteAllText(dexPath, detailedJson);
                //var s = $"{newlyLoaded}, {alreadyLoaded}";
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
        //public void LoadPokemonDetails(Pokemon pokemon)
        //{
        //    var url = pokeapiSpeciesUrl(pokemon.ID);
        //    using var http = new HttpClient();
        //    var getPokemon = http.GetAsync(url);
        //    getPokemon.Wait();
        //    var getPokemonResult = getPokemon.Result;
        //    var getPokemonTask = getPokemonResult.Content.ReadAsStringAsync();
        //    getPokemonTask.Wait();
        //    string pokemonRes = getPokemonTask.Result;
        //    dynamic parsedResult = JsonConvert.DeserializeObject(pokemonRes);
        //    if (parsedResult == null)
        //    {
        //        return;
        //    }
        //    pokemon.Details ??= new();
        //    pokemon.Details.IsBaby = parsedResult["is_baby"];
        //    pokemon.Details.IsLegendary = parsedResult["is_legendary"];
        //    pokemon.Details.IsMythical = parsedResult["is_mythical"];
        //    pokemon.Details.HasGenderDifference = parsedResult["has_gender_differences"];
        //    var entries = ((Newtonsoft.Json.Linq.JArray)parsedResult["flavor_text_entries"]);
        //    var descriptions = entries
        //        .Where(d => d["language"]["name"].ToString() == "en")
        //        .Select(d => new PokemonDescription
        //        {
        //            Description = d["flavor_text"].ToString().Replace("\n", " ").Replace("\f", " "),
        //            Version = d["version"]["name"].ToString()
        //        })
        //        .ToList();
        //    pokemon.Details.Descriptions = descriptions;
        //    var evolvesFrom = parsedResult["evolves_from_species"];
        //    if(evolvesFrom != null)
        //    {
        //        string previousMonUrl = evolvesFrom["url"];
        //        var previousMon = int.Parse(previousMonUrl.Split("/", StringSplitOptions.RemoveEmptyEntries).Last());
        //        pokemon.Details.EvolvesFrom = previousMon;
        //        //var mon = GetPokemonByName(previousMon);
        //        //if (mon != null)
        //        //{
        //        //    pokemon.Details.EvolvesFrom = mon.ID;
        //        //}
        //    }
        //}

        public bool LoadPokemonDetails(Pokemon pokemon)
        {
            if(pokemon.Details.Stats.HP != 0)
            {
                alreadyLoaded++;
                return false;
            }
            var url = pokedevsSpeciesUrl(pokemon.ID);
            using var http = new HttpClient();
            var getPokemon = http.GetAsync(url);
            getPokemon.Wait();
            var getPokemonResult = getPokemon.Result;
            var getPokemonTask = getPokemonResult.Content.ReadAsStringAsync();
            getPokemonTask.Wait();
            var pokemonRes = getPokemonTask.Result;

            if(pokemonRes.Contains("Too many requests, please try again later."))
            {
                throw new ArgumentOutOfRangeException();
            }

            var parsedResultArr = JsonConvert.DeserializeObject<List<PokedevsRaw>>(pokemonRes);
            var parsedResult = parsedResultArr.First();

            var height = parsedResult.Height.Split(" ").First();
            pokemon.Details.Stats.Height = double.Parse(height);
            pokemon.Details.Stats.Weight = double.Parse(parsedResult.Weight.Split(" ").First());
            pokemon.Details.Stats.HP = parsedResult.BaseStats.Hp;
            pokemon.Details.Stats.Attack = parsedResult.BaseStats.Attack;
            pokemon.Details.Stats.Defence = parsedResult.BaseStats.Defense;
            pokemon.Details.Stats.SpecialAttack = parsedResult.BaseStats.SpAtk;
            pokemon.Details.Stats.SpecialDefence = parsedResult.BaseStats.SpDef;
            pokemon.Details.CatchRate = double.Parse(((string)parsedResult.Training.CatchRate).Split(" ").First());
            pokemon.Details.MaleRarity = double.Parse(((string)parsedResult.Breeding.Gender).Split(" ").First().Replace("%", string.Empty));
            newlyLoaded++;
            return true;
        }
    }

}
