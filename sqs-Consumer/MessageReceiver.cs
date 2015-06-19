using System;
using System.Configuration;
using System.Threading;
using Amazon;

namespace sqs_Consumer
{
    public class MessageReceiver
    {
        public void Read(CancellationTokenSource cancellationTokenSource, int delay)
        {
            var maxAttemptsBeforeCompleting = 5;

            var queueUrl = ConfigurationManager.AppSettings["queueName"];

            var client = new Amazon.SQS.AmazonSQSClient();
            client.Config.RegionEndpoint = RegionEndpoint.EUWest1;

            var receiveRequest = new Amazon.SQS.Model.ReceiveMessageRequest(queueUrl);

            var attempt = 1;
            do
            {
                if (cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                var response = client.ReceiveMessage(receiveRequest);

                // check for messages
                var hasMessage = response.Messages.Count == 1;

                if (hasMessage)
                {
                    // Process message
                    var message = response.Messages[0];
                    Console.Write("Message received: {0} ", message.Body);

                    // Send complete
                    var delete = new Amazon.SQS.Model.DeleteMessageRequest(queueUrl, message.ReceiptHandle);
                    var deleteResponse = client.DeleteMessage(delete);

                    if (deleteResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine(" [Message Processing complete]");
                        attempt = 0;
                    }

                    continue;
                }

                attempt++;
                Thread.Sleep(delay);
            }
            while (attempt <= maxAttemptsBeforeCompleting);
        } 
    }
}