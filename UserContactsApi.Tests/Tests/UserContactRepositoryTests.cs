namespace UserContactsApi.Tests
{
    using Microsoft.EntityFrameworkCore;
    using UserContactsApi.Data;
    using UserContactsApi.Dtos;
    using UserContactsApi.Models;
    using UserContactsApi.Repositories;

    /// <summary>
    /// Defines the <see cref="UserContactRepositoryTests" />
    /// </summary>
    public class UserContactRepositoryTests : IDisposable
    {
        /// <summary>
        /// Defines the _context
        /// </summary>
        private readonly UserContactsDbContext _context;

        /// <summary>
        /// Defines the _repository
        /// </summary>
        private readonly UserContactRepository _repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContactRepositoryTests"/> class.
        /// </summary>
        public UserContactRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<UserContactsDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

            _context = new UserContactsDbContext(options);
            _repository = new UserContactRepository(_context);
        }

        /// <summary>
        /// The AddContactAsync_ShouldAddNewContact
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            // Arrange
            var contact = new ContactDto
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Phone = "123456789"
            };

            // Act
            var result = await _repository.AddContactAsync(contact);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact.FirstName, result.FirstName);
            Assert.Equal(contact.LastName, result.LastName);
            Assert.NotEqual(0, result.Id); // ID should be generated
        }

        /// <summary>
        /// The GetContactByIdAsync_ShouldReturnContact
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetContactByIdAsync_ShouldReturnContact()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "Jane",
                LastName = "Smith",
                Email = "jane@example.com",
                Phone = "987654321",
                Created = DateTime.Now
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetContactByIdAsync(contact.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contact.FirstName, result.FirstName);
        }

        /// <summary>
        /// The GetAllContactsAsync_ShouldReturnAllContacts
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetAllContactsAsync_ShouldReturnAllContacts()
        {
            // Arrange
            _context.Contacts.Add(new Contact
            {
                FirstName = "Alice",
                LastName = "Brown",
                Email = "alice@example.com",
                Phone = "111222333",
                Created = DateTime.Now
            });
            _context.Contacts.Add(new Contact
            {
                FirstName = "Bob",
                LastName = "Taylor",
                Email = "bob@example.com",
                Phone = "444555666",
                Created = DateTime.Now
            });
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllContactsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        /// <summary>
        /// The UpdateContactAsync_ShouldUpdateExistingContact
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task UpdateContactAsync_ShouldUpdateExistingContact()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "Chris",
                LastName = "Johnson",
                Email = "chris@example.com",
                Phone = "777888999",
                Created = DateTime.Now
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            var contactDto = new ContactDto
            {
                Id = contact.Id,
                FirstName = "Chris",
                LastName = "Updated",
                Email = "updated@example.com",
                Phone = "000111222"
            };

            // Act
            var result = await _repository.UpdateContactAsync(contactDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactDto.LastName, result.LastName);
            Assert.Equal(contactDto.Email, result.Email);
        }

        /// <summary>
        /// The DeleteContactAsync_ShouldRemoveContact
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task DeleteContactAsync_ShouldRemoveContact()
        {
            // Arrange
            var contact = new Contact
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Phone = "123456789",
                Created = DateTime.Now
            };
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.DeleteContactAsync(contact.Id);

            // Assert
            Assert.True(result);
        }

        /// <summary>
        /// The UpdateContactAsync_ContactNotFound_ShouldReturnNull
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task UpdateContactAsync_ContactNotFound_ShouldReturnNull()
        {
            // Arrange
            var contactDto = new ContactDto
            {
                Id = 999, // Assuming this ID does not exist in the database
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Phone = "123456789"
            };

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _repository.UpdateContactAsync(contactDto));
        }

        //Create unit test for delete when id not found

        /// <summary>
        /// The DeleteContactAsync_ContactNotFound_ShouldReturnFalse
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task DeleteContactAsync_ContactNotFound_ShouldReturnFalse()
        {
            // Arrange
            var contactDto = new ContactDto
            {
                Id = 999, // Assuming this ID does not exist in the database
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Phone = "123456789"
            };

            // Act
            var result = await _repository.DeleteContactAsync(contactDto.Id);

            // Assert
            Assert.Null(result);
        }

        // Clean up the database

        /// <summary>
        /// The Dispose
        /// </summary>
        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }

}
