using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace DataGridValidationDemo
{
    /// <summary>
    /// A list of customers.
    /// </summary>
    public class Customer : INotifyDataErrorInfo
    {
        #region Field & Object Definitions
       

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        private List<string> errors = new List<string>();
        #endregion

        #region Property Definitions
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Address { get; set; }
        public Boolean IsNew { get; set; }

        public bool HasErrors
        {
            get
            {

                if (this.LastName.Contains("Smith"))
                    return true;
                return false;
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="address"></param>
        /// <param name="isNew"></param>
        public Customer(String firstName, String lastName, String address, Boolean isNew)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Address = address;
            this.IsNew = isNew;
        }

        /// <summary>
        /// Class default constructor
        /// </summary>
        public Customer() { }

        /// <summary>
        /// Initialize the customer list.
        /// </summary>
        /// <returns></returns>
        public static List<Customer> Customers()
        {
            return new List<Customer>(new Customer[4] 
                {
                new Customer("A.", "Zero", "12 North Third Street, Apartment 45", false),
                new Customer("B.", "One", "34 West Fifth Street, Apartment 67", false),
                new Customer("C.", "Two", "56 East Seventh Street, Apartment 89", true),
                new Customer("D.", "Three", "78 South Ninth Street, Apartment 10", true)
                }
            );
        }

        /// <summary>
        /// Gets the errors associated with this validation.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public System.Collections.IEnumerable GetErrors(string propertyName)
        {

            if (propertyName == null) return null;

            if (!propertyName.Equals("LastName"))
                return null;

            if (this.LastName.Contains("Smith"))
            {
                errors.Add("Smith is not a valid Name" + this.LastName);
                OnErrorsChanged(this.LastName);
            }
              
            return errors;
        }

        #endregion

        #region Event Handlers

     

        /// <summary>
        /// Handles the ErrosChanged event.
        /// </summary>
        /// <param name="propertyName"></param>
        private void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        #endregion
    }

    public class ViewModel
    {
        private List<Customer> customers = new List<Customer>();
        public List<Customer> Customers
        {
            get { return customers; }
            set { customers = value; }
        }

        public ViewModel()
        {
            this.Customers = new List<Customer>(Customer.Customers());
        }
    }
}
