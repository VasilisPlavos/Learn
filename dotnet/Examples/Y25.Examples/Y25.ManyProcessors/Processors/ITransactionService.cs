using Microsoft.EntityFrameworkCore;
using Y25.Database;

namespace Y25.ManyProcessors.Processors;

public interface ITransactionService
{
    Task RunTransactionAsync(CancellationToken cancellationToken);
}

public class TransactionService : ITransactionService
{
    private readonly ApplicationDbContext _db;

    public TransactionService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task RunTransactionAsync(CancellationToken cancellationToken)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // Update products in the specified category
            var rowsAffected = await _db.Contacts
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.Name, "Maria"), cancellationToken: cancellationToken);

            Console.WriteLine($"{rowsAffected} rows updated.");

            //rowsAffected = await _db.Contacts.ExecuteDeleteAsync(cancellationToken);
            //throw new Exception("nada");

            // Commit the transaction
            await transaction.CommitAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            // Rollback in case of failure
            await transaction.RollbackAsync(cancellationToken);
            Console.WriteLine($"Transaction rolled back due to: {ex.Message}");
        }

    }
}