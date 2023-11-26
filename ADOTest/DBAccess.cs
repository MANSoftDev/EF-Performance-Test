using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;

namespace ADOTest
{
    public class DBAccess
    {
        public List<Customer> GetAllCustomers()
        {
            List<Customer> customers = new List<Customer>();

            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("GetFullCustomer", conn))
                using(SqlCommand cmd2 = new SqlCommand("GetPhonesForCustomer", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd2.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while(dr.Read())
                    {
                        Country country = new Country(dr.GetInt32(9), dr.GetString(10));
                        State state = new State(dr.GetInt32(7), dr.GetString(8), country);
                        Address addr = new Address(dr.GetInt32(4), dr.GetString(5), dr.GetString(6),
                            state, dr.GetString(11));
                        addr.ID = dr.GetInt32(4);
                        Customer customer = new Customer(dr.GetInt32(0),
                            dr.GetString(1), dr.GetString(2), addr, dr.GetString(3));

                        customer.Phones = new List<Phone>();

                        cmd2.Parameters.AddWithValue("@CustomerID", customer.ID);
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        while(dr2.Read())
                        {
                            customer.Phones.Add(new Phone(dr2.GetInt32(0), dr2.GetString(1),
                                    new PhoneType(dr2.GetInt32(2), dr2.GetString(3))));
                        }
                        cmd2.Parameters.Clear();
                        dr2.Close();
                        customers.Add(customer);
                    }
                }
            }

            return customers;
        }

        public void InsertCustomer(List<ADOTest.Customer> customers)
        {
            int addressID = 0;
            int customerID = 0;

            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("InsertCustomer", conn))
                using(SqlCommand cmdAddr = new SqlCommand("InsertAddress", conn))
                using(SqlCommand cmdPhone = new SqlCommand("InsertPhone", conn))
                using(SqlCommand cmdCustomerPhone = new SqlCommand("InsertCustomerPhone", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@FName", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@LName", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add(new SqlParameter("@Address_ID", System.Data.SqlDbType.Int));
                    cmd.Parameters.Add(new SqlParameter("@Email", System.Data.SqlDbType.NVarChar));
                    cmd.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                    cmdAddr.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdAddr.Parameters.Add(new SqlParameter("@Street", System.Data.SqlDbType.NVarChar));
                    cmdAddr.Parameters.Add(new SqlParameter("@City", System.Data.SqlDbType.NVarChar));
                    cmdAddr.Parameters.Add(new SqlParameter("@State_ID", System.Data.SqlDbType.Int));
                    cmdAddr.Parameters.Add(new SqlParameter("@PostalCode", System.Data.SqlDbType.NVarChar));
                    cmdAddr.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                    cmdPhone.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdPhone.Parameters.Add(new SqlParameter("@Number", System.Data.SqlDbType.NVarChar));
                    cmdPhone.Parameters.Add(new SqlParameter("@Type_ID", System.Data.SqlDbType.Int));
                    cmdPhone.Parameters.Add("@ReturnValue", System.Data.SqlDbType.Int).Direction = System.Data.ParameterDirection.ReturnValue;

                    cmdCustomerPhone.CommandType = System.Data.CommandType.StoredProcedure;
                    cmdCustomerPhone.Parameters.Add(new SqlParameter("@Customer_ID", System.Data.SqlDbType.Int));
                    cmdCustomerPhone.Parameters.Add(new SqlParameter("@Phone_ID", System.Data.SqlDbType.Int));

                    foreach(var customer in customers)
                    {
                        if(conn.State != System.Data.ConnectionState.Open)
                            conn.Open();

                        addressID = InsertAddress(customer.Address, cmdAddr);
                        
                        cmd.Parameters["@FName"].Value = customer.FName;
                        cmd.Parameters["@LName"].Value = customer.LName;
                        cmd.Parameters["@Address_ID"].Value = addressID;
                        cmd.Parameters["@Email"].Value = customer.Email;


                        cmd.ExecuteNonQuery();

                        customerID = (int)cmd.Parameters["@ReturnValue"].Value;

                        foreach(Phone phone in customer.Phones)
                        {
                            InsertCustomerPhone(customerID, phone, cmdPhone, cmdCustomerPhone);
                        }
                    }
                }
            }
        }

        public static void Clear()
        {
            using(SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using(SqlCommand cmd = new SqlCommand("Clear", conn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    conn.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        #region Private Methods

        private int InsertAddress(Address addr, SqlCommand cmd)
        {
            cmd.Parameters["@Street"].Value = addr.Street;
            cmd.Parameters["@City"].Value =addr.City;
            cmd.Parameters["@State_ID"].Value =addr.State.ID;
            cmd.Parameters["@PostalCode"].Value = addr.PostalCode;

            cmd.ExecuteNonQuery();

            return (int)cmd.Parameters["@ReturnValue"].Value;
        }

        public void InsertCustomerPhone(int customerID, Phone phone, SqlCommand cmdPhone, SqlCommand cmd)
        {
            int phoneID = InsertPhone(phone, cmdPhone);

            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            cmd.Parameters["@Customer_ID"].Value = customerID;
            cmd.Parameters["@Phone_ID"].Value = phoneID;

            cmd.ExecuteNonQuery();
        }

        private int InsertPhone(Phone phone, SqlCommand cmd)
        {
            cmd.Parameters["@Number"].Value = phone.Number;
            cmd.Parameters["@Type_ID"].Value = phone.PhoneType.ID;

            cmd.ExecuteNonQuery();

            return (int)cmd.Parameters["@ReturnValue"].Value;
        }

        #endregion

        #region Properties

        private static string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["ADOTest"].ConnectionString; }
        }

        #endregion
    }
}
