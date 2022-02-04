using Application.Interfaces.Repositories;
using Application.Interfaces.Repositories.Support;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Support.Tickets.Commands
{
    public class CreateCommentAttachmentCommand : IRequest<Response<string>>
    {
        public string TicketNo { get; set; }
        public long CommentId { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
    }
    public class CreateCommentAttachmentHandler : IRequestHandler<CreateCommentAttachmentCommand, Response<string>>
    {
        private readonly ICommentRepositoryAsync _commentRepo;
        public CreateCommentAttachmentHandler(ICommentRepositoryAsync repo)
        {
            _commentRepo = repo;
        }
        public async Task<Response<string>> Handle(CreateCommentAttachmentCommand request, CancellationToken cancellationToken)
        {
            var response = await _commentRepo.AddCommentsAttachment(request.TicketNo, request.CommentId, request.FilePath, request.FileName);
            return new Response<string>(message: response.ToString());
        }
    }
}
