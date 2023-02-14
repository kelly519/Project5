using System;

namespace RandomNumberGenerators
{
    public class LinearCongruentialGenerator
    {
        private readonly long _a;
        private readonly long _c;
        private readonly long _m;
        private long _x;

        public LinearCongruentialGenerator(long seed, long a, long c, long m)
        {
            _a = a;
            _c = c;
            _m = m;
            _x = seed;
        }

        public long Next(long minValue, long maxValue)
        {
            _x = (_a * _x + _c) % _m;
            return minValue + _x % (maxValue - minValue);
        }
    }

    public class Xorshift32
    {
        private uint _x;

        public Xorshift32(uint seed)
        {
            _x = seed;
        }

        public uint Next(uint minValue, uint maxValue)
        {
            _x ^= _x << 13;
            _x ^= _x >> 17;
            _x ^= _x << 5;
            return minValue + _x % (maxValue - minValue);
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Choose a RNG engine: ");
            Console.WriteLine("1. Linear Congruential Generator");
            Console.WriteLine("2. Xorshift32");
            Console.WriteLine("3. Built-in Random");

            int choice = Convert.ToInt32(Console.ReadLine());
            int[] result = new int[10];

            switch (choice)
            {
                case 1:
                    var lcg = new LinearCongruentialGenerator(1, 1103515245, 12345, (long)Math.Pow(2, 31));
                    for (int i = 0; i < 100000; i++)
                    {
                        int randomNumber = (int)lcg.Next(0, 10);
                        result[randomNumber]++;
                    }
                    break;
                case 2:
                    var xorshift = new Xorshift32(1);
                    for (int i = 0; i < 100000; i++)
                    {
                        int randomNumber = (int)xorshift.Next(0, 10);
                        result[randomNumber]++;
                    }
                    break;
                case 3:
                    var random = new Random();
                    for (int i = 0; i < 100000; i++)
                    {
                        int randomNumber = random.Next(0, 10);
                        result[randomNumber]++;
                    }
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

            for (int i = 0; i < 10; i++)
            {
                Console.Write($"{i}: ");
                //display a little bar graph made with #
                for (int j = 0; j < result[i] / 1000; j++)
                {
                    Console.Write("#");
                }
                Console.WriteLine($"  {result[i] / 1000.0}%");
            }
        }
    }
}