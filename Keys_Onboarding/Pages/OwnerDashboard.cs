using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;


namespace Keys_Onboarding
{
    class OwnerDashboard
    {
        //Create a constructor for the class
        public OwnerDashboard()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElements Definition
        
        //Define Dashboard tab
        [FindsBy(How = How.XPath, Using = "html/body/div[1]/div/div[2]/a[1]")]
        private IWebElement DashboardTab { set; get; }

        //Define My Tenants Link
        [FindsBy(How = How.XPath, Using = "//h5[contains(.,'    My Tenants')]")]
        private IWebElement MyTenantsLink { set; get; }

        #endregion


        //A method to verify that a given tenant is added for a given property
        internal void VerifyPropertyTenant(int TestDataSetNumber)
        {
            //Navigate into the tenants page
            DashboardTab.Click();
            MyTenantsLink.Click();

        }

    }
}
