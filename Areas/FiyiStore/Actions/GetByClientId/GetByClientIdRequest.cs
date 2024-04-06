using MediatR;

namespace FiyiStore.Areas.FiyiStore.Actions.GetByClientId;
    public class GetByClientIdRequest : IRequest<GetByClientIdResponse>
    {
            public int ClienId { get; set; }
    }
