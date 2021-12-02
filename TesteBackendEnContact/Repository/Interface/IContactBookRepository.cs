using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook;

namespace TesteBackendEnContact.Repository.Interface
{
    public interface IContactBookRepository
    {
        Task<bool> EditAsync(IContactBook contactBook);
        Task<IContactBook> SaveAsync(IContactBook contactBook);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<IContactBook>> GetAllAsync();
        Task<IContactBook> GetAsync(int id);
        Task<bool> HasCompany(int id);
        Task<bool> HasContact(int id);

    }
}
