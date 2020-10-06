using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CianLib;

namespace CianLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            startLoading().GetAwaiter().GetResult();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }

        static async Task startLoading() 
        {
            Console.WriteLine("Loader starts");
            var cianLib = new CianLibrary();
            await cianLib.LoadOffers();
        }
    }
}
