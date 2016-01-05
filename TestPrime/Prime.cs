using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestPrime
{
    /// <summary>
    /// 素数を求めるクラス
    /// </summary>
    public class Prime
    {
        List<int> primes = new List<int>() { 2 };

        /// <summary>
        /// 引数 n までの素数を求める(同期型)
        /// 結局、このメソッドは使わない。
        /// </summary>
        /// <param name="n">求める素数の最大数</param>
        /// <returns>GetPrimesを実行するTask</returns>
        public Task<List<int>> GetPrimesAsync(int n)
        {
            // 引数を渡して復帰値を得るTaskを生成
            //return new Task<List<int>>(o => GetPrimes((int)o), n);

            // 上記よりシンプルな記述
            // 引数はnをキャプチャし使用している
            return new Task<List<int>>(() => GetPrimes(n));
        }

        /// <summary>
        /// 引数 n までの素数を求める(非同期型)
        /// </summary>
        /// <param name="n">求める素数の最大数</param>
        /// <returns>素数の配列</returns>
        public List<int> GetPrimes(int n)
        {
            for (int i = 2; i <= n; i++)
            {
                int primesCount = primes.Count;
                bool notPrimeFlag = false;
                for (int j = 0; j < primesCount; j++)
                {
                    if ((i % primes[j]) == 0)
                    {
                        notPrimeFlag = true;
                        j = primesCount;
                        break;
                    }
                }
                if (!notPrimeFlag)
                {
                    primes.Add(i);
                }
            }
            return primes;
        }
    }
}
