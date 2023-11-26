using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHarness
{
    internal class ADOTests
    {
        private List<ADOTest.Customer> m_Customers = new List<ADOTest.Customer>();


        public ADOTests(int numCustomers, int numPhones)
        {
            CreateCustomers(numCustomers, numPhones);
        }

        public void InsertCustomers()
        {
            ADOTest.DBAccess db = new ADOTest.DBAccess();
            
            db.InsertCustomer(Customers);
        }

        public void GetCustomers()
        {
            ADOTest.DBAccess db = new ADOTest.DBAccess();
            List<ADOTest.Customer> customers = db.GetAllCustomers();
        }

        public void Clear()
        {
            ADOTest.DBAccess.Clear();
        }

        #region Private Methods

        /// <summary>
        /// Helper method to create customers
        /// </summary>
        /// <param name="numCustomers"></param>
        /// <param name="numPhones"></param>
        private void CreateCustomers(int numCustomers, int numPhones)
        {
            for(int x = 0; x < numCustomers; x++)
            {
                ADOTest.Customer customer = new ADOTest.Customer(0, "ADO",
                    "Doe" + x + 1, CreateAddress(x + 1), "john@doe.com");

                customer.Phones = new List<ADOTest.Phone>();
                for(int y = 0; y < numPhones; y++)
                {
                    customer.Phones.Add(CreatePhone(y + 1));
                }

                Customers.Add(customer);
            }
        }

        /// <summary>
        /// Helper method to create address
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private ADOTest.Address CreateAddress(int x)
        {
            ADOTest.Country country = new ADOTest.Country();
            country.ID = 1;
            ADOTest.State state = new ADOTest.State();
            state.ID = 1;
            state.Country = country;
            return new ADOTest.Address(0, "ADO Main Street " + x, "Anytown", state, "12345");
        }

        /// <summary>
        /// Helper method to create phone
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        private ADOTest.Phone CreatePhone(int number)
        {
            ADOTest.PhoneType type = new ADOTest.PhoneType();
            type.ID = 1;
            return new ADOTest.Phone(0, number.ToString(), type);
        }

        #endregion

        #region Properties

        private List<ADOTest.Customer> Customers
        {
            get { return m_Customers; }
        }

        #endregion
    }
}
