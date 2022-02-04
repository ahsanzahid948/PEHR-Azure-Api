using Application.DTOs;
using Application.DTOs.Notes;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities.Notes;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Clients.Queries
{
    public class GetClientNoteDetailByIdQuery : IRequest<Response<NotesView_VM>>
    {
        public long TicketId;
        public long CommentId;
        public PayLoad payload;
        public GetClientNoteDetailByIdQuery(PayLoad _payload, long entitySeq, long commentId)
        {
            payload = _payload;
            TicketId = entitySeq;
            CommentId = commentId;
        }
    }
    public class GetClientNoteDetailHandler : IRequestHandler<GetClientNoteDetailByIdQuery, Response<NotesView_VM>>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepositoryAsync _noteRepo;
        public GetClientNoteDetailHandler(IMapper _mapper, INoteRepositoryAsync noteRepo)
        {
            this._mapper = _mapper;
            _noteRepo = noteRepo;
        }

        public async Task<Response<NotesView_VM>> Handle(GetClientNoteDetailByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _noteRepo.TicketClientNotes(request.payload, request.TicketId
                , request.CommentId);
            return new Response<NotesView_VM>(response);

        }
    }
}
