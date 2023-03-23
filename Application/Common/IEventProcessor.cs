using Domain.Entities;

namespace Application.Common;

public interface IEventProcessor
{
    Task ProcessAsync(AggregateBase aggregate, CancellationToken cancellationToken);
}