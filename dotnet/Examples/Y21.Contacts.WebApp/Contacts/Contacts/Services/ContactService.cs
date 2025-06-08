using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Data;
using Contacts.DTOs;
using Contacts.Entities;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Services
{
    public interface IContactService
    {
        Task AddAsync(ContactDto contactDto);
        Task<IList<ContactDto>> GetDtosAsync();
        Task<ContactDto> GetDtoByIdAsync(int? id);
        Task RemoveByIdAsync(int? contactId);
        Task<bool> UpdateContact(ContactDto contactDto);
    }

    public class ContactService : IContactService
    {
        private readonly ApplicationDbContext _db;
        public ContactService(ApplicationDbContext db) { _db = db; }
        public async Task AddAsync(ContactDto contactDto)
        {
            if (!EmailIsValid(contactDto.Email)) return;
            if (!MobileIsValid(contactDto.MobilePhone)) return;

            var contact = new Contact
            {
                Email = contactDto.Email,
                FirstName = contactDto.FirstName,
                LastName = contactDto.LastName,
                LocalPhone = contactDto.LocalPhone,
                MobilePhone = contactDto.MobilePhone,
                ZipCodeId = contactDto.ZipCode.Id
            };

            await _db.Contacts.AddAsync(contact);
            await _db.SaveChangesAsync();
        }

        public async Task<ContactDto> GetDtoByIdAsync(int? id)
        {
            if (id == null) return null;
            var contactIds = new List<int?> { id };
            var contacts = await GetDtosByIdsAsync(contactIds);
            return contacts.FirstOrDefault();
        }

        private async Task<IList<ContactDto>> GetDtosByIdsAsync(List<int?> contactIds)
        {
            var contacts = await _db.Contacts.Where(x => contactIds.Contains(x.Id)).ToListAsync();
            var zipCodeIds = contacts.Select(x => x.ZipCodeId).ToList();
            var zipCodes = await _db.ZipCodes.Where(x => zipCodeIds.Contains(x.Id)).ToListAsync();
            var contactsDto = MakeDtosList(contacts, zipCodes);
            return contactsDto;
        }

        public async Task<IList<ContactDto>> GetDtosAsync()
        {
            var contacts = await _db.Contacts.ToListAsync();
            var zipCodeIds = contacts.Select(x => x.ZipCodeId).ToList();
            var zipCodes = await _db.ZipCodes.Where(x => zipCodeIds.Contains(x.Id)).ToListAsync();

            var contactsDto = MakeDtosList(contacts, zipCodes);
            return contactsDto;
        }

        private List<ContactDto> MakeDtosList(List<Contact> contacts, List<ZipCode> zipCodes)
        {
            return contacts.Select(contact => new ContactDto
                {
                    Id = contact.Id,
                    Email = contact.Email,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    LocalPhone = contact.LocalPhone,
                    MobilePhone = contact.MobilePhone,
                    ZipCode = zipCodes.FirstOrDefault(x => x.Id == contact.ZipCodeId),
                })
                .ToList();
        }

        public async Task RemoveByIdAsync(int? contactId)
        {
            if (contactId == null) return;
            var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == contactId);
            if (contact != null)
            {
                _db.Contacts.Remove(contact);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<bool> UpdateContact(ContactDto contactDto)
        {
            if (!EmailIsValid(contactDto.Email)) return false;
            if (!MobileIsValid(contactDto.MobilePhone)) return false;

            var contact = await _db.Contacts.FirstOrDefaultAsync(x => x.Id == contactDto.Id);
            if (contact == null) return false;

            contact.Email = contactDto.Email;
            contact.FirstName = contactDto.FirstName;
            contact.LastName = contactDto.LastName;
            contact.LocalPhone = contactDto.LocalPhone;
            contact.MobilePhone = contactDto.MobilePhone;
            contact.ZipCodeId = contactDto.ZipCode.Id;

            _db.Contacts.Update(contact);
            await _db.SaveChangesAsync();
            return true;
        }



        private bool MobileIsValid(string mobilePhone)
        {
            if (mobilePhone.StartsWith("+3069") || mobilePhone.StartsWith("3069") || mobilePhone.StartsWith("003069") || mobilePhone.StartsWith("69"))
                return false;

            if (mobilePhone.Length is > 9 and < 14)
                return false;

            return true;
        }

        private bool EmailIsValid(string email)
        {
            if (!email.Contains("@"))
                return false;

            if (email.Split(" ").Length > 1)
                return false;

            return true;
        }
    }




}
