namespace UserContactsApi.Data
{
    using Microsoft.EntityFrameworkCore;
    using UserContactsApi.Models;

    /// <summary>
    /// Defines the <see cref="UserContactsDbContext" />
    /// </summary>
    public class UserContactsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserContactsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options<see cref="DbContextOptions{UserContactsDbContext}"/></param>
        public UserContactsDbContext(DbContextOptions<UserContactsDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the Contacts
        /// </summary>
        public DbSet<Contact> Contacts { get; set; }
    }
}
