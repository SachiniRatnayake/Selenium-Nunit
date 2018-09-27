using Keys_Onboarding.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Linq;
using System.Collections.Generic;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    public class RentalListingsAndNewApplications
    {
        //Create a constructor for the class
        public RentalListingsAndNewApplications()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region Webelemet Definition
        //Define the Search Input Box
        [FindsBy(How = How.Id, Using = "SearchBox")]
        private IWebElement SearchBox { get; set; }

        //Define the Search Button
        [FindsBy(How = How.Id, Using = "icon-submitt")]
        private IWebElement SearchButton { get; set; }
        #endregion


        //A method to search for a given listing using a given search string
        internal void SearchForListing(string SearchString)
        {
            SearchBox.SendKeys(SearchString);
            SearchButton.Click();

        }

        //A method to verify that a given listing is displayed in Rental Listings
        internal void VerifyReantalListing(int TestDataSetNumber)
        {
            //Set the expected value using the test data
            string ExpectedTitle = ExcelLib.ReadData(TestDataSetNumber, "Title");

            //Search for the expected listing using the title
            SearchForListing(ExpectedTitle);

            //Check if the actual results matches the expected results
            string ListedTitle;
            string ListingFound = "InProgress";
            int LoopCount = 1;
            try
            {
                //Loop through all the returned values if the Property Title field is not unique
                while (true)
                {
                    //Retrieve the actual displayed title values in the rental listings page
                    ListedTitle = Driver.driver.FindElement(By.XPath(".//*[@id='main-content']/section/div[1]/div[3]/div/div[" + LoopCount + "]/div[2]/div[1]/div[1]/a")).Text;

                    //Check the returned values with the expected values 
                    if (ExpectedTitle == ListedTitle)
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
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Pass, "Test Passed. Rental Listing Displayed");
                // Save screenshot
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Rental Listing - Verification Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
            }
            else
            {
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Fail, "Test Failed. Rental Listing Not Displayed");
                // Save screenshot
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Rental Listing - Verification Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
            }
        }

    }
}

