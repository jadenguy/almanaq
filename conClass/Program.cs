using System;
using System.Collections.Generic;
using System.Linq;

namespace conClass
{
    class Program
    {
        static void Main(string[] args)
        {
            const int count = 5;
            var loop = new Card[count];
            for (int i = 0; i < count; i++) { loop[i] = new Card(); }
            // loop[0].SideA[0] = new Line('B', 1, LineType.Start);
            var permutations = EveryPermutation(loop.Skip(1)).Select(k => loop.Take(1).Union(k)).ToArray();
            // var z = permutations.Select(k => k.TakeWhile(l => l != 0).ToArray()).Distinct().ToArray();
            // System.Diagnostics.Debug.WriteLine(permutations.Length);
        }
        private static IEnumerable<IEnumerable<T>> EveryPermutation<T>(IEnumerable<T> array)
        {
            if (array.Count() == 1) { yield return array; }
            else
            {
                var stack = new Queue<T>(array);
                int length = stack.Count;
                for (int i = 0; i < length; i++)
                {
                    var temp = stack.Dequeue();
                    foreach (var item in EveryPermutation(stack))
                    {
                        yield return new List<T>(item) { temp };
                    }
                    stack.Enqueue(temp);
                }
            }
        }
    }
}
