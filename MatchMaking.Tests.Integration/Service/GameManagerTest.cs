using FluentAssertions;
using MatchMaking.Domain;
using MatchMaking.Service;
using MatchMaking.Tests.Integration.TestExtensions;
using Testcontainers.PostgreSql;

namespace MatchMaking.Tests.Integration.Service;

public sealed class GameManagerTest : IAsyncLifetime
{
    private readonly PostgreSqlContainer postgresContainer = new PostgreSqlBuilder()
        .WithImage("postgres:16.4")
        .Build();

    private IGameManager? gameManager;

    private IGameManager getGameManager()
    {
        return gameManager ?? throw new ArgumentNullException(nameof(gameManager));
    }

    public async Task InitializeAsync()
    {
        await postgresContainer.StartAsync();
        gameManager = new GameManager(postgresContainer.ToDbContext());
    }

    public Task DisposeAsync()
    {
        return postgresContainer.DisposeAsync().AsTask();
    }

    [Fact]
    public async void GameAdditionWorks()
    {
        var testGame = new Game(GameName.AngryBirds, 2);
        await getGameManager().Add(testGame);
        var persistedTestGame = await getGameManager().Get(testGame.GameName);
        persistedTestGame.Should().BeEquivalentTo(testGame);
    }

    [Fact]
    public async void GameRemovalWorks()
    {
        var testGame = new Game(GameName.SmallTownMurders, 3);
        await getGameManager().Add(testGame);
        await getGameManager().Remove(testGame);
        var persistedTestGame = await getGameManager().Get(testGame.GameName);
        persistedTestGame.Should().BeNull();
    }
}