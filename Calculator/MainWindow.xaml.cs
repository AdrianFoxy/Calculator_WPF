using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;
        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            nagativeButton.Click += NagativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtraction(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Division(lastNumber, newNumber);
                        break;
                }

                resultLabel.Content = result.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;

                if (lastNumber != 0)
                    tempNumber *= lastNumber;

                resultLabel.Content = tempNumber.ToString();
            }

        }

        private void NagativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplicationButton)
                selectedOperator = SelectedOperator.Multiplication;
            else if (sender == divisionButton)
                selectedOperator = SelectedOperator.Division;
            else if (sender == addButton)
                selectedOperator = SelectedOperator.Addition;
            else
                selectedOperator = SelectedOperator.Subtraction;
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains(","))
            {
                resultLabel.Content += ",";
            }
        }


        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && int.TryParse(button.Content?.ToString(), out int selectedValue))
            {
                resultLabel.Content = resultLabel.Content.ToString() == "0"
                    ? selectedValue.ToString()
                    : $"{resultLabel.Content}{selectedValue}";
            }
        }

    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public static class SimpleMath
    {
        public static double Add(double n1, double n2) => n1 + n2;
        public static double Subtraction(double n1, double n2) => n1 - n2;
        public static double Multiply(double n1, double n2) => n1 * n2;
        public static double Division(double n1, double n2) 
        {
            if(n2 is 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return n1 / n2;
        }
    }
}