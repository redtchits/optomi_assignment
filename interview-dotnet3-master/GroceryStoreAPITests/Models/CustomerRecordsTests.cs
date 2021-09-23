using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GroceryStoreAPI.Models.Tests
{
    [TestClass()]
    public class CustomerRecordsTests
    {

        [TestMethod()]
        public void GetCustomers_Success()
        {
            var result = new Customer().GetCustomers();

            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void GetCustomersFilterById_Success()
        {
            var result = new Customer().GetCustomers(1);

            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void AddCustomer_ExistingId()
        {
            var result = new Customer().AddCustomer(new Person { Id = 1, Name = "Test" });

            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void UpdateCustomer_IdDoesNotExist()
        {
            var result = new Customer().UpdateCustomer(new Person { Id = 1001, Name = "Test" });

            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void DeleteCustomer_IdDoesNotExist()
        {
            var result = new Customer().DeleteCustomer(1001);

            Assert.IsFalse(result.Success);
        }
    }
}