﻿using Application.DTOs.Support.PracticeSetup;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tickets.Queries
{
    public class GetTicketAssigneeByQuery : IRequest<Response<PracticeSetupViewModel>>
    {
        public long PracticeId;
        public GetTicketAssigneeByQuery(long practiceId)
        {
            PracticeId = practiceId;
        }

    }
    public class GetTicketAssigneeHandler : IRequestHandler<GetTicketAssigneeByQuery, Response<PracticeSetupViewModel>>
    {
        private readonly IMapper _mapper;
        private readonly ITicketRepositoryAsync _ticketRepo;
        public GetTicketAssigneeHandler(IMapper mapper, ITicketRepositoryAsync repo)
        {
            _mapper = mapper;
            _ticketRepo = repo;
        }
        public async Task<Response<PracticeSetupViewModel>> Handle(GetTicketAssigneeByQuery request, CancellationToken cancellationToken)
        {
            var response = await _ticketRepo.GetTicketAssignee(request.PracticeId);
            return new Response<PracticeSetupViewModel>(_mapper.Map<PracticeSetupViewModel>(response));
        }
    }
}
