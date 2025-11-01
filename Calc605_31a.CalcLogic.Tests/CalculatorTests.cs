using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Calc605_31a.CalcLogic.Tests
{
    [TestClass]
    public class CalculatorTests
    {
        [TestMethod]
        public void TestMethod0()
        {
            var calculator = new Calculator();
            Assert.AreEqual("0", calculator.Display);
        }

        [TestMethod]
        public void TestMethodPoltora()
        {
            var calculator = new Calculator();

            calculator.Input("1");

            Assert.AreEqual("1", calculator.Display);
        }

        [TestMethod]
        public void TestMethodPoltoraDva()
        {
            var calculator = new Calculator();

            calculator.Input("2");

            Assert.AreEqual("2", calculator.Display);
        }

        [TestMethod]
        public void Display_ShouldBeResult46_WhenSum12and34()
        {
            var calculator = new Calculator();

            calculator.Input("1");
            calculator.Input("2");
            calculator.Input("+");
            calculator.Input("3");
            calculator.Input("4");
            calculator.Input("=");

            Assert.AreEqual("46", calculator.Display);
        }

        [TestMethod]
        public void TestMethod3()
        {
            var calculator = new Calculator();

            Assert.ThrowsException<Exception>(() => calculator.Input("AbraCadabra"));
        }

        // === Backspace ===
        [TestMethod]
        public void Display_ShouldDeleteLastDigit_WhenBackspacePressed()
        {
            var calculator = new Calculator();

            calculator.Input("1");
            calculator.Input("2");
            calculator.Input("3");
            calculator.Input("←");

            Assert.AreEqual("12", calculator.Display);
        }

        // === Clear ===
        [TestMethod]
        public void Display_ShouldResetToZero_WhenClearPressed()
        {
            var calculator = new Calculator();

            calculator.Input("9");
            calculator.Input("8");
            calculator.Input("C");

            Assert.AreEqual("0", calculator.Display);
        }

        [TestMethod]
        public void Display_ShouldBeZeroAfterCalculationAndClear()
        {
            var calculator = new Calculator();

            calculator.Input("5");
            calculator.Input("+");
            calculator.Input("5");
            calculator.Input("=");
            calculator.Input("C");

            Assert.AreEqual("0", calculator.Display);
        }

        [TestMethod]
        public void Display_ShouldDeleteAllDigitsToZero_WhenMultipleBackspaces()
        {
            var calculator = new Calculator();

            calculator.Input("7");
            calculator.Input("8");
            calculator.Input("9");
            calculator.Input("←");
            calculator.Input("←");
            calculator.Input("←");

            Assert.AreEqual("0", calculator.Display);
        }

        // === ПАМЯТЬ ===

        [TestMethod]
        public void Memory_ShouldStoreValue_WhenMPlusPressed()
        {
            var calculator = new Calculator();

            calculator.Input("5");
            calculator.Input("M+");
            calculator.Input("C");
            calculator.Input("MR");

            Assert.AreEqual("5", calculator.Display);
        }

        [TestMethod]
        public void Memory_ShouldSubtractValue_WhenMMinusPressed()
        {
            var calculator = new Calculator();

            calculator.Input("10");
            calculator.Input("M+");
            calculator.Input("4");
            calculator.Input("M-");
            calculator.Input("C");
            calculator.Input("MR");

            Assert.AreEqual("6", calculator.Display);
        }

        [TestMethod]
        public void Memory_ShouldClear_WhenMCPressed()
        {
            var calculator = new Calculator();

            calculator.Input("9");
            calculator.Input("M+");
            calculator.Input("MC");
            calculator.Input("C");
            calculator.Input("MR");

            Assert.AreEqual("0", calculator.Display);
        }

        [TestMethod]
        public void Memory_ShouldRecallCorrectValue_WhenMRPressed()
        {
            var calculator = new Calculator();

            calculator.Input("3");
            calculator.Input("M+");
            calculator.Input("C");
            calculator.Input("MR");

            Assert.AreEqual("3", calculator.Display);
        }

        [TestMethod]
        public void Memory_ShouldAccumulateMultipleMPlusCalls()
        {
            var calculator = new Calculator();

            calculator.Input("2");
            calculator.Input("M+");
            calculator.Input("3");
            calculator.Input("M+");
            calculator.Input("C");
            calculator.Input("MR");

            Assert.AreEqual("5", calculator.Display);
        }
    }
}
