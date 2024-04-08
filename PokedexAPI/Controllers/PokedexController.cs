using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PokedexAPI.PokeDex;
using PokedexAPI.Models;

using static PokedexAPI.Logger;

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

        [HttpGet("Get/{id}")]
        public Pokemon Get(int id)
        {
            return _pokedex.Get(id);
        }
    }
}
