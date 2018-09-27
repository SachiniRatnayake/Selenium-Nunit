using Keys_Onboarding.Global;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using RelevantCodes.ExtentReports;
using System;
using System.Threading;
using static Keys_Onboarding.Global.CommonMethods;


namespace Keys_Onboarding
{
    public class PropertyOwner
    {
        //Create a constructor for the class
        public PropertyOwner()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElements Definition
        //Define Owners tab
        [FindsBy(How =How.XPath,Using = "html/body/div[1]/div/div[2]/div[1]")]
        private IWebElement Ownertab { set; get; }
        
        //Define Properties page
        [FindsBy(How = How.XPath, Using = "html/body/div[1]/div/div[2]/div[1]/div/a[1]")]
        private IWebElement PropertiesPage { set; get; }

        //Define search bar        
        [FindsBy(How = How.XPath, Using = "//input[@id='SearchBox']")]
        private IWebElement SearchBar { set; get; }

        //Define search button
        [FindsBy(How = How.XPath, Using = "//*[@id='icon-submitt']")]
        private IWebElement SearchButton { set; get; }

        //Define 'List a Rental' button
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/div[1]/div/div[2]/div/div[2]/a[1]")]
        private IWebElement ListRentalButton { set; get; }

        //Define 'Add Tenant' button
        [FindsBy(How = How.XPath, Using = ".//*[@id='main-content']/section/div[1]/div/div[3]/div/div/div/div/div[2]/div[2]/div/a[1]")]
        private IWebElement AddTenantButton { set; get; }

        //Define 'Add New Property' task
        [FindsBy(How = How.XPath, Using = "//a[contains(@href,'/PropertyOwners/Property/AddNewProperty')]")]
        private IWebElement AddNewPropertyButton { set; get; }

        //Define Options Icon for a property
        [FindsBy(How = How.XPath, Using = ".//*[@id='main-content']/section/div[1]/div/div[3]/div/div[1]/div/div/div[2]/div[1]/div[3]/div")]
        private IWebElement PropertyOptions { set; get; }

        //Define Manage Tenants link for a property
        [FindsBy(How = How.XPath, Using = "//a[contains(.,'Manage Tenant')]")]
        private IWebElement ManageTenantsLink { set; get; }

        //Define the Quick Links pop up skip button
        [FindsBy(How = How.XPath, Using = "html/body/div[5]/div/div[5]/a[1]")]
        private IWebElement QuickLinksSkipButton { set; get; }
        #endregion

        //Common methods to navigate into the 'My Properties' page
        public void Common_methods()
        {
            //Skipping the actions on the Quick Links Window
            Global.Driver.wait(5);
            QuickLinksSkipButton.Click();

           Thread.Sleep(1000);

            //Click on the Owners tab
            Global.Driver.wait(10);
            Ownertab.Click();

            //Select properties page   
            PropertiesPage.Click();
        }

        internal void SearchAProperty()
        {
            try
            {
                //Calling the common methods
                Common_methods();
                Driver.wait(5);

                //Enter the value in the search bar
                SearchBar.SendKeys("TestingProperty");
                Global.Driver.wait(5);

                //Click on the search button
                SearchButton.Click();
                Driver.wait(5);

                string ExpectedValue = "TestingProperty";
                string ActualValue = Global.Driver.driver.FindElement(By.XPath("//*[@id='mainPage']/div[4]/div[1]/div/div/div[2]/div[2]/div[1]/div[1]/div[1]")).Text;

                //Assert.AreEqual(ExpectedValue, ActualValue);
                if (ExpectedValue == ActualValue)
                                    
                    Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed, Search successfull");
                
                else
                    Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Search Unsuccessfull");

            }

            catch(Exception e)
            {
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed, Search Unsuccessfull",e.Message);
            }
         }


        //A method to navigate to the 'List Rental Property' page  
        internal void ClickListARental()
        {
                //Click on the list a rental button to navigate to the List Rental Property Page
                ListRentalButton.Click();                
        }


        //A method to navigate into the property details page  
        internal void ClickAddNewProperty()
        {            
            //Click on the Add New Property button to navigate to the Property Details Page
            AddNewPropertyButton.Click();      
        }


        //A method to navigate into the 'Add Tenant' page for a given property 
        internal void ClickAddTenant(string PropertyName)
        {
            //Search for the property using the title
            SearchForProperty(PropertyName);

            //Click on the Add Tenant button to navigate to the 'Add Tenant' page for the given property
            AddTenantButton.Click();
        }


        //A method to verify that a specific property is displayed in the properties page
        internal void VerifyNewProperty(int TestDataSetNumber)
        {
            //Set the expected value using the test data
            string ExpectedPropertyName = ExcelLib.ReadData(TestDataSetNumber, "Property Name");

            //Search for the expected property using the title
            SearchForProperty(ExpectedPropertyName);

            //Check if the actual results matches the expected results
            string ListedPropertyName;
            string ListingFound = "InProgress";
            int LoopCount = 1;
            try
            {
                //Loop through all the returned values if the Property Title field is not unique
                while (true)
                {
                    //Retrieve the actual displayed title values in the properties page
                    ListedPropertyName = Driver.driver.FindElement(By.XPath(".//*[@id='main-content']/section/div[1]/div/div[3]/div/div[" + LoopCount + "]/div/div/div[2]/div[1]/div[1]/a/h3")).Text;

                    //Check the returned values with the expected values 
                    if (ExpectedPropertyName == ListedPropertyName)
                    {
                        ListingFound = "Valid";
                        ///* Further tests to be added here to test for the specific item if the Property Title is not a unique field  *///
                    }
                    else
                    {
                        //If the search returns any item with a wrong title the search result is invalid
                        ListingFound = "Invalid";
                        break;
                    }
                    LoopCount++;
                }
            }
            catch (Exception e)
            {
                string InfoMsg = e.Message;
            }

            //Log the test results
            if (ListingFound == "Valid")
            {
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed. New Property is Displayed");
                // Save screenshot
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Add New Property - Verification Report - Pass");
               // Base.test.Log(LogStatus.Info, "Image example: " + img);
            }
            else
            {
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed. New Property is Not Displayed");
                // Save screenshot
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Add New Property - Verification Report - Fail");
               // Base.test.Log(LogStatus.Info, "Image example: " + img);
            }
        }


        //A method to navigate to the 'Tenants In Property' page for a specified property
        internal void ClickManageTenant(string PropertyName)
        {
            //Search for the expected property using the title
            SearchForProperty(PropertyName);

            //Click on the options icon for the selected property
            PropertyOptions.Click();
            Thread.Sleep(1000);
            ManageTenantsLink.Click();
        }


        //A method to search for a property using a given search string
        internal void SearchForProperty(string SearchString)
        {
            SearchBar.SendKeys(SearchString);
            SearchButton.Click();
        }

    }
}