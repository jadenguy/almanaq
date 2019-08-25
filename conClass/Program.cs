using System;
using System.Collections.Generic;
using System.Linq;

namespace conClass
{
    static class Program
    {
        static void Main(string[] args)
        {
            const int count = 5;
            IEnumerable<Card[]> permutations = GeneratePermutations(count);
            // System.Diagnostics.Debug.WriteLine(string.Join('\n', permutations.Select(x => string.Join<Card>(" ", x))));
            foreach (var loop in permutations)
            {
                // System.Diagnostics.Debug.WriteLine(string.Join<Card>(" ", loop));
                for (int i = 0; i < 1; i++)
                {
                    var flipMask = EverySubSet(4);
                    System.Diagnostics.Debug.WriteLine(Process(loop, i));
                }
            }
        }

        private static bool[][] EverySubSet(int v)
        {
            var ret = new bool[v * v][];
            for (int i = 0; i < v * v; i++)
            {
                ret[i] = new bool[v];
                for (int j = 0; j < v; j++)
                {
                    if ((i & j) == i) { ret[i][j] = true; }
                    else { ret[i][j] = false; }
                }
            }
            return ret;
        }

        private static string Process(Card[] loop, int twistAfter)
        {
            LineType place;
            var ret = string.Empty;
            var i = 0;
            var height = 0;
            var breakDeadlock = 0;
            const int deadLockedAt = 50;
            do
            {
                Line line = loop[i].SideA[height];
                ret += line.Letter;
                place = line.Type;
                height = line.NextHeight;
                i++;
                i = i % loop.Length;
                breakDeadlock++;
                if (breakDeadlock == deadLockedAt) { place = LineType.End; }
            } while (place != LineType.End);
            return ret;
        }
        private static Card[][] GeneratePermutations(int count)
        {
            IEnumerable<Card> loop = SetUp(count);
            var permutations = loop.Skip(1).EveryPermutation().Select(k => loop.Take(1).Union(k).ToArray()).ToArray();
            return permutations;
        }

        private static IEnumerable<Card> SetUp(int count)
        {
            var loop = new Card[count];
            Card card = new Card();
            loop[0] = card;
            card.SideA[0] = new Line('B', 0, LineType.Start);
            card.SideA[1] = new Line('M', 2);
            card.SideA[2] = new Line('G', 1);
            card.SideB[0] = new Line('A', 2);
            card.SideB[1] = new Line('T', 0);
            card.SideB[2] = new Line('A', 1);
            card = new Card();
            loop[1] = card;
            card.SideA[0] = new Line('I', 2);
            card.SideA[1] = new Line('M', 0, LineType.End);
            card.SideA[2] = new Line('I', 1);
            card.SideB[0] = new Line('P', 0);
            card.SideB[1] = new Line('N', 2);
            card.SideB[2] = new Line('&', 1);
            card = new Card();
            loop[2] = card;
            card.SideA[0] = new Line('E', 2);
            card.SideA[1] = new Line('A', 1);
            card.SideA[2] = new Line('L', 0);
            card.SideB[0] = new Line('A', 1);
            card.SideB[1] = new Line('F', 0);
            card.SideB[2] = new Line('T', 2);
            card = new Card();
            loop[3] = card;
            card.SideA[0] = new Line('A', 1);
            card.SideA[1] = new Line('U', 2);
            card.SideA[2] = new Line('E', 0);
            card.SideB[0] = new Line('E', 1);
            card.SideB[1] = new Line('G', 2);
            card.SideB[2] = new Line('R', 0);
            card = new Card();
            loop[4] = card;
            card.SideA[0] = new Line('S', 1);
            card.SideA[1] = new Line('L', 2);
            card.SideA[2] = new Line('R', 0);
            card.SideB[0] = new Line('K', 1);
            card.SideB[1] = new Line('A', 2);
            card.SideB[2] = new Line('R', 0);
            return loop;
        }

        private static IEnumerable<IEnumerable<T>> EveryPermutation<T>(this IEnumerable<T> array)
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
