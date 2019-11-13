//MP2 Calculator 
//This file contains the CalculusCalculator class.

//You should implement the requesed methods.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



namespace MP2
{
    public class CalculusCalculator
    {
        string polynomial = string.Empty;
        List<double> coefficientList = new List<double>();

        public CalculusCalculator(string input)
        {
            SetPolynomialHelper(input);
        }
        public CalculusCalculator()
        {
            //SetPolynomial();
        }

        public bool SetPolynomialHelper(string input)
        {
            polynomial = input;
            if (IsValidPolynomial(polynomial))
            { 
                string[] coefficients = polynomial.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                polynomial = coefficients.ToString();
                int coefficientCount = coefficients.Length;

                double[] CoefficientArray = new double[coefficientCount];

                for (int i = 0; i < coefficientCount; i++)
                {
                    CoefficientArray[i] = double.Parse(coefficients[i]);
                }

                coefficientList = CoefficientArray.ToList();

                return true;
            }

            else
            {
                return false;
            }  

        }


        /// <summary>
        /// Prompts the user for the coefficients of a polynomial, and sets the 
        /// polynomial field and the coefficientList field of the object.
        /// It must use the isValidPolynomial method to check for the validity
        /// of the polynomial entered by the user, otherwise the fields must 
        /// not change.
        /// The acceptable format of the coefficients received from the user is 
        /// a series of numbers (one for each coefficient) separated by spaces. 
        /// All coefficients values must be entered even those that are zero.
        /// </summary>
        /// <returns>True if the polynomial is succeffully set, false otherwise.</returns>
        public bool SetPolynomial()
        {
            Console.WriteLine("Please enter the polynomial function: ");
            polynomial = Console.ReadLine().Trim();
            string[] coefficients = polynomial.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            polynomial = coefficients.ToString();

            if (IsValidPolynomial(polynomial))
            {
                
                /*
                int coefficientCount = polynomial.Length;

                double[] CoefficientArray = new double[coefficientCount];

                for (int i = 0; i < coefficientCount; i++)
                {
                    CoefficientArray[i] = double.Parse(coefficients[i]);

                }*/
                
                foreach (string element in coefficients)
                {
                    coefficientList.Add(double.Parse(element));
                }

                //coefficientList = CoefficientArray.ToList();

                return true;
            }

            else
            {
                return false;
            }

        }

        /// <summary>
        /// Checks if the passed polynomial string is valid.
        /// The acceptable format of the coefficient string is a series of 
        /// numbers (one for each coefficient) separated by spaces. 
        /// </summary>
        /// <example>
        /// Examples of valid strings: "2   3.5 0  ", or "-2 -3.5 0 0"
        /// Examples of invalid strings: "3..5", or "2x^2+1", or "a b c", or "3 - 5"
        /// </example>
        /// <param name="polynomial">
        /// A string containing the coefficient of a polynomial. Index 0 is the
        /// highest order, and all coefficients exist (even 0's).
        /// </param>
        /// <returns>True if a valid polynomial, false otherwise.</returns>
        public bool IsValidPolynomial(string polynomial)
        {
            string[] coefficients = polynomial.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int coefficientCount = coefficients.Length;
            try
            {
                for (int index1 = 0; index1 < coefficientCount; index1++)
                {
                  double temp=  double.Parse(coefficients[index1]);
                    Console.WriteLine(temp);
                }
            }
            catch (FormatException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Returns a string representing this polynomial.
        /// </summary>
        /// <returns>
        /// A string containing the polynomial in the format:
        /// (a_n)*x^n + (a_n_1)*x^n_1 + ... + (a1)*x + (a0) 
        /// It does not display the term of any coefficient that is 0.
        /// If all coefficients are 0, then it returns "0".
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public string GetPolynomialString()
        {
            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }
            else
            {
                
                double[] coefficientArray = coefficientList.ToArray();

                /*int count = 0;
                for (int index = 0; index < coefficientArray.Length; index++)
                {
                    if (coefficientArray[index] == 0)
                    {
                        count++;
                    }
                }
                if (count == coefficientArray.Length)
                {
                    return " ";
                }*/

                int power = coefficientArray.Length - 1;

                StringBuilder polynomialEqn = new StringBuilder();

                for (int index = 0; index < coefficientArray.Length; index++)
                {
                    polynomialEqn.Append($"{coefficientArray[index]}x^{power} + ");

                    power--;
                }

                polynomialEqn.Remove((polynomialEqn.Length - 3), 2);
                return polynomialEqn.ToString();

            }
        }

        /// <summary>
        /// Evaluates this polynomial at the x passed to the method.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial.</param>
        /// <returns>The result of the polynomial evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomial(double x)
        {
            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }
            else
            {
                double[] coefficientArray1 = coefficientList.ToArray();
                double solution = 0;
                int power = coefficientArray1.Length - 1;

                for (int index = 0; index < coefficientArray1.Length; index++)
                {
                    solution += (coefficientArray1[index]) * (Math.Pow(x, power));
                    power--;
                }

                return solution;
            }
        }

        /// <summary>
        /// Finds a root of this polynomial using the provided guess.
        /// </summary>
        /// <param name="guess">The initial value for the Newton method.</param>
        /// <param name="epsilon">The desired accuracy: stops when |f(result)| is
        /// less than or equal epsilon.</param>
        /// <param name="iterationMax">A max cap on the number of iterations in the
        /// Newton-Raphson method. This is to also guarantee no infinite loops.
        /// If this iterationMax is reached, a double.NaN is returned.</param>
        /// <returns>
        /// The root found using the Netwon-Raphson method. 
        /// A double.NaN is returned if a root cannot be found.
        /// The return value is rounded to have 4 digits after the decimal point.
        /// </returns>
        public double NewtonRaphson(double guess, double epsilon, int iterationMax)
        {
            int count = 0;
            double x = guess;

            while (Math.Abs(EvaluatePolynomial(x)) > epsilon && count < iterationMax)
            {
                x -= (EvaluatePolynomial(x) / EvaluatePolynomialDerivative(x));
                count++;
            }

            if (count == iterationMax)
            {
                return double.NaN;
            }

            return Math.Round(x, 4); //4 decimal places
        }

        /// <summary>
        /// Calculates and returns all unique real roots of this polynomial 
        /// that can be found using the NewtonRaphson method. 
        /// The method uses all initial guesses between -50 and 50 with 
        /// steps of 0.5 to find all unique roots it can find. 
        /// A root is considered unique, if there is no root already found 
        /// that is within our desired accuracy level.
        /// Uses 10 as the max number of iterations used by Newton-Raphson method.
        /// </summary>
        /// <param name="epsilon">The desired accuracy.</param>
        /// <returns>A list containing all the unique roots that the method finds.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty. 
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public List<double> GetAllRoots(double epsilon)
        {
            if (epsilon <= 0)
            {
                throw new ArgumentException("Invalid parameter for epsilon");
            }

            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set");

            }
            List<double> rootList = new List<double>();

            for (double x = -50.0; x <= 50.0; x += 0.5)
            {
                if ((NewtonRaphson(x, epsilon, 10) != double.NaN) && !(rootList.Contains(NewtonRaphson(x, epsilon, 10))))
                {
                    rootList.Add(NewtonRaphson(x, epsilon, 10));

                }
            }

            return rootList;
        }

        /// <summary>
        /// Evaluates the 1st derivative of this polynomial at x, passed to the method.
        /// The method uses the exact numerical technique, since it is easy to derive the 
        /// derivative of a polynomial.
        /// </summary>
        /// <param name="x">The x at which we are evaluating the polynomial derivative.</param>
        /// <returns>The result of the polynomial derivative evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialDerivative(double x)
        {
            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }
            else
            {
                double[] coefficientArray2 = coefficientList.ToArray();

                int originalPower = coefficientArray2.Length - 1;

                for (int index = 0; index < coefficientArray2.Length; index++)
                {
                    coefficientArray2[index] *= originalPower;
                    originalPower--;

                }

                int power = coefficientArray2.Length - 2;
                double derivative = 0;

                for (int index = 0; index < coefficientArray2.Length; index++)
                {
                    derivative += coefficientArray2[index] * Math.Pow(x, power);
                    power--;
                }

                return derivative;
                
            }
        }

        /// <summary>
        /// Evaluates the definite integral of this polynomial from a to b.
        /// The method uses the exact numerical technique, since it is easy to derive the 
        /// indefinite integral of a polynomial.
        /// </summary>
        /// <param name="a">The lower limit of the integral.</param>
        /// <param name="b">The upper limit of the integral.</param>
        /// <returns>The result of the integral evaluation.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the polynomial field is empty.
        /// Exception message used: "No polynomial is set."
        /// </exception>
        public double EvaluatePolynomialIntegral(double a, double b)
        {
            if (coefficientList.Count == 0)
            {
                throw new InvalidOperationException("No polynomial is set.");
            }
            else
            {
                double[] coefficientArray3 = coefficientList.ToArray();

                double originalPower = coefficientArray3.Length - 1;

                for (int index = 0; index < coefficientArray3.Length - 1; index++)
                {
                    coefficientArray3[index] /= (originalPower + 1);
                    originalPower--;
                }

                int power = coefficientArray3.Length;
                double integralA = 0;
                double integralB = 0;
                double integral;

                if (a == 0)
                {
                    integralA = 0;
                }
                else
                {
                    for (int index2 = 0; index2 < coefficientArray3.Length; index2++)
                    {
                        integralA += (coefficientArray3[index2] * Math.Pow(a, power));
                        power--;
                    }
                }

                if (b == 0)
                {
                    integralB = 0;
                }
                else
                {
                    for (int index1 = 0; index1 < coefficientArray3.Length; index1++)
                    {
                        integralB += coefficientArray3[index1] * Math.Pow(b, power);
                        power--;
                    }
                }

                integral = integralB - integralA;
                return integral;
            }
        }
    }
}

