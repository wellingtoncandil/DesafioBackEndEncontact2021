using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactRepository
    {
        Task<bool> EditAsync(IContact contact);
        Task<IContact> SaveAsync(IContact contact);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<IContact>> GetAllAsync(int? page);
        Task<IContact> GetAsync(int id);
        Task<IEnumerable<IContact>> ImportContact(string path);
        Task<IEnumerable<IContact>> GetContactByContactBook(string contactBookName, int? page);
        Task<IEnumerable<IContact>> GetContactByCompany(string companyId, int? page);
        Task<IEnumerable<IContact>> GetContactByName(string name, int? page);
        Task<IEnumerable<IContact>> GetContactByPhone(string phone, int? page);
        Task<IEnumerable<IContact>> GetContactByEmail(string email, int? page);
        Task<IEnumerable<IContact>> GetContactByAddress(string address, int? page);
        Task<IEnumerable<IContact>> GetContactByCompanyContactBook(string companyId, string contactBookId, int? page);




    }
}
