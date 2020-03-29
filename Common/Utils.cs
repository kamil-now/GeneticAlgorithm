using System;

namespace Common
{
    public static class Utils
    {
        public static Random Random = new Random();
        public static double RandomDouble(double min, double max)
        {
            return Random.NextDouble() * (max - min) + min;
        }
        public static void Shuffle<T>(T[] arr)
        {
            int n = arr.Length;
            while (n > 1)
            {
                n--;
                int k = Random.Next(n + 1);
                T value = arr[k];
                arr[k] = arr[n];
                arr[n] = value;
            }
        }
    }
}
