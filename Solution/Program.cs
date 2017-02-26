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
                int[] dataArray = Array.ConvertAll(fileContent, int.Parse);
                dataArray = twoSum(dataArray, dataArray[1]);
                file.LogMessage("Process ended.. "+ dataArray.ToString());
            }
            catch (Exception ex) {
                file.LogMessage(ex.Message);
            }
        }
        public static int[] twoSum(int[] numbers, int target)
        {
            var dictionary = new Dictionary<int, List<int>>();

            for (int i = 0; i < numbers.Length; i++)
            {
                if (dictionary.ContainsKey(numbers[i]))
                {
                    dictionary[numbers[i]].Add(i);
                }
                else
                {
                    dictionary.Add(numbers[i], new List<int> { i });
                }
            }

            foreach (var keyValuePair in dictionary)
            {
                List<int> listOfIndexes;
                var remained = target - keyValuePair.Key;
                if (!dictionary.TryGetValue(remained, out listOfIndexes))
                {
                    continue;
                }


                if (remained == keyValuePair.Key && listOfIndexes.Count > 1)
                {
                    var result = listOfIndexes.GetRange(0, 2);
                    result.Sort();
                    return result.ToArray();
                }

                if (remained != keyValuePair.Key && listOfIndexes.Count > 0)
                {
                    var result = new List<int>() { keyValuePair.Value[0], listOfIndexes[0] };
                    result.Sort();

                    return result.ToArray();
                }
            }

            return null;
        }
    }
}
