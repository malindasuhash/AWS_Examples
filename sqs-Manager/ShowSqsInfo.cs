using System;
using System.Collections.Generic;
using System.Configuration;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace sqs_Manager
{
    public class ShowSqsInfo
    {
        public async void Show()
        {
            var client = new AmazonSQSClient();
            client.Config.RegionEndpoint = RegionEndpoint.EUWest1;
            
            // queue details
            var simpleQueueName = ConfigurationManager.AppSettings["queueName"];

            // queue url req
            var queueUrl = new GetQueueUrlRequest(simpleQueueName);
            var queueUrlResult = client.GetQueueUrl(queueUrl);
            
            Console.WriteLine("Url: {0}", queueUrlResult.QueueUrl);

            // Details about queues
            var queues = await client.ListQueuesAsync(new ListQueuesRequest());
            foreach (var urls in queues.QueueUrls)
            {
                Console.WriteLine("Queue url: {0}", urls);
            }
        }
    }
}