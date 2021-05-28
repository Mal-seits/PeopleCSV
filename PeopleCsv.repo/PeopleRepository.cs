using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PeopleCsv.data
{
    public class PeopleRepository
    {
        private readonly string _connectionString;
        public PeopleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Person> GetPeople()
        {
            using var context = new CSVDbContext(_connectionString);
            return context.People.ToList();
        }
        public void DeleteAllPeople()
        {
            using var context = new CSVDbContext(_connectionString);
            context.Database.ExecuteSqlInterpolated($"DELETE FROM People");
        }
    }
}
