using System;
using System.Threading;
using System.Threading.Tasks;
using Examples.Core;

namespace Producer
{
    /// <summary>
    /// Simple application that sends messages to the AWS SQS endpoint.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var waiter = new ConsoleWaiter();
            var sender = new MessageSender();

            Console.WriteLine("Starting ...");

            var token = new CancellationTokenSource();

            Task.Factory.StartNew(() => sender.Send(token, waiter.Message), token.Token);
           
            Action terminateSender = () => { token.Cancel(); };

            waiter.WaitFor(ConsoleKey.X, terminateSender);
        }
    }
}
