using MatchMaking.Domain;

namespace MatchMaking.Service;

public interface IGameManager
{
    public Task Add(Game game);
    public Task Remove(Game game);
    public Task<Game?> Get(GameName gameName);
}