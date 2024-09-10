namespace UserContactProject.Interfaces
{
    using UserContactsApi.Dtos;

    /// <summary>
    /// Defines the <see cref="IUserContactRepository" />
    /// </summary>
    public interface IUserContactRepository
    {
        /// <summary>
        /// The GetContactByIdAsync
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        Task<ContactDto> GetContactByIdAsync(int id);

        /// <summary>
        /// The GetAllContactsAsync
        /// </summary>
        /// <returns>The <see cref="Task{IEnumerable{ContactDto}}"/></returns>
        Task<IEnumerable<ContactDto>> GetAllContactsAsync();

        /// <summary>
        /// The AddContactAsync
        /// </summary>
        /// <param name="contact">The contact<see cref="ContactDto"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        Task<ContactDto> AddContactAsync(ContactDto contact);

        /// <summary>
        /// The UpdateContactAsync
        /// </summary>
        /// <param name="contact">The contact<see cref="ContactDto"/></param>
        /// <returns>The <see cref="Task{ContactDto}"/></returns>
        Task<ContactDto> UpdateContactAsync(ContactDto contact);

        /// <summary>
        /// The DeleteContactAsync
        /// </summary>
        /// <param name="id">The id<see cref="int"/></param>
        /// <returns>The <see cref="Task{bool?}"/></returns>
        Task<bool?> DeleteContactAsync(int id);
    }
}
