using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Contacts.Data;
using Contacts.DTOs;
using Contacts.Entities;
using Contacts.Services;

namespace Contacts.Pages
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IContactService _contacts;

        public EditModel(ApplicationDbContext db, IContactService contacts)
        {
            _db = db;
            _contacts = contacts;
        }

        [BindProperty]
        public ContactDto ContactDto { get; set; }
        public IList<ZipCode> ZipCodes { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ContactDto = await _contacts.GetDtoByIdAsync(id);
            ZipCodes = await _db.ZipCodes.ToListAsync();

            if (ContactDto == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var changesSaved = await _contacts.UpdateContact(ContactDto);
            if (!changesSaved) return NotFound();
            return RedirectToPage("./Index");
        }
    }
}
