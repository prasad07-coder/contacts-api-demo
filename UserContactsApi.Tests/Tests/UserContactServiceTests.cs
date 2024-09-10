namespace UserContactsApi.Tests
{
    using Moq;
    using UserContactProject.Interfaces;
    using UserContactsApi.Dtos;
    using UserContactsApi.Services;

    /// <summary>
    /// Defines the <see cref="UserContactServiceTests" />
    /// </summary>
    public class UserContactServiceTests
    {
        /// <summary>
        /// Defines the _mockRepository
        /// </summary>
        private readonly Mock<IUserContactRepository> _mockRepository;

        /// <summary>
        /// Defines the _service
        /// </summary>
        private readonly UserContactService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContactServiceTests"/> class.
        /// </summary>
        public UserContactServiceTests()
        {
            _mockRepository = new Mock<IUserContactRepository>();
            _service = new UserContactService(_mockRepository.Object);
        }

        /// <summary>
        /// The GetContactByIdAsync_ShouldReturnContact_WhenContactExists
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetContactByIdAsync_ShouldReturnContact_WhenContactExists()
        {
            // Arrange
            var contactId = 1;
            var contactDto = new ContactDto
            {
                Id = contactId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                Phone = "123456789"
            };
            _mockRepository.Setup(repo => repo.GetContactByIdAsync(contactId))
                .ReturnsAsync(contactDto);

            // Act
            var result = await _service.GetContactByIdAsync(contactId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(contactId, result.Id);
            Assert.Equal("John", result.FirstName);
        }

        /// <summary>
        /// The GetAllContactsAsync_ShouldReturnAllContacts
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetAllContactsAsync_ShouldReturnAllContacts()
        {
            // Arrange
            var contacts = new List<ContactDto>
            {
                new ContactDto { Id = 1, FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com", Phone = "111222333" },
                new ContactDto { Id = 2, FirstName = "Bob", LastName = "Smith", Email = "bob@example.com", Phone = "444555666" }
            };
            _mockRepository.Setup(repo => repo.GetAllContactsAsync())
                .ReturnsAsync(contacts);

            // Act
            var result = await _service.GetAllContactsAsync();

            // Assert
            Assert.Equal(2, result.Count());
        }

        /// <summary>
        /// The AddContactAsync_ShouldAddNewContact
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task AddContactAsync_ShouldAddNewContact()
        {
            // Arrange
            var contactDto = new ContactDto
            {
                FirstName = "Jane",
                LastName = "Doe",
                Email = "jane.doe@example.com",
                Phone = "987654321"
            };
            _mockRepository.Setup(repo => repo.AddContactAsync(contactDto))
                .ReturnsAsync(contactDto);

            // Act
            var result = await _service.AddContactAsync(contactDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Jane", result.FirstName);
            Assert.Equal("Doe", result.LastName);
        }

        /// <summary>
        /// The UpdateContactAsync_ShouldUpdateExistingContact
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task UpdateContactAsync_ShouldUpdateExistingContact()
        {
            // Arrange
            var contactDto = new ContactDto
            {
                Id = 1,
                FirstName = "Tom",
                LastName = "Hardy",
                Email = "tom.hardy@example.com",
                Phone = "777888999"
            };
            _mockRepository.Setup(repo => repo.UpdateContactAsync(contactDto))
                .ReturnsAsync(contactDto);

            // Act
            var result = await _service.UpdateContactAsync(contactDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Tom", result.FirstName);
            Assert.Equal("Hardy", result.LastName);
        }

        /// <summary>
        /// The DeleteContactAsync_ShouldCallRepositoryDeleteMethod
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task DeleteContactAsync_ShouldCallRepositoryDeleteMethod()
        {
            // Arrange
            var contactId = 1;
            _mockRepository.Setup(repo => repo.DeleteContactAsync(contactId))
                .Verifiable();

            // Act
            await _service.DeleteContactAsync(contactId);

            // Assert
            _mockRepository.Verify(repo => repo.DeleteContactAsync(contactId), Times.Once);
        }
    }
}
