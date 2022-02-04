using Application.DTOs.Notes;
using Application.Interfaces.Repositories;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Notes
{
    public class GetNoteAttachmentsByIdQuery : IRequest<Response<List<TicketAttachmentsViewModel>>>
    {
        public long NoteId;
        public long CommentId;
        public GetNoteAttachmentsByIdQuery(long noteId, long commentId)
        {
            NoteId = noteId;
            CommentId = commentId;
        }
    }
    public class GetNoteAttachmentsHandler : IRequestHandler<GetNoteAttachmentsByIdQuery, Response<List<TicketAttachmentsViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepositoryAsync _noteRepositoryAsync;
        public GetNoteAttachmentsHandler(IMapper map, INoteRepositoryAsync repo)
        {
            _mapper = map;
            _noteRepositoryAsync = repo;
        }
        public async Task<Response<List<TicketAttachmentsViewModel>>> Handle(GetNoteAttachmentsByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _noteRepositoryAsync.GetNoteAttachments(request.NoteId, request.CommentId);
            return new Response<List<TicketAttachmentsViewModel>>(_mapper.Map<List<TicketAttachmentsViewModel>>(response));
        }
    }
}
