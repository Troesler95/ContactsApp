using ContactsApp.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace ContactsApp.Models
{
    struct Address
    {
        public string StreetAddress
        {
            get { return this.StreetAddress; }
            set { this.StreetAddress = value ?? ""; }
        }

        public string City
        {
            get { return this.City; }
            set { this.City = value ?? ""; }
        }

        public string State
        {
            get { return this.State; }
            set { this.State = value ?? ""; }
        }

        public string ZipCode
        {
            get { return this.ZipCode; }
            set { if (!string.IsNullOrWhiteSpace(value) || value.Length != 5) this.ZipCode = value; }
        }

        public Address(string adr, string city, string state, string zip)
        {
            this.StreetAddress = adr;
            this.City = city;
            this.State = state;
            this.ZipCode = zip;
        }
    }

    class Contact
    {
        #region Private Data Members

        private string _firstName = null;

        private string _lastName = null;

        private Dictionary<PhoneNumberType, string> _phoneNumbers = null;

        private String _email = null;

        // ? is the shorthand for the Nullable<T> type
        // this means we can default this property to null
        private Address? _address;

        #endregion

        #region Public Methods

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public Contact()
        {
            _firstName = "";
            _lastName = "";
            _phoneNumbers = new Dictionary<PhoneNumberType, string>();
            _email = null;
            _address = null;
        }

        /// <summary>
        /// Contact paramterized constructor
        /// </summary>
        /// <param name="name" >full name of the contact. required</param>
        /// <param name="email">email string</param>
        /// <param name="adr">address structure</param>
        /// <param name="phoneNumbers">variable list of phone numbers tuples. Item1=type of number, Item2=number string</param>
        public Contact(string name, string email="", Address? adr=null, params Tuple<PhoneNumberType, string>[] phoneNumbers)
        {
            FullName = name;
            _address = adr;
            _phoneNumbers = new Dictionary<PhoneNumberType, string>();

            Email = email;

            foreach (var number in phoneNumbers)
                _phoneNumbers.Add(number.Item1, number.Item2);
        }

        #endregion

        #region Getters/Setters
        
        /// <summary>
        /// Public accessor for the full name of the contact
        /// This accessor validates and returns first and last name strings
        /// </summary>
        public string FullName
        {
            get { return _firstName+" "+_lastName; }
            set
            {
                // Check if string is not null, whitespace, and contains only alphabet letters
                bool match = Regex.IsMatch(value, @"^[a-zA-Z]*[\s]{1}[a-zA-Z]*$");
                if (!String.IsNullOrWhiteSpace(value) && match)
                {
                    // Separate the first and last names by a space deliminator
                    String[] subStr = value.Split(' ');
                    if (subStr.Length > 0 && subStr.Length < 3)
                    {
                        _firstName = subStr[0];
                        _lastName = subStr[1];
                    }
                    else
                        return;
                }
            }
        }
        
        /// <summary>
        /// Public accessor for the contact's email
        /// </summary>
        public String Email
        {
            get { return this._email; }
            set
            {
                if (IsValidEmail(value))
                    _email = value;
            }
        }

        /// <summary>
        /// Public accessor for the contact's address structure
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Validate and insert a new entry in the contact phone number structure
        /// </summary>
        /// <param name="phone"></param>
        public void AddPhoneNumber(Tuple<PhoneNumberType, string> phone)
        {
            bool valid = Regex.IsMatch(phone.Item2,
                @"^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$");
            if (valid)
                _phoneNumbers.Add(phone.Item1, phone.Item2);
            else
                throw new ArgumentException("Contact ERR: Invalid phone number added!");
        }

        /*Do we need to make this more robust?*/
        public Dictionary<PhoneNumberType, string> GetPhoneNumbers()
        {
            return this._phoneNumbers;
        }

        #endregion

        #endregion

        #region Private Helpers

        /// <summary>
        ///     Validates an email before setting it.
        ///     <para>
        ///         This code is adapted from the MSDN article 
        ///         <a href="https://docs.microsoft.com/en-us/dotnet/standard/base-types/how-to-verify-that-strings-are-in-valid-email-format">
        ///             How to: Verify that Strings Are in Valid Email Format
        ///         </a>
        ///     </para>
        /// </summary>
        /// <param name="mailString">Input string to validate</param>
        /// <returns></returns>
        private bool IsValidEmail(string mailString)
        {
            if (String.IsNullOrEmpty(mailString))
                return false;

            // Use IdnMapping class to convert Unicode domain names.
            try
            {
                mailString = Regex.Replace(mailString, @"(@)(.+)$", new MatchEvaluator(this.DomainMapper),
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            // Return true if mailString is in valid e-mail format.
            try
            {
                return Regex.IsMatch(mailString,
                      @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$",
                      RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        /// <summary>
        /// Class to map unicode to valid ascii and shows an error box if it encounters invalid characters.
        /// This method is an implementation of the MatchEvaluator delegate.
        /// </summary>
        /// <param name="match">Match made during the Regex.Replace() process</param>
        /// <returns>The mapped ascii string</returns>
        private string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            IdnMapping idn = new IdnMapping();

            string domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Contact ERR: Inavlid parameter given to DomainMapper\n");
            }
            return match.Groups[1].Value + domainName;
        }

        #endregion
    }
}
