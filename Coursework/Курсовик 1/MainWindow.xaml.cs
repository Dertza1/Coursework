using BusinessLogic;
using LiveCharts;
using LiveCharts.Wpf;
using Module;
using OperationWithFile;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;
using Window = System.Windows.Window;

namespace Курсовик_1
{
    public partial class MainWindow : Window
    {
        private string M { get; set; } = string.Empty;
        private string B { get; set; } = string.Empty; 

        private bool IsSelectedControlData { get; set; }
        private bool NewData { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public List<string> LabelsX { get; set; }
        public LineSeries Mylineseries2 = new LineSeries();
        public LineSeries Mylineseries = new LineSeries();


        public MainWindow()
        {
            InitializeComponent();

            TextboxB.Focus();

            BttnCalcArrayA.IsEnabled = false;
            BttnCalcArrayC.IsEnabled = false;
            BttnCalcArrayX.IsEnabled = false;
            BttnCalcArrayY.IsEnabled = false;
            BttnSortArrayY.IsEnabled = false;

            GridArrayA.ColumnWidth = 75;
            GridArrayC.ColumnWidth = 75;
            GridArrayX.ColumnWidth = 75;
            GridArrayY.ColumnWidth = 75;
            GridSortedArrayY.ColumnWidth = 75;

            MenuItemDataOutput.Visibility = Visibility.Collapsed;
            MenuItemSaveFile.Visibility = Visibility.Collapsed;
            MenuItemOpenFile.Visibility = Visibility.Collapsed;
            MenuItemCreateGraph.Visibility = Visibility.Collapsed;
        }


        private void BttnCalcArrayA_Click(object sender, RoutedEventArgs e)
        {
            if (Data.A == null)
            {
                WindowCalcArrayA randA = new WindowCalcArrayA(Data.M);

                randA.ShowDialog();
            }
            else if (MessageBox.Show("Вы хотите перезаписать массив?", "???", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                double[,] oldArray = Data.A;

                WindowCalcArrayA randA = new WindowCalcArrayA(Data.M);
                randA.ShowDialog();

                bool IsDifferent = false;

                for (int i = 0; i < Data.A.GetLength(0); i++)
                {
                    for (int j = 0; j < Data.A.GetLength(1); j++)
                    {
                        if (oldArray[i, j] == Data.A[i, j])
                        {
                            continue;
                        }
                        else
                        {
                            IsDifferent = true;
                        }
                    }
                }

                if (IsDifferent)
                {
                    GridArrayC.Columns.Clear();
                    GridArrayX.Columns.Clear();
                    GridArrayY.Columns.Clear();
                    GridSortedArrayY.Columns.Clear();

                    Data.C = null;
                    Data.X = null;
                    Data.Y = null;
                    Data.Y_Sorted = null;

                    BttnCalcArrayX.IsEnabled = false;
                    BttnCalcArrayY.IsEnabled = false;
                    BttnSortArrayY.IsEnabled = false;
                }

            }

            if (Data.A != null)
            {
                GridArrayA.ItemsSource = FormirationDataGrid.ToDataTable(Data.A).DefaultView;
            }

            if (Data.A != null && Data.C != null)
            {
                BttnCalcArrayX.IsEnabled = true;
            }
        }

        private void BttnCalcArrayC_Click(object sender, RoutedEventArgs e)
        {
            if (Data.C == null)
            {
                WindowCalcArrayC arrayC = new WindowCalcArrayC(Data.M);
                arrayC.ShowDialog();
            }
            else if (MessageBox.Show("Вы хотите перезаписать массив?", "???", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                double[] oldArray = Data.C;

                WindowCalcArrayC arrayC = new WindowCalcArrayC(Data.M);
                arrayC.ShowDialog();

                if (!oldArray.SequenceEqual(Data.C))
                {
                    Data.X = null;
                    Data.Y = null;
                    Data.Y_Sorted = null;

                    GridArrayX.Columns.Clear();
                    GridArrayY.Columns.Clear();
                    GridSortedArrayY.Columns.Clear();

                    BttnCalcArrayX.IsEnabled = false;
                    BttnCalcArrayY.IsEnabled = false;
                    BttnSortArrayY.IsEnabled = false;
                }
            }

            if (Data.C != null)
            {
                GridArrayC.ItemsSource = FormirationDataGrid.ToDataTable(Data.C).DefaultView;
            }

            if (Data.A != null && Data.C != null)
            {
                BttnCalcArrayX.IsEnabled = true;
            }
        }

        private void TextboxB_TextChanged(object sender, TextChangedEventArgs e)
        {
            B = TextboxB.Text;
        }

        private void TextboxM_TextChanged(object sender, TextChangedEventArgs e)
        {
            M = TextboxM.Text;
        }

        private void TextboxB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",") && (!TextboxB.Text.Contains(",") && TextboxB.Text.Length != 0) || (e.Text == "-") && (!TextboxB.Text.Contains("-") && TextboxB.Text.Length == 0)))
            {
                e.Handled = true;
            }
        }

        private void TextboxM_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
            }
        }

        private void BttnSendVariables_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(M) && !string.IsNullOrEmpty(B))
            {
                BttnCalcArrayA.IsEnabled = true;
                BttnCalcArrayC.IsEnabled = true;

                TextboxB.IsEnabled = false;
                TextboxM.IsEnabled = false;

                Data.M = int.Parse(M);
                Data.B = double.Parse(B);

                VariablesGroupBox.Content = "m = " + Data.M + " (размерность массива A и С)" + "\nB = " + Data.B + " (переменная для вычислений)";
            }
            else
            {
                MessageBox.Show("Заполните все поля!");
            }
        }

        private void BttnCalcArrayX_Click(object sender, RoutedEventArgs e)
        {
            Data.X = MathFunctions.CalcArrayX(Data.C, Data.A, Data.M, Data.B);

            if (Data.X.Length == Data.C.Length)
            {
                MessageBox.Show("Массив Х сформирован!");

                BttnCalcArrayY.IsEnabled = true;

                GridArrayX.ItemsSource = FormirationDataGrid.ToDataTable(Data.X).DefaultView;
            }
        }

        private void BttnCalcArrayY_Click(object sender, RoutedEventArgs e)
        {
            if (Data.Y == null)
            {
                WindowCalcArrayY arrayY = new WindowCalcArrayY();
                arrayY.ShowDialog();
            }

            else if (MessageBox.Show("Вы хотите перезаписать массив?", "???", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                double[] oldArray = Data.Y; 

                WindowCalcArrayY arrayY = new WindowCalcArrayY();
                arrayY.ShowDialog();

                if (!oldArray.SequenceEqual(Data.Y))
                {
                    GridSortedArrayY.Columns.Clear();

                    Data.Y_Sorted = null;

                    BttnSortArrayY.IsEnabled = true;
                }
            }

            if (Data.Y != null)
            {
                GridArrayY.ItemsSource = FormirationDataGrid.ToDataTable(Data.Y).DefaultView;

                BttnSortArrayY.IsEnabled = true;
            }
        }

        private void BttnSortArrayY_Click(object sender, RoutedEventArgs e)
        {
            Data.Y_Sorted = MathFunctions.SortingArrayY(Data.Y);

            if (Data.Y_Sorted != null)
            {
                GridSortedArrayY.ItemsSource = FormirationDataGrid.ToDataTable(Data.Y_Sorted).DefaultView;
            }

            NewData = true;
            IsSelectedControlData = false;

            FileOperations.WriteToFileNewData();
        }

        private void BttnClearForm_Click(object sender, RoutedEventArgs e)
        {
            if (Form1.IsSelected)
            {
                if (MessageBox.Show("Очистить данные с формы?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    BttnSendVariables.IsEnabled = true;
                    BttnCalcArrayC.IsEnabled = false;
                    BttnCalcArrayA.IsEnabled = false;
                    BttnCalcArrayX.IsEnabled = false;
                    BttnCalcArrayY.IsEnabled = false;
                    BttnSortArrayY.IsEnabled = false;

                    M = string.Empty;
                    B = string.Empty;

                    TextboxB.IsEnabled = true;
                    TextboxM.IsEnabled = true;
                    TextboxB.Text = string.Empty;
                    TextboxM.Text = string.Empty;

                    Data.C = null;
                    Data.A = null;
                    Data.X = null;
                    Data.Y = null;
                    Data.Y_Sorted = null;

                    VariablesGroupBox.Content = string.Empty;

                    GridArrayA.Columns.Clear();
                    GridArrayC.Columns.Clear();
                    GridArrayX.Columns.Clear();
                    GridArrayY.Columns.Clear();
                    GridSortedArrayY.Columns.Clear();

                    Data.ListNewData.Clear();
                    Data.ListControlData.Clear();
                }
            }

            if (Form2.IsSelected)
            {
                if (MessageBox.Show("Очистить текстовый редактор?", "", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    textBoxFileText.Text = string.Empty;

                    Data.ListNewData.Clear();
                    Data.ListControlData.Clear();
                }
            }

            if (Form3.IsSelected)
            {
                DataContext = null;
            }
        }

        private async void BttnDataOutput_Click(object sender, RoutedEventArgs e)
        {
            if (NewData)
            {
                if (Data.ListNewData.Count == 0)
                {
                    await FileOperations.ReadToFileAsync();

                    for (int i = 0; i < Data.ListNewData.Count; i++)
                    {
                        textBoxFileText.Text += Data.ListNewData[i].ToString() + "\r\n";
                    }

                    Data.ListNewData.Clear();
                }

            }

            else if (IsSelectedControlData)
            {
                if (Data.ListControlData.Count == 0)
                {
                    await FileOperations.ReadToFileAsyncControlData();

                    for (int i = 0; i < Data.ListControlData.Count; i++)
                    {
                        textBoxFileText.Text += Data.ListControlData[i].ToString() + "\r\n";
                    }

                    Data.ListControlData.Clear();
                }
            }

            else if (!IsSelectedControlData && !NewData)
            {
                MessageBox.Show("Заполните все данные");

                Form1.IsSelected = true;
            }
        }

        private void BttnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Текстовый файл (*.txt)|*.txt|Документ Word (*.docx)|*.docx";

            if (openFileDialog.ShowDialog() == true)
            {
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);

                StreamReader reader = new StreamReader(fileInfo.Open(FileMode.Open, FileAccess.Read), Encoding.UTF8);

                textBoxFileText.Text = reader.ReadToEnd();

                reader.Close();
                return;
            }
        }

        private void BttnSaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            saveFileDialog1.Filter = "Текстовый документ (*.txt)|*.txt|Документ Word (*.docx)|*.docx";

            if (saveFileDialog1.ShowDialog() == true)
            {
                StreamWriter sw = new StreamWriter(saveFileDialog1.OpenFile(), Encoding.UTF8);
                
                sw.Write(textBoxFileText.Text);

                sw.Close();
                return;
            }

        }

        private void BttnControlData_Click(object sender, RoutedEventArgs e)
        {
            Data.ListNewData.Clear();

            BttnSendVariables.IsEnabled = false;
            BttnDataOutput.IsEnabled = true;

            Data.M = 3;
            TextboxM.Text = Data.M.ToString();
            TextboxM.IsEnabled = false;

            Data.B = -3.5;
            TextboxB.Text = Data.B.ToString();
            TextboxB.IsEnabled = false;


            BttnCalcArrayA.IsEnabled = false;
            BttnCalcArrayC.IsEnabled = false;
            BttnCalcArrayX.IsEnabled = false;
            BttnCalcArrayY.IsEnabled = false;
            BttnSortArrayY.IsEnabled = false;

            VariablesGroupBox.Content = "m = " + Data.M + " (размерность массива A и С)" + "\nB = " + Data.B + " (переменная для вычислений)";

            Data.h = 0.2;

            Data.A = new double[,] { { 5, 2, 7 }, { 10, 9, 1 }, { 10, 5, 6 } };
            GridArrayA.ItemsSource = FormirationDataGrid.ToDataTable(Data.A).DefaultView;

            Data.C = new double[] { 4, 7, 3 };
            GridArrayC.ItemsSource = FormirationDataGrid.ToDataTable(Data.C).DefaultView;

            Data.X = MathFunctions.CalcArrayX(Data.C, Data.A, Data.M, Data.B);
            GridArrayX.ItemsSource = FormirationDataGrid.ToDataTable(Data.X).DefaultView;

            Data.Y = MathFunctions.CalcArrayY(Data.X, Data.h);
            GridArrayY.ItemsSource = FormirationDataGrid.ToDataTable(Data.Y).DefaultView;

            Data.Y_Sorted = MathFunctions.SortingArrayY(Data.Y);
            GridSortedArrayY.ItemsSource = FormirationDataGrid.ToDataTable(Data.Y_Sorted).DefaultView;

            IsSelectedControlData = true;
            NewData = false;

        }

        private void BttnCreateGraph_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext == null)
            {
                if (Data.Y != null || Data.Y_Sorted != null)
                {
                    LabelsX = new List<string>();
                    SeriesCollection = new SeriesCollection { };

                    if (LabelsX.Count == 0)
                    {
                        for (int i = 0; i < Data.Y.Length; i++)
                        {
                            LabelsX.Add(Convert.ToString(i));
                        }
                    }

                    Mylineseries.Title = "Y";
                    Mylineseries.LineSmoothness = 0;
                    Mylineseries.PointGeometry = null;
                    Mylineseries.Values = new ChartValues<double>(Data.Y);         
                    
                    SeriesCollection.Add(Mylineseries);

                    Mylineseries2.Title = "Y_Sort";
                    Mylineseries2.LineSmoothness = 0;
                    Mylineseries2.PointGeometry = null;
                    Mylineseries2.Values = new ChartValues<double>(Data.Y_Sorted);

                    SeriesCollection.Add(Mylineseries2);

                    DataContext = this;
                }
                else
                {
                    MessageBox.Show("Заполните все данные");

                    Form1.IsSelected = true;
                }
            }



        }

        private void Form1_GotFocus(object sender, RoutedEventArgs e)
        {
            MenuItemClearForm.Header = "Очистить данные";

            MenuItemControlData.Visibility = Visibility.Visible;
            MenuItemClearForm.Visibility = Visibility.Visible;
            MenuItemDataOutput.Visibility = Visibility.Collapsed;
            MenuItemSaveFile.Visibility = Visibility.Collapsed;
            MenuItemOpenFile.Visibility = Visibility.Collapsed;
            MenuItemCreateGraph.Visibility = Visibility.Collapsed;
        }

        private void Form2_GotFocus(object sender, RoutedEventArgs e)
        {
            MenuItemClearForm.Header = "Очистить текстовый редактор";

            MenuItemControlData.Visibility = Visibility.Collapsed;
            MenuItemDataOutput.Visibility = Visibility.Visible;
            MenuItemSaveFile.Visibility = Visibility.Visible;
            MenuItemOpenFile.Visibility = Visibility.Visible;
            MenuItemCreateGraph.Visibility = Visibility.Collapsed;
        }

        private void Form3_GotFocus(object sender, RoutedEventArgs e)
        {
            MenuItemClearForm.Header = "Очистить поле графика";

            MenuItemControlData.Visibility = Visibility.Collapsed;
            MenuItemDataOutput.Visibility = Visibility.Collapsed;
            MenuItemSaveFile.Visibility = Visibility.Collapsed;
            MenuItemOpenFile.Visibility = Visibility.Collapsed;
            MenuItemCreateGraph.Visibility = Visibility.Visible;
        }



        private void Helper_Click(object sender, RoutedEventArgs e)
        {
            Help.ShowHelp(null, "help.chm");
        }
    }

}
