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
    public class DeleteModel : PageModel
    {
        private readonly IContactService _contacts;

        public DeleteModel(IContactService contacts)
        {
            _contacts = contacts;
        }

        [BindProperty]
        public ContactDto ContactDto { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactDto = await _contacts.GetDtoByIdAsync(id);
            
            if (ContactDto == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            await _contacts.RemoveByIdAsync(id);
            return RedirectToPage("./Index");
        }
    }
}
