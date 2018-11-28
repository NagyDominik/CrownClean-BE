using System.Collections.Generic;
using System.IO;

namespace CrownCleanApp.Core.Entity
{
    public class User
    {
        private int _id;

        public int ID
        {
            get { return this._id; }
            set
            {
                if (value < 0)
                {
                    throw new InvalidDataException("Cannot add a negative ID");
                }
                else
                {
                    this._id = value;
                }
            }
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsCompany { get; set; }
        public List<string> Addresses { get; set; }
        public string TaxNumber { get; set; }
        public string CompanyName { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Order> Orders { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt{ get; set; }
        public bool IsApproved { get; set; }
    }
}
