using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace Keys_Onboarding
{
    class PropertyTenantSummary
    {
        //Create a constructor for the class
        public PropertyTenantSummary()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElement Definitions

        // Setting the Submit Button
        [FindsBy(How = How.XPath, Using = "//button[contains(.,'Submit')]")]
        private IWebElement SubmitButton { get; set; }

        #endregion

        //A method to enter and save a given test data set for adding a tenant to a property  
        internal void SavePropertyTenantSummary(int TestDataSet)
        {
            //Tests to be added for the verification of displyed details in the summary

            //Click on the plus icon to add a new liability
            SubmitButton.Click();
        }
    }
}
