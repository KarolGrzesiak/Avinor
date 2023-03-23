// using Domain.Events;
// using Marten;
// using MediatR;
//
// namespace Application.Flights.Events;
//
// public class FlightTimeChangedHandler : INotificationHandler<FlightTimeChanged>
// {
//     private readonly IDocumentSession _session;
//
//     public FlightTimeChangedHandler(IDocumentSession session)
//     {
//         _session = session;
//     }
//     public async Task Handle(FlightTimeChanged notification, CancellationToken cancellationToken)
//     {
//         _session.Events.Append(notification.Id.ToString(),notification);
//         await _session.SaveChangesAsync(cancellationToken);
//     }
// }