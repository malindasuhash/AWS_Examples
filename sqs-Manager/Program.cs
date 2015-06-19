using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Examples.Core;

namespace sqs_Manager
{
    class Program
    {
        static void Main(string[] args)
        {
            var waiter = new ConsoleWaiter();

            var showInfo = new ShowSqsInfo();
            showInfo.Show();

            waiter.WaitFor(ConsoleKey.V, () => {});
        }
    }
}
