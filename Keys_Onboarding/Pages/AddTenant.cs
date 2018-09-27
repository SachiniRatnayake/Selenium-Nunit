using Keys_Onboarding.Global;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Threading;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding
{
    class AddTenant
    {
        //Create a constructor for the class
        public AddTenant()
        {
            PageFactory.InitElements(Global.Driver.driver, this);
        }

        #region WebElement Definitions in the 'Tenant Details' Section
        // Setting the 'TenantEmail' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Tenant Email']")]
        private IWebElement TenantEmail { get; set; }

        // Setting the 'FirstName' Field
        [FindsBy(How = How.XPath, Using = "//input[contains(@placeholder,'First Name')]")]
        private IWebElement FirstName { get; set; }

        // Setting the 'LastName' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Last Name']")]
        private IWebElement LastName { get; set; }

        // Setting the 'RentStartDate' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Rent Start Date']")]
        private IWebElement RentStartDate { get; set; }

        // Setting the 'RentAmount' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Rent Amount']")]
        private IWebElement RentAmount { get; set; }

        // Setting the 'PaymentStartDate' Field
        [FindsBy(How = How.XPath, Using = "//input[@placeholder='Payment Start Date']")]
        private IWebElement PaymentStartDate { get; set; }

        // Setting the Next Button
        [FindsBy(How = How.XPath, Using = "//input[@value='Next']")]
        private IWebElement TenantNextButton { get; set; }

        #endregion

        #region WebElement Definitions in the 'Liabilities Details' Section
        // Setting the Plus Icon for Adding a Liability
        [FindsBy(How = How.XPath, Using = "//i[@class='plus circle icon']")]
        private IWebElement PlusIcon { get; set; }

        // Setting the Liability Amount Field
        [FindsBy(How = How.XPath, Using = ".//*[@id='LiabilityDetail']/div[1]/div/table/tbody/tr/td[2]/input")]                                            
        private IWebElement LiabilityAmount { get; set; }

        // Setting the Next Button
        [FindsBy(How = How.XPath, Using = "//button[contains(.,'Next')]")]
        private IWebElement LiabilitiesNextButton { get; set; }
        #endregion

        #region WebElement Definitions in the 'Summary' Section
        // Setting the Submit Button
        [FindsBy(How = How.XPath, Using = "//button[contains(.,'Submit')]")]
        private IWebElement SubmitButton { get; set; }
        #endregion


        // Common methods to navigate into the "Add Tenant" page
        public void NavigateToPage(int TestDataSetNumber)
        {
            //Calling the common methods to navigate into the ''My Properties' page
            PropertyOwner PropertyOwnerObj = new PropertyOwner();
            PropertyOwnerObj.Common_methods();

            //Click on the 'Add Tenant' button to navigate to the 'Add Tenant' Page
            PropertyOwnerObj.ClickAddTenant(ExcelLib.ReadData(TestDataSetNumber, "PropertyName"));
        }
        
        
        //A method to add a new tenant into an existing property
        internal void AddTenantToProperty(int TestDataSet)
        {
            //Navigate into the "Property Details" page
            NavigateToPage(TestDataSet);

            //Save Tenant Details, Liabilities and Summary to Add a new tenant to the property
            SaveTenantDetails(TestDataSet);
            SaveTenantLiabilities(TestDataSet);
            SaveTenantSummary(TestDataSet);            
        }

        //A method to enter and save a given test data set for tenant details for an existing property
        internal void SaveTenantDetails(int TestDataSet)
        {
            //Enter the testdata into the relevant input fields
            TenantEmail.SendKeys(ExcelLib.ReadData(TestDataSet, "TenantEmail"));
            RentStartDate.SendKeys(ExcelLib.ReadData(TestDataSet, "RentStartDate"));
            RentAmount.SendKeys(ExcelLib.ReadData(TestDataSet, "RentAmount"));
            PaymentStartDate.SendKeys(ExcelLib.ReadData(TestDataSet, "PaymentStartDate"));
            Thread.Sleep(1000);

            //Click on the Next Button to move to the next section
            TenantNextButton.Click();
        }

        //A method to enter and save a given test data set for tenant liability details 
        internal void SaveTenantLiabilities(int TestDataSet)
        {
                //Click on the plus icon to add a new liability
                PlusIcon.Click();

                //Enter the testdata into the relevant input fields
                LiabilityAmount.SendKeys(ExcelLib.ReadData(TestDataSet, "LiabilityAmount"));

                //Click on the Next Button to move to the next section
                LiabilitiesNextButton.Click();
        }
        
        //A method to enter and save a given test data set for adding a tenant to a property  
        internal void SaveTenantSummary(int TestDataSet)
        {
            //Tests to be added for the verification of displyed details in the summary

            //Click on the plus icon to add a new liability
            SubmitButton.Click();
        }
        
    }
}
