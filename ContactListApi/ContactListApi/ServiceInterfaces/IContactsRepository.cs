using ContactListApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListApi.ServiceInterfaces
{
    public interface IContactsRepository
    {
        IEnumerable<Person> GetAll();
        Person GetById(int id);
        IEnumerable<Person> GetByNameFilter(string filter);
        Person Add(Person person);
        void Delete(int id);

    }
}
