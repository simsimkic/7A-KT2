using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Serializer;
using BookingApp.Domain.Models.Guest1;
using BookingApp.Domain.RepositoryInterfaces.Guest1;


namespace BookingApp.Repository.Guest1
{
    public class RequestRepository:IRequestRepository

    {
    private const string filePath = "../../../Resources/Data/reservationRequests.csv";

    private readonly Serializer<MoveReservationRequest> serializer;

    private List<MoveReservationRequest> requests;

    public RequestRepository()
    {
        serializer = new Serializer<MoveReservationRequest>();
        requests = serializer.FromCSV(filePath);
    }

    public MoveReservationRequest Save(MoveReservationRequest request)
    {
        request.id = NextId();
        requests = serializer.FromCSV(filePath);
        requests.Add(request);
        serializer.ToCSV(filePath, requests);

        return request;
    }

    public List<MoveReservationRequest> GetByUserId(int id)
    {
        requests = serializer.FromCSV(filePath);
        var filteredRequests = requests.Where(x => x.userId == id);

        return filteredRequests.ToList();
    }

    private int NextId()
    {
        requests = serializer.FromCSV(filePath);
        if (requests.Count < 1)
            return 1;

        return requests.Max(x => x.id) + 1;
    }


    }
}
