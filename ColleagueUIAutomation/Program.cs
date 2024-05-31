using System;
using System.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Interactions;
using System.Threading;
using OpenQA.Selenium.Support.UI;
using SeleniumWebDriverTest;
using OpenQA.Selenium.Chrome;




namespace SeleniumWebDriverTest
{
    class Constants
    {
        public static string filePath = AppDomain.CurrentDomain.BaseDirectory + "/" + "config.ini"; // File path where you want to create the config file
        public static string username = "erpadmin8";
        public static string password = "!.abc123";
        public static string url = "https://miles.colleague.elluciancloud.com/UI/home/index.html?sso=true";
        public static string webpagewait = "10";
        public static string keywait = "3";
        public static string clickwait = "2";
    }
    class Program
    {
       
        static void Main(string[] args)
        {
           
            //System.Environment.SetEnvironmentVariable("webdriver.edge.driver", "C:\\MicrosoftEdgeWebDriver\\edgedriver_win64\\msedgedriver.exe");

            // Create an instance of the EdgeDriver
            IWebDriver driver = new EdgeDriver();

           // IWebDriver driver = new ChromeDriver();
            Actions actions = new Actions(driver);

            // Create an instance of Config file and write the configurable fields

            Config configuration = new Config();
            configuration.runConfig();


            // String ColleagueID = "0249975";

            // Navigate to a website
            driver.Navigate().GoToUrl(Constants.url);

            //Colleague Login Screens
            User_Login_Colleague_UIAutomation(driver, Constants.username, Constants.password);


            // Waiting for Colleague UI to load - 10 seconds
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.webpagewait)));

            // Simulate "Welcome Screen and Click OK - 'O' and Click Enter.
            actions.SendKeys("O").Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

            actions.SendKeys(Keys.Enter);
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

            // Navigate to the NAE Screen
            NAE_Colleague_UIAutomation(driver);

            //File.Close();
            // Close the browser
            driver.Quit();
        }

        public static void User_Login_Colleague_UIAutomation(IWebDriver driver, string username, string password)
        {
            //Create an instance of the Actions class
            Actions actions = new Actions(driver);

            // Get rid of Microsoft Edge Help screen
            // Simulate pressing the "Esc" key
            actions.SendKeys(Keys.Escape).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));


            /***************************************************************************/
            /* Enter Username and Password - Currently Hard Coded*/
            //TO DO: fetch the username and password from a Config File.

            IWebElement Usernameelement = driver.FindElement(By.Name("username"));
            Usernameelement.SendKeys(username);
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

            // Simulate pressing the "Esc" key
            actions.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

            // Find an element by its ID and interact with it
            IWebElement Pwdelement = driver.FindElement(By.Name("password"));
            Pwdelement.SendKeys(password);
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

            /*************************************************************************/
            /* Screen 2 : Begin Setup Authentication Screen                          */
            /* Action : Press the "Skip to Continue" - Esc + Tab + Tab + Enter       */
            /*************************************************************************/

            
            // Simulate pressing the "Esc" key
            actions.SendKeys(Keys.Escape).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));
            actions.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

            // Simulate pressing the "Tab" key
            actions.SendKeys(Keys.Tab).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));
            // Simulate pressing the "Tab" key
            actions.SendKeys(Keys.Tab).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));
            // Simulate pressing the "Enter" key
            actions.SendKeys(Keys.Enter).Perform();
            Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

            // Set up an explicit wait with a timeout of, for example, 10 seconds
            // WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            /*  IWebElement dialog = wait.Until(driver =>
              {
                  IWebElement button = driver.FindElement(By.Id("btnFormSearch"));
              return button.Displayed ? button : null;
              });*/


            // Display a dialog indicating waiting for user input
            Console.WriteLine("Waiting for user input...");
            Console.ReadLine();

        }


        public static void NAE_Colleague_UIAutomation(IWebDriver driver)
        {
            try
            {
                string emailtypestring = "/0";
                string emailaddress_string = "/0";
                // string linkemail_string = "/0";

                //Create an instance of the Actions class
                Actions actions = new Actions(driver);

                /***************************************************************************/
                /* Click on the Form Search Button                                        */
                /***************************************************************************/

                IWebElement FormButtonElement = driver.FindElement(By.Id("btnFormSearch"));
                FormButtonElement.Click();
                Thread.Sleep(TimeSpan.FromSeconds(3));

                /***************************************************************************/
                /* Capture on the Form Search and Navigate to NAE                          */
                /***************************************************************************/

                IWebElement FormSearchElement = driver.FindElement(By.Id("form-search"));
                FormSearchElement.SendKeys("NAE");
                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.clickwait)));
                // Simulate pressing the "Esc" key
                actions.SendKeys(Keys.Enter).Perform();
                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

                /***************************************************************************/
                /* To Do: Read the Colleague ID file, put the id's in a list and iterate through the list performing the below steps */
                /***************************************************************************/

                /***************************************************************************/
                /* Person Lookup   
                 * To Do: Currently hard-coded the Colleague ID*/
                /***************************************************************************/

                IWebElement PersonLookupElement = driver.FindElement(By.Id("popup-lookup"));
                PersonLookupElement.SendKeys("0249975");
                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.keywait)));

                IWebElement PersonLookupOKElement = driver.FindElement(By.Id("popup_lookup_button_0"));
                PersonLookupOKElement.Click();
                Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.clickwait)));

                /***************************************************************************/
                /* Email Lookup : Search for INT and save it in a global variable.         */
                /***************************************************************************/
                /* To do: 
                 * While btnWindowControlNext is enabled 
                 * Capture the type - If type is Int - capture the email.
                 * otherwise click the btnWindowControlNext
                 **************************************************************************/


                IWebElement PersonEmailAddressElement = driver.FindElement(By.Id("PERSON-EMAIL-ADDRESSES_1"));
                PersonEmailAddressElement.Click();
                PersonEmailAddressElement.Clear();

                IWebElement PersonEmailTypeElement = driver.FindElement(By.Id("PERSON-EMAIL-TYPES_1"));

                bool multi_page_flag = true;

                IWebElement btnWindowControlNextElement = driver.FindElement(By.Id("btnWindowControlNext"));
                while (multi_page_flag)
                {
                    if (btnWindowControlNextElement.Enabled)
                    {
                        PersonEmailTypeElement = driver.FindElement(By.Id("PERSON-EMAIL-TYPES_1"));
                        PersonEmailTypeElement.Click();
                        Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.clickwait)));
                        emailtypestring = PersonEmailTypeElement.GetAttribute("value");
                        Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.clickwait)));
                        Console.WriteLine(String.Format("Type is {0}", emailtypestring));

                        // if (string.Compare(emailtypestring,"INT") == 0)
                        //{
                        PersonEmailAddressElement = driver.FindElement(By.Id("PERSON-EMAIL-ADDRESSES_1"));
                        PersonEmailAddressElement.Click();
                        Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.clickwait)));
                        emailaddress_string = PersonEmailAddressElement.GetAttribute("value");
                        Thread.Sleep(TimeSpan.FromSeconds(int.Parse(Constants.clickwait)));
                        Console.WriteLine(String.Format("{0} address is {1}", emailtypestring, emailaddress_string));
                        // }
                        btnWindowControlNextElement.Click();
                        Thread.Sleep(TimeSpan.FromSeconds(1));

                        multi_page_flag = true;
                        PersonEmailTypeElement.Clear();
                        PersonEmailAddressElement.Clear(); ;
                        emailtypestring = "\0";

                    }
                    else
                    {
                        multi_page_flag = false;
                        Console.WriteLine("Button is not enabled.");
                    }
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element does not exist");
            }


        }



    }

}  




