using BusinessLogic;
using Module;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Курсовик_1
{
    public partial class WindowCalcArrayY : Window
    {
        private string step { get; set; } = String.Empty;

        public WindowCalcArrayY()
        {
            InitializeComponent();
        }

        private void BttnCalcArrayY_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(step))
            {
                MessageBox.Show("Заполните поле!");
            }
            else
            {
                Data.h = double.Parse(step);

                Data.Y = BL.CalcArrayY(Data.X, Data.h);

                if (Data.Y != null)
                {
                    MessageBox.Show("Массив Y сформирован!");
                }

                Close();
            }
        }

        private void TextboxStepArrayY_TextChanged(object sender, TextChangedEventArgs e)
        {
            step = TextboxStepArrayY.Text;
            
        }

        private void TextboxStepArrayY_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == ",") && (!TextboxStepArrayY.Text.Contains(",") && TextboxStepArrayY.Text.Length != 0)))
            {
                e.Handled = true;
            }
        }
    }
}
