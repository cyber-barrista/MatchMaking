using System.ComponentModel.DataAnnotations;

namespace MatchMaking.Database.Entities
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


    public class PlayerRequest
    {
        [Key] public Guid Id { get; init; } = Guid.NewGuid();
        [Required] public RequestType RequestType { get; set; }
        public Guid? GameSessionId { get; set; }
        [Required] public RequestStatus Status { get; set; }
        [Required] public Latency Latency { get; set; }

        [Required] public Game Game { get; set; }
        [Required] public Player Player { get; set; }
    }
}