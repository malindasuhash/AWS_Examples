using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sqs_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting receiving from queue");
            ReadFromQueue();
            Console.WriteLine("End receiving from queue");
            Console.ReadKey();
        }

        private static void ReadFromQueue()
        {
            var maxAttemptsBeforeCompleting = 5;

            var queueUrl = ConfigurationManager.AppSettings["queueName"];

            var client = new Amazon.SQS.AmazonSQSClient();
            var receiveRequest = new Amazon.SQS.Model.ReceiveMessageRequest(queueUrl);

            var attempt = 1;
            do
            {
                var response = client.ReceiveMessage(receiveRequest);

                // check for messages
                var hasMessage = response.Messages.Count() == 1;

                if (hasMessage)
                {
                    // Process message
                    var message = response.Messages[0];
                    Console.Write("Message received: {0}", message.Body);
                    
                    // Send complete
                    var delete = new Amazon.SQS.Model.DeleteMessageRequest(queueUrl, message.ReceiptHandle);
                    var deleteResponse = client.DeleteMessage(delete);

                    if (deleteResponse.HttpStatusCode == System.Net.HttpStatusCode.OK)
                    {
                        Console.WriteLine(" [Message Processing complete]");
                    }

                    continue;
                }

                attempt++;
            }
            while (attempt <= maxAttemptsBeforeCompleting);
        }
    }
}
