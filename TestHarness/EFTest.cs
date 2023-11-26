using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestHarness
{
    internal class EFTests
    {
        private List<EFTest.Customer> m_Customers = new List<EFTest.Customer>();
        private EFTest.EFContext m_Context = new EFTest.EFContext();

        public EFTests(int numCustomers, int numPhones)
        {
            NumCustomers = numCustomers;
            NumPhones = numPhones;
        }

        /// <summary>
        /// Insert customers
        /// </summary>
        public void InsertCustomers_Batch()
        {
            // Create customers so any entities already inserted
            // do not have EntityKeys
            CreateCustomers();
            EFContext.InsertCustomers_Batch(Customers);
        }

        /// <summary>
        /// Insert customers
        /// </summary>
        public void InsertCustomers_Single()
        {
            // Create customers so any entities already inserted
            // do not have EntityKeys
            CreateCustomers();
            EFContext.InsertCustomers_Single(Customers);
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        public void GetCustomers()
        {
            List<EFTest.Customer> customers = EFContext.GetAllCustomers();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        public void GetCustomers_Load()
        {
            List<EFTest.Customer> customers = EFContext.GetAllCustomers_Load();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        public void GetCustomers_Compiled()
        {
            List<EFTest.Customer> customers = EFContext.GetAllCustomers_Compiled();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        public void GetCustomers_CompiledNoTracking()
        {
            List<EFTest.Customer> customers = EFContext.GetAllCustomers_CompiledNoTracking();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        public void GetCustomers_Execute()
        {
            List<EFTest.Customer> customers = EFContext.GetAllCustomers_Execute();
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        public void GetCustomers_NoTracking()
        {
            List<EFTest.Customer> customers = EFContext.GetAllCustomers_NoTracking();
        }

        #region Private Methods

        /// <summary>
        /// Helper method to create customers
        /// </summary>
        /// <param name="numCustomers"></param>
        /// <param name="numPhones"></param>
        private void CreateCustomers()
        {
            if(Customers == null)
                Customers = new List<EFTest.Customer>();
            else
                Customers.Clear();

            for(int x = 0; x < NumCustomers; x++)
            {
                EFTest.Customer customer = EFTest.Customer.CreateCustomer(0, "EF", "Doe" + (x + 1));
                customer.email = "john@doe.com";
                customer.Address = CreateAddress(x + 1);
                CreatePhones(NumPhones, customer);
                Customers.Add(customer);
            }
        }

        /// <summary>
        /// Helper method to create phone numbers
        /// </summary>
        /// <param name="numPhones"></param>
        /// <param name="customer"></param>
        private static void CreatePhones(int numPhones, EFTest.Customer customer)
        {
            for(int y = 0; y < numPhones; y++)
            {
                EFTest.Phone phone = EFTest.Phone.CreatePhone(0, (y + 1).ToString());
                phone.PhoneTypeReference.EntityKey = new System.Data.EntityKey("Entities.PhoneType", "ID", 1);
                customer.Phone.Add(phone);
            }
        }

        /// <summary>
        /// Helper method to create address
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private EFTest.Address CreateAddress(int x)
        {
            EFTest.Address addr = EFTest.Address.CreateAddress(0, x.ToString(), "Anytown", "12345");
            addr.StateReference.EntityKey = new System.Data.EntityKey("Entities.State", "ID", 1);

            return addr;
        }

        #endregion

        #region Properties

        private EFTest.EFContext EFContext
        {
            get { return m_Context; }
        }
        private List<EFTest.Customer> Customers { get; set; }
        private int NumCustomers { get; set; }
        private int NumPhones { get; set; }

        #endregion
    }
}
