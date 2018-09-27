using Keys_Onboarding.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Threading;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    class TenantDetails
    {
        //Create a constructor for the class
        public TenantDetails()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElement Definitions
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

        //A method to enter and save a given test data set for tenant details
        internal void SaveTenantDetails(int TestDataSet)
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
            }
            catch(Exception e)
            {
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Error, "Error Occured when entering Tenant Details for the new property: " + e.Message.ToString());
            }
        }

    }
}
