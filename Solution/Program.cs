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
                List<int[]> dataArray = new List<int[]>();
                foreach (var d in fileContent)
                {
                    dataArray.Add(Array.ConvertAll(d.Split(' '), Int32.Parse));
                }
                file.LogMessage("Process ended.. " + FloydWarshall(dataArray.ToArray(), dataArray.Count));
            }
            catch (Exception ex)
            {
                file.LogMessage(ex.Message);
            }
        }

        public static int FloydWarshall(int[][] graph, int verticesCount)
        {
            int[][] distance = new int[verticesCount][];

            for (int i = 0; i < verticesCount; ++i)
                for (int j = 0; j < verticesCount; ++j)
                    distance[i][j] = graph[i][j];

            for (int k = 0; k < verticesCount; ++k)
            {
                for (int i = 0; i < verticesCount; ++i)
                {
                    for (int j = 0; j < verticesCount; ++j)
                    {
                        if (distance[i][k] + distance[k][j] < distance[i][j])
                            distance[i][j] = distance[i][k] + distance[k][j];
                    }
                }
            }

            return GetShortestDistance(distance);
        }
        public static int GetShortestDistance(int[][] dis)
        {
            int sDis = Int32.MaxValue;
            foreach (var d in dis)
            {
                foreach (int a in d)
                {
                    if (sDis > a)
                    {
                        sDis = a;
                    }
                }
            }
            return sDis;
        }
    }
}
