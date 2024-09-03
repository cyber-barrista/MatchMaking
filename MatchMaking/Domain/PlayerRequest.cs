namespace MatchMaking.Domain
{
    public enum RequestStatus
    {
        InProgress,
        Joined,
        SessionFull,
    }

    public enum RequestType
    {
        Join,
        Leave,
    }

    public enum Latency
    {
        Low,
        Medium,
        High,
    }


    public record PlayerRequest(
        Guid Id,
        RequestType RequestType,
        Guid GameId,
        Guid PlayerId,
        Guid? GameSessionId,
        RequestStatus Status,
        Latency Latency
    );
}