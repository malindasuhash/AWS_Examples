using System;
using System.Threading;

namespace Examples.Core
{
    public class ConsoleWaiter
    {
        public void WaitFor(ConsoleKey key, Action onExit, int waitForMiliseconds = 5000)
        {
            Console.WriteLine("\rPress {0} to stop!", key);

            var keyInfo = Console.ReadKey();

            while (keyInfo.Key != key)
            {
                Thread.Sleep(waitForMiliseconds);

                if (keyInfo.Key == key)
                {
                    onExit();
                    break;
                }

                keyInfo = Console.ReadKey();
            }
        }

        public void Message(string message)
        {
            Console.Write("\r{0}", message);
        }
    }
}