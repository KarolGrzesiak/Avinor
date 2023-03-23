// using Domain.Events;
// using Marten;
// using MediatR;
//
// namespace Application.Flights.Events;
//
// public class FlightInfoChangedHandler : INotificationHandler<FlightInfoChanged>
// {
//     private readonly IDocumentSession _session;
//
//     public FlightInfoChangedHandler(IDocumentSession session)
//     {
//         _session = session;
//     }
//     public async Task Handle(FlightInfoChanged notification, CancellationToken cancellationToken)
//     {
//         _session.Events.Append(notification.Id.ToString(),notification);
//         await _session.SaveChangesAsync(cancellationToken);
//     }
// }