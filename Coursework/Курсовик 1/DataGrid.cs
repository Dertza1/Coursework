using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Курсовик_1
{
    public static class FormirationDataGrid
    {
        public static DataTable ToDataTable<T>(T[] vector)
        {
            var result = new DataTable();
            int n = vector.Length;
            for (int i = 0; i < n; i++)
            {
                result.Columns.Add($"{i}", typeof(T));
            }
            var row = result.NewRow();
            for (int i = 0; i < n; i++)
            {
                row[i] = vector[i];
            }
            result.Rows.Add(row);
            return result;
        }
        public static DataTable ToDataTable<T>(T[,] matrix)
        {
            var result = new DataTable();
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            for (int i = 0; i < n; i++)
            {
                result.Columns.Add($"{i}", typeof(T));
            }
            for (int i = 0; i < n; i++)
            {
                var row = result.NewRow();
                for (int j = 0; j < m; j++)
                {
                    row[j] = matrix[i, j];
                }
                result.Rows.Add(row);
            }
            return result;
        }
    }
}
