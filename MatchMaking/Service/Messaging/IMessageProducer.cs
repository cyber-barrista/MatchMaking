namespace MatchMaking.Service.Messaging;

public interface IMessageProducer<M>
{
    public Task Produce(M message);
}