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
                
                int count = Quicksort(values.ToArray(), values[0], values[values.Count -1], values[0]);
                file.LogMessage("Process ended.." + count);
                count = Quicksort(values.ToArray(), values[0], values[values.Count - 1], values[values.Count - 1]);
                file.LogMessage("Process ended.." + count);
                count = Quicksort(values.ToArray(), values[0], values[values.Count - 1], values[values.Count / 2 - 1]);
                file.LogMessage("Process ended.." + count);

            }
            catch (Exception ex) {
                file.LogMessage(ex.Message);
            }
        }
        public static int Quicksort(int[] arr, int arr1, int arr2, int b)
        {
            int swapCount = 0;
            if (arr1 < arr2)
            {
                int v = b;
                for (int i = arr1 + 1; i < arr2; i++)
                {
                    if (arr[i] > arr[arr1])
                    {
                        Swap(arr, i, ++v);
                        swapCount++;
                    }
                }
                Swap(arr, arr1, v);
                swapCount += Quicksort(arr, arr1, v, arr1);
                swapCount += Quicksort(arr, v + 1, arr2, v + 1);
                swapCount++;
            }
            return swapCount;
        }

        /**
         * Swaps the elements of the array
         * @param array array
         * @param left index of the first element
         * @param right index of the second element
         */
        private static void Swap(int[] arr, int left, int arr2)
        {
            int tmp = arr[arr2];
            arr[arr2] = arr[left];
            arr[left] = tmp;
        }
    }
