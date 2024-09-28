namespace PokedexAPI.Models
{
    public class PokemonStats
    {
        public int Attack { get; set; }
        public int HP { get; set; }

        public int Defence { get; set; }
        public int SpecialAttack { get; set; }
        public int SpecialDefence { get; set; }
        public double Speed { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }

        public PokemonStats Clone()
        {
            return new PokemonStats
            {
                Attack = Attack,
                HP = HP,
                Defence = Defence,
                SpecialAttack = SpecialAttack,
                SpecialDefence = SpecialDefence,
                Speed = Speed,
                Weight = Weight
            };
        }
    }
}
