using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Core.Domain.ContactBook.Contactt
{
    public class Contact : IContact
    {
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Contact(int id, int contactBookId, int companyId, string name, string phone, string email, string address)
        {
            Id = id;
            ContactBookId = contactBookId;
            CompanyId = companyId;
            Name = name;
            Phone = phone;
            Email = email;
            Address = address;
        }
        
        public override string ToString()
        {
            return "Id" + " " + Id + " " + "ContactBookId" + " " + ContactBookId + " " + "CompanyId" + " " + CompanyId + " " +
                "Name" + " " + Name + " " + "Phone" + " " + Phone + " " + "Email" + " " + Email + " " + "Address" + " " + Address;
        }

    }
}
