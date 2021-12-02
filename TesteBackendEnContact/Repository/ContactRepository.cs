using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;
using TesteBackendEnContact.Core.Domain.ContactBook.Contactt;
using System.Collections.Generic;
using System.Text;
using Dapper;
using System.Linq;
using System.IO;
using System;


namespace TesteBackendEnContact.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly DatabaseConfig databaseConfig;
       

        public ContactRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }

        public async Task<bool> EditAsync(IContact contact)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactDao(contact);

            var res = await connection.UpdateAsync(dao);

            return res;
        }

        public async Task<IContact> SaveAsync(IContact contact)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactDao(contact);

            if (dao.Id == 0)
                dao.Id = await connection.InsertAsync(dao);
            else
                await connection.UpdateAsync(dao);

            return dao.Export();
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var sql = "DELETE FROM Contact WHERE Id = @id";
            var result = await connection.ExecuteAsync(sql, new { id });

            return result;

        }

        public async Task<IEnumerable<IContact>> GetAllAsync(int? page)
        {
            const int pageSize = 10;

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact";
            var result = await connection.QueryAsync<ContactDao>(query);
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();
            
            return paginatedResult?.Select(item => item.Export());

        }

        public async Task<IContact> GetAsync(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact where Id = @id";
            var result = await connection.QuerySingleOrDefaultAsync<ContactDao>(query, new { id });

            return result?.Export();
        }

        public async Task<IEnumerable<IContact>> ImportContact(string path)
        {
            var reader = new StreamReader(File.OpenRead(path));
            List<IContact> list = new List<IContact>();

            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');
                IContact contact;
                try
                {
                    //company test
                    if (values[1] == "null" || Int32.Parse(values[1]) == 0)
                    {
                        contact = new Contact(0,Int32.Parse(values[0]), 0, values[2], values[3], values[4], values[5]);
                    }
                    else
                    {
                        contact = new Contact(0,Int32.Parse(values[0]), Int32.Parse(values[1]), values[2], values[3], values[4], values[5]);
                    }

                    //IContact con = new ContactDao(contact);

                    //pegar agenda e perquisar no bd
                    IContactBookRepository rep = new ContactBookRepository(databaseConfig);
        
                    if (contact.ContactBookId >= 0 || rep.GetAsync(contact.ContactBookId) != null)
                    {
                        _ = await SaveAsync(contact);
                        list.Add(contact);
                    }
                    else
                    {
                        Console.WriteLine("Error setting contact. ContactBook does not exist");
                    }
                }
                catch
                {
                    Console.WriteLine("Error setting contact");
                }
            }
            return list.ToList(); ;
            
        }

        public async Task<IEnumerable<IContact>> GetContactByContactBook(string contactBookId, int? page)
        {
            const int pageSize = 3;
           
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact WHERE ContactBookId = @contactBookId ";
            
            var result = await connection.QueryAsync<ContactDao>(query, new { contactBookId });
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return paginatedResult?.Select(item => item.Export());

        }

        public async Task<IEnumerable<IContact>> GetContactByCompany(string companyId, int? page)
        {
            const int pageSize = 3;

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact WHERE CompanyId = @companyId" ;
            var result = await connection.QueryAsync<ContactDao>(query, new { companyId });
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return paginatedResult?.Select(item => item.Export());

        }

        public async Task<IEnumerable<IContact>> GetContactByName(string name, int? page)//ok
        {
            const int pageSize = 3;

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact WHERE Name = @name";
            var result = await connection.QueryAsync<ContactDao>(query, new { name });
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return paginatedResult?.Select(item => item.Export());

        }

        public async Task<IEnumerable<IContact>> GetContactByPhone(string phone, int? page)
        {
            const int pageSize = 3;

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact WHERE Phone = @phone";
            var result = await connection.QueryAsync<ContactDao>(query, new { phone });
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return paginatedResult?.Select(item => item.Export());

        }

        public async Task<IEnumerable<IContact>> GetContactByEmail(string email, int? page)
        {
            const int pageSize = 3;

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact WHERE Email = @email";
            var result = await connection.QueryAsync<ContactDao>(query, new { email });
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return paginatedResult?.Select(item => item.Export());
        }

        public async Task<IEnumerable<IContact>> GetContactByAddress(string address, int? page)
        {
            const int pageSize = 3;

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM Contact WHERE Address = @address";
            var result = await connection.QueryAsync<ContactDao>(query, new { address });
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return paginatedResult?.Select(item => item.Export());
        }

        public async Task<IEnumerable<IContact>> GetContactByCompanyContactBook(string companyId, string contactBookId, int? page)
        {
            const int pageSize = 3;

            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT *"
                        +"From Contact c"
                        + " INNER JOIN ContactBook cb ON c.ContactBookId = cb.Id"
                        + " INNER JOIN Company co ON cb.Id = co.ContactBookId"
                        + " WHERE c.CompanyId = @companyId AND c.ContactBookId = @contactBookId";

            var result = await connection.QueryAsync<ContactDao>(query, new { companyId, contactBookId });
            var paginatedResult = result.Skip((page ?? 0) * pageSize).Take(pageSize).ToList();

            return paginatedResult?.Select(item => item.Export());
        }
    }

    [Table("Contact")]
    public class ContactDao : IContact
    {
        [Dapper.Contrib.Extensions.Key]
        public int Id { get; set; }
        public int ContactBookId { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public ContactDao()
        {
        }


        public ContactDao(IContact contact)
        {
            Id = contact.Id;
            ContactBookId = contact.ContactBookId;
            CompanyId = contact.CompanyId;
            Name = contact.Name;
            Phone = contact.Phone;
            Email = contact.Email;
            Address = contact.Address;

        }

        public IContact Export() => new Contact(Id, ContactBookId, CompanyId, Name, Email, Phone, Address);
    }
    
}

