using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects.DataClasses;
using System.Data.Objects;

namespace EFTest
{
    public class EFContext
    {
        public List<Customer> GetAllCustomers()
        {
            using(Entities context = new Entities())
            {
                List<Customer> customers = context.Customer
                    .Include("Address")
                    .Include("Address.State")
                    .Include("Address.State.Country")
                    .Include("Phone")
                    .Include("Phone.PhoneType")
                    .ToList();

                return customers;
            }
        }

        public List<Customer> GetAllCustomers_Load()
        {
            using(Entities context = new Entities())
            {
                List<Customer> customers = context.Customer
                    .ToList();

                foreach(var customer in customers)
                {
                    customer.AddressReference.Load();
                    customer.Address.StateReference.Load();
                    customer.Address.State.CountryReference.Load();
                    customer.Phone.Load();

                    foreach(var phone  in customer.Phone)
                    {
                        phone.PhoneTypeReference.Load();
                    }
                }
                return customers;
            }
        }

        public void InsertCustomers_Batch(List<Customer> customers)
        {
            using(Entities context = new Entities())
            {
                foreach(Customer customer in customers)
                {
                    context.AddToCustomer(customer);
                }

                context.SaveChanges();
            }
        }

        public void InsertCustomers_Single(List<Customer> customers)
        {
            using(Entities context = new Entities())
            {
                foreach(Customer customer in customers)
                {
                    context.AddToCustomer(customer);
                    context.SaveChanges();
                }                
            }
        }

        public List<Customer> GetAllCustomers_Compiled()
        {
            using(Entities context = new Entities())
            {
                return m_CompiledQuery.Invoke(context).ToList();
            }
        }

        public List<Customer> GetAllCustomers_CompiledNoTracking()
        {
            using(Entities context = new Entities())
            {
                IQueryable<Customer> query = m_CompiledQuery(context);
                ((ObjectQuery)query).MergeOption = MergeOption.NoTracking;
                return query.ToList();
            }
        }

        public List<Customer> GetAllCustomers_Execute()
        {
            using(Entities context = new Entities())
            {
                var customers = context.Customer
                    .Include("Address")
                    .Include("Address.State")
                    .Include("Address.State.Country")
                    .Include("Phone")
                    .Include("Phone.PhoneType").Execute(MergeOption.NoTracking);
                return customers.ToList();
            }
        }

        public List<Customer> GetAllCustomers_NoTracking()
        {
            using(Entities context = new Entities())
            {
                var customers = context.Customer
                    .Include("Address")
                    .Include("Address.State")
                    .Include("Address.State.Country")
                    .Include("Phone")
                    .Include("Phone.PhoneType");
                customers.MergeOption = MergeOption.NoTracking;
                return customers.ToList();
            }
        }

        /// <summary>
        /// Compiled query
        /// </summary>
        private static Func<Entities, IQueryable<Customer>> m_CompiledQuery = 
            CompiledQuery.Compile((Entities ctx) => from c in ctx.Customer
                               .Include("Address")
                               .Include("Address.State")
                               .Include("Address.State.Country")
                               .Include("Phone")
                               .Include("Phone.PhoneType")
                                select c);
    }
}
