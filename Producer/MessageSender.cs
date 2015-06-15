using System;
using System.Configuration;
using System.Threading;

namespace Producer
{
    public class MessageSender
    {
        public void Send(CancellationTokenSource cancellationToken, int sendDelay, Action<string> updater)
        {
            var client = new Amazon.SQS.AmazonSQSClient();
            client.Config.RegionEndpoint = Amazon.RegionEndpoint.EUWest1;
            
            var queueName = ConfigurationManager.AppSettings["queueName"];

            int i = 0;

            while (true)
            {
                Thread.Sleep(sendDelay);

                var message = new Amazon.SQS.Model.SendMessageRequest()
                {
                    QueueUrl = queueName,
                    MessageBody = string.Join(",", "Sending (", i, ")")
                };

                updater(string.Format("Sending .... {0}", (i + 1)));
                client.SendMessage(message);

                if (cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                i++;
            }
        }
    }
}