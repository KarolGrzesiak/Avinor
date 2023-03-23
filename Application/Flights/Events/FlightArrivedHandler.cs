// using Domain.Events;
// using Marten;
// using MediatR;
//
// namespace Application.Flights.Events;
//
// public class FlightArrivedHandler : INotificationHandler<FlightArrived>
// {
//     private readonly IDocumentSession _session;
//
//     public FlightArrivedHandler(IDocumentSession session)
//     {
//         _session = session;
//     }
//     public async Task Handle(FlightArrived notification, CancellationToken cancellationToken)
//     {
//         _session.Events.Append(notification.Id.ToString(),notification);
//         await _session.SaveChangesAsync(cancellationToken);
//     }
// }