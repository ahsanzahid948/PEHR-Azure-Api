using Application.DTOs.Notes;
using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support
{
    public class GetCommentAttachmentByIdQuery : IRequest<Response<List<TicketAttachmentsViewModel>>>
    {
        public long TickedId;
        public long CommentTicketId;
        public GetCommentAttachmentByIdQuery(long ticketId, long commentId)
        {
            TickedId = ticketId;
            CommentTicketId = commentId;
        }
    }

    public class GetCommentAttachmentHandler : IRequestHandler<GetCommentAttachmentByIdQuery, Response<List<TicketAttachmentsViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly ICommentRepositoryAsync _commentRepo;
        public GetCommentAttachmentHandler(IMapper map, ICommentRepositoryAsync repo)
        {
            _mapper = map;
            _commentRepo = repo;
        }
        public async Task<Response<List<TicketAttachmentsViewModel>>> Handle(GetCommentAttachmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _commentRepo.GetCommentsAttachments(request.TickedId, request.CommentTicketId);
            return new Response<List<TicketAttachmentsViewModel>>(_mapper.Map<List<TicketAttachmentsViewModel>>(response));
        }
    }
}
