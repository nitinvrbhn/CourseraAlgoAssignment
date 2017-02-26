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
                List<int> values = fileContent.Select(t => Convert.ToInt32(t.Split(' '))).ToList();
                List<int> left = new List<int>(), right = new List<int>();
                int count = CountSort(values).Item1;
                file.LogMessage("Process ended.." + count);
            }
            catch (Exception ex) {
                file.LogMessage(ex.Message);
            }
        }

        private static Tuple<int, List<int>> CountMerge(List<int> left, List<int> right)
        {
            int inv = 0;
            var result = new List<int>();
            int i = 0, j = 0;

            while (i < left.Count && j < right.Count)
            {
                if (left[i] < right[j])
                {
                    result.Add(left[i]);
                    i++;
                }
                else
                {
                    result.Add(right[j]);
                    j++;
                    inv += left.Count - i;
                }
            }

            // we still have values in one of lists 
            if (i < left.Count)
                result.AddRange(left.GetRange(i, left.Count - i));
            else if (j < right.Count)
                result.AddRange(right.GetRange(j, right.Count - j));

            return new Tuple<int, List<int>>(inv, result);
        }
        
        public static Tuple<int, List<int>> CountSort(List<int> list)
        {
            if (list.Count <= 1)
                return new Tuple<int, List<int>>(0, list);

            // divide
            int middle = list.Count / 2;
            var leftList = list.GetRange(0, middle);
            var rightList = list.GetRange(middle, list.Count - leftList.Count);

            // process recursively
            Tuple<int, List<int>> left = CountSort(leftList);
            Tuple<int, List<int>> right = CountSort(rightList);

            // merge
            Tuple<int, List<int>> mergeResult = CountMerge(left.Item2, right.Item2);

            return new Tuple<int, List<int>>(left.Item1 + right.Item1 + mergeResult.Item1,
                mergeResult.Item2);
        }
    }
}
