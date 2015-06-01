using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer
{
    /// <summary>
    /// Simple application that sends messages to the AWS SQS endpoint.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please wait....");

            SendMessagesPlain(args[0]).Wait();

            Console.ReadKey();
        }

        private static async Task SendMessagesPlain(string consumerName)
        {
            Amazon.SQS.AmazonSQSClient client = new Amazon.SQS.AmazonSQSClient();

            var random = new Random(100);
            var cancellationTokenSource = new CancellationTokenSource();

            for (var i = 0; i <= 100; i++)
            {
                var message = new Amazon.SQS.Model.SendMessageRequest()
                {
                    QueueUrl = ConfigurationManager.AppSettings["queueName"],
                    MessageBody = string.Format("({2}) Number-[{0}-{1}]", DateTime.Now.Millisecond, random.Next(), consumerName)
                };

                await client.SendMessageAsync(message, cancellationTokenSource.Token);

                Console.WriteLine("Sending message - {0}", message.MessageBody);
            }

            Console.WriteLine("Done!");
        }
    }
}
