using Keys_Onboarding.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    public class FinanceDetails
    {
        //Create a constructor for the class
        public FinanceDetails()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElement Definitions
        // Setting the 'Purchase Price' Field
        [FindsBy(How = How.XPath, Using = "//input[@name='purchasePrice']")]
        private IWebElement PurchasePrice { get; set; }

        // Setting the 'Mortgage' Field
        [FindsBy(How = How.XPath, Using = "//input[@name='mortgagePrice']")]
        private IWebElement Mortgage { get; set; }

        // Setting the 'Home Value' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='  Enter Home Value']")]
        private IWebElement HomeValue { get; set; }

        // Setting the 'Next' Button
        [FindsBy(How = How.XPath, Using = ".//*[@id='financeSection']/div[8]/button[3]")]
        private IWebElement NextButton { get; set; }

        #endregion

        internal void SaveFinanceDetails(int TestDataSet)
        {
            try
            {
                //Enter the testdata into the relevant input fields
                PurchasePrice.SendKeys(ExcelLib.ReadData(TestDataSet, "PurchasePrice"));
                Mortgage.SendKeys(ExcelLib.ReadData(TestDataSet, "Mortgage"));
                HomeValue.SendKeys(ExcelLib.ReadData(TestDataSet, "HomeValue"));
                Thread.Sleep(1000);

                //Click on the Next Button to move to the Tenant Details
                NextButton.Click();
            }
            catch(Exception e)
            {
                Base.test.Log(RelevantCodes.ExtentReports.LogStatus.Error, "Error occured when entering Finance Details for the new property: " + e.Message.ToString());
            }
        }

        }
}
