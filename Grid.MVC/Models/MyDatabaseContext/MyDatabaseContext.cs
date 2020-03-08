using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Grid.MVC.Models.DatabaseContext
{
    public class MyDatabaseContext:DbContext
    {
        public DbSet<Personal> Personals { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<Address> Addresses { get; set; }
        public MyDatabaseContext()
        {
            Database.SetInitializer(new DatabaseGenerator());
        }
    }

    public class DatabaseGenerator : CreateDatabaseIfNotExists<MyDatabaseContext>
    {
        protected override void Seed(MyDatabaseContext context)
        {
            List<Country> countries = new List<Country>();
            for (int i = 0; i < 10; i++)
            {
                Country country = new Country()
                {
                    Name = FakeData.PlaceData.GetCountry()
                };
                countries.Add(country);
                context.Countries.Add(country);
            }
            context.SaveChanges();

            List<Address> addresses = new List<Address>();
            for (int i = 0; i < 100; i++)
            {
                Address address = new Address()
                {
                    Name = FakeData.PlaceData.GetAddress(),
                    Country = countries[FakeData.NumberData.GetNumber(1,10)]
                };
                addresses.Add(address);
                context.Addresses.Add(address);
            }

            context.SaveChanges();

            for (int i = 0; i < 100; i++)
            {
                Personal personal = new Personal()
                {
                    Name = FakeData.NameData.GetFirstName(),
                    Surname = FakeData.NameData.GetSurname(),
                    Age = FakeData.NumberData.GetNumber(18, 65),
                    Address = addresses[i]
                };
                context.Personals.Add(personal);
            }
            context.SaveChanges();
        }
    }
}