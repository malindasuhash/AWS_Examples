using System;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Examples.Core;

namespace sqs_Consumer
{
    class Program
    {
        static void Main(string[] args)
        {

            var waiter = new ConsoleWaiter();
            var receiver = new MessageReceiver();

            int delayBetweenReceiveCalls = int.Parse(args[0]);

            Console.WriteLine("Starting ...");

            var token = new CancellationTokenSource();

            Task.Factory.StartNew(() => receiver.Read(token, delayBetweenReceiveCalls), token.Token);

            Action terminateSender = () =>
            {
                token.Cancel();
            };

            waiter.WaitFor(ConsoleKey.Y, terminateSender);
        }
    }
}
