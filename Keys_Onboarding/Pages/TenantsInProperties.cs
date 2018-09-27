using Keys_Onboarding.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Threading;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    class TenantsInProperties
    {
        //Create a constructor for the class
        public TenantsInProperties()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        // Common methods to navigate into the "Tenants In Property" page
        public void NavigateToPage(string PropertyName)
        {
            //Calling the common methods to navigate into the ''My Properties' page
            PropertyOwner PropertyOwnerObj = new PropertyOwner();
            
            //Click on the 'Add Tenant' button to navigate to the 'Add Tenant' Page
            PropertyOwnerObj.ClickManageTenant(PropertyName);
        }


        //A method to verify that a given tenant is added for a given property
        internal void VerifyTenantInProperty(int TestDataSetNumber)
        {
            //Navigate into the "Tenants In Property" page
            NavigateToPage(ExcelLib.ReadData(TestDataSetNumber, "PropertyName"));
            Thread.Sleep(1000);

            //Set the expected value using the test data
            string ExpectedTenantEmail = ExcelLib.ReadData(TestDataSetNumber, "TenantEmail");

            //Check if the actual results matches the expected results
            string ListedTenantEmail;
            string ListingFound = "InProgress";
            int LoopCount = 1;
            try
            {
                //Loop through all the tenants for the property
                while (true)
                {
                    //Retrieve the actual displayed email in the tenants in properties page
                    ListedTenantEmail = Driver.driver.FindElement(By.XPath(".//*[@id='property-grid']/div[1]/div[" + LoopCount + "]/div/div[2]/div/div[3]/div/span")).Text;

                    //Check the returned values with the expected values 
                    if (ExpectedTenantEmail == ListedTenantEmail)
                    {
                        ListingFound = "Valid";
                        //Break the loop if the added tenant is found
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
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed. Tenant is Displayed");
                // Save screenshot
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Add Tenant - Verification Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
            }
            else
            {
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed. Tenant is Not Displayed");
                // Save screenshot
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Add Tenant - Verification Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
            }
         }
    }
}
