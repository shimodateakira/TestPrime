using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace TestPrime
{
    class Program
    {
        static void Main(string[] args)
        {
            const int PrimesCount = 100;
            List<int> primes;
            Prime prime = new Prime(PrimesCount);
            Stopwatch sw = new Stopwatch();

            // 同期で呼び出す
            sw.Reset();
            sw.Start();
            primes = prime.GetPrimes();
            sw.Stop();
            Console.WriteLine("同期で呼び出す");
            Console.WriteLine($"{nameof(sw.Elapsed)} = {sw.Elapsed}");
            Console.WriteLine($"{nameof(primes.Count)} = {primes.Count}");
#if DEBUG
            Debug.WriteLine(string.Join(" ", primes.GetRange(0, 20)));
#endif

            // 同期で呼び出す
            sw.Reset();
            sw.Start();

            // 非同期はこの呼び方でもよいが．．．．
            Task<List<int>> task = prime.GetPrimesAsync();
            task.Start();

            // 利用する側(Programクラス)が非同期で呼び出す方が望ましい
            //Task<List<int>> task = Task.Run(() => prime.GetPrimes(PrimesCount));

            sw.Stop();

            // タスクが終了するまで待つ。が．．．
            // task.ResultがWaitを兼ねている。
            // つまり、Resultを取得できるようになるまで待機する。
            //task.Wait();

            primes = task.Result;
            Console.WriteLine("\n非同期で呼び出す");
            Console.WriteLine($"{nameof(sw.Elapsed)} = {sw.Elapsed}");
            Console.WriteLine($"{nameof(primes.Count)} = {primes.Count}");

            // 列挙で呼び出す
            Console.WriteLine("");
            prime.Reset();
            prime.PrimeCount = PrimesCount;
            foreach (var item in prime)
            {
                Console.Write($"{item} ");
            }

            Console.WriteLine("\n\nPush any key!");
            Console.ReadKey();
        }
    }
}
