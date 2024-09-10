namespace UserContactsApi.Services
{
    using UserContactProject.Interfaces;
    using UserContactsApi.Dtos;
    using UserContactsApi.Interfaces;

    /// <summary>
    /// Defines the <see cref="UserContactService" />
    /// </summary>
    public class UserContactService : IUserContactService
    {
        /// <summary>
        /// Defines the _userContactRepository
        /// </summary>
        private readonly IUserContactRepository _userContactRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserContactService"/> class.
        /// </summary>
        /// <param name="userContactRepository">The userContactRepository<see cref="IUserContactRepository"/></param>
        public UserContactService(IUserContactRepository userContactRepository)
        {
            _userContactRepository = userContactRepository;
        }

        /// <summary>
        /// The GetContactByIdAsync
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        public async Task<ContactDto> GetContactByIdAsync(int id)
        {
            return await _userContactRepository.GetContactByIdAsync(id);
        }

        /// <summary>
        /// The GetAllContactsAsync
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{ContactDto}}"/></returns>
        public async Task<IEnumerable<ContactDto>> GetAllContactsAsync()
        {
            return await _userContactRepository.GetAllContactsAsync();
        }

        /// <summary>
        /// The AddContactAsync
        /// </summary>
        /// <param name="contact">The contact<see cref="ContactDto"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        public async Task<ContactDto> AddContactAsync(ContactDto contact)
        {
            return await _userContactRepository.AddContactAsync(contact);
        }

        /// <summary>
        /// The UpdateContactAsync
        /// </summary>
        /// <param name="contact">The contact<see cref="ContactDto"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        public async Task<ContactDto> UpdateContactAsync(ContactDto contact)
        {
            return await _userContactRepository.UpdateContactAsync(contact);
        }

        /// <summary>
        /// The DeleteContactAsync
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool?}"/></returns>
        public async Task<bool?> DeleteContactAsync(int id)
        {
            return await _userContactRepository.DeleteContactAsync(id);
        }
    }
}
