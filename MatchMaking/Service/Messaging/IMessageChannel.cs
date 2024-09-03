namespace MatchMaking.Service.Messaging;

public interface IMessageChannel<M> : IMessageConsumer<M>, IMessageProducer<M>;