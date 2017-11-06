using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CalcUnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        static IWebDriver _driver;

        [ClassInitialize]
        public static void ClassSetUp(TestContext context)
        {
            _driver = new ChromeDriver();

            /* //OperaOptions srv = new OperaOptions();
             //srv.BinaryLocation = @"C:\Program Files\Opera\launcher.exe";
             //driver = new OperaDriver(srv);

             //FirefoxOptions ffOptions = new FirefoxOptions();
             //ffOptions.BrowserExecutableLocation = @"C:\Program Files\Mozilla Firefox\firefox.exe";            
             //driver = new FirefoxDriver(ffOptions);

             //driver = new EdgeDriver();

             //driver = new InternetExplorerDriver();

             //driver = new SafariDriver();

             */
            _driver.Navigate().GoToUrl("http://localhost:88/calc123/calculatorFull.html");
        }

        [ClassCleanup]
        public static void ClassTeardown()
        {
            _driver.Quit();
        }

        [TestInitialize]
        public void SetUp()
        {
            _driver.Navigate().Refresh();
        }

        [DataTestMethod]
        [DataRow("btn1", true)]
        [DataRow("btn4", true)]
        [DataRow("btn7", true)]
        [DataRow("btn0", true)]
        [DataRow("btnEqual", true)]
        public void TestIsPresent(string id, bool exp)
        {
            bool actual = _driver.FindElement(By.Id(id)).Displayed;
            Assert.AreEqual(exp, actual);
        }

        [DataTestMethod]
        [DataRow("0", "0")]
        [DataRow("11", "11")]
        [DataRow("125", "125")]
        public void TestSimpleTest(string key, string exp)
        {
            foreach (var digit in key.ToCharArray())
            {
                _driver.FindElement(By.Id("btn" + digit)).Click();
            }

            string actual = _driver.FindElement(By.Id("aTxt")).GetAttribute("value");
            Assert.AreEqual(exp, actual);
        }

        [DataTestMethod]
        [DataRow("7", "+", "5", "12")]
        [DataRow("9", "-", "5", "4")]
        [DataRow("3", "*", "5", "15")]
        [DataRow("8", "/", "2", "4")]
        public void TestRealJob(string keyA, string keyO, string keyB, string exp)
        {
            foreach (var digit in keyA.ToCharArray())
            {
                _driver.FindElement(By.Id("btn" + digit)).Click();
            }

            var strO = "btn";
            switch (keyO[0])
            {
                case '+': strO += "Plus"; break;
                case '-': strO += "Minus"; break;
                case '/': strO += "Div"; break;
                case '*': strO += "Multi"; break;
            }

            _driver.FindElement(By.Id(strO)).Click();

            foreach (var digit in keyB.ToCharArray())
            {
                _driver.FindElement(By.Id("btn" + digit)).Click();
            }

            _driver.FindElement(By.Id("btnEqual")).Click();

            string actual = _driver.FindElement(By.Id("aTxt")).GetAttribute("value");
            Assert.AreEqual(exp, actual);
        }

        /*[DataTestMethod]
        [DataRow("aTxt", true)]
        [DataRow("bTxt", true)]
        [DataRow("oTxt", true)]
        [DataRow("resTxt", true)]
        [DataRow("btnCalc", true)]
        public void TestIsPresent(string id, bool exp)
        {
            bool actual = _driver.FindElement(By.Id(id)).Displayed;
            Assert.AreEqual(exp, actual);
        }

        [DataTestMethod]
        [DataRow("aTxt", "0", "0")]
        [DataRow("bTxt", "1", "1")]
        [DataRow("oTxt", "+", "+")]
        public void TestSimpleTest(string id, string key, string exp)
        {
            _driver.FindElement(By.Id(id)).SendKeys(key);
            string actual = _driver.FindElement(By.Id(id)).GetAttribute("value");
            Assert.AreEqual(exp, actual);
        }

        [DataTestMethod]
        [DataRow("aTxt", "12345", "12345")]
        [DataRow("bTxt", "12345", "12345")]*/
        //  [DataRow("oTxt", "+-*/", "+-*/")]
        /*  public void TestComplexTest(string id, string key, string exp)
          {
              _driver.FindElement(By.Id(id)).SendKeys(key);
              string actual = _driver.FindElement(By.Id(id)).GetAttribute("value");
              Assert.AreEqual(exp, actual);
          }

          [DataTestMethod]
          [DataRow("12", "+", "5", "17")]
          [DataRow("12", "-", "5", "7")]
          [DataRow("3", "*", "5", "15")]
          [DataRow("8", "/", "2", "4")]
          public void TestRealJob(string keyA, string keyO, string keyB, string exp)
          {
              _driver.FindElement(By.Id("aTxt")).SendKeys(keyA);
              _driver.FindElement(By.Id("oTxt")).SendKeys(keyO);
              _driver.FindElement(By.Id("bTxt")).SendKeys(keyB);
              _driver.FindElement(By.Id("btnCalc")).Click();
              string actual = _driver.FindElement(By.Id("resTxt")).GetAttribute("value");
              Assert.AreEqual(exp, actual);
          }
          */

        //[TestMethod]
        //public void TestJsCalc()
        //{
        //    var script = new TestScript();
        //    script.AppendFile(@"C:\my stuff\ORT courses\Homeworks\jsCalcFields\main.js");
        //    script.AppendBlock(new JsAssertLibrary());

        //    script.RunTest(@"assert.equal(5, calc(2,3,'+'))");
        //}
    }
}
