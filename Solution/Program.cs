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
            string fileContent;
            FileAccess file = new FileAccess();
            file.LogMessage("File read started..");
            try
            {
                fileContent = file.ReadAsText("quicksort_w2_pq1");
                file.LogMessage("File read ended..");


                int vertexCount = int.Parse(fileContent.Split('\n')[0]);
                Graph graph = Graph.CreateGraph(vertexCount);
                for (int i = 0; i < vertexCount; i++)
                {
                    String[] edges = fileContent.Split('\n')[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int j = 0; j < edges.Length - 1; j++)
                    {
                        int d = int.Parse(edges[j]);
                        graph.Vertices[i].Dependencies.Add(graph.Vertices[d - 1]);
                    }
                }

                GetCycle cycle = new GetCycle();
                List<List<Vertex>> nodes = cycle.DetectCycle(graph);

                file.LogMessage("Process ended..");
            }
            catch (Exception ex)
            {
                file.LogMessage(ex.Message);
            }
        }
    }
    public class Vertex
    {
        public int id = -1;
        public int index = -1;
        public int low = -1;
        public List<Vertex> Dependencies = new List<Vertex>();
    }


    public class Graph
    {
        public readonly List<Vertex> Vertices = new List<Vertex>();

        public static Graph CreateGraph(int nodeCount)
        {
            Graph graph = new Graph();
            for (int i = 1; i <= nodeCount; i++)
            {
                Vertex v = new Vertex();
                v.id = i;
                graph.Vertices.Add(v);
            }
            return graph;
        }
    }


    public class GetCycle
    {
        private List<List<Vertex>> output;
        private Stack<Vertex> stackVer;
        private int index;

        public List<List<Vertex>> DetectCycle(Graph g)
        {
            output = new List<List<Vertex>>();
            index = 0;
            stackVer = new Stack<Vertex>();


            foreach (Vertex v in g.Vertices)
            {
                if (v.index < 0)
                {
                    ConvertVertex(v);
                }
            }
            return output;
        }

        private void ConvertVertex(Vertex v)
        {
            v.index = index;
            v.low = index;
            index++;
            stackVer.Push(v);

            foreach (Vertex w in v.Dependencies)
            {
                if (w.index < 0)
                {
                    ConvertVertex(w);
                    v.low = Math.Min(v.low, w.low);
                }
                else if (stackVer.Contains(w))
                {
                    v.low = Math.Min(v.low, w.index);
                }
            }

            if (v.low == v.index)
            {
                List<Vertex> scc = new List<Vertex>();
                Vertex w;
                do
                {
                    w = stackVer.Pop();
                    scc.Add(w);
                } while (v != w);
                output.Add(scc);
            }
        }
    }
}
