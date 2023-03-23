// using Domain.Events;
// using Marten;
// using MediatR;
//
// namespace Application.Flights.Events;
//
// public class FlightDepartedHandler : INotificationHandler<FlightDeparted>
// {
//     private readonly IDocumentSession _session;
//
//     public FlightDepartedHandler(IDocumentSession session)
//     {
//         _session = session;
//     }
//     public async Task Handle(FlightDeparted notification, CancellationToken cancellationToken)
//     {
//         _session.Events.Append(notification.Id.ToString(),notification);
//         await _session.SaveChangesAsync(cancellationToken);
//     }
// }