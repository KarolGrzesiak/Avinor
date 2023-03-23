using Domain.Events;
using Marten;
using MediatR;

namespace Application.Flights.Events;

public class FlightCreatedHandler : INotificationHandler<FlightCreated>
{
    private readonly IDocumentSession _session;

    public FlightCreatedHandler(IDocumentSession session)
    {
        _session = session;
    }

    public async Task Handle(FlightCreated notification, CancellationToken cancellationToken)
    {
        _session.Events.StartStream(notification.Id.ToString(), notification);
        await _session.SaveChangesAsync(cancellationToken);
    }
}