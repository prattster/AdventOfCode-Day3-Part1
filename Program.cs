using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var oneAndZeroCounts = ReadLines()
                .Select(line => line.ToArray())
                .Select(line => new
                {
                    zeroCount = line.Select(n => n == '0' ? 1 : 0), oneCount = line.Select(n => n == '1' ? 1 : 0)
                })
                .Aggregate((counts, next) => new
                {
                    zeroCount = next.zeroCount.Zip(counts.zeroCount, (n, c) => n + c),
                    oneCount = next.oneCount.Zip(counts.oneCount, (n, c) => n + c)
                });

            var gammaRate = oneAndZeroCounts.zeroCount.Zip(oneAndZeroCounts.oneCount, (zero, one) => zero > one ? 0 : 1);
            var epsilonRate = oneAndZeroCounts.zeroCount.Zip(oneAndZeroCounts.oneCount, (zero, one) => zero > one ? 1 : 0);
            
            var gamma = gammaRate.Reverse().Select((val, index) => Math.Pow(2, index) * val).Sum();
            var epsilon = epsilonRate.Reverse().Select((val, index) => Math.Pow(2, index) * val).Sum();

            Console.WriteLine(gamma * epsilon);

            Console.ReadKey();
        }

        private static IEnumerable<string> ReadLines()
        {
            return File.ReadLines(@"input.txt");
        }
        
    }
}
