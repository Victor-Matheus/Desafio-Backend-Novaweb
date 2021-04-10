using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace contacts.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // var stringListToJson = new ValueConverter<IList<ValueObjects.PhoneNumber>, string>(
            //     v => (v.Equals("undefined") || v.Equals("null")) ? JsonConvert.SerializeObject("[\"\"]") : JsonConvert.SerializeObject(v),
            //     v => (String.IsNullOrEmpty(v)) ? new[] { "" } : JsonConvert.DeserializeObject<IList<string>>(v)
            // );

            modelBuilder.Entity<Models.Contact>().ToTable("contacts");
            modelBuilder.Entity<Models.Contact>().HasKey(x => x.Id);
            modelBuilder.Entity<Models.Contact>().OwnsOne(x => x.Name).Property(z => z.FirstName).HasColumnName("first_name");
            modelBuilder.Entity<Models.Contact>().OwnsOne(x => x.Name).Property(z => z.LastName).HasColumnName("last_name");
            modelBuilder.Entity<Models.Contact>().OwnsOne(x => x.Name).Ignore(z => z.Notifications);
            modelBuilder.Entity<Models.Contact>().OwnsOne(x => x.Email).Property(z => z.Address).HasColumnName("email_address");
            modelBuilder.Entity<Models.Contact>().OwnsOne(x => x.Email).Ignore(z => z.Notifications);
            modelBuilder.Entity<Models.Contact>().OwnsMany(x => x.PhoneNumbers).Property(z => z.Number);
            modelBuilder.Entity<Models.Contact>().OwnsMany(x => x.PhoneNumbers).Ignore(z => z.Notifications);

        }
    }
}