using System.Collections.Generic;

namespace MLCacheExamples
{
    using System.Linq;

    public class UserRepository
    {
        private IEnumerable<User> data = new List<User>
        {
            new User { Id = 1, Email = "john.smith@gmail.com", FirstName = "John", LastName = "Smith"},
            new User { Id = 2, Email = "pero.peric@gmail.com", FirstName = "Pero", LastName = "Peric"},
            new User { Id = 3, Email = "marko.markovic@gmail.com", FirstName = "Marko", LastName = "Markovic"},
            new User { Id = 4, Email = "ivan.horvat@gmail.com", FirstName = "Ivan", LastName = "Horvat"},
            new User { Id = 5, Email = "mirko.perkovic@gmail.com", FirstName = "Mirko", LastName = "Perkovic"},
            new User { Id = 6, Email = "ante.antic@gmail.com", FirstName = "ante", LastName = "Antic"},
        };

        public IEnumerable<User> GetAll()
        {
            return data;
        }

        public User Get(int id)
        {
            return data.First(user => user.Id == id);
        }

        public User GetByEmail(string email)
        {
            return data.First(user => user.Email == email);
        }

        public IEnumerable<User> Search(string query)
        {
            return data.Where(user => user.Email.Contains(query) || user.FirstName.Contains(query) || user.LastName.Contains(query)).ToList();
        }
    } 
}
