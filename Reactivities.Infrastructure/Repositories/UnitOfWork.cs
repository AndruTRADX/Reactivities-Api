using MediatR;
using Reactivities.Application.Contracts.Persistence;
using Reactivities.Domain.Common;
using Reactivities.Infrastructure.Persistence;

namespace Reactivities.Infrastructure.Repositories;

public class UnitOfWork(AppDbContext context, IPublisher publisher) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private readonly IPublisher _publisher = publisher;
    private readonly Dictionary<Type, object> _repositories = [];

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entitiesWithEvents = _context.ChangeTracker
            .Entries<BaseDomainModel>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Count != 0)
            .ToList();

        var result = await _context.SaveChangesAsync(cancellationToken);

        foreach (var entity in entitiesWithEvents)
        {
            var domainEvents = entity.DomainEvents.ToList();
            entity.ClearDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent, cancellationToken);
            }
        }

        return result;
    }

    public IAsyncRepository<TEntity> Repository<TEntity>() where TEntity : BaseDomainModel
    {
        var type = typeof(TEntity);

        if (!_repositories.TryGetValue(type, out var repository))
        {
            repository = new RepositoryBase<TEntity>(_context);
            _repositories[type] = repository;
        }

        return (IAsyncRepository<TEntity>)repository;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();
        }
    }
}
