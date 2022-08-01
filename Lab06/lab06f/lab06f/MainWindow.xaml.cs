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
    public partial class MainWindow : Window
    {
        int o = 0;
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

        public static string ToRPN(string initialString)
        {
            Stack<char> operationsStack = new Stack<char>();

            char lastOperation;

            string result = string.Empty;
            
            initialString = initialString.Replace(" ", "");

            for (int i = 0; i < initialString.Length; i++)
            {
                if (Char.IsDigit(initialString[i]))
                {
                    result += initialString[i];
                    continue;
                }

                if (IsOperation(initialString[i]))
                {
                    if (!(operationsStack.Count == 0))
                        lastOperation = operationsStack.Peek();
                    else
                    {
                        operationsStack.Push(initialString[i]);
                        continue;
                    }
                    if (GetOperationPriority(lastOperation) < GetOperationPriority(initialString[i]))
                    {
                        operationsStack.Push(initialString[i]);
                        continue;
                    }
                    else
                    {
                        result += operationsStack.Pop();
                        operationsStack.Push(initialString[i]);
                        continue;
                    }
                }

                if (initialString[i].Equals('('))
                {
                    operationsStack.Push(initialString[i]);
                    continue;
                }
                if (initialString[i].Equals(')'))
                {
                    while (operationsStack.Peek() != '(')
                    {
                        result += operationsStack.Pop();
                    }
                    operationsStack.Pop();
                }
            }
            while (!(operationsStack.Count == 0))
            {
                result += operationsStack.Pop();
            }

            return result;
        }


        public static int CalculateRPN(string rpnString)
        {
            Stack<int> numbersStack = new Stack<int>();

            int op1, op2;

            for (int i = 0; i < rpnString.Length; i++)
            {

                if (Char.IsDigit(rpnString[i]))
                    numbersStack.Push(int.Parse(rpnString[i].ToString()));
                else
                {
                    op2 = numbersStack.Pop();
                    op1 = numbersStack.Pop();
                    numbersStack.Push(ApplyOperation(rpnString[i], op1, op2));
                }
            }

            return numbersStack.Pop();
        }

        private static bool IsOperation(char c)
        {
            if (c == '+' ||
                c == '-' ||
                c == '*' ||
                c == '/')
                return true;
            else
                return false;
        }


        private static int GetOperationPriority(char c)
        {
            switch (c)
            {
                case '+': return 1;
                case '-': return 1;
                case '*': return 2;
                case '/': return 2;
                default: return 0;
            }
        }

     
        private static int ApplyOperation(char operation, int op1, int op2)
        {
            switch (operation)
            {
                case '+': return (op1 + op2);
                case '-': return (op1 - op2);
                case '*': return (op1 * op2);
                case '/': return (op1 / op2);
                default: return 0;
            }
        }


        private void Calc_Btn_Click(object sender, RoutedEventArgs e)
        {
            TextLabelLeft.Text = ToRPN(PrimerBox.Text);
        }

    }
}
