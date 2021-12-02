﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly ILogger<ContactBookController> _logger;

        public ContactBookController(ILogger<ContactBookController> logger)
        {
            _logger = logger;
        }

        [HttpPut]
        public async Task<bool> Put(ContactBook contactBook, [FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.EditAsync(contactBook);
        }

        [HttpPost]
        public async Task<IContactBook> Post(ContactBook contactBook, [FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.SaveAsync(contactBook);
        }

        [HttpDelete]
        public async Task<int> Delete(int id, [FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.DeleteAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<IContactBook>> Get([FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<IContactBook> Get(int id, [FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.GetAsync(id);
        }
    }
}
