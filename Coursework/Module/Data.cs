using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Module
{
    public static class Data
    {
        public static int M { get; set; }
        public static double B { get; set; }
        public static double[,] A { get; set; }
        public static double[] C { get; set; }
        public static double[] X { get; set; }
        public static double h { get; set; }
        public static double[] Y { get; set; }
        public static double[] Y_Sorted { get; set; }

        public static List<string> ListNewData = new List<string>();
        public static List<string> ListControlData = new List<string>();

    }
}
