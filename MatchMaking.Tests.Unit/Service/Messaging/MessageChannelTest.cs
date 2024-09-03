using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using FluentAssertions;
using MatchMaking.Service.Messaging;
using MatchMaking.Tests.TestExtensions;

namespace MatchMaking.Tests.Service.Messaging;

public class MessageChannelTest
{
    [Fact]
    public async void MessagingChannelPassesDataThrough()
    {
        var messaging = new MessageChannel<string>(m => m);
        string[] messages = ["one", "two", "three", "four", "five", "six"];

        foreach (var message in messages)
        {
            await messaging.Produce(message);
        }

        var consumedMessages = await messaging
            .Consume()
            .Merge()
            .Take(messages.Length)
            .ToArray();

        consumedMessages.Should().BeEquivalentTo(messages);
    }

    private enum Latency
    {
        Low,
        Medium,
        High,
    }

    private record TestData(Latency Latency, string Payload);

    [Fact]
    public async void MessagingChannelgroupsDataCorrectly()
    {
        var messaging = new MessageChannel<TestData>(m => m.Latency.ToString());
        TestData[] messages =
        [
            new(Latency.Low, "one"),
            new(Latency.Medium, "one"),
            new(Latency.High, "one"),
            new(Latency.Low, "two"),
            new(Latency.Medium, "two"),
            new(Latency.High, "two"),
        ];

        foreach (var message in messages)
        {
            await messaging.Produce(message);
        }

        var consumedMessages = await await messaging
            .Consume()
            .Select(stream => stream.Take(TimeSpan.FromSeconds(1)).ToArray().ToTask())
            .Take(TimeSpan.FromSeconds(1))
            .ToArray()
            .Select(tasks => tasks.WhenAll())
            .ToTask();


        TestData[][] expectedMessages =
        [
            [new TestData(Latency.Low, "one"), new TestData(Latency.Low, "two")],
            [new TestData(Latency.Medium, "one"), new TestData(Latency.Medium, "two")],
            [new TestData(Latency.High, "one"), new TestData(Latency.High, "two")],
        ];

        consumedMessages.Should().BeEquivalentTo(expectedMessages);
    }
}