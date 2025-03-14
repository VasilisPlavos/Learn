using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Threading;
using Y25.Database;

namespace Y25.ExecuteUpdateAsync;

public class ConsoleApp8Service : IHostedLifecycleService
{
    private readonly ApplicationDbContext _db;

    public ConsoleApp8Service(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartAsync));
        await RunTransactionAsync(cancellationToken);        
        await Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StopAsync));
        await Task.CompletedTask;
    }

    public async Task StartingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartingAsync));
        await Task.CompletedTask;
    }

    public async Task StartedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StartedAsync));
        await Task.CompletedTask;
    }

    public async Task StoppingAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StoppingAsync));
        await Task.CompletedTask;
    }

    public async Task StoppedAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine(nameof(StoppedAsync));
        await Task.CompletedTask;
    }
    private async Task RunTransactionAsync(CancellationToken cancellationToken)
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