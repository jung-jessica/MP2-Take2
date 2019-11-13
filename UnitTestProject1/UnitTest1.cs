using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MP2;
using System.Linq;
using System.Text;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {


        //a valid polynomial that returns true
        [TestMethod]
        public void TestMethod1()
        {
            CalculusCalculator calculus = new CalculusCalculator("2 3 0");
            string polynomial = "2 3 0";
            bool result = calculus.IsValidPolynomial(polynomial);
            Assert.AreEqual(true, result);
        }
        
        //a valid polynomial that returns false
        [TestMethod]
        public void TestMethod2()
        {
            CalculusCalculator calculus = new CalculusCalculator("3..5");
            string polynomial = "3.5";
            bool result = calculus.IsValidPolynomial(polynomial);
            Assert.AreEqual(false, result);
        }

        //exception for GetPolynomialString
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestMethod3()
        {
            CalculusCalculator calculus = new CalculusCalculator(" ");
            calculus.GetPolynomialString();
        }

        //normal output 
        [TestMethod]
        public void TestMethod4()
        {
            CalculusCalculator calculus = new CalculusCalculator("2 4 5 ");
            string result = calculus.GetPolynomialString();
            Assert.AreEqual("2x^2 + 4x^1 + 5x^0 ", result);
        }

        //exception for evaluate polynomial 
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestMethod5()
        {
            CalculusCalculator calculus = new CalculusCalculator("");
            calculus.EvaluatePolynomial(2);
        }

        //normal output
        [TestMethod]
        public void TestMethod6()
        {
            CalculusCalculator calculus = new CalculusCalculator("1 2 1 ");
            double result = calculus.EvaluatePolynomial(2);
            Assert.AreEqual(9.0, result, 0.01);
        }

        //returns double.Nan
        [TestMethod]
        public void TestMethod7()
        {
            CalculusCalculator calculus = new CalculusCalculator("1 0 1 ");
            double result = calculus.NewtonRaphson(2, 0.00001, 10);
            Assert.AreEqual(double.NaN, result);
        }

        //returns root
        [TestMethod]
        public void TestMethod8()
        {
            CalculusCalculator calculus = new CalculusCalculator("1 0 -1 ");
            double result = calculus.NewtonRaphson(0.5, 0.001, 10);
            Assert.AreEqual(1.0, result, 0.001);
        }

        //exception when elipson <0
        [TestMethod]
        [ExpectedException(typeof(System.ArgumentException))]
        public void TestMethod9()
        {
            CalculusCalculator calculus = new CalculusCalculator("2 5 6 ");
            calculus.GetAllRoots(-1.0);
        }

        //exception when setpolynomial is false
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestMethod10()
        {
            CalculusCalculator calculus = new CalculusCalculator(" ");
            calculus.GetAllRoots(0.001);
        }

        //normal output
        [TestMethod]
        public void TestMethod11()
        {
            CalculusCalculator calculus = new CalculusCalculator("1 0 -4 ");
            List<double> result = calculus.GetAllRoots(0.001);
            Assert.IsTrue((new List<double> { -2, 2 }).SequenceEqual(result));
        }

        ////exception for derivative
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestMethod12()
        {
            CalculusCalculator calculus = new CalculusCalculator("");
            calculus.EvaluatePolynomialDerivative(3.0);
        }

        //evaluates the derivative
        [TestMethod]
        public void TestMethod13()
        {
            CalculusCalculator calculus = new CalculusCalculator("4 2 ");
            double result = calculus.EvaluatePolynomialDerivative(1.0);
            Assert.AreEqual(4.0, result, 0.01);
        }

        //exception for integral
        [TestMethod]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void TestMethod14()
        {
            CalculusCalculator calculus = new CalculusCalculator(" ");
            calculus.EvaluatePolynomialIntegral(0, 1);
        }

        //evaluates the integral
        [TestMethod]
        public void TestMethod15()
        {
            CalculusCalculator calculus = new CalculusCalculator("2 2 1");
           
          double result = calculus.EvaluatePolynomialIntegral(0, 1);
          Assert.AreEqual(2.667, result, 0.01);
            
        }

    }
}

