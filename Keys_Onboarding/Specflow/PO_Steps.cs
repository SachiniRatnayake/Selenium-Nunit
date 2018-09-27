using Keys_Onboarding.Config;
using Keys_Onboarding.Global;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using RelevantCodes.ExtentReports;
using System;
using TechTalk.SpecFlow;

namespace Keys_Onboarding
{
    [Binding]
    public class PropertyOwnerSteps : Global.Base
    {
        [Given(@"user have logged into the application")]
        public void GivenUserHaveLoggedIntoTheApplication()
        {
            switch (Browser)
            {

                case 1:
                    Driver.driver = new FirefoxDriver();
                    break;
                case 2:
                    Driver.driver = new ChromeDriver();
                    Driver.driver.Manage().Window.Maximize();
                    break;

            }
            if (Keys_Resource.IsLogin == "true")
            {
                Login loginobj = new Login();
                loginobj.LoginSuccessfull();
            }
            else
            {
                Register obj = new Register();
                obj.Navigateregister();
            }

            extent = new ExtentReports(ReportPath, false, DisplayOrder.NewestFirst);
            extent.LoadConfig(Keys_Resource.ReportXMLPath);
        }
        
        [Then(@"User search results for property are successfull")]
        public void ThenUserSearchResultsForPropertyAreSuccessfull()
        {
            // Creates a toggle for the given test, adds all log events under it    
            test = extent.StartTest("Search for a Property");

            // Create an class and object to call the method
            PropertyOwner obj = new PropertyOwner();
            obj.SearchAProperty();

            //Close the broswer
            Global.Driver.driver.Close();
        }
    }
}
