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
                file.LogMessage("Process ended..");
            }
            catch (Exception ex) {
                file.LogMessage(ex.Message);
            }
        }
    }
}
