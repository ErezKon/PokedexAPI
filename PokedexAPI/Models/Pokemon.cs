namespace PokedexAPI.Models
{
    public class Pokemon
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<string> Type { get; set; }
        public List<string> Weakness { get; set; }
    }
}
