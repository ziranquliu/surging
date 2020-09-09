using Confluent.Kafka;
//using Confluent.Kafka.Serialization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace Surging.Core.EventBusKafka.Implementation
{
    public class KafkaConsumerPersistentConnection : KafkaPersistentConnectionBase
    {
        private readonly ILogger<KafkaConsumerPersistentConnection> _logger;
        private ConcurrentBag<IConsumer<Null, string>> _consumerClients;
        private IConsumer<Null, string> _consumerClient;
        private readonly IDeserializer<string> _stringDeserializer;
        bool _disposed;

        public KafkaConsumerPersistentConnection(ILogger<KafkaConsumerPersistentConnection> logger)
            : base(logger, AppConfig.KafkaConsumerConfig)
        {
            _logger = logger;
            _stringDeserializer = Deserializers.Utf8;
            _consumerClients = new ConcurrentBag<IConsumer<Null, string>>();
        }

        public override bool IsConnected => _consumerClient != null && !_disposed;

        public override Action Connection(IEnumerable<KeyValuePair<string, object>> options)
        {
            return () =>
            {
                ConsumerBuilder<Null, string> consumerBuilder = new ConsumerBuilder<Null, string>(options as IEnumerable<KeyValuePair<string, string>>);
                _consumerClient = consumerBuilder.Build();
                consumerBuilder.SetOffsetsCommittedHandler(OnConsumeError);
                consumerBuilder.SetErrorHandler(OnConnectionException);
                //_consumerClient.OnConsumeError += OnConsumeError;
                //_consumerClient.OnError += OnConnectionException;
                _consumerClients.Add(_consumerClient);

            };
        }

        public void Listening(TimeSpan timeout)
        {
            if (!IsConnected)
            {
                TryConnect();
            }
            while (true)
            {
                foreach (var client in _consumerClients)
                {
                    //client.Poll(timeout);
                    ConsumeResult<Null, string> consumeResult = client.Consume(timeout);
                    if (!consumeResult.IsPartitionEOF)
                    {
                        continue;
                    }
                    if (consumeResult.Offset % 5 == 0)
                    {
                        var committedOffsets = client.Commit();
                    }
                }
            }
        }

        public override object CreateConnect()
        {
            TryConnect();
            return _consumerClient;
        }

        private void OnConsumeError(IConsumer<Null, string> consumer, CommittedOffsets committedOffsets)
        {
            //var message = e.Deserialize<Null, string>(null, _stringDeserializer);
            if (_disposed) return;

            //_logger.LogWarning($"An error occurred during consume the message; Topic:'{e.Topic}'," +
            //    $"Message:'{message.Value}', Reason:'{e.Error}'.");
            _logger.LogWarning($"An error occurred during consume the message; ErrorCode:'{committedOffsets.Error.Code}'," +
                $"Reason:'{committedOffsets.Error.Reason}'.");
            foreach (var item in committedOffsets.Offsets)
            {
                if (item != null)
                    _logger.LogWarning($"An error occurred during consume the message; "
                        + $"Topic:'{item.Topic}',"
                        + $"ErrorCode:'{item.Error.Code}', "
                        + $"Reason:'{committedOffsets.Error.Reason}'.");
            }
            TryConnect();
        }

        private void OnConnectionException(object sender, Error error)
        {
            if (_disposed) return;

            _logger.LogWarning($"A Kafka connection throw exception.info:{error} ,Trying to re-connect...");

            TryConnect();
        }

        public override void Dispose()
        {
            if (_disposed) return;

            _disposed = true;

            try
            {
                _consumerClient.Dispose();
            }
            catch (IOException ex)
            {
                _logger.LogCritical(ex.ToString());
            }
        }
    }
}
