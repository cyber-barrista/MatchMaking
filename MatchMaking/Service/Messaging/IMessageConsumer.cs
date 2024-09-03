namespace MatchMaking.Service.Messaging;

public interface IMessageConsumer<M>
{
    public IObservable<IObservable<M>> Consume();
}