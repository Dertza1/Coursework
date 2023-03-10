using Module;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Курсовик_1
{
    public partial class WindowCalcArrayA : Window
    {
        private string beginRangeStr { get; set; } = string.Empty;
        private string endRangeStr { get; set; } = string.Empty;
        private int sizeArray { get; set; }

        public WindowCalcArrayA(int sizeArray)
        {
            InitializeComponent();
            this.sizeArray = sizeArray;
        }

        private void BttnCalcArrayA_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(beginRangeStr) || string.IsNullOrEmpty(endRangeStr))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                int beginRange = Convert.ToInt32(beginRangeStr);
                int endRange = Convert.ToInt32(endRangeStr);
                int blank;

                if (beginRange > endRange)
                {
                    blank = beginRange;
                    beginRange = endRange;
                    endRange = blank;
                }


                Data.A = new double[sizeArray, sizeArray];

                Random rnd = new Random();

                for (int i = 0; i < Data.A.GetLength(0); i++)
                {
                    for (int j = 0; j < Data.A.GetLength(1); j++)
                    {
                        Data.A[i, j] = rnd.Next(beginRange, endRange);
                    }
                }

                MessageBox.Show("Массив заполнен");
                Close();
            }

        }

        private void TextboxBeginRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            beginRangeStr = TextboxBeginRange.Text;
        }

        private void TextboxEndRange_TextChanged(object sender, TextChangedEventArgs e)
        {
            endRangeStr = TextboxEndRange.Text;
        }

        private void TextboxBeginRange_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",") || (e.Text == "-") && (!TextboxBeginRange.Text.Contains("-") && TextboxBeginRange.Text.Length == 0)))
            {
                e.Handled = true;
            }
        }

        private void TextboxEndRange_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",") || (e.Text == "-") && (!TextboxEndRange.Text.Contains("-") && TextboxEndRange.Text.Length == 0)))
            {
                e.Handled = true;
            }
        }
    }
}
