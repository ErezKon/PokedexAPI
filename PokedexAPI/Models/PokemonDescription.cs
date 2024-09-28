namespace PokedexAPI.Models
{
    public class PokemonDescription
    {
        public string Description { get; set; }
        public string Version { get; set; }

        public PokemonDescription Clone()
        {
            return new PokemonDescription { Description = Description, Version = Version };
        }
    }
}
