using MatchMaking.Conversions;
using MatchMaking.Database;
using MatchMaking.Domain;
using Microsoft.EntityFrameworkCore;

namespace MatchMaking.Service;

public sealed class GameManager(ApplicationDbContext dbContext) : IGameManager
{
    private readonly ApplicationDbContext dbContext = dbContext;

    public async Task Add(Game game)
    {
        dbContext.Games.Add(game.ToEntity());
        await dbContext.SaveChangesAsync();
    }

    public async Task Remove(Game game)
    {
        var persisted = dbContext.Games.FirstOrDefault(g => g.GameName == game.GameName.ToEntity());

        if (persisted != null)
        {
            dbContext.Games.Remove(persisted);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task<Game?> Get(GameName gameName)
    {
        var gameEntity = await dbContext.Games
            .Where(game => game.GameName == gameName.ToEntity())
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return gameEntity?.ToDomain();
    }
}