using FiyiStore.Areas.BasicCore;
using FiyiStore.Areas.FiyiStore.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FiyiStore.Areas.FiyiStore.Actions.GetAll;
    public class GetAllRequestHandler : IRequestHandler<GetAllRequest, GetAllResponse>
    {
        public readonly IClientRepository _clientRepository;    

        public GetAllRequestHandler(IClientRepository clientRepository) 
        {
            _clientRepository = clientRepository;
        }

        public async Task<GetAllResponse> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            throw new Exception("Hola puto");

            var lstClient = await _clientRepository.AsQueryable().ToListAsync();

            return new GetAllResponse { lstClient = lstClient };
        }
    }
