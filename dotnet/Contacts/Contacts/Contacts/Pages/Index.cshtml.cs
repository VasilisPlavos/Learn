using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Contacts.DTOs;
using Contacts.Services;

namespace Contacts.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IContactService _contacts;

        public IndexModel(IContactService contacts)
        {
            _contacts = contacts;
        }

        public IList<ContactDto> ContactDto { get;set; }

        public async Task OnGetAsync()
        {
            ContactDto = await _contacts.GetDtosAsync();
        }
    }
}
