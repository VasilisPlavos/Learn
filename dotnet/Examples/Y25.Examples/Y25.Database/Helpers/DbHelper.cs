using Bogus;
using Y25.Database.Entities;

namespace Y25.Database.Helpers;

public class DbHelper()
{
    private readonly ApplicationDbContext _db;

    public DbHelper(ApplicationDbContext db) : this()
    {
        _db = db;
    }
    public async Task GenerateFakeContactsAsync()
    {
        var contacts = GenerateFakeContacts(300);
        _db.Contacts.AddRange(contacts);
        await _db.SaveChangesAsync();
    }

    static List<Contact> GenerateFakeContacts(int count)
    {
        var faker = new Faker<Contact>()
            .RuleFor(u => u.Name, f => f.Name.FirstName())
            .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.Name));

        return faker.Generate(count);
    }
}