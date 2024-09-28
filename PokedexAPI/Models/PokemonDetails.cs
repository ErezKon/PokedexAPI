namespace PokedexAPI.Models
{
    public class PokemonDetails
    {
        public bool IsBaby { get; set; }
        public bool IsLegendary { get; set; }
        public bool IsMythical { get; set; }
        public bool HasGenderDifference { get; set; }
        public List<PokemonDescription> Descriptions { get; set; }
        public int Generation { get; set; }
        public double? CatchRate { get; set; }
        public PokemonStats Stats { get; set; }
        public int? EvolvesFrom { get; set; }
        public int? EvolvesTo { get; set; }
        public double? MaleRarity { get; set; }

        public Pokemon? NextEvolution { get; set; }
        public Pokemon? PrevEvolution { get; set; }

        public PokemonDetails Clone()
        {
            return new PokemonDetails
            {
                IsBaby = IsBaby,
                IsLegendary = IsLegendary,
                IsMythical = IsMythical,
                HasGenderDifference = HasGenderDifference,
                Descriptions = Descriptions.Select(d => d.Clone()).ToList(),
                Generation = Generation,
                Stats = Stats.Clone(),
                EvolvesFrom = EvolvesFrom,
                EvolvesTo = EvolvesTo,
                MaleRarity = MaleRarity,
                NextEvolution = NextEvolution?.Clone(),
                PrevEvolution = PrevEvolution?.Clone(),
            };
        }
    }
}
