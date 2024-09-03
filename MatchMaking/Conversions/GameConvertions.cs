using MatchMaking.Domain;

namespace MatchMaking.Conversions;

public static class GameConversions
{
    public static Database.Entities.Game ToEntity(this Game game)
    {
        return new Database.Entities.Game
        {
            GameName = game.GameName.ToEntity(),
            PlayerCount = game.PlayerCount,
        };
    }

    public static Game ToDomain(this Database.Entities.Game game)
    {
        return new Game(game.GameName.ToDomain(), game.PlayerCount);
    }
}