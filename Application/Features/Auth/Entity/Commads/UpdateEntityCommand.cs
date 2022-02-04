using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Auth.Entity.Commads
{
    public class UpdateEntityCommand : IRequest<Response<int>>
    {
        [Required]
        public long AuthEntityId { get; set; }
        public string PaymentGateway { get; set; }
        public virtual string AccountType { get; set; }
        public  string PracticeSetup { get; set; }

    }

    public class UpdateEntityCommandHandler : IRequestHandler<UpdateEntityCommand, Response<int>>
    {
        private readonly IEntityRepositoryAsync _entityRepository;
        private readonly IMapper _mapper;
        public UpdateEntityCommandHandler(IEntityRepositoryAsync entityRepository, IMapper mapper)
        {
            _entityRepository = entityRepository;
            _mapper = mapper;
        }
        public async Task<Response<int>> Handle(UpdateEntityCommand request, CancellationToken cancellationToken)
        {
            var entityObject = await _entityRepository.UpdateAsync(request.AuthEntityId, request.PaymentGateway, request.AccountType, request.PracticeSetup).ConfigureAwait(false);
            return new Response<int>(_mapper.Map<int>(entityObject));
        }
    }
}