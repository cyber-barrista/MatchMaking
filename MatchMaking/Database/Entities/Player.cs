using System.ComponentModel.DataAnnotations;

namespace MatchMaking.Database.Entities
{
    public class Player
    {
        [Key] public Guid Id { get; init; } = Guid.NewGuid();

        public GameSession? GameSession { get; set; }
    }
}