using McMaster.Extensions.CommandLineUtils;
using Newtonsoft.Json;
using Surging.Core.CPlatform.Messages;
using Surging.Tools.Cli.Commands;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Surging.Tools.Cli.Internal.Netty
{
    public class DotnettyTransportClient : ITransportClient
    {
        private readonly IMessageSender _messageSender;
        private readonly IMessageListener _messageListener;

        private readonly ConcurrentDictionary<string, TaskCompletionSource<TransportMessage>> _resultDictionary =
            new ConcurrentDictionary<string, TaskCompletionSource<TransportMessage>>();

        private readonly CommandLineApplication<CurlCommand> _app;
        //private readonly IHttpClientProvider _httpClientProvider;

        public DotnettyTransportClient(IMessageSender messageSender, IMessageListener messageListener, CommandLineApplication app)
        {
            _app = app as CommandLineApplication<CurlCommand>;
            _messageSender = messageSender;
            _messageListener = messageListener;
            messageListener.Received += MessageListener_Received;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<RemoteInvokeResultMessage> SendAsync(CancellationToken cancellationToken)
        {
            try
            {
                var def = new Dictionary<string, object>();
                var command = _app.Model;
                var transportMessage = TransportMessage.CreateInvokeMessage(new RemoteInvokeMessage
                {
                    DecodeJOject = true,
                    Parameters = string.IsNullOrEmpty(command.Data) ? def : JsonConvert.DeserializeObject<IDictionary<string, object>>(command.Data),
                    ServiceId = command.ServiceId,
                    ServiceKey = command.ServiceKey,
                    Attachments = string.IsNullOrEmpty(command.Attachments) ? def : JsonConvert.DeserializeObject<IDictionary<string, object>>(command.Attachments)
                });

                var callbackTask = RegisterResultCallbackAsync(transportMessage.Id);

                try
                {
                    await _messageSender.SendAndFlushAsync(transportMessage);
                }
                catch (Exception)
                {
                    throw;
                }

                return await callbackTask;
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private async Task<RemoteInvokeResultMessage> RegisterResultCallbackAsync(string id)
        {
            var task = new TaskCompletionSource<TransportMessage>();
            _resultDictionary.TryAdd(id, task);
            try
            {
                var result = await task.Task;
                return result.GetContent<RemoteInvokeResultMessage>();
            }
            finally
            {
                //删除回调任务
                _resultDictionary.TryRemove(id, out _);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task MessageListener_Received(IMessageSender sender, TransportMessage message)
        {
            if (!_resultDictionary.TryGetValue(message.Id, out TaskCompletionSource<TransportMessage> task))
                return;

            if (message.IsInvokeResultMessage())
            {
                var content = message.GetContent<RemoteInvokeResultMessage>();
                if (!string.IsNullOrEmpty(content.ExceptionMessage))
                {
                    task.TrySetException(new Exception(content.ExceptionMessage));
                }
                else
                {
                    task.SetResult(message);
                }
            }
            if (message.IsInvokeMessage())
                throw new Exception("message type error");
        }
    }
}
