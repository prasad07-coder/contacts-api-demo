namespace UserContactsApi.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using UserContactProject.Interfaces;
    using UserContactsApi.Data;
    using UserContactsApi.Dtos;
    using UserContactsApi.Models;

    /// <summary>
    /// Defines the <see cref="UserContactRepository" />
    /// </summary>
    public class UserContactRepository : IUserContactRepository
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly UserContactsDbContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContactRepository"/> class.
        /// </summary>
        /// <param name="context">The context<see cref="UserContactsDbContext"/></param>
        public UserContactRepository(UserContactsDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// The GetContactByIdAsync
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        public async Task<ContactDto> GetContactByIdAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                throw new ArgumentException("Contact not found"); ;
            }

            return new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone
            };
        }

        /// <summary>
        /// The GetAllContactsAsync
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{ContactDto}}"/></returns>
        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            return await _context.Contacts.Select(contact => new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone
            }).ToListAsync();
        }

        /// <summary>
        /// The AddContactAsync
        /// </summary>
        /// <param name="contactDto">The contactDto<see cref="ContactDto"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        public async Task<ContactDto> AddContactAsync(ContactDto contactDto)
        {
            var contact = new Contact
            {
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                Email = contactDto.Email,
                Phone = contactDto.Phone,
                Created = DateTime.Now
            };

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            return new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone
            };
        }

        /// <summary>
        /// The UpdateContactAsync
        /// </summary>
        /// <param name="contactDto">The contactDto<see cref="ContactDto"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        public async Task<ContactDto> UpdateContactAsync(ContactDto contactDto)
        {
            var contact = await _context.Contacts.FindAsync(contactDto.Id);
            if (contact == null)
            {
                throw new ArgumentException("Contact not found");
            }

            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;
            contact.Email = contactDto.Email;
            contact.Phone = contactDto.Phone;
            contact.Updated = DateTime.Now;

            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();

            return new ContactDto
            {
                Id = contact.Id,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                Email = contact.Email,
                Phone = contact.Phone
            };
        }

        /// <summary>
        /// The DeleteContactAsync
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool?}"/></returns>
        public async Task<bool?> DeleteContactAsync(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);
            if (contact == null)
            {
                return null;
            }

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
