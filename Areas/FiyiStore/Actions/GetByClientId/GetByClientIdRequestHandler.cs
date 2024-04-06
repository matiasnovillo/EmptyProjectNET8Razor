using FiyiStore.Areas.FiyiStore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FiyiStore.Areas.FiyiStore.Actions.GetByClientId;
    public class GetByClientIdRequestHandler : IRequestHandler<GetByClientIdRequest, GetByClientIdResponse>
    {
        public readonly IClientRepository _clientRepository;    

        public GetByClientIdRequestHandler(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        public async Task<GetByClientIdResponse> Handle(GetByClientIdRequest request, CancellationToken cancellationToken)
        {
            var Client = await _clientRepository
                                    .AsQueryable()
                                    .Where(x => x.ClientId == request.ClienId)
                                    .FirstOrDefaultAsync();

            return new GetByClientIdResponse { Client = Client };
        }
    }
