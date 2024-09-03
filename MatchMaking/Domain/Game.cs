namespace MatchMaking.Domain
{
    public enum GameName
    {
        AngryBirds,
        SmallTownMurders,
    }

    public record Game(GameName GameName, uint PlayerCount);
}