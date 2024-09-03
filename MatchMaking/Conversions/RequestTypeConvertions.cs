using MatchMaking.Domain;

namespace MatchMaking.Conversions;

public static class RequestTypeConversions
{
    public static Database.Entities.RequestType ToEntity(this RequestType requestType)
    {
        return requestType switch
        {
            RequestType.Join => Database.Entities.RequestType.Join,
            RequestType.Leave => Database.Entities.RequestType.Join,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
        };
    }

    public static RequestType ToDomain(this Database.Entities.RequestType requestType)
    {
        return requestType switch
        {
            Database.Entities.RequestType.Join => RequestType.Join,
            Database.Entities.RequestType.Leave => RequestType.Leave,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(requestType), requestType, null)
        };
    }
}