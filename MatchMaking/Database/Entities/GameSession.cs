using System.ComponentModel.DataAnnotations;

namespace MatchMaking.Database.Entities
{
    public enum GameState
    {
        Full,
        NotFull,
        Finished,
    }

    public class GameSession
    {
        [Key] public Guid Id { get; init; } = Guid.NewGuid();
        public GameState? State { get; set; }
        public int? LatencyMs { get; set; }

        [Required] public Game Game { get; set; }
    }
}