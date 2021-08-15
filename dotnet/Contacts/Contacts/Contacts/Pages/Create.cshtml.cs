using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Contacts.Data;
using Contacts.DTOs;
using Contacts.Entities;
using Contacts.Services;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Pages
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        private readonly IContactService _contacts;

        public CreateModel(ApplicationDbContext db, IContactService contacts)
        {
            _db = db;
            _contacts = contacts;
        }

        public IList<ZipCode> ZipCode { get; set; }
        public async Task OnGetAsync()
        {
            ZipCode = await _db.ZipCodes.ToListAsync();
        }

        [BindProperty]
        public ContactDto ContactDto { get; set; }



        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _contacts.AddAsync(ContactDto);
            return RedirectToPage("./Index");
        }
    }
}
