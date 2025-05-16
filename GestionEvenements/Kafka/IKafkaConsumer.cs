using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEvenements.Kafka
{
    public interface IKafkaConsumer<T>
    {
        Task ConsumeAsync(Func<T, Task> onMessageReceived, CancellationToken cancellationToken);
    }
}
