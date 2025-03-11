using MediatR;
using Y25.CqrsExample.Infrastructure.Databases;
using Y25.CqrsExample.Processors.Dtos;

namespace Y25.CqrsExample.Processors.Queries;

public class GetContactsQuery : IRequest<IQueryable<ContactDto>>
{ }

public class GetContactsQueryHandler(ApplicationDbContext db) : IRequestHandler<GetContactsQuery, IQueryable<ContactDto>>
{
    public Task<IQueryable<ContactDto>> Handle(GetContactsQuery request, CancellationToken cancellationToken)
    {
        var result = db.Contacts.Select(x => new ContactDto{ FirstName = x.FirstName, LastName = x.LastName, Id = x.Id});
        return Task.FromResult(result);
    }
}