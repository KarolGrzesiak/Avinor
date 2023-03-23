// using Domain.Events;
// using Marten;
// using MediatR;
//
// namespace Application.Flights.Events;
//
// public class FlightCancelledHandler : INotificationHandler<FlightCancelled>
// {
//     private readonly IDocumentSession _session;
//
//     public FlightCancelledHandler(IDocumentSession session)
//     {
//         _session = session;
//     }
//     public async Task Handle(FlightCancelled notification, CancellationToken cancellationToken)
//     {
//         _session.Events.Append(notification.Id.ToString(),notification);
//         await _session.SaveChangesAsync(cancellationToken);
//     }
// };