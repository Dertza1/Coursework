using Module;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Курсовик_1
{
    public partial class WindowCalcArrayC : Window
    {
        private string beginRangeStr { get; set; } = string.Empty;
        private string endRangeStr { get; set; } = string.Empty;
        private int arraySize { get; set; }

        public WindowCalcArrayC(int arraySize)
        {
            InitializeComponent();
            this.arraySize = arraySize;
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

        private void BttnCalcArrayC_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(endRangeStr) || string.IsNullOrEmpty(beginRangeStr))
            {
                MessageBox.Show("Заполните все поля!");
            }
            else
            {
                int beginRange = int.Parse(beginRangeStr);
                int endRange = int.Parse(endRangeStr);
                int blank;
               
                if (beginRange > endRange)
                {
                    blank = beginRange;
                    beginRange = endRange;
                    endRange = blank;
                }


                Data.C = new double[arraySize];

                Random rnd = new Random();
                for (int i = 0; i < Data.C.Length; i++)
                {
                    Data.C[i] = rnd.Next(beginRange, endRange);
                }

                MessageBox.Show("Массив заполнен");
                Close();
            }
        }
    }
}
