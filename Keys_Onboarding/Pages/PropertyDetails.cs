using Keys_Onboarding.Config;
using Keys_Onboarding.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Threading;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    public class PropertyDetails
    {
        //Create a constructor for the class
        public PropertyDetails()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }


        //Web element definitions in the Property Details Section
        #region 
        // Setting the 'Property Name' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  Enter property name']")]
        private IWebElement PropertyName { get; set; }

        //Setting the Search Address
        [FindsBy(How = How.XPath, Using = "//input[@id='autocomplete']")]
        private IWebElement SearchAddress { get; set; }

        // Setting the Description Field
        [FindsBy(How = How.XPath, Using = "//textarea[@class='add-prop-desc']")]
        private IWebElement Description { get; set; }

        // Setting the Number Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  Number']")]
        private IWebElement Number { get; set; }
    
        // Setting the Street Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  Street']")]
        private IWebElement Street { get; set; }

        // Setting the city Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  City']")]
        private IWebElement City { get; set; }

        // Setting the Postcode Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='PostCode']")]
        private IWebElement Postcode { get; set; }

        // Setting the Region Field
        [FindsBy(How = How.XPath, Using = "//input[@id='region']")]
        private IWebElement Region { get; set; }

        // Setting the Target Rent Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Rent Amount']")]
        private IWebElement TargetRent { get; set; }

        // Setting the Bedrooms Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Number of Bedrooms']")]
        private IWebElement Bedrooms { get; set; }
    
        // Setting the Bathrooms Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Number of Bathrooms']")]
        private IWebElement Bathrooms { get; set; }

        // Setting the Carparks Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Number of car parks']")]
        private IWebElement Carparks { get; set; }

        // Setting the YearBuilt Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Year Built']")]
        private IWebElement YearBuilt { get; set; }

        // Setting the File Upload Button
        [FindsBy(How = How.XPath, Using = ".//*[@id='file-upload']")]
        private IWebElement FileUploadButton { get; set; }
        
        // Setting the Property Section's Next Button
        [FindsBy(How = How.XPath, Using = ".//*[@id='property-details']/div[10]/div/button[1]")]
        private IWebElement PropertyNextButton { get; set; }
        #endregion

        //Web element definitions in the Finance Details Section
        #region
        // Setting the 'Purchase Price' Field
        [FindsBy(How = How.XPath, Using = "//input[@name='purchasePrice']")]
        private IWebElement PurchasePrice { get; set; }

        // Setting the 'Mortgage' Field
        [FindsBy(How = How.XPath, Using = "//input[@name='mortgagePrice']")]
        private IWebElement Mortgage { get; set; }

        // Setting the 'Home Value' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  Enter Home Value']")]
        private IWebElement HomeValue { get; set; }

        // Setting the Finance Page's Next Button
        [FindsBy(How = How.XPath, Using = ".//*[@id='financeSection']/div[8]/button[3]")]
        private IWebElement FinanceNextButton { get; set; }
        #endregion
        
        //Web element definitions in the Tenant Details Section
        #region
        // Setting the 'TenantEmail' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  Enter Tenant Email']")]
        private IWebElement TenantEmail { get; set; }

        // Setting the 'First Name' Field
        [FindsBy(How = How.XPath, Using = "//input[@id='fname']")]
        private IWebElement FirstName { get; set; }

        // Setting the 'Last Name' Field
        [FindsBy(How = How.XPath, Using = "//input[@id='lname']")]
        private IWebElement LastName { get; set; }

        // Setting the 'Start Date' Field
        [FindsBy(How = How.XPath, Using = "//input[@id='sdate']")]
        private IWebElement StartDate { get; set; }

        // Setting the 'End Date' Field
        [FindsBy(How = How.XPath, Using = "//input[@id='edate']")]
        private IWebElement EndDate { get; set; }

        // Setting the 'Rent Amount' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  Enter Rent Amount']")]
        private IWebElement RentAmount { get; set; }

        // Setting the 'PaymentStartDate' Field
        [FindsBy(How = How.XPath, Using = "//input[@id='psdate']")]
        private IWebElement PaymentStartDate { get; set; }

        // Setting the Save Button
        [FindsBy(How = How.XPath, Using = ".//*[@id='saveProperty']")]
        private IWebElement SaveButton { get; set; }
        #endregion


        // Common methods to navigate into the "Property Details" page
        public void NavigateToPage()
        {
            //Calling the common methods to navigate into the properties page
            PropertyOwner PropertyOwnerObj = new PropertyOwner();
            PropertyOwnerObj.Common_methods();

            //Click on the Add New Property button to navigate to the Property Details Page
            PropertyOwnerObj.ClickAddNewProperty();
        }

        //A method to create a new property with a given data set
        internal bool AddNewProperty(int TestDataSet)
        {
            //Navigate into the "Property Details" page
            NavigateToPage();

            // A variable to indicate the status of the completion of the steps
            bool TestStatus = false;

            TestStatus = SavePropertyDetails(TestDataSet);
            //Proceed to the next sections only if the previous sections were successfully completed.
            if (TestStatus == true)
            {
                TestStatus = SaveFinanceDetails(TestDataSet);
                if (TestStatus == true)
                    TestStatus = SaveTenantDetails(TestDataSet);
                else
                    return TestStatus;
            }
            else
                return TestStatus;
            
            return TestStatus;
        }

        //A method to enter and save a given test data set for property details
        internal bool SavePropertyDetails(int TestDataSet)
        {
            try
            {
                //A variable that contains the search address used for the Google API
                string SearchAddressString;
                SearchAddressString = ExcelLib.ReadData(TestDataSet, "Number") + " " +
                    ExcelLib.ReadData(TestDataSet, "Street") + " " +
                    ExcelLib.ReadData(TestDataSet, "Suburb") + " " +
                    ExcelLib.ReadData(TestDataSet, "City") + " " +
                    ExcelLib.ReadData(TestDataSet, "PostCode") + " " +
                    ExcelLib.ReadData(TestDataSet, "City");


                //Enter the testdata into the relevant input fields
                PropertyName.SendKeys(ExcelLib.ReadData(TestDataSet, "Property Name"));
                Description.SendKeys(ExcelLib.ReadData(TestDataSet, "Description"));

                //Using the Google API search results to populate the address fields
                SearchAddress.SendKeys(SearchAddressString);
                Thread.Sleep(1000);
                SearchAddress.SendKeys(Keys.Down);
                SearchAddress.SendKeys(Keys.Enter);                
                
                TargetRent.SendKeys(ExcelLib.ReadData(TestDataSet, "TargetRent"));
                Bedrooms.SendKeys(ExcelLib.ReadData(TestDataSet, "Bedrooms"));
                Bathrooms.SendKeys(ExcelLib.ReadData(TestDataSet, "Bathrooms"));
                Carparks.SendKeys(ExcelLib.ReadData(TestDataSet, "Carparks"));
                YearBuilt.SendKeys(ExcelLib.ReadData(TestDataSet, "YearBuilt"));

                //Finding the path of the Image
                String ImagePath = Keys_Resource.ImagePath;
                ImagePath = ImagePath + ExcelLib.ReadData(TestDataSet, "Photo");
                
                //Upload the photo of the property from file
                FileUploadButton.SendKeys(ImagePath);
               
                Thread.Sleep(5000);
                //Click on the Next Button to move on to the Next Section
                PropertyNextButton.Click();
                Thread.Sleep(1000);
                return true;
            }
            catch(Exception e)
            {
                // Log the error details in the report
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Error, "Error occured when entering property details for the new property: " + e.Message.ToString());
                // Save Screenshot to display the error
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Exception Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
                return false;
            }
        }

        //A method to enter and save a given test data set for finance details
        internal bool SaveFinanceDetails(int TestDataSet)
        {
            try
            {
                //Enter the testdata into the relevant input fields
                PurchasePrice.SendKeys(ExcelLib.ReadData(TestDataSet, "PurchasePrice"));
                Mortgage.SendKeys(ExcelLib.ReadData(TestDataSet, "Mortgage"));
                HomeValue.SendKeys(ExcelLib.ReadData(TestDataSet, "HomeValue"));
                Thread.Sleep(1000);

                //Click on the Next Button to move to the Tenant Details
                FinanceNextButton.Click();
                return true;
            }
            catch (Exception e)
            {
                // Add details of the error into the report
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Error, "Error occured when entering Finance Details for the new property: " + e.Message.ToString());
                // Save Screenshot to display the error
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Exception Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
                return false;
            }
        }

        //A method to enter and save a given test data set for tenant details
        internal bool SaveTenantDetails(int TestDataSet)
        {
            try
            {
                //Enter the testdata into the relevant input fields
                TenantEmail.SendKeys(ExcelLib.ReadData(TestDataSet, "TenantEmail"));
                FirstName.SendKeys(ExcelLib.ReadData(TestDataSet, "FirstName"));
                LastName.SendKeys(ExcelLib.ReadData(TestDataSet, "LastName"));
                StartDate.SendKeys(ExcelLib.ReadData(TestDataSet, "StartDate"));
                EndDate.SendKeys(ExcelLib.ReadData(TestDataSet, "EndDate"));
                RentAmount.SendKeys(ExcelLib.ReadData(TestDataSet, "RentAmount"));
                PaymentStartDate.SendKeys(ExcelLib.ReadData(TestDataSet, "PaymentStartDate"));
                Thread.Sleep(1000);

                //Click on the Save Button to Save the Property Details
                SaveButton.Click();
                return true;
            }
            catch (Exception e)
            {
                // Log the error details in the report
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Error, "Error Occured when entering Tenant Details for the new property: " + e.Message.ToString());
                // Save Screenshot to display the error
                String img = SaveScreenShotClass.SaveScreenshot(Driver.driver, "Exception Report");
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Info, "Image example: " + img);
                return false;
            }
        }


    }
}
