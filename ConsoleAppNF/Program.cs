using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Fundamentals;

namespace ConsoleAppNF
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var logger = new ErrorLogger();
            
            logger.ErrorLogged += (sender, guid) =>
            {
                Console.WriteLine($"Event Raised: {nameof(logger.ErrorLogged)}");
                Console.WriteLine($"Sender: {sender.GetType().Name}");
                Console.WriteLine($"e object (Data): {guid}");

            };

            logger.Log("My error.....");
        }
    }
}
