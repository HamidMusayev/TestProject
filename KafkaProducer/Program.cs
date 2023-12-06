// See https://aka.ms/new-console-template for more information
using Confluent.Kafka;

string bootstrapServers = "localhost:9092";
string topic = "test_topic";

var config = new ProducerConfig
{
    BootstrapServers = bootstrapServers
};

using (var producer = new ProducerBuilder<Null, string>(config).Build())
{
    Console.WriteLine("Enter messages (press 'q' to quit):");

    while (true)
    {
        string messageText = Console.ReadLine()!;

        if (messageText.ToLower() == "q")
            break;

        var message = new Message<Null, string> { Value = messageText };

        var messageResult = await producer.ProduceAsync(topic, message);

        Console.WriteLine($"Produced message to: {messageResult.TopicPartitionOffset}");
    }
}