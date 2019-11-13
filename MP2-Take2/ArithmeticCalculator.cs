//MP2 Calculator 
//This file contains the ArithmethicCalculator class.

//You should implement the BasicArithmetic method.

using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace MP2
{
    public class ArithmethicCalculator
    {
        /// <summary>
        /// Prompts the user for an arithmetic expression in the simplified
        /// Reverse Polish Notation, and returns a string that contains
        /// the arithmetic expression (in normal notation with parenthesis) 
        /// and the result.
        /// If the expression provided by the user is not correct, simply returns
        /// "Invalid expression".
        /// </summary>
        /// <returns>
        /// Returns the string that contains the arithmetic expression and the result,
        /// or the requested error message. 
        /// </returns>
        /// <example>
        /// If the user enters "2 3 +" then the method returns "2 + 3 = 5".
        /// If the user enters "4 5 + 6 * 8 / 2 ^" then the method returns:
        /// (((4 + 5) * 6) / 8 ) ^ 2 = 45.5625
        /// Extra spaces are fine, so if the user enters " 2   3    ^" then 
        /// the method returns "2 ^ 3 = 8".
        /// If the user enters "4 5" or "4 +" or any incorrect or unbalanced 
        /// expression, then the method returns "Invalid expression".
        /// </example>
        public static string BasicArithmetic()
        {
            Console.WriteLine();
            Console.WriteLine("Enter an expression (Reverse Polish Notation)");
            string expression = Console.ReadLine().Trim();
            string[] elements = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            char[] input = string.Join(string.Empty, elements).ToCharArray();

            List<char> operators = new List<char>();
            List<int> numbers = new List<int>();
            StringBuilder output = new StringBuilder();


            foreach (char element in input)
            {
                if (Char.IsDigit(element))
                    numbers.Add(element - 48);  //accounts for ASCII table values
                else
                    operators.Add(element);
            }

            char[] operatorArray = operators.ToArray();
            int[] numberArray = numbers.ToArray();

            for (int index1 = 0; index1 < operatorArray.Length; index1++)
            {
                if (operatorArray[index1] == '+' || operatorArray[index1] == '-' || operatorArray[index1] == '/' || operatorArray[index1] == '*')
                {

                }
                else
                {
                    throw new ArgumentException("The operator entered is not valid!");
                }
            }


            for (int count = 0; count <= operatorArray.Length; count++)
            {
                output.Append("( ");
            }

            output.Append($"{numberArray[0]} ");

            for (int count = 1; count < numberArray.Length; count++)
            {
                output.Append($"{operatorArray[count - 1]} ");
                output.Append($"{numberArray[count]} ");
                output.Append(") ");
            }

            double result = numberArray[0];

            for (int index = 0; index < operatorArray.Length; index++)
            {
                if (operatorArray[index] == '+')
                    result += numberArray[index + 1];
                else if (operatorArray[index] == '-')
                    result -= numberArray[index + 1];
                else if (operatorArray[index] == '*')
                    result *= numberArray[index + 1];
                else if (operatorArray[index] == '/')
                    result /= numberArray[index + 1];
            }

            string outputString = output.ToString();
            Console.Write($"{outputString}");
            Console.WriteLine($"= {result}");

            return outputString;

        }
    }
}
