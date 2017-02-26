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
                foreach (string data in fileContent) {
                    int i;
                    Int32.TryParse(data, out i);
                }
                List<int[]> dataArray = new List<int[]>();
                foreach (var i in fileContent)
                {
                    dataArray.Add(Array.ConvertAll(i.Split(' '), int.Parse));
                }
                int[] distance = Dijkstra(dataArray.ToArray(), dataArray.Count);
                file.LogMessage("Process ended.." + distance.ToString());
            }
            catch (Exception ex) {
                file.LogMessage(ex.Message);
            }
        }
        private static int MinimumDistance(int[] distance, bool[] shortestPathTreeSet, int verticesCount)
        {
            int min = int.MaxValue;
            int minIndex = 0;

            for (int v = 0; v < verticesCount; ++v)
            {
                if (shortestPathTreeSet[v] == false && distance[v] <= min)
                {
                    min = distance[v];
                    minIndex = v;
                }
            }

            return minIndex;
        }

        private static void Print(int[] distance, int verticesCount)
        {
            Console.WriteLine("Vertex    Distance from source");

            for (int i = 0; i < verticesCount; ++i)
                Console.WriteLine("{0}\t  {1}", i, distance[i]);
        }

        public static int[] Dijkstra(int[][] graph, int verticesCount)
        {
            int[] distance = new int[verticesCount];
            bool[] shortestPathTreeSet = new bool[verticesCount];

            for (int i = 0; i < verticesCount; ++i)
            {
                distance[i] = int.MaxValue;
                shortestPathTreeSet[i] = false;
            }
            

            for (int count = 0; count < verticesCount - 1; ++count)
            {
                int u = MinimumDistance(distance, shortestPathTreeSet, verticesCount);
                shortestPathTreeSet[u] = true;

                for (int v = 0; v < verticesCount; ++v)
                    if (!shortestPathTreeSet[v] && Convert.ToBoolean(graph[u][v]) && distance[u] != int.MaxValue && distance[u] + graph[u][v] < distance[v])
                        distance[v] = distance[u] + graph[u][v];
            }

            return distance;
        }

    }

}
