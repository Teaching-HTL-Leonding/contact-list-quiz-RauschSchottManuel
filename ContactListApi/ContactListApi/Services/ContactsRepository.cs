using ContactListApi.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactListApi.Services
{
    public record Person(int Id, string FirstName, string LastName, string Email);

    public class ContactsRepository :IContactsRepository
    {
        List<Person> Persons { get; } = new();

        public IEnumerable<Person> GetAll() => Persons;

        public Person GetById(int id) => Persons.FirstOrDefault(p => p.Id == id);

        public IEnumerable<Person> GetByNameFilter(string filter) => Persons.Where(p => p.FirstName.Contains(filter) || p.LastName.Contains(filter)).ToList();

        public Person Add(Person person)
        {
            Persons.Add(person);
            return person;
        }

        public void Delete(int id)
        {
            var person = GetById(id);

            if(person is null)
                throw new ArgumentException("There exists no person with this id");

            Persons.Remove(person);
        }
    }
}
