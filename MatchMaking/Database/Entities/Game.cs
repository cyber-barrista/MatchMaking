using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace MatchMaking.Database.Entities
{
    public enum GameName
    {
        AngryBirds,
        SmallTownMurders,
    }

    [Index(nameof(GameName), IsUnique = true)]
    public class Game
    {
        [Key] public Guid Id { get; init; } = Guid.NewGuid();
        [Required] public GameName GameName { get; set; }
        [Required] public uint PlayerCount { get; set; }
    }
}