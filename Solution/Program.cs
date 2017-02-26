using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraAlgoSolution
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] fileContent;
            FileAccess file = new FileAccess();
            file.LogMessage("File read started..");
            try
            {
                fileContent = file.ReadAsArray("quicksort_w2_pq1");
                file.LogMessage("File read ended..");
                List<Item> items = new List<Item>();
                Item item;
                int maxWeight = 0;

                foreach (var str in fileContent)
                {
                    item = new Item();
                    item.v = Convert.ToInt32(str.Split(' ')[0]);
                    item.v = Convert.ToInt32(str.Split(' ')[1]);
                    if (maxWeight < item.w) { maxWeight = item.w; }
                    items.Add(item);
                }
                Knapsack.Init(items, maxWeight);


                file.LogMessage("Result is: " + Knapsack.Run());
            }
            catch (Exception ex)
            {
                file.LogMessage(ex.Message);
            }
        }

    }

    static class Knapsack
    {
        static int[][] M { get; set; }
        static int[][] P { get; set; }
        static Item[] I { get; set; }
        public static int MaxValue { get; private set; }
        static int W { get; set; }

        public static void Init(List<Item> items, int maxWeight)
        {
            I = items.ToArray();
            W = maxWeight;

            var n = I.Length;
            M = new int[n + 1][];
            P = new int[n + 1][];
            for (var i = 0; i < M.Length; i++) { M[i] = new int[W + 1]; }
            for (var i = 0; i < P.Length; i++) { P[i] = new int[W + 1]; }
        }

        public static int Run()
        {
            var n = I.Length;

            for (var i = 1; i <= n; i++)
            {
                for (var j = 0; j <= W; j++)
                {
                    if (I[i - 1].w <= j)
                    {
                        M[i][j] = Max(M[i - 1][j], I[i - 1].v + M[i - 1][j - I[i - 1].w]);
                        if (I[i - 1].v + M[i - 1][j - I[i - 1].w] > M[i - 1][j])
                        {
                            P[i][j] = 1;
                        }
                        else
                        {
                            P[i][j] = -1;
                        }
                    }
                    else
                    {
                        P[i][j] = -1;
                        M[i][j] = M[i - 1][j];
                    }
                }
            }
            MaxValue = M[n][W];
            return GetValue();
        }

        public static int GetValue()
        {
            var list = new List<Item>();
            list.AddRange(I);
            var w = W;
            var i = list.Count;



            var valueSum = 0;
            var weightSum = 0;
            while (i >= 0 && w >= 0)
            {
                if (P[i][w] == 1)
                {
                    valueSum += list[i - 1].v;
                    weightSum += list[i - 1].w;
                    w -= list[i - 1].w;
                }

                i--;
            }
            return valueSum;
        }

        public static void PrintPicksMatrix(Action<object> write)
        {
            write("\n\n");
            foreach (var i in P)
            {
                foreach (var j in i)
                {
                    var s = j.ToString();
                    var _ = s.Length > 1 ? " " : "  ";
                    write(string.Concat(s, _));
                }
                write("\n");
            }
        }

        static int Max(int a, int b)
        {
            return a > b ? a : b;
        }
    }

    class Item
    {
        private static int i;
        public int id { get; private set; }
        public int v { get; set; }
        public int w { get; set; }
        public Item()
        {
            id = ++i;
        }

        public override string ToString()
        {
            return string.Format("Id: {0}  v: {1}  w: {2}",
                                 id, v, w);
        }
    }
}
