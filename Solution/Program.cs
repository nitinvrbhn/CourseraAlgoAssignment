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
                
                int count = QuickSortComparionCount(fileContent.Length);

                file.LogMessage("Process ended..");
                file.LogMessage("Result is: " + count);
            }
            catch (Exception ex) {
                file.LogMessage(ex.Message);
            }
        }

        public static int QuickSortComparionCount(int dataCount) {
            int sum = 0;
            if (dataCount == 1 || dataCount == 2) {
                return dataCount - 1;
            }
            sum += QuickSortComparionCount(dataCount / 2);
            sum += QuickSortComparionCount((dataCount - (dataCount/ 2)));
            sum += (dataCount - 1);
            return sum;
        }
    }
}
