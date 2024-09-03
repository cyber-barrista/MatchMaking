using FluentAssertions;
using MatchMaking.Domain;
using MatchMaking.Service;
using MatchMaking.Tests.Integration.TestExtensions;
using Testcontainers.PostgreSql;

namespace MatchMaking.Tests.Integration.Service;

public sealed class IdempotencyCheckerTest : IAsyncLifetime
{
    private readonly PostgreSqlContainer postgresContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.4")
        .Build();

    private IIdempotencyChecker<PlayerRequest>? idempotencyChecker;

    private IIdempotencyChecker<PlayerRequest> getIdempotencyChecker()
    {
        return idempotencyChecker ?? throw new ArgumentNullException(nameof(idempotencyChecker));
    }

    public async Task InitializeAsync()
    {
        await postgresContainer.StartAsync();
        idempotencyChecker = new IdempotencyChecker(postgresContainer.ToDbContext());
    }

    public Task DisposeAsync()
    {
        return postgresContainer.DisposeAsync().AsTask();
    }

    private PlayerRequest samplePlayerRequest = new PlayerRequest(
        Guid.NewGuid(),
        RequestType.Join,
        Guid.NewGuid(),
        Guid.NewGuid(),
        null,
        RequestStatus.InProgress,
        Latency.Low
    );

    [Fact]
    // Fails since there are no cards & players in the database
    public async void SubmittingRecordsWithDifferentIdsWorks()
    {
        var newRequest = samplePlayerRequest with { Id = Guid.NewGuid() };
        await getIdempotencyChecker().Check(samplePlayerRequest);
        var newRequestChecked = await getIdempotencyChecker().Check(newRequest);

        newRequestChecked.Should().BeEquivalentTo(newRequest);
    }

    [Fact]
    public async void DuplicateRecordsIsNotSubmited()
    {
        await getIdempotencyChecker().Check(samplePlayerRequest);
        var deduplicatedRequest = await getIdempotencyChecker().Check(samplePlayerRequest);

        deduplicatedRequest.Should().BeNull();
    }
}