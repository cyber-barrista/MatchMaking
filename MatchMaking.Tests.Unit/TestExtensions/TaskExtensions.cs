namespace MatchMaking.Tests.TestExtensions;

public static class TaskExtensions
{
    public static Task<T[]> WhenAll<T>(this Task<T>[] tasks)
    {
        return Task.WhenAll(tasks);
    }
}