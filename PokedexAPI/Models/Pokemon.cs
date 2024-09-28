namespace PokedexAPI.Models
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<string> Type { get; set; }
        public List<string> Weakness { get; set; }
        public PokemonDetails? Details { get; set; }

        public Pokemon Clone()
        {
            return new Pokemon 
            {
                ID = ID, 
                Name = Name, 
                Image = Image, 
                Type = Type, 
                Weakness = Weakness,
                Details = Details?.Clone()
            };
        }
    }
}
