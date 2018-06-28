using System;
using System.IO;
using System.Reflection;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {

        }

        [Fact]
        public void TestWithChromeDriver() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");
                var link = driver.FindElement(By.Id("valid"));
                var jsToBeExecuted = $"window.scroll(0, {link.Location.Y});";
                ((IJavaScriptExecutor)driver).ExecuteScript(jsToBeExecuted);
                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void AdminAddFight() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("a@a.a");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Fights")));
                clickableElement.Click();

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Add")));
                clickableElement.Click();

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Fighter1")));
                inputElement.SendKeys("test1");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Odds1")));
                inputElement.SendKeys("115");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Fighter2")));
                inputElement.SendKeys("test2");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Odds2")));
                inputElement.SendKeys("-120");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Event")));
                inputElement.SendKeys("test event");

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("body > div > div.row > div > form > div:nth-child(7) > input")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void Logout() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("a@a.a");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("#logoutForm > ul > li:nth-child(2) > button")));
                clickableElement.Click();

            }
        }

        [Fact]
        public void LoginTest() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("m@m.m");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void RankingsViewTest() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("m@m.m");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Rankings")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void BetsViewTest() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("m@m.m");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("My Bets")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void ProfileViewTest() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("m@m.m");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Hello")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void HomeTest() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("m@m.m");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Home")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void BetTest() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {

                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("m@m.m");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Fights")));
                clickableElement.Click();

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("body > div > table > tbody > tr:nth-child(5) > td:nth-child(8) > a")));
                clickableElement.Click();

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("BetAmount")));
                inputElement.SendKeys("100");

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.CssSelector("body > div > div.row > div > form > div:nth-child(12) > input")));
                clickableElement.Click();
            }
        }

        [Fact]
        public void AdminDeleteFight() {
            using (var driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))) {
                driver.Navigate().GoToUrl(@"https://betsyproject.azurewebsites.net/");

                var wait = new WebDriverWait(driver, TimeSpan.FromMinutes(1));
                var inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("UserName")));
                inputElement.SendKeys("a@a.a");

                inputElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("Password")));
                inputElement.SendKeys("P@$$w0rd");

                var clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.Id("valid")));
                clickableElement.Click();

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.PartialLinkText("Fights")));
                clickableElement.Click();

                clickableElement = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath("/html/body/div/table/tbody/tr[5]/td[7]/a[2]")));
                clickableElement.Click();
            }
        }
    }
}
