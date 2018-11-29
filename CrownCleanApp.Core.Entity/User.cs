using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CrownCleanApp.Core.Entity
{
    public class User
    {
        //private int _id;

        public int ID { get; set; }
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("ID:\t " + this.ID.ToString()));
            sb.Append(string.Format("First name: \t" + this.FirstName));
            sb.Append(string.Format("Last name: \t" + this.LastName));
            sb.Append(string.Format("Phone number: \t" + this.PhoneNumber));
            sb.Append(string.Format("Email: \t" + this.Email));
            sb.Append(string.Format("Addresses: \n"));
            foreach (string item in Addresses)
            {
                sb.Append(string.Format("\t" + item));
            }
            sb.Append(string.Format("Vehicles: \n"));
            foreach (Vehicle item in Vehicles)
            {
                sb.Append(string.Format("\t" + item.ToString()));
            }
            if (IsCompany)
            {
                sb.Append(string.Format("--Corporate customer--"));
                sb.Append(string.Format("Company name: \t" + this.CompanyName));
                sb.Append(string.Format("Tax number: \t" + this.TaxNumber));
            }

            return sb.ToString();
        }
    }
}
