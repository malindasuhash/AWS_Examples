using System;
using System.Configuration;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;

namespace sqs_Manager
{
    public class ShowSqsInfo
    {
        public void Show()
        {
            var client = new AmazonSQSClient();
            client.Config.RegionEndpoint = RegionEndpoint.EUWest1;
            
            // queue details
            var queueName = ConfigurationManager.AppSettings["queueName"];

            // queue url req
            var queueUrl = new GetQueueUrlRequest(queueName);
            var queueUrlResult = client.GetQueueUrl(queueUrl);
            
            Console.WriteLine("Url {0}", queueUrlResult.QueueUrl);
        }
    }
}