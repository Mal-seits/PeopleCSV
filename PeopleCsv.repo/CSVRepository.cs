using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;


namespace PeopleCsv.data
{
    public class CSVRepository
    {
        private readonly string _connectionString;
        public CSVRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        private List<Person> GetPeopleByAmount(int amount)
        {
            var result = new List<Person>();
            for(int i = 0; i < amount; i++)
            {
                var person = new Person
                {
                    Id = 0,
                    FirstName = Faker.Name.First(),
                    LastName = Faker.Name.Last(),
                    Address = Faker.Address.StreetAddress(),
                    Age = Faker.RandomNumber.Next(1, 80),
                    Email = Faker.Internet.Email()

                };
                result.Add(person);    
            }
            return result;
        }
        public string GetCSV(int amount)
        {
            var people = GetPeopleByAmount(amount);
            return GenerateCSV(people);
        }
        private string GenerateCSV(List<Person> list)
        {
            var builder = new StringBuilder();
            var stringWriter = new StringWriter(builder);
            using var csv = new CsvWriter(stringWriter, CultureInfo.InvariantCulture);
            csv.WriteRecords(list);
            return builder.ToString();
        }
        private List<Person> ConvertFromCsv(byte[] csvBytes)
        {
            using var memoryStream = new MemoryStream(csvBytes);
            using var reader = new StreamReader(memoryStream);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csv.GetRecords<Person>().ToList();
        }
        public void AddPeople(byte[] csvBytes)
        {
            var people = ConvertFromCsv(csvBytes);
            using var context = new CSVDbContext(_connectionString);
            foreach(Person person in people)
            {
                context.Add(person);
            }
            context.SaveChanges();

        }
    }
}
