using MediatR;
using Y25.CqrsExample.Domain.Entities;
using Y25.CqrsExample.Infrastructure.Databases;
using Y25.CqrsExample.Processors.Dtos;

namespace Y25.CqrsExample.Processors.Commands;

public class CreateContactCommand : IRequest<ContactDto>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

public class CreateContactCommandHandler(ApplicationDbContext db) : IRequestHandler<CreateContactCommand, ContactDto>
{
    public async Task<ContactDto> Handle(CreateContactCommand request, CancellationToken cancellationToken)
    {
        var contact = new Contact
        {
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        db.Contacts.Add(contact);
        await db.SaveChangesAsync(cancellationToken);
        return new ContactDto
        {
            Id = contact.Id,
            FirstName = contact.FirstName,
            LastName = contact.LastName
        };
    }
}