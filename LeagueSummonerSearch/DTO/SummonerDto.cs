namespace LeagueSummonerSearch.DTO
{
    internal class SummonerDto
    {
        public string? Name { get; set; }
        public int Level { get; set; }

        public SummonerDto(string? name, int level)
        {
            Name = name;
            Level = level;
        }
    }
}
