using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            SendMessagesPlain(args[0]);

            Console.WriteLine("Waiting to complete....");
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
                    QueueUrl = "https://sqs.eu-west-1.amazonaws.com/903119102693/MyQueue",
                    MessageBody = string.Format("({2}) Number-[{0}-{1}]", DateTime.Now.Millisecond, random.Next(), consumerName)
                };

                await client.SendMessageAsync(message, cancellationTokenSource.Token);

                Console.WriteLine("Sending message - {0}", message.MessageBody);

                if (i == 50) // Attempt to simulate a cancellation halfway.
                {
                    cancellationTokenSource.Cancel();

                    Console.WriteLine("Done!");
                }
            }
        }
    }
}
