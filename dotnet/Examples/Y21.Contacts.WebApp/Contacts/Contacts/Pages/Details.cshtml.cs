using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Contacts.Data;
using Contacts.DTOs;
using Contacts.Entities;
using Contacts.Services;

namespace Contacts.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IContactService _contacts;

        public DetailsModel(IContactService contacts)
        {
            _contacts = contacts;
        }

        public ContactDto ContactDto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var contactDto = await _contacts.GetDtoByIdAsync(id);
            if (contactDto == null) return NotFound();
            ContactDto = contactDto;
            return Page();
        }
    }
}
