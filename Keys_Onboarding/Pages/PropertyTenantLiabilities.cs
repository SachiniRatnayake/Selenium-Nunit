using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    class PropertyTenantLiabilities
    {
        //Create a constructor for the class
        public PropertyTenantLiabilities()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElement Definitions

        // Setting the Plus Icon for Adding a Liability
        [FindsBy(How = How.XPath, Using = "//i[@class='plus circle icon']")]
        private IWebElement PlusIcon { get; set; }

        // Setting the Liability Amount Field
        [FindsBy(How = How.XPath, Using = ".//*[@id='LiabilityDetail']/div/div[1]/div/table/tbody/tr/td[2]/input")]
        private IWebElement LiabilityAmount { get; set; }

        // Setting the Next Button
        [FindsBy(How = How.XPath, Using = "//button[contains(.,'Next')]")]
        private IWebElement NextButton { get; set; }
        #endregion

        //A method to enter and save a given test data set for tenant liability details 
        internal void SavePropertyTenantLiabilities(int TestDataSet)
        {
            //Click on the plus icon to add a new liability
            PlusIcon.Click();

            //Enter the testdata into the relevant input fields
            LiabilityAmount.SendKeys(ExcelLib.ReadData(TestDataSet, "LiabilityAmount"));

            //Click on the Next Button to move to the next section
            NextButton.Click();
        }
    }
}
