using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ADOTest
{
    public class Customer
    {
        public Customer()
        {

        }

        public Customer(int id, string fname, string lname, Address address, string email)
        {
            ID = id;
            FName = fname;
            LName = lname;
            Address = address;
            Email = email;
        }

        public int ID { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public Address Address { get; set; }
        public string Email { get; set; }
        public List<Phone> Phones { get; set; }
    }

    public class Address
    {
        public Address()
        {

        }

        public Address(int id, string street, string city, State state, string postalCode)
        {
            ID = id;
            Street = street;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public State State { get; set; }
        public string PostalCode { get; set; }
    }

    public class State
    {
        public State()
        {

        }

        public State(int id, string name, Country country)
        {
            ID = id;
            Name = name;
            Country = country;
        }

        public int ID { get; set; }
        public string Name { get; set; }
        public string Abbr { get; set; }
        public Country Country { get; set; }
    }

    public class Country
    {
        public Country()
        {

        }

        public Country(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }

    public class Phone
    {
        public Phone()
        {

        }

        public Phone(int id, string number, PhoneType type)
        {
            ID = id;
            Number = number;
            PhoneType = type;
        }

        public int ID { get; set; }
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
    }

    public class PhoneType
    {
        public PhoneType()
        {

        }

        public PhoneType(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public int ID { get; set; }
        public string Name { get; set; }
    }
}
