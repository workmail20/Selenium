using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;

namespace selenium
{
    public partial class Form1 : Form
    {
        IWebDriver driver;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            /*
            driver.Navigate().GoToUrl("http://google.com");

            IWebElement SearchInput = driver.FindElement(By.Name("q"));
            SearchInput.SendKeys("последние фильмы" + OpenQA.Selenium.Keys.Enter);
            */
        }

        private void button2_Click(object sender, EventArgs e)
        {
            driver.Quit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            driver.Navigate().GoToUrl("https://yahoo.com");

            IWebElement element = driver.FindElement(By.PartialLinkText("Finance"));
            element.Click();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            driver.Navigate().GoToUrl("https://youtube.com");
            List<IWebElement> News = driver.FindElements(By.CssSelector("#contents h3 a")).ToList();

            for (int i = 0; i < News.Count; i++)
            {
                textBox1.AppendText(News[i].GetAttribute("aria-label")+ "\r\n------------------------\r\n");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            driver.Navigate().GoToUrl("https://youtube.com");
            List<IWebElement> News = driver.FindElements(By.CssSelector("#contents h3 a")).ToList();

            String s;
            for (int i = 0; i < News.Count; i++)
            {
                s = News[i].GetAttribute("aria-label");

                if (s.StartsWith("М"))
                {
                    textBox1.AppendText("Видео № " +(i+1).ToString()+ " начинается с текста 'М'" + "\r\n------------------------\r\n");
                }
                if (s.EndsWith("!"))
                {
                    textBox1.AppendText("Видео № " + (i + 1).ToString() + " заканчивается текстом '!'" + "\r\n------------------------\r\n");

                }
                if (s.Contains("серия"))
                {
                    textBox1.AppendText("Видео № " + (i + 1).ToString() + " содержит текст 'серия'" + "\r\n------------------------\r\n");
                    News[i].Click();
                    break;
                }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            IJavaScriptExecutor jse = driver as IJavaScriptExecutor;
            jse.ExecuteScript(textBox2.Text);
        }

        private string FindWindow(String Url)
        {
            String StartWindow = driver.CurrentWindowHandle;
            String Result = "";
            for(int i=0; i < driver.WindowHandles.Count; i++)
            {
                if (driver.WindowHandles[i] != StartWindow)
                {
                    driver.SwitchTo().Window(driver.WindowHandles[i]);
                    if (driver.Url.Contains(Url))
                    {
                        Result = driver.WindowHandles[i];
                        break;
                    }
                }
            }
            driver.SwitchTo().Window(StartWindow);
            return Result;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            IJavaScriptExecutor jse = driver as IJavaScriptExecutor;

            driver.Navigate().GoToUrl("https://youtube.com");
            jse.ExecuteScript("window.open()");
            driver.SwitchTo().Window(driver.WindowHandles[1]);

            driver.Navigate().GoToUrl("https://google.com");
            jse.ExecuteScript("window.open()");
            driver.SwitchTo().Window(driver.WindowHandles[2]);

            driver.Navigate().GoToUrl("https://yahoo.com");

            driver.SwitchTo().Window(driver.WindowHandles[0]);
            System.Windows.Forms.MessageBox.Show(driver.Title + "\r\n" + driver.Url);

            driver.SwitchTo().Window(driver.WindowHandles[1]);
            System.Windows.Forms.MessageBox.Show(driver.Title + "\r\n" + driver.Url);

            driver.SwitchTo().Window(driver.WindowHandles[2]);
            System.Windows.Forms.MessageBox.Show(driver.Title + "\r\n" + driver.Url);

            String TabWindow = FindWindow("youtube");
            driver.SwitchTo().Window(TabWindow);
            System.Windows.Forms.MessageBox.Show(driver.Title + "\r\n" + driver.Url);




        }

        private void button8_Click(object sender, EventArgs e)
        {
            driver.Navigate().GoToUrl("https://youtube.com");

            IWebElement input = driver.FindElement(By.CssSelector("#search"));

            input.SendKeys("ТЕХНОЛОГИИ КОТОРЫЕ ИЗМЕНЯТ МИР" + OpenQA.Selenium.Keys.Enter);
            WebDriverWait ww = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            IWebElement txt = ww.Until(ExpectedConditions.ElementExists(By.CssSelector("#description-text")));
            textBox1.Text=(txt.Text);
        }
    }
}
