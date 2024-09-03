namespace MatchMaking.Service;

public interface IIdempotencyChecker<T>
{
    public Task<T?> Check(T data);
}