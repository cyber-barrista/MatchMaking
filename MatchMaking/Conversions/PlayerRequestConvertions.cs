using MatchMaking.Domain;

namespace MatchMaking.Conversions;

public static class PlayerRequestConversions
{
    public static Database.Entities.PlayerRequest ToEntity(this PlayerRequest playerRequest)
    {
        return new Database.Entities.PlayerRequest
        {
            Id = playerRequest.Id,
            RequestType = playerRequest.RequestType.ToEntity(),
            GameSessionId = playerRequest.GameSessionId,
            Status = playerRequest.Status.ToEntity(),
            Latency = playerRequest.Latency.ToEntity(),
        };
    }

    public static PlayerRequest ToDomain(this Database.Entities.PlayerRequest playerRequest)
    {
        return new PlayerRequest(
            playerRequest.Id,
            playerRequest.RequestType.ToDomain(),
            playerRequest.Id,
            playerRequest.Player.Id,
            playerRequest.GameSessionId,
            playerRequest.Status.ToDomain(),
            playerRequest.Latency.ToDomain()
        );
    }
}