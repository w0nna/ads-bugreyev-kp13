using System;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lab06f
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ToolBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWin(object sender, RoutedEventArgs e)
        {
            Exit exit = new Exit();
            exit.Show();
            this.Close();
        }

        private void MinWin(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrimerBox.Text = "";
        }

        private void Calc_Btn_Click(object sender, RoutedEventArgs e)
        {
            BorderLeft.Visibility = Visibility.Visible;
            BorderRight.Visibility = Visibility.Visible;
            String func = PrimerBox.Text;

            List<string> arguments = new List<string>();
            string operand1 = "";
            string operand2 = "";
            string lastOperation = "";
            List<string> operations = new List<string> { "(", "+", "-", "*", "/", "^", ")", "(", "()" };
            List<string> functions = new List<string> { "sin", "cos", "exp", "tan", "ln", "lg", "sh", "ch" };

            int symbolNumber = 0;
            String sourceExpression = PrimerBox.Text.Replace(" ", "");

            while (symbolNumber < sourceExpression.Length)
            {
                int currentIndex = arguments.Count - 1;

                //Open brackets
                if (sourceExpression[symbolNumber] == '(')
                {
                    arguments.Add(sourceExpression[symbolNumber].ToString());
                    while (operations.Contains(arguments[currentIndex]) &&
                               operations.LastIndexOf(sourceExpression[symbolNumber].ToString()) >
                               operations.IndexOf(arguments[currentIndex]))
                    {
                        arguments[currentIndex + 1] = arguments[currentIndex];
                        currentIndex--;
                    }

                    arguments[currentIndex + 1] = sourceExpression[symbolNumber].ToString();
                    lastOperation = sourceExpression[symbolNumber].ToString();
                    symbolNumber++;
                    continue;
                }
                //Close brackets
                else if (sourceExpression[symbolNumber] == ')')
                {
                    arguments[arguments.LastIndexOf("(")] = "()";
                    lastOperation = "()";
                    symbolNumber++;
                    continue;
                }
                //Operation
                else if (operations.Contains(sourceExpression[symbolNumber].ToString()))
                {
                    //Always after (
                    if (lastOperation == "(")
                    {
                        int index = arguments.LastIndexOf(lastOperation);
                        arguments.Add(lastOperation);
                        while (currentIndex >= index)
                        {
                            arguments[currentIndex + 1] = arguments[currentIndex];
                            currentIndex--;
                        }

                        arguments[currentIndex + 1] = sourceExpression[symbolNumber].ToString();
                    }
                    //priority
                    else
                    {
                        arguments.Add(lastOperation.ToString());
                        while (
                            operations.Contains(arguments[currentIndex]) &&
                            operations.IndexOf(sourceExpression[symbolNumber].ToString()) >
                            operations.IndexOf(arguments[currentIndex]))
                        {
                            arguments[currentIndex + 1] = arguments[currentIndex];
                            currentIndex--;
                        }

                        arguments[currentIndex + 1] = sourceExpression[symbolNumber].ToString();
                    }

                    lastOperation = sourceExpression[symbolNumber].ToString();
                    symbolNumber++;
                }
                //Operand1
                else if (operand1 == "")
                {
                    while (symbolNumber < sourceExpression.Length &&
                        !operations.Contains(sourceExpression[symbolNumber].ToString()))
                    {
                        operand1 += sourceExpression[symbolNumber];
                        symbolNumber++;
                    }

                    arguments.Add(operand1);
                    if (functions.Contains(operand1))
                    {
                        arguments.Add("Compose");
                    }
                    operand2 = "";
                }
                //Operand2
                else if (operand2 == "")
                {
                    while (symbolNumber < sourceExpression.Length &&
                        !operations.Contains(sourceExpression[symbolNumber].ToString()))
                    {
                        operand2 += sourceExpression[symbolNumber];
                        symbolNumber++;
                    }

                    int index = arguments.LastIndexOf(lastOperation.ToString());
                    int bracketIndex = arguments.IndexOf("(");
                    if (bracketIndex > 0 &&
                        arguments.LastIndexOf(lastOperation.ToString(), bracketIndex) > 0 &&
                        arguments.LastIndexOf(lastOperation.ToString(), bracketIndex) < index)
                    {
                        index = arguments.LastIndexOf(lastOperation.ToString(), arguments.IndexOf("("));
                    }
                    arguments.Add(operand2);
                    if (functions.Contains(operand2))
                    {
                        arguments.Add("Compose");
                    }

                    #region Put arg2 at right place
                    currentIndex = arguments.Count - 1;
                    while (currentIndex > index)
                    {
                        if (functions.Contains(operand2))
                        {
                            arguments[currentIndex] = arguments[currentIndex - 2];
                        }
                        else
                        {
                            arguments[currentIndex] = arguments[currentIndex - 1];
                        }
                        currentIndex--;
                    }

                    arguments[index] = operand2;
                    if (functions.Contains(operand2))
                    {
                        arguments[index + 1] = "Compose";
                    }
                    #endregion

                    operand2 = "";
                }
                TextLabelLeft.Text += String.Join(",", arguments) + Environment.NewLine;
            }

            //ResultTextBox.Text = "";
            TextLabelRight.Text = String.Join(",", arguments);

            Dictionary<string, string> DiffsList = new Dictionary<string, string>();

            //Calculating f'
            while (arguments.Count > 1)
            {
                String firstOper = "";
                int firstOperIndex = arguments.Count;
                foreach (String str in operations)
                {
                    if (arguments.IndexOf(str) > 0 && arguments.IndexOf(str) < firstOperIndex)
                    {
                        firstOperIndex = arguments.IndexOf(str);
                        firstOper = str;
                    }
                }

                String f1 = "", f2 = "", df1 = "", df2 = "", oper = "", expression = "", diff = "";

                if (firstOper == "()")
                {
                    arguments.RemoveAt(firstOperIndex);
                    int ComposeIndex = arguments.LastIndexOf("Compose", firstOperIndex);
                    if (ComposeIndex > 0)
                    {
                        f1 = arguments[ComposeIndex - 1];
                        df1 = Proizv.Diff(f1, DiffsList);
                        f2 = arguments[ComposeIndex + 1];
                        df2 = Proizv.Diff(f2, DiffsList);
                        oper = "Compose";
                        expression = f1 + "(" + f2 + ")";
                        diff = Proizv.Diff(f1, df1, f2, df2, oper);

                        arguments.RemoveAt(ComposeIndex + 1);
                        arguments.RemoveAt(ComposeIndex);
                        arguments[ComposeIndex - 1] = expression;
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    f1 = arguments[firstOperIndex - 2];
                    df1 = Proizv.Diff(f1, DiffsList);
                    f2 = arguments[firstOperIndex - 1];
                    df2 = Proizv.Diff(f2, DiffsList);
                    oper = arguments[firstOperIndex];
                    expression = f1 + oper + f2;
                    diff = Proizv.Diff(f1, df1, f2, df2, oper);

                    arguments.RemoveAt(firstOperIndex);
                    arguments.RemoveAt(firstOperIndex - 1);
                    arguments[firstOperIndex - 2] = expression;
                }

                TextLabelLeft.Text += Environment.NewLine + expression;
                TextLabelRight.Text += Environment.NewLine + diff;

                if (!DiffsList.ContainsKey(expression))
                {
                    DiffsList.Add(expression, diff);
                }
            }


        }

    }
}
