namespace Contact.API.Infrastructure.Repositories
{
    public class ContactDbContext : DbContext
    {
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options) 
        {}

        public DbSet<Models.Contact> Contacts { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<InfoType> InfoTypes { get; set; }

    }
}
