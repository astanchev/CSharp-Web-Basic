namespace Async_Processing_Demo
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        public static async Task Main()
        {
            int count = 0;
            var lockObj = new object();
            var sw = Stopwatch.StartNew();
            /*348514 => 00:00:04.7752138   FOR*/
            /*346042 => 00:00:01.4498852   Parallel.FOR without lock*/
            /*348514 => 00:00:01.3502636   Parallel.FOR with lock*/

            //for (int i = 1; i <= 5000000; i++)
            Parallel.For(1, 5000001, (i) =>
            {
                bool isPrime = true;

                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    lock (lockObj)
                    {
                        count++;
                    }
                }
            });

            Console.WriteLine(count);
            Console.WriteLine(sw.Elapsed);


            //------------------------------------------------------------------------
            //Object lockObj = new object();

            //int a = 0;


            //List<Thread> threads = new List<Thread>();
            //for (int i = 0; i < 10; i++)
            //{
            //    Thread t = new Thread(() =>
            //    {
            //        for (int j = 0; j < 10000; j++)
            //        {
            //            //lock (lockObj)
            //            {
            //                a++;
            //            }
            //        }
            //    });
            //    threads.Add(t);
            //    t.Start();
            //}

            //foreach (var thread in threads)
            //{
            //    thread.Join();
            //}

            //Console.WriteLine(a);
            //Console.WriteLine("Done")
            //------------------------------------------------------------------------
            //Thread tr = new Thread(MyThreadMain)
            //{
            //    Priority = ThreadPriority.Highest
            //};
            //tr.Start();

            ////tr.Join();

            //for (int i = 0; i < 4; i++)
            //{
            //    Thread tr2 = new Thread(() => { while (true) { } });
            //    tr2.Priority = ThreadPriority.Lowest;
            //    tr2.Start();
            //}

            //while (true)
            //{
            //    string input = Console.ReadLine();
            //    Console.WriteLine(input);

            //    if (input == "exit")
            //    {
            //        break;
            //    }
            //}

        }

        private static void MyThreadMain()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Console.WriteLine(FindPrimes());
            Console.WriteLine(sw.Elapsed);
        }

        private static int FindPrimes(int start = 2, int end = 4000000)
        {
            int count = 0;

            for (int i = start; i <= end; i++)
            {
                bool isPrime = true;

                for (int j = 2; j <= Math.Sqrt(i); j++)
                {
                    if (i % j == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    count++;
                }
            }

            return count;
        }
    }
}
