using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;

namespace ConnNewTopLeap
{
    class Program
    {
        static void Main(string[] args)
        {
            MatchCollection matches;

            var regex = new Regex("(?<=\"可点击更换\">)[A-Z]{1}[A-Z]{1}[A-Z]{1}[A-Z]{1}[A-Z]{1}");

            try
            {

                var option = new ChromeOptions();
                option.AddArgument("disable-infobars");
                IWebDriver webDriver = new ChromeDriver(option);

                webDriver.Navigate().GoToUrl("http://192.168.5.204/OASYS/Login.aspx");
                // 窗口最大化，便于脚本执行
                //webDriver.Manage().Window.Maximize();
                // 根据元素名称清除元素文本
                webDriver.FindElement(By.Name("xhtLoginCtrl$_$txtUserId")).Clear();
                webDriver.FindElement(By.Name("xhtLoginCtrl$_$txtPassWord")).Clear();
                // 根据元素名称设置元素文本（用户名）：这里有多种方式获取元素，大家可以自行尝试
                webDriver.FindElement(By.Name("xhtLoginCtrl$_$txtUserId")).SendKeys("180");
                //Thread.Sleep(1000);
                // 根据元素名称设置元素文本（用户密码）
                webDriver.FindElement(By.Name("xhtLoginCtrl$_$txtPassWord")).SendKeys("123456");
                //Thread.Sleep(1000);
                // 模拟点击登录按钮
                //webDriver.FindElement(By.ClassName("btn-primary")).Click();
                // 这个睡眠需要注意一下，因为网络的影响，有可能会造成tcaptcha_iframe还未加载，这时如果不睡眠直接获取就有可能失败，我在这里被坑了很久，气.......
                //Thread.Sleep(4000);
                string checkCode = string.Empty;
                matches = regex.Matches(webDriver.PageSource);
                Console.WriteLine(webDriver.PageSource);
                if (matches.Count != 0)
                {
                    foreach (Match m in matches)
                    {
                        checkCode = m.Value;
                        Console.WriteLine(m.Value);

                    }
                }
                
                webDriver.FindElement(By.Name("xhtLoginCtrl$_$txtCheckCode")).SendKeys(checkCode);
                webDriver.FindElement(By.Name("xhtLoginCtrl$_$btnEnter")).Click();
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Debug.WriteLine(ex.Message);
            }
        }

       
    }
}
