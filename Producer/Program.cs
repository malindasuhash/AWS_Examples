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

            SendMessages(args[0]);

            Console.ReadKey();
        }

        private static void SendMessages(string consumerName)
        {
            Amazon.SQS.AmazonSQSClient client = new Amazon.SQS.AmazonSQSClient();
            client.Config.RegionEndpoint = Amazon.RegionEndpoint.EUWest1;

            var random = new Random(100);

            for (var i = 0; i <= 100; i++)
            {
                var message = new Amazon.SQS.Model.SendMessageRequest()
                {
                    QueueUrl = ConfigurationManager.AppSettings["queueName"],
                    MessageBody = string.Format("({2}) Number-[{0}-{1}]", DateTime.Now.Millisecond, random.Next(), consumerName)
                };

                client.SendMessage(message);

                Console.WriteLine("Sending message - {0}", message.MessageBody);
            }

            Console.WriteLine("Done!");
        }
    }
}
