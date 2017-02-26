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
                fileContent = file.ReadAsArray("file1");
                file.LogMessage("File read ended..");

                List<Tuple<long, long>> dataTuple = new List<Tuple<long, long>>();
                foreach (var str in fileContent)
                {
                    dataTuple.Add(new Tuple<long, long>(Array.ConvertAll(str.Split(' '), int.Parse)[0], Array.ConvertAll(str.Split(' '), int.Parse)[1]));
                }

                file.LogMessage("Process ended.. " + Greedy(dataTuple, true));
                file.LogMessage("Process ended.. " + Greedy(dataTuple, false));

                fileContent = file.ReadAsArray("file2");

                List<int[]> dataArray = new List<int[]>();
                foreach (var i in fileContent)
                {
                    dataArray.Add(Array.ConvertAll(i.Split(' '), int.Parse));
                }



                file.LogMessage("Process ended.." + Prim(dataArray.ToArray(), dataArray.Count));
            }
            catch (Exception ex)
            {
                file.LogMessage(ex.Message);
            }
        }
        public static long Greedy(List<Tuple<long, long>> data, bool isSubtraction)
        {
            long sum = 0;
            long length = 0;
            foreach (var a in data.OrderByDescending(d => isSubtraction ? d.Item1 - d.Item2 : (double)(d.Item1 / d.Item2)).ThenByDescending(d => d.Item1))
            {
                length += a.Item2;
                sum += length * a.Item1;
            }
            return sum;
        }


        public static int Prim(int[][] graph, int vCount)
        {
            int minCost = 0;
            int[] key = new int[vCount];
            bool[] minSet = new bool[vCount];

            for (int i = 0; i < vCount; ++i)
            {
                key[i] = int.MaxValue;
                minSet[i] = false;
            }

            key[0] = 0;
            for (int count = 0; count < vCount - 1; ++count)
            {
                int u = MinKey(key, minSet, vCount);
                minSet[u] = true;

                for (int v = 0; v < vCount; ++v)
                {
                    if (Convert.ToBoolean(graph[u][v]) && minSet[v] == false && graph[u][v] < key[v])
                    {
                        minCost += u;
                        key[v] = graph[u][v];
                    }
                }
            }
            return minCost;
        }

        private static int MinKey(int[] key, bool[] set, int verticesCount)
        {
            int min = int.MaxValue, minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (set[v] == false && key[v] < min)
                {
                    min = key[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }
    }
}

