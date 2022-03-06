namespace Contact.API.Infrastructure.Repositories
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) 
        {}

        public DbSet<Models.Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<InfoType> InfoTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Data seed islemi icin
            modelBuilder.Entity<Models.Contact>().HasData(
                new Models.Contact { Id = 3, FirstName = "Steve", MiddleName = "", LastName = "Jobs", Company = "Apple" },
                new Models.Contact { Id = 4, FirstName = "Bill", MiddleName = "", LastName = "Gates", Company = "Microsoft" },
                new Models.Contact { Id = 5, FirstName = "Sergey", MiddleName = "", LastName = "Brin", Company = "Google" },
                new Models.Contact { Id = 6, FirstName = "Larry", MiddleName = "", LastName = "Page", Company = "Google" },
                new Models.Contact { Id = 7, FirstName = "Jeff", MiddleName = "", LastName = "Bezos", Company = "Amazon" },
                new Models.Contact { Id = 8, FirstName = "Elon", MiddleName = "", LastName = "Musk", Company = "Tesla" }
                );

            modelBuilder.Entity<InfoType>().HasData(
                new InfoType { Id = 1, Culture = "tr", Name = "email", DisplayName = "E-Posta" },
                new InfoType { Id = 2, Culture = "tr", Name = "cellphone", DisplayName = "Cep No" },
                new InfoType { Id = 3, Culture = "tr", Name = "telephone", DisplayName = "Telefon No" },
                new InfoType { Id = 4, Culture = "tr", Name = "location", DisplayName = "Konum" }
                );
            modelBuilder.Entity<ContactInfo>().HasData(
                new ContactInfo { Id = 1, ContactId = 2, InfoTypeId = 1, Value = "necdet.inkaya@inkaya.com" },
                new ContactInfo { Id = 2, ContactId = 2, InfoTypeId = 2, Value = "+12005550000" },
                new ContactInfo { Id = 3, ContactId = 2, InfoTypeId = 4, Value = "Istanbul, TR" },
                new ContactInfo { Id = 4, ContactId = 3, InfoTypeId = 1, Value = "steve.jobs@apple.com" },
                new ContactInfo { Id = 5, ContactId = 3, InfoTypeId = 2, Value = "+12015551111" },
                new ContactInfo { Id = 6, ContactId = 3, InfoTypeId = 4, Value = "Cupertino, CA" },
                new ContactInfo { Id = 7, ContactId = 4, InfoTypeId = 1, Value = "bill.gates@microsoft.com" },
                new ContactInfo { Id = 8, ContactId = 4, InfoTypeId = 2, Value = "+12025552222" },
                new ContactInfo { Id = 9, ContactId = 4, InfoTypeId = 4, Value = "Redmond, WA" },
                new ContactInfo { Id = 10, ContactId = 5, InfoTypeId = 1, Value = "sergey.brin@google.com" },
                new ContactInfo { Id = 11, ContactId = 5, InfoTypeId = 2, Value = "+12035553333" },
                new ContactInfo { Id = 12, ContactId = 5, InfoTypeId = 4, Value = "Mountain View, CA" },
                new ContactInfo { Id = 13, ContactId = 6, InfoTypeId = 1, Value = "larry.page@google.com" },
                new ContactInfo { Id = 14, ContactId = 6, InfoTypeId = 2, Value = "+12045554444" },
                new ContactInfo { Id = 15, ContactId = 6, InfoTypeId = 4, Value = "Mountain View, CA" },
                new ContactInfo { Id = 16, ContactId = 7, InfoTypeId = 1, Value = "jeff.bezos@amazon.com" },
                new ContactInfo { Id = 17, ContactId = 7, InfoTypeId = 2, Value = "+12055555555" },
                new ContactInfo { Id = 18, ContactId = 7, InfoTypeId = 4, Value = "Seattle, WA" },
                new ContactInfo { Id = 19, ContactId = 8, InfoTypeId = 1, Value = "elon.mask@tesla.com" },
                new ContactInfo { Id = 20, ContactId = 8, InfoTypeId = 2, Value = "+12065556666" },
                new ContactInfo { Id = 21, ContactId = 8, InfoTypeId = 4, Value = "Austin, TX" }
                );
        }
    }
}
