using System;
using _3pl_invoice.Services;

namespace _3pl_invoice
{
    class Program
    {
        static void Main(string[] args)
        {
            FileFetcher.GatherData();
            Console.ReadLine();
        }
    }
}
