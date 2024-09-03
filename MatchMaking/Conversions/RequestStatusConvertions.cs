using MatchMaking.Domain;

namespace MatchMaking.Conversions;

public static class RequestStatusConversions
{
    public static Database.Entities.RequestStatus ToEntity(this RequestStatus requestStatus)
    {
        return requestStatus switch
        {
            RequestStatus.InProgress => Database.Entities.RequestStatus.InProgress,
            RequestStatus.SessionFull => Database.Entities.RequestStatus.SessionFull,
            RequestStatus.Joined => Database.Entities.RequestStatus.Joined,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(requestStatus), requestStatus, null)
        };
    }

    public static RequestStatus ToDomain(this Database.Entities.RequestStatus requestStatus)
    {
        return requestStatus switch
        {
            Database.Entities.RequestStatus.InProgress => RequestStatus.InProgress,
            Database.Entities.RequestStatus.SessionFull => RequestStatus.SessionFull,
            Database.Entities.RequestStatus.Joined => RequestStatus.Joined,
            // :sad:
            _ => throw new ArgumentOutOfRangeException(nameof(requestStatus), requestStatus, null)
        };
    }
}