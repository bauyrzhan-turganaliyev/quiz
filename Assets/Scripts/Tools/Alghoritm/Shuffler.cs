using System;
using System.Collections.Generic;

namespace Tools.Alghoritm
{
    public class Shuffler<T> : IShuffler<T>
    {
        public List<T> Shuffle(List<T> list)
        {
            int n = list.Count;
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

    public interface IShuffler<T>
    {
        public List<T> Shuffle(List<T> list);
    }
}