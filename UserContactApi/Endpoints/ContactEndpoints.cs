namespace UserContactsApi.Endpoints
{
    using FluentValidation;
    using UserContactsApi.Dtos;
    using UserContactsApi.Interfaces;

    /// <summary>
    /// Defines the <see cref="ContactEndpoints" />
    /// </summary>
    public static class ContactEndpoints
    {
        /// <summary>
        /// The MapContactEndpoints
        /// </summary>
        /// <param name="routes">The routes<see cref="IEndpointRouteBuilder"/></param>
        public static void MapContactEndpoints(this IEndpointRouteBuilder routes)
        {
            // GET all contacts
            routes.MapGet("/contacts", async (IUserContactService contactService) =>
            {
                return Results.Ok(await contactService.GetAllContactsAsync());
            });

            // GET contact by Id
            routes.MapGet("/contacts/{id:int}", async (int id, IUserContactService contactService) =>
            {
                var contact = await contactService.GetContactByIdAsync(id);
                return contact is not null ? Results.Ok(contact) : Results.NotFound();
            });

            // POST a new contact
            routes.MapPost("/contacts", async (ContactDto contactDto, IUserContactService contactService, IValidator<ContactDto> validator) =>
            {
                ArgumentNullException.ThrowIfNull(contactDto);

                // Validate contactDto using ContactValidator
                var validationResult = await validator.ValidateAsync(contactDto);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                await contactService.AddContactAsync(contactDto);
                return Results.Created($"/contacts/{contactDto.Id}", contactDto);
            });

            // PUT to update a contact
            routes.MapPut("/contacts", async (ContactDto updatedContactDto, IUserContactService contactService, IValidator<ContactDto> validator) =>
            {
                // Validate updatedContactDto using ContactValidator
                var validationResult = await validator.ValidateAsync(updatedContactDto);
                if (!validationResult.IsValid)
                {
                    return Results.ValidationProblem(validationResult.ToDictionary());
                }

                var contact = await contactService.UpdateContactAsync(updatedContactDto);
                return contact is not null ? Results.Ok(contact) : Results.NotFound();
            });

            // DELETE a contact
            routes.MapDelete("/contacts/{id:int}", async (int id, IUserContactService contactService) =>
            {
                var result = await contactService.DeleteContactAsync(id);
                return result.HasValue && result.Value ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
