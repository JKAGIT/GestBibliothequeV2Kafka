using System;
using System.Diagnostics;

public class Class1
{
	public Class1()
	{

        private readonly IConsumer<string, string> _consumer;
    private readonly string _topic;

    public KafkaConsumer(string bootstrapServers, string groupId, string topic)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = bootstrapServers,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
        _consumer = new ConsumerBuilder<string, string>(config).Build();
        _topic = topic;
    }
    public void calcul()
    {
        int a = 5 * 5;
    }
}
}
