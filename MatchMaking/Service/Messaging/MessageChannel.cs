using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace MatchMaking.Service.Messaging;

/*
 * The class models kafka messaging.
 * IMessageChannel encompasses both consumer (IMessageConsumer) and producer (IMessageProducer)
 */
public class MessageChannel<M>(Func<M, string> groupBy) : IMessageChannel<M>
{
    private readonly ISubject<M> subject = new ReplaySubject<M>();

    public IObservable<IObservable<M>> Consume()
    {
        return subject.GroupBy(groupBy);
    }

    public Task Produce(M message)
    {
        subject.OnNext(message);
        return Task.CompletedTask;
    }
}