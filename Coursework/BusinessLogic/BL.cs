using System;

namespace BusinessLogic
{
    public class BL
    {
        public static double SumMainDiagonal(double[,] A)
        {
            double Sum = 0;

            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(1); j++)
                {
                    if (i == j)
                    {
                        Sum += A[i, j];
                    }
                }
            }
            return Sum;
        }


        public static double[] CalcArrayX(double[] C, double[,] A, int m, double B)
        {
            double P = 1;
            double[] X = new double[m];

            double SumMainDiag = SumMainDiagonal(A);

            double sum = 0;
            double array = 0;

            for (int i = 0; i < X.Length; i++)
            {
                for (int k = 0; k < A.GetLength(1); k++)
                {
                    sum += C[k] / (P * A[i, k] + (C[k] / 5));
                }

                array = ((SumMainDiag + B) / C[i]) * sum;

                X[i] = Math.Round(array, 3);
                P = X[i];

                sum = 0;
            }

            return X;
        }

        public static double FuncLagranzh(double[] x, double[] func, double t)
        {
            int n = x.Length;
            double s = 0;
            for (int i = 0; i < n; i++)
            {
                double p = 1;
                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        p = p * (t - x[j]) / (x[i] - x[j]);
                    }
                }
                s += func[i] * p;
            }
            return s;
        }
        public static double[] CalcArrayY(double[] x, double h)
        {
            int n = x.Length;
            int m = (int)((n - 1) / h) + 1;

            double[] arg = new double[n];
            double[] y = new double[m];

            for (int i = 0; i < n; i++)
            {
                arg[i] = i;
            }
            int j = 0;
            for (double t = arg[0]; t < m; t += h)
            {
                if (j < m)
                {
                    y[j] = FuncLagranzh(arg, x, t);
                    j++;
                }
            }
            return y;
        }


        public static double[] SortingArrayY(double[] y)
        {
            //    Array.Sort(y);
            //    Array.Reverse(y);
            //    return y;

            double[] y_sort = new double[y.Length];

            for (int i = 0; i < y_sort.Length; i++)
            {
                y_sort[i] = y[i];
            }

            double temp;

            for (int i = 0; i < y_sort.Length; i++)
            {
                for (int j = i + 1; j < y_sort.Length; j++)
                {
                    if (y_sort[i] < y_sort[j])
                    {
                        temp = y_sort[i];
                        y_sort[i] = y_sort[j];
                        y_sort[j] = temp;
                    }
                }
            }


            return y_sort;
        }
    }
}
