namespace MergeSortAsync
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    public class Program
    {
        public static void Main()
        {
            /*sync => 00:00:00.0035503*/
            /*async => 00:00:00.0090872*/

            var arr = File.ReadAllText("./data.txt").Split(", ").Select(int.Parse).ToArray();
		
            Stopwatch sw = Stopwatch.StartNew();

            Task sort = SortAlgorithm<int>.MergeSortAsync(arr);
            sort.Wait();

            Console.WriteLine(sw.Elapsed);
            Console.WriteLine();

            //SortAlgorithm<int>.MergeSort(arr);

            //Console.WriteLine(sw.Elapsed);

            //Console.WriteLine(string.Join(", ", arr));
        }
    }
}
