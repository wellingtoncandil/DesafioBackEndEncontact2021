using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contactt;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        public async Task<bool> Put(Contact contact, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.EditAsync(contact);
        }
        [HttpPost]
        public async Task<IContact> Post(Contact contact, [FromServices] IContactRepository contactRepository)
        {
        return await contactRepository.SaveAsync(contact);
        }

        [HttpDelete]
        public async Task<int> DeleteAsync(int id, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<IContact>> Get(int? page,[FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetAllAsync(page);
        }

        [HttpGet("{id}")]
        public async Task<IContact> Get(int id, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetAsync(id);
        }

        [HttpPost("{path}")]
        public async Task<IEnumerable<IContact>> Post(string path, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.ImportContact(path);
        }

        [HttpGet("search/name/{name}")]
        public async Task<IEnumerable<IContact>> GetContactByName(string name, int? page, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetContactByName(name, page);
        }

        [HttpGet("search/phone/{phone}")]
        public async Task<IEnumerable<IContact>> GetContactByPhone(string phone, int? page, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetContactByPhone(phone, page);
        }

        [HttpGet("search/email/{email}")]
        public async Task<IEnumerable<IContact>> GetContactByEmail(string email, int? page, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetContactByEmail(email, page);
        }
        [HttpGet("search/address/{address}")]
        public async Task<IEnumerable<IContact>> GetContactByAddress(string address, int? page, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetContactByAddress(address, page);
        }
        [HttpGet("search/contactBook/{contactBookId}")]

        public async Task<IEnumerable<IContact>> GetContactByContactBook(string contactBookId, int? page, [FromServices] IContactRepository contactRepository)

        {
            return await contactRepository.GetContactByContactBook(contactBookId, page);
        }

        [HttpGet("search/company/{companyId}")]
        public async Task<IEnumerable<IContact>> GetContactByCompany(string companyId, int? page, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetContactByCompany(companyId, page);
        }

        [HttpGet("search/contactBook/{companyId}/{contactBookId}")]
        public async Task<IEnumerable<IContact>> GetContactByCompanyContactBook(string companyId, string contactBookId, int? page, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetContactByCompanyContactBook(companyId, contactBookId, page);
        }
    }
}

