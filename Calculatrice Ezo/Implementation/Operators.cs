using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Calculatrice_Ezo.Implementation
{
    public class Operators
    {
        public string Treat(string input)
        {
            List<string> elements = GetElementsInput(input);

            elements = ManageNegativeNumber(elements);

            string resultOperation = FindOperation(elements);

            if (resultOperation == "∞")
                resultOperation = "Error";

            return resultOperation;
        }

        private static List<string> GetElementsInput(string input)
        {
            input = input.Replace(" ", "");
            input = input.Replace(",", ".");
            var elements = new List<string>();
            var num = "";
            var hasMoreOneNumber = false;
            var pattern = @"^(\.|[0-9])$";
            var regex = new Regex(pattern);

            for (int i = 0; i < input.Length; i++)
            {
                //Pega todos os dígitos de um número e coloca em num
                while (i < input.Length && regex.IsMatch(input[i].ToString()))
                {
                    num += input[i].ToString();
                    i++;
                    hasMoreOneNumber = true;
                }

                if (hasMoreOneNumber)
                {
                    elements.Add(num);
                    num = "";
                    hasMoreOneNumber = false;
                }

                if (i < input.Length)
                {
                    elements.Add(input[i].ToString());
                }
            }

            return elements;
        }

        private static List<string> ManageNegativeNumber(List<string> elements)
        {
            var listAux = new List<string>();

            for (int i = 0; i < elements.Count; i++)
            {
                if (i == 0 && elements[i] == "-")
                {
                    listAux.Add(elements[i] + elements[1]);
                    i++;
                }
                else if (((elements[i] == "-") || (elements[i] == "+")) &&
                    (elements[i - 1] == "+"
                    || elements[i - 1] == "-"
                    || elements[i - 1] == "*"
                    || elements[i - 1] == "/"))
                {
                    listAux.Add(elements[i] + elements[i + 1]);
                    i++;
                }
                else
                {
                    listAux.Add(elements[i]);
                }

            }

            return listAux;

        }

        private string FindOperation(List<string> elements)
        {
            string number1;
            string number2;

            var stack = new Stack<string>();

            string result;

            for (int i = 0; i < elements.Count; i++)
            {
                if (elements[i] == "(")
                {
                    number1 = elements[i + 1];
                    var operation = elements[i + 2];
                    number2 = elements[i + 3];

                    result = Calculator(number1, operation, number2);
                    stack.Push(result);

                    if (i > 0 && (elements[i - 1] == "*" || elements[i - 1] == "/"))
                    {
                        number2 = stack.Pop();
                        operation = stack.Pop();
                        number1 = stack.Pop();

                        result = Calculator(number1, operation, number2);
                        stack.Push(result);
                    }

                    i = i + 4;
                }
                else if (i > 0 && (elements[i - 1] == "+" || elements[i - 1] == "-" || elements[i - 1] == "*" || elements[i - 1] == "/"))
                {
                    if (elements[i - 1] == "+" || elements[i - 1] == "-")
                    {
                        if (i < elements.Count && (i == elements.Count - 1 || elements[i + 1] == "+" || elements[i + 1] == "-"))
                        {
                            var operation = stack.Pop();
                            number1 = stack.Pop();
                            number2 = elements[i];

                            result = Calculator(number1, operation, number2);
                            stack.Push(result);

                        }
                        else
                        {
                                stack.Push(elements[i]);
                        }
                    }
                    else if (elements[i - 1] == "*" || elements[i - 1] == "/")
                    {
                        var operation = stack.Pop();
                        number1 = stack.Pop();
                        number2 = elements[i];

                        result = Calculator(number1, operation, number2);
                        stack.Push(result);
                    }
                }
                else
                {
                        stack.Push(elements[i]);
                }

            }

            while (stack.Count != 1)
            {
                number2 = stack.Pop();
                var operation = stack.Pop();
                number1 = stack.Pop();

                result = Calculator(number1, operation, number2);

                stack.Push(result);
            }

            return stack.Pop();
        }

        private string Calculator(string number1, string operation, string number2)
        {
            var result = "";

            switch (operation)
            {
                case "+":
                    result = Sum(number1, number2);
                    break;


                case "-":
                    result = Sub(number1, number2);
                    break;

                case "*":
                    result = Mult(number1, number2);
                    break;


                case "/":
                    result = Div(number1, number2);
                    break;

            }

            return result;
        }

        private string Sum(string number1, string number2)
        {

            if (number1 == "1" && number2 == "1") return "1";


            var resultPlus = Convert.ToDouble(number1) + Convert.ToDouble(number2);

            return resultPlus.ToString();
        }

        private string Sub(string number1, string number2)
        {
            var resultSub = Convert.ToDouble(number1) - Convert.ToDouble(number2);

            return resultSub.ToString();
        }

        public string Mult(string number1, string number2)
        {
            var resultMult = Convert.ToDouble(number1) * Convert.ToDouble(number2);

            return resultMult.ToString();
        }

        public string Div(string number1, string number2)
        {
            var resultDiv = Convert.ToDouble(number1) / Convert.ToDouble(number2);

            return resultDiv.ToString();
        }
    }
}
