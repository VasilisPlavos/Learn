using MediatR;
using Microsoft.AspNetCore.Mvc;
using Y25.CqrsExample.Processors.Commands;
using Y25.CqrsExample.Processors.Queries;

namespace Y25.CqrsExample.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController(IMediator mediator) : ControllerBase
    {
        [HttpGet(Name = "GetContacts")]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await mediator.Send(new GetContactsQuery());
            return Ok(contacts);
        }

        [HttpPost(Name = "CreateContact")]
        public async Task<IActionResult> CreateContact(CreateContactCommand command)
        {
            var contact = await mediator.Send(command);
            return Ok(contact);
        }
    }
}
