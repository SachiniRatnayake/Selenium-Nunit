using Keys_Onboarding.Global;
using NUnit.Framework;
using static Keys_Onboarding.Global.CommonMethods;

namespace Keys_Onboarding.Test
{
    class Sprint 
    {
      [TestFixture]
      [Category("Sprint1")]
       class Tenant : Base
       {          
            [Test, Description("Verify that a Property Owner can add a new property")]
            [TestCase("Add Property", 2)]
            public void PO_AddANewProperty(string TestDataSheetName, int TestDataSetNumber)
            {
                // Create a toggle for the test to log events   
                test = extent.StartTest("Add a New Property");
                //Set the excel path and sheet name for the test data for the test case 
                ExcelLib.PopulateInCollection(Base.ExcelPath, TestDataSheetName);

                // Create a Property Details page object to add a new property
                PropertyDetails PropertyDetailsObj = new PropertyDetails();
                
                //Verify that the new listing is displayed in the "My Properties" page
                //Proceed to verification if the property was added successfully
                if (PropertyDetailsObj.AddNewProperty(TestDataSetNumber) == true)
                {
                    PropertyOwner PropertyOwnerObj = new PropertyOwner();
                    PropertyOwnerObj.VerifyNewProperty(TestDataSetNumber);
                }
            }
                        
            [Test, Description("Verify that a Property Owner can list an existing property for rental")]
            [TestCase("List Rental", 2)]
            public void PO_ListARental(string TestDataSheetName, int TestDataSetNumber)
            {
                // Create a toggle for the test to log events   
                test = extent.StartTest("List a Rental");
                //Set the excel path and sheet name for the test data for the test case 
                ExcelLib.PopulateInCollection(Base.ExcelPath, TestDataSheetName);

                // Create an object of the List Rental Property page to perform the task
                ListRentalProperty ListRentalPropertyObj = new ListRentalProperty();

                //Verify that the the new listing is correctly displayed
                //Proceed to verification if the property was added successfully
                if (ListRentalPropertyObj.SaveRentalListing(TestDataSetNumber) == true)
                {
                    //Create an object of the rental listings page to check for the new listing
                    RentalListingsAndNewApplications RentalListingsObj = new RentalListingsAndNewApplications();
                    RentalListingsObj.VerifyReantalListing(TestDataSetNumber);
                }
            }

            [Test, Description("Verify that a Property Owner can add a Tenant to an existing Property")]
            [TestCase("Add Tenant", 2)]
            public void PO_AddTenantToProperty(string TestDataSheetName, int TestDataSetNumber)
            {
                // Create a toggle for the test to log events 
                test = extent.StartTest("Add a Tenant to Property");
                //Set the excel path and sheet name for the test data for the test case 
                ExcelLib.PopulateInCollection(Base.ExcelPath, TestDataSheetName);

                // Create an object of the 'Add Tenant' page to perform the given action
                AddTenant AddTenantObj = new AddTenant();

                //Add a new tenant to the specified property
                AddTenantObj.AddTenantToProperty(TestDataSetNumber);

                //Verify that the the tenant is displayed as a tenant for the property
                //Create an instance of the 'Tenants in Properties' page and perform the check
                TenantsInProperties TenantsInPropertiesObj = new TenantsInProperties();
                TenantsInPropertiesObj.VerifyTenantInProperty(TestDataSetNumber);
            }


            /* Commenting the existing test for competition task purposes
            [Test]
            public void PO_AddNewProperty()
            {
                // Creates a toggle for the given test, adds all log events under it    
                test = extent.StartTest("Search for a Property");               

                // Create an class and object to call the method
                PropertyOwner obj = new PropertyOwner();
                obj.SearchAProperty();

            }
            */

        }
    }
}
