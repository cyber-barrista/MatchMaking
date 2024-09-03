using MatchMaking.Conversions;
using MatchMaking.Database;
using MatchMaking.Domain;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace MatchMaking.Service;

public class IdempotencyChecker(ApplicationDbContext dbContext) : IIdempotencyChecker<PlayerRequest>
{
    public async Task<PlayerRequest?> Check(PlayerRequest data)
    {
        await using var transaction = await dbContext.Database.BeginTransactionAsync();
        {
            var existingRequest = await dbContext.PlayerRequests
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Id == data.Id);

            if (existingRequest == null)
            {
                dbContext.PlayerRequests.Add(data.ToEntity());
                await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                return data;
            }
            else
            {
                await transaction.CommitAsync();
                return null;
            }
        }


        // try
        // {
        //     dbContext.PlayerRequests.Add(data.ToEntity());
        //     await dbContext.SaveChangesAsync();
        // }
        // catch (DbUpdateException ex)
        // {
        //     if (ex.InnerException is PostgresException pgEx)
        //     {
        //         if (pgEx.SqlState == "23505") // Unique violation error code
        //         {
        //             return null;
        //         }
        //     }
        //     else
        //     {
        //         throw new InvalidOperationException("Idempotency check failed");
        //     }
        // }
        //
        // return data;
    }
}