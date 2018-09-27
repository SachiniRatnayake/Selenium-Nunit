using Keys_Onboarding.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    public class ListRentalProperty
    {
        //Create a constructor for the page
        public ListRentalProperty()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElements Definition
        //Define Select Property Dropdown Menu
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[2]/select")]
        private IWebElement SelectPropertyMenu { set; get; }

        //Define Title Text Box
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[3]/div[1]/input[1]")]
        private IWebElement TitleTextBox { set; get; }

        //Define Description Text Area
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[3]/div[2]/textarea")]
        private IWebElement DescriptionTextArea { set; get; }

        //Define Moving Cost Text Box
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[3]/div[1]/input[2]")]
        private IWebElement MovingCostTextBox { set; get; }

        //Define Target Rent Text Box
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[4]/div[1]/input")]
        private IWebElement TargetRentTextBox { set; get; }

        //Define Furnishing Text Box
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[4]/div[2]/input")]
        private IWebElement FurnishingTextBox { set; get; }

        //Define Available Date Date Picker
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[5]/div[1]/input")]
        private IWebElement AvailableDateDatePicker { set; get; }

        //Define Ideal Tenant Text Box
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[5]/div[2]/input")]
        private IWebElement IdealTenantTextBox { set; get; }

        //Define Occupants Count Text Box
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[6]/div[1]/input")]
        private IWebElement OccupantsCountTextBox { set; get; }

        //Define Pets Allowed Menu
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[6]/div[2]/select")]
        private IWebElement PetsAllowedMenu { set; get; }

        //Define Choose Files Button
        [FindsBy(How = How.XPath, Using = "//*[@id='file-upload']")]
        private IWebElement ChooseFileButton { set; get; }

        //Define Save Button
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[8]/div/button[1]")]
        private IWebElement SaveButton { set; get; }

        //Define Cancel Button
        [FindsBy(How = How.XPath, Using = "//*[@id='main-content']/section/form/fieldset/div[8]/div/button[2]")]
        private IWebElement CancelButton { set; get; }

        #endregion


        // Common methods to navigate into the "List Rental Property" page
        public void NavigateToPage()
        {
            //Calling the common methods to navigate into the my properties page
            PropertyOwner PropertyOwnerObj = new PropertyOwner();
            PropertyOwnerObj.Common_methods();

            //Click on the List A Rental button to navigate to the 'List Rental Property' Page
            PropertyOwnerObj.ClickListARental();
        }


        //Method to create a new rental listing for a property
        internal bool SaveRentalListing(int TestDataSet)
        {
            //Navigate into the "List Rental Property" page
            NavigateToPage();

            try
            {
                //Select the property to be listed
                SelectElement SelectedProperty = new SelectElement(SelectPropertyMenu);
                //SelectedProperty.SelectByIndex(Convert.ToInt32(ExcelLib.ReadData(TestDataSet, "Property Index")));
                SelectedProperty.SelectByText(ExcelLib.ReadData(TestDataSet, "Address"));

                //Enter test data for the listing
                TitleTextBox.SendKeys(ExcelLib.ReadData(TestDataSet, "Title"));
                DescriptionTextArea.SendKeys(ExcelLib.ReadData(TestDataSet, "Description"));
                MovingCostTextBox.SendKeys(ExcelLib.ReadData(TestDataSet, "Moving Cost"));
                TargetRentTextBox.SendKeys(ExcelLib.ReadData(TestDataSet, "Target Rent"));
                AvailableDateDatePicker.SendKeys(ExcelLib.ReadData(TestDataSet, "Available Date"));
                OccupantsCountTextBox.SendKeys(ExcelLib.ReadData(TestDataSet, "Occupants Count"));

                //Save the new listing
                SaveButton.Click();
                //Confirm the action of saving a new listing
                Driver.driver.SwitchTo().Alert().Accept();
                return true;
            }
            catch (Exception e)
            {
                // Log the error details in the report
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Error, "Error occured when listing a property for rental: " + e.Message.ToString());
                // Save Screenshot to display the error
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Exception Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
                return false;
            }
        }
    }
}
