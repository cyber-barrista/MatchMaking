using MatchMaking.Domain;

namespace MatchMaking.Conversions;

public static class GameNameConversions
{
    public static Database.Entities.GameName ToEntity(this GameName gameName)
    {
        return gameName switch
        {
            GameName.AngryBirds => Database.Entities.GameName.AngryBirds,
            GameName.SmallTownMurders => Database.Entities.GameName.SmallTownMurders,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(gameName), gameName, null)
        };
    }

    public static GameName ToDomain(this Database.Entities.GameName gameName)
    {
        return gameName switch
        {
            Database.Entities.GameName.AngryBirds => GameName.AngryBirds,
            Database.Entities.GameName.SmallTownMurders => GameName.SmallTownMurders,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(gameName), gameName, null)
        };
    }
}