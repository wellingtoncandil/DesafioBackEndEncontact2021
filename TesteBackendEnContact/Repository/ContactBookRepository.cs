﻿using Dapper;
using Dapper.Contrib.Extensions;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Database;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Repository
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly DatabaseConfig databaseConfig;

        public ContactBookRepository(DatabaseConfig databaseConfig)
        {
            this.databaseConfig = databaseConfig;
        }
        public async Task<bool> EditAsync(IContactBook contactBook)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactBookDao(contactBook);

            var res = await connection.UpdateAsync(dao);

            return res;
        }
        public async Task<IContactBook> SaveAsync(IContactBook contactBook)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var dao = new ContactBookDao(contactBook);

            dao.Id = await connection.InsertAsync(dao);

            return dao.Export();
        }
        //tentativa de exclusão do ContactBook caso ele esteja vazio e sem vinculo com alguma empresa
        public async Task<int> DeleteAsync(int id)
        {
            if (await HasCompany(id) == false && await HasContact(id) == false)
            {
                using var connection = new SqliteConnection(databaseConfig.ConnectionString);

                var sql = "DELETE FROM ContactBook WHERE Id = @id";
                var result = await connection.ExecuteAsync(sql, new { id });

                return result;
            }
            return 0;
        }    
        public async Task<bool> HasCompany(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var sql = "SELECT * FROM Company WHERE ContactBookId = @id";
            var result = await connection.QueryAsync<ContactBookDao>(sql, new { id });

            if(!result.Any())
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        public async Task<bool> HasContact(int id)
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);
            var sql = "SELECT * FROM Contact WHERE ContactBookId = @id";
            var result = await connection.QueryAsync<ContactBookDao>(sql, new { id });

            if (!result.Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task<IEnumerable<IContactBook>> GetAllAsync()
        {
            using var connection = new SqliteConnection(databaseConfig.ConnectionString);

            var query = "SELECT * FROM ContactBook";
            var result = await connection.QueryAsync<ContactBookDao>(query);

            var returnList = new List<IContactBook>();

            foreach (var AgendaSalva in result.ToList())
            {
                IContactBook Agenda = new ContactBook(AgendaSalva.Id, AgendaSalva.Name.ToString());
                returnList.Add(Agenda);
            }
            return returnList.ToList();
        }
        public async Task<IContactBook> GetAsync(int id)
        {
            var list = await GetAllAsync();

            return list.ToList().Where(item => item.Id == id).FirstOrDefault();
        }
    }
    [Table("ContactBook")]
    public class ContactBookDao : IContactBook
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public ContactBookDao()
        {
        }

        public ContactBookDao(IContactBook contactBook)
        {
            Id = contactBook.Id;
            Name = contactBook.Name;
        }
        public IContactBook Export() => new ContactBook(Id, Name);
    }
}
