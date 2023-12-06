// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;

string bootstrapServers = "localhost:9092";
string groupId = "test_group_id";
string topic = "test_topic";

var config = new ConsumerConfig
{
    BootstrapServers = bootstrapServers,
    GroupId = groupId,
    AutoOffsetReset = AutoOffsetReset.Earliest
};

using (var consumer = new ConsumerBuilder<Ignore, string>(config).Build())
{
    consumer.Subscribe(topic);

    CancellationTokenSource cts = new CancellationTokenSource();

    Console.CancelKeyPress += (_, e) =>
    {
        e.Cancel = true;
        cts.Cancel();
    };

    try
    {
        while (true)
        {
            try
            {
                var consumeResult = consumer.Consume(cts.Token);
                Console.WriteLine($"Consumed message: {consumeResult.Message.Value}");
            }
            catch (ConsumeException e)
            {
                Console.WriteLine($"Error consuming message: {e.Error.Reason}");
            }
        }
    }
    catch (OperationCanceledException)
    {
        // This is expected when the application is stopped (Ctrl+C is pressed).
    }
    finally
    {
        consumer.Close();
    }
}
