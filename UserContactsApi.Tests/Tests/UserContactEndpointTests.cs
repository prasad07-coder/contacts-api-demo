namespace UserContactsApi.Tests.Tests
{
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Moq;
    using System.Net;
    using System.Net.Http.Json;
    using UserContactsApi.Dtos;
    using UserContactsApi.Interfaces;

    /// <summary>
    /// Defines the <see cref="ContactEndpointsTests" />
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="ContactEndpointsTests"/> class.
    /// </remarks>
    /// <param name="factory">The factory<see cref="WebApplicationFactory{Program}"/></param>
    public class ContactEndpointsTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        /// <summary>
        /// Defines the _factory
        /// </summary>
        private readonly WebApplicationFactory<Program> _factory = factory;

        /// <summary>
        /// Defines the _mockService
        /// </summary>
        private readonly Mock<IUserContactService> _mockService = new();

        /// <summary>
        /// The GetAllContacts_ShouldReturnOkWithContacts
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetAllContacts_ShouldReturnOkWithContacts()
        {
            // Arrange
            var contacts = new List<ContactDto>
        {
            new() { Id = 1, FirstName = "Alice", LastName = "Smith", Email = "alice@example.com", Phone = "123456" },
            new() { Id = 2, FirstName = "Bob", LastName = "Johnson", Email = "bob@example.com", Phone = "654321" }
        };
            _mockService.Setup(service => service.GetAllContactsAsync()).ReturnsAsync(contacts);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockService.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/contacts");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<List<ContactDto>>();
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
        }

        /// <summary>
        /// The GetContactById_ShouldReturnOkWithContact_WhenContactExists
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetContactById_ShouldReturnOkWithContact_WhenContactExists()
        {
            // Arrange
            var contact = new ContactDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john@example.com", Phone = "123456789" };
            _mockService.Setup(service => service.GetContactByIdAsync(1)).ReturnsAsync(contact);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockService.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/contacts/1");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<ContactDto>();
            Assert.NotNull(result);
            Assert.Equal(contact.Id, result.Id);
        }

        /// <summary>
        /// The GetContactById_ShouldReturnNotFound_WhenContactDoesNotExist
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task GetContactById_ShouldReturnNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            var contact = new ContactDto { Id = 99, FirstName = "John", LastName = "Doe", Email = "john@example.com", Phone = "123456789" };
            _mockService.Setup(service => service.GetContactByIdAsync(contact.Id)).ReturnsAsync(contact);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockService.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.GetAsync("/contacts/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        /// <summary>
        /// The AddContact_ShouldReturnCreated_WhenContactIsAdded
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task AddContact_ShouldReturnCreated_WhenContactIsAdded()
        {
            // Arrange
            var contactDto = new ContactDto { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Phone = "123456789" };
            _mockService.Setup(service => service.AddContactAsync(It.IsAny<ContactDto>())).ReturnsAsync(contactDto);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockService.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/contacts", contactDto);

            // Assert
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            var result = await response.Content.ReadFromJsonAsync<ContactDto>();
            Assert.NotNull(result);
            Assert.Equal(contactDto.Id, result.Id);
        }


        /// <summary>
        /// The UpdateContact_ShouldReturnMethodNotAllowed_WhenHttpMethodIsNotAllowed
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task UpdateContact_ShouldReturnMethodNotAllowed_WhenHttpMethodIsNotAllowed()
        {
            // Arrange
            var contactId = 1;
            var updateDto = new ContactDto
            {
                Id = contactId,
                FirstName = "UpdatedFirstName",
                LastName = "UpdatedLastName",
                Email = "updated.email@example.com",
                Phone = "987654321"
            };

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockService.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"/contacts/{contactId}", updateDto);

            // Assert
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        //Create unit test when delete return false 

        /// <summary>
        /// The DeleteContact_ShouldReturnNotFound_WhenContactDoesNotExist
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task DeleteContact_ShouldReturnNotFound_WhenContactDoesNotExist()
        {
            // Arrange
            _mockService.Setup(service => service.DeleteContactAsync(1)).ReturnsAsync(false);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockService.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.DeleteAsync("/contacts/1");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
        /// <summary>
        /// The AddContact_ShouldReturnBadRequest_WhenContactIsInvalid
        /// </summary>
        /// <returns>The <see cref="Task"/></returns>
        [Fact]
        public async Task AddContact_ShouldReturnBadRequest_WhenContactIsInvalid()
        {
            // Arrange
            var invalidContactDto = new ContactDto { Id = 1, FirstName = "", LastName = "", Email = "invalid-email", Phone = "123" };
            _mockService.Setup(service => service.AddContactAsync(It.IsAny<ContactDto>())).ThrowsAsync(new ArgumentException("Invalid contact"));

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockService.Object);
                });
            }).CreateClient();

            // Act
            var response = await client.PostAsJsonAsync("/contacts", invalidContactDto);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
