using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokedexAPI.PokeDex;
using PokedexAPI.Models;

using static PokedexAPI.Logger;
using Newtonsoft.Json;

namespace PokedexAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PokedexController : ControllerBase
    {

        private Pokedex _pokedex;
        public PokedexController(Pokedex pokedex)
        {
            _pokedex = pokedex;
        }

        [HttpGet("GetPokedex")]
        public List<Pokemon> GetPokedex()
        {
            Log("[Pokedex]", "Getting All Pokedex");
            return _pokedex.GetAll();
        }

        //[HttpGet("Transform")]
        //public List<Pokemon> Transform()
        //{
        //    var dex = _pokedex.GetAll(true)
        //        .Select(p =>
        //        {
        //            p.Image = $"https://raw.githubusercontent.com/PokeMiners/pogo_assets/refs/heads/master/Images/Pokemon/pokemon_icon_{p.ID:000}_00.png";
        //            return p;
        //        })
        //        .ToList();
        //    var file = @"C:\Users\Erez_Konforti\source\repos\PokedexAPI\PokedexAPI\Mocks\detailed-pokedex-2.json";
        //    var json = JsonConvert.SerializeObject(dex, Formatting.Indented);
        //    System.IO.File.WriteAllText(file, json);
        //    return dex;
        //}


        [HttpGet("Get/{id}")]
        public Pokemon Get(int id)
        {
            var ret = _pokedex.GetDetailedPokemon(id);
            return ret;
        }
    }
}
