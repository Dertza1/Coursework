using Module;
using System;
using System.IO;
using System.Threading.Tasks;


namespace OperationWithFile
{
    public class FileOperations
    {
        public static string path = @"RecordingFile.txt";
        public static string pathControlData = @"ControlDan.txt";
        
        public static void WriteToFileNewData()
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                writer.WriteLine($"Переменная B = {Convert.ToString(Data.B)}\n");

                writer.WriteLine($"Количество строк/столбцов: {Convert.ToString(Data.M)}\n");

                writer.WriteLine("Массив A\n");
                for (int i = 0; i < Data.A.GetLength(0); i++)
                {
                    for (int j = 0; j < Data.A.GetLength(1); j++)
                    {
                        writer.Write($"A[{i}; {j}] = {Convert.ToString(Math.Round(Data.A[i, j], 4))}\t");
                    }
                    writer.WriteLine();
                }

                writer.WriteLine("\nМассив C\n");
                for (int i = 0; i < Data.C.Length; i++)
                {
                    writer.WriteLine($"C[{i}] = {Convert.ToString(Math.Round(Data.C[i], 4))}");
                }

                writer.WriteLine("\nМассив X\n");
                for (int i = 0; i < Data.X.Length; i++)
                {
                    writer.WriteLine($"X[{i}] = {Convert.ToString(Math.Round(Data.X[i], 4))}");
                }

                writer.WriteLine("\nМассив Y\n");
                for (int i = 0; i < Data.Y.Length; i++)
                {
                    writer.WriteLine($"Y[{i}] = {Convert.ToString(Math.Round(Data.Y[i], 4))}");
                }

                writer.WriteLine("\nОтсортированный массив Y\n");
                for (int i = 0; i < Data.Y_Sorted.Length; i++)
                {
                    writer.WriteLine($"SY[{i}] = {Convert.ToString(Math.Round(Data.Y_Sorted[i], 4))}");
                }
            }
        }

        public static void WriteToFileControlData()
        {
            using (StreamWriter writer = new StreamWriter(pathControlData, false))
            {
                writer.WriteLine($"Переменная B = {Convert.ToString(Data.B)}\n");


                writer.WriteLine($"Количество строк/столбцов: {Convert.ToString(Data.M)}\n");

                writer.WriteLine("Массив A\n");
                for (int i = 0; i < Data.A.GetLength(0); i++)
                {
                    for (int j = 0; j < Data.A.GetLength(1); j++)
                    {
                        writer.Write($"A[{i}; {j}] = {Convert.ToString(Math.Round(Data.A[i, j], 4))}\t");
                    }
                    writer.WriteLine();
                }

                writer.WriteLine("\nМассив C\n");
                for (int i = 0; i < Data.C.Length; i++)
                {
                    writer.WriteLine($"C[{i}] = {Convert.ToString(Math.Round(Data.C[i], 4))}");
                }

                writer.WriteLine("\nМассив X\n");
                for (int i = 0; i < Data.X.Length; i++)
                {
                    writer.WriteLine($"X[{i}] = {Convert.ToString(Math.Round(Data.X[i], 4))}");
                }

                writer.WriteLine("\nМассив Y\n");
                for (int i = 0; i < Data.Y.Length; i++)
                {
                    writer.WriteLine($"Y[{i}] = {Convert.ToString(Math.Round(Data.Y[i], 4))}");
                }

                writer.WriteLine("\nОтсортированный массив Y\n");
                for (int i = 0; i < Data.Y_Sorted.Length; i++)
                {
                    writer.WriteLine($"SY[{i}] = {Convert.ToString(Math.Round(Data.Y_Sorted[i], 4))}");
                }
            }
        }

        public static async Task ReadToFileAsync()
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        Data.ListNewData.Add(line);
                    }
                }

            }

        public static async Task ReadToFileAsyncControlData()
        {
            if (Data.ListControlData.Count == 0)
            {
                using (StreamReader reader = new StreamReader(pathControlData))
                {
                    string line;

                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        Data.ListControlData.Add(line);
                    }
                }
            }
           

        }
    }
}
