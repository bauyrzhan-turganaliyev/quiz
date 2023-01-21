using System;

namespace Tools.Algorithm
{
    public class Shuffler<T>
    {
        public T[] Shuffle(T[] list)
        {
            int n = list.Length;
            Random rng = new Random();
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
            return list;
        }
    }
}