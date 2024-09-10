namespace UserContactsApi.Models
{
    /// <summary>
    /// Defines the <see cref="Contact" />
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Gets or sets the Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the FirstName
        /// </summary>
        public required string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the LastName
        /// </summary>
        public required string LastName { get; set; }

        /// <summary>
        /// Gets or sets the Email
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Gets or sets the Phone
        /// </summary>
        public required string Phone { get; set; }

        /// <summary>
        /// Gets or sets the Created
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// Gets or sets the Updated
        /// </summary>
        public DateTime? Updated { get; set; }
    }
}
