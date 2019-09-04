using System;
using System.Collections.Generic;
using System.Linq;
using Loop;

namespace conClass
{
    static class Program
    {
        static void Main(string[] args)
        {
            const int count = 5;
            IEnumerable<Card[]> permutations = GeneratePermutations(count);
            foreach (var loop in permutations)
            {
                Process(loop, true);
                Process(loop, false);
            }
        }
        private static bool[][] EveryBitmask(int Size)
        {
            int v = 2 << (Size - 1);
            var ret = new bool[v][];
            int length = Size;
            for (int i = 0; i < Math.Pow(2, length); i++)
            {
                ret[i] = new bool[Size];
                for (int j = 0; j < length; j++)
                {
                    if ((i & 1 << length - j - 1) != 0)
                    {
                        ret[i][j] = true;
                    }
                }
            }
            return ret;
        }
        private static string[] Process(Card[] Loop, bool TwistAfter)
        {
            char firstLetter = Loop[0].SideA[0].Letter;
            var v = new bool[] { false };
            var flipMask = EveryBitmask(Loop.Length - 1).Select(g =>
            {
                var q = new bool[Loop.Length];
                q[0] = false;
                Array.Copy(g, 0, q, 1, g.Length);
                return q;
            }).ToArray(); //you can't flip the starting position
            int length = EveryBitmask(Loop.Length - 1).Length;
            var ret = new string[length];
            const int deadLockedAt = 100;
            System.Diagnostics.Debug.WriteLine("");
            // System.Diagnostics.Debug.WriteLine("");
            System.Diagnostics.Debug.Write(string.Join(",", Loop.Select(k => k.ToString())));
            System.Diagnostics.Debug.WriteLine(TwistAfter, " Twist");
            for (int x = 0; x < length; x++)
            {
                var bitMask = flipMask[x];
                var success = TryBuildString(Loop, deadLockedAt, bitMask, TwistAfter, firstLetter, out var result);
                ret[x] = result;
                if (success)
                {
                    var printer = new string[3];
                    for (int i = 0; i < bitMask.Length; i++)
                    {
                        var print = Loop[i].Print(bitMask[i]);
                        for (int p = 0; p < 3; p++)
                        {
                            printer[p] += print[p];
                        }
                    }
                    if (TwistAfter)
                    {
                        for (int i = 0; i < bitMask.Length; i++)
                        {
                            var print = Loop[i].Print(!bitMask[i]);
                            for (int p = 0; p < 3; p++)
                            {
                                printer[p] += print[p];
                            }
                        }
                    }
                    foreach (var row in printer)
                    {
                        System.Diagnostics.Debug.WriteLine(row);
                    }
                    System.Diagnostics.Debug.Write(string.Join("", bitMask.Select(g => g ? '1' : '0')) + (TwistAfter ? 'X' : '-'));
                    System.Diagnostics.Debug.Write("\t");
                    System.Diagnostics.Debug.WriteLine(result);
                    System.Diagnostics.Debug.WriteLine("");
                }
            }
            System.Diagnostics.Debug.WriteLine("");
            return ret;
        }
        private static bool TryBuildString(Card[] Loop, int deadLockedAt, bool[] bitMask, bool TwistAfter, char firstLetter, out string ret)
        {
            LineType place;
            ret = firstLetter.ToString();
            var breakDeadlock = 0;
            var i = 1;
            var height = 0;
            var twist = false;
            do
            {
                if (TwistAfter && i == 0)
                {
                    twist = !twist;
                }
                bool top = !bitMask[i];
                Line line = Loop[i].GetLine(height, top ^ twist);
                ret += line.Letter;
                place = line.Type;
                height = line.NextHeight;
                i++;
                i = i % Loop.Length;
                breakDeadlock++;
                if (breakDeadlock >= deadLockedAt) { place = LineType.Start; }
            } while (place == LineType.Continue);
            if (place == LineType.Start) { ret += " **INVALID LOOP**"; }
            return place == LineType.End;
        }
        private static Card[][] GeneratePermutations(int count)
        {
            IEnumerable<Card> loop = SetUp(count);
            var permutations = new List<Card[]>();
            int i = 3; // take 5 cards since 1 and 2 are guaranteed
            // while ( i >= 0) // take fewer than 5 cards
            {
                IEnumerable<Card[]> enumerable = loop.Skip(1).Take(i + 1).EveryPermutation().Select(k => loop.Take(1).Union(k).ToArray());
                permutations.AddRange(enumerable);
                i--;
            }
            return permutations.ToArray();
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
            foreach (var item in loop)
            {

                System.Diagnostics.Debug.WriteLine("Front");
                System.Diagnostics.Debug.WriteLine(string.Join("\n", item.Print()));
                System.Diagnostics.Debug.WriteLine("Back");
                System.Diagnostics.Debug.WriteLine(string.Join("\n", item.Print(true)));
            }
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
