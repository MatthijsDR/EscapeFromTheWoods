using System;
using System.Diagnostics;

namespace EscapeFromTheWoodsFinal
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            Async.AsyncExecution.CreateAndExportFilesAndDBWoodsAsync(3, 15, 2500, 250, 250);
            watch.Stop();
            Console.WriteLine("Time ellapsed: " + watch.Elapsed);

        }
    }
}
