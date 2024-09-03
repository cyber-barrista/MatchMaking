using MatchMaking.Domain;

namespace MatchMaking.Conversions;

public static class LatencyConversions
{
    public static Database.Entities.Latency ToEntity(this Latency latency)
    {
        return latency switch
        {
            Latency.Low => Database.Entities.Latency.Low,
            Latency.Medium => Database.Entities.Latency.Medium,
            Latency.High => Database.Entities.Latency.High,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(latency), latency, null)
        };
    }

    public static Latency ToDomain(this Database.Entities.Latency latency)
    {
        return latency switch
        {
            Database.Entities.Latency.Low => Latency.Low,
            Database.Entities.Latency.Medium => Latency.Medium,
            Database.Entities.Latency.High => Latency.High,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(latency), latency, null)
        };
    }
}