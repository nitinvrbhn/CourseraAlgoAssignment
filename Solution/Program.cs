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
                foreach (string data in fileContent)
                {
                    int i;
                    Int32.TryParse(data, out i);
                }

                file.LogMessage("Process ended.. " + GetData(fileContent));
            }
            catch (Exception ex)
            {
                file.LogMessage(ex.Message);
            }
        }
        static bool IsExists(int s, List<List<int>> G)
        {
            List<int> Q = new List<int>();
            Q.Add(s);
            bool[] seen = new bool[Q.Count];
            seen[s] = true;

            while (Q.Count != 0)
            {
                int v = Q[0];
                Q.Remove(Q[0]);
                foreach (int w in G[v])
                {
                    if (seen[w]) continue;
                    if (Math.Abs(w) / 2 == Math.Abs(s) / 2) return true;
                    Q.Add(w);
                    seen[w] = true;
                }
            }
            return false;
        }

        static void GetBuffers(int s, List<List<int>> G, List<int> assignment)
        {
            List<int> Q = new List<int>();
            Q.Add(s);
            assignment[Math.Abs(s) / 2] = (s % 2 == 0 ? 0 : 1);

            while (Q.Count != 0)
            {
                int v = Q[0];
                Q.Remove(Q[0]);

                foreach (int x in G[v])
                {
                    if (assignment[(Math.Abs(x) / 2)] != -1) continue;
                    assignment[Math.Abs(x) / 2] = (x % 2 == 0 ? 0 : 1);
                    Q.Add(x);
                }
            }
        }

        static int GetData(string[] fileData)
        {
            int n = int.Parse(fileData[0]);
            int sum = 0;
            List<int> b = new List<int>();
            List<List<int>> G = new List<List<int>>();
            List<List<char>> x = new List<List<char>>();
            for (int i = 1; i < n; i += 2)
            {
                b.Add(int.Parse(fileData[i]));
                x.Add(fileData[2].Split(' ').Select(d => d[0]).ToList());
            }
            for (int i = 0; i < n / 2; i++)
            {
                int m = x[i].Count;
                bool[] contradictions = new bool[2 * m];
                int sgn = x[i][0], letter = x[i][1], sgn2 = x[i][2], letter2 = x[i][3];
                bool sat = true;

                while (m-- > 0)
                {

                    letter -= 'A';
                    letter2 -= 'A';

                    int p, q;
                    p = (int)letter * 2;
                    q = (int)letter2 * 2;

                    if (sgn == '-') p++;
                    if (sgn2 == '-') q++;

                    G[(p % 2 == 0 ? ((p) - 1) : ((p) + 1))].Remove(q);
                    G[(q % 2 == 0 ? ((q) - 1) : ((q) + 1))].Remove(p);
                }

                for (int k = 0; k < 2 * n / 2; k++)
                {
                    contradictions[k] = IsExists(k, G);
                    if (contradictions[k] && contradictions[(k % 2 == 0 ? ((k) - 1) : ((k) + 1))])
                    {
                        sat = false;
                        break;
                    }
                }

                if (sat)
                {
                    for (int j = 0; j < 2 * m; j++)
                    {
                        if (!contradictions[j] && b[Math.Abs(j) / 2] == -1) GetBuffers(j, G, b);
                    }
                    for (int j = 0; j < m; j++)
                    {
                        if (b[j] == 0) sum += 1;
                    }
                }
            }
            return sum;
        }
    }
}
