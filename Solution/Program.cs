using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseraAlgoSolution
{
    class Program
    {
        private Edge[] edge;
        private static Random rand = new Random();
        private Vertex[] vert;
        static void Main(string[] args)
        {
            string fileContent;
            FileAccess file = new FileAccess();
            file.LogMessage("File read started..");
            try
            {
                fileContent = file.ReadAsText("quicksort_w2_pq1");
                file.LogMessage("File read ended..");
                //foreach (string data in fileContent)
                //{
                //    int i;
                //    Int32.TryParse(data, out i);
                //}
                Program pr = new Program();
                pr.Load(fileContent);
                file.LogMessage("Process ended.." + pr.GetMinCut());
            }
            catch (Exception ex)
            {
                file.LogMessage(ex.Message);
            }
        }

        public void Load(string data) {

            List<Edge> edges = new List<Edge>();
            List<Vertex> vertex = new List<Vertex>();

            string[] sdel = data.Split('\t');
            int id = int.Parse(sdel[0]);
            for (int i = 1; i < sdel.Length; i++)
            {
                try
                {
                    edges.Add(new Edge(id, int.Parse(sdel[i])));
                }
                catch
                { }
            }
            Vertex n = new Vertex(id);
            vertex.Add(n);
            vert = vertex.ToArray();
        }
        
        public int GetMinCut()
        {
            Array.ForEach(vert, v =>
            {
                v.Parent = v;
                v.Rank = 0;
            });

            int mVertex = 0;
            int range = edge.Length - 1;
            while (range >= 0)
            {
                int index = rand.Next(0, range);
                Edge selectedEdge = edge[index];
                Vertex mHead = vert[selectedEdge.Head - 1];
                Vertex mTail = vert[selectedEdge.Tail - 1];
                //skip if self loop  
                if (FindRoot(mHead).Id == FindRoot(mTail).Id)
                {
                    Swap(edge, index, range);
                    range--;
                    continue;
                }

                //merge adjacent vertices
                MergeVertices(mHead, mTail);
                mVertex++;
                if (mVertex == vert.Length - 2)
                {
                    break;
                }

                Swap(edge, index, range);
                range--;
            }
            // calculate min cut for vertice list            
            int firstVerticeRootId = FindRoot(vert.First()).Id;
            var hashSet = new HashSet<int>(vert.Where(v => FindRoot(v).Id == firstVerticeRootId).Select(v => v.Id));
            var minCut = edge.Count(e => hashSet.Contains(e.Head) && !hashSet.Contains(e.Tail));
            return minCut;
        }

        private static void Swap<T>(T[] edges, int leftIndex, int rightIndex)
        {
            var temp = edges[leftIndex];
            edges[leftIndex] = edges[rightIndex];
            edges[rightIndex] = temp;
        }

        private void MergeVertices(Vertex mergedHead, Vertex mergedTail)
        {
            var leftRoot = FindRoot(mergedHead);
            var rightRoot = FindRoot(mergedTail);
            if (leftRoot.Id != rightRoot.Id)
            {
                int compare = leftRoot.Rank.CompareTo(rightRoot.Rank);
                if (compare < 0)
                {
                    leftRoot.Parent = rightRoot;
                }
                else if (compare > 0)
                {
                    rightRoot.Parent = leftRoot;
                }
                else
                {
                    if (rand.Next(0, 1) == 0)
                    {
                        leftRoot.Parent = rightRoot;
                        rightRoot.Rank++;
                    }
                    else
                    {
                        rightRoot.Parent = leftRoot;
                        leftRoot.Rank++;
                    }
                }
            }
        }

        public static Vertex FindRoot(Vertex start)
        {
            var element = start;
            while (element.Parent != element)
            {
                element = element.Parent;
            }
            return element;
        }
    }
}

