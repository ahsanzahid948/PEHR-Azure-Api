using Application.DTOs.Common.CreateRequestResponse;
using Application.DTOs.Notes;
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

namespace Application.Features.Notes.Commands
{
    public class CreateNoteCommand : IRequest<Response<CreateRequestResponse>>
    {
        public NotesViewModel ClientNotes { get; set; }
        public CreateNoteCommand(NotesViewModel _obj)
        {
            ClientNotes = _obj;
        }
    }
    public class CreateNoteHandler : IRequestHandler<CreateNoteCommand, Response<CreateRequestResponse>>
    {
        private readonly INoteRepositoryAsync _clientrepo;

        public CreateNoteHandler(INoteRepositoryAsync repo)
        {
            _clientrepo = repo;
        }

        public async Task<Response<CreateRequestResponse>> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            var response = await _clientrepo.SaveNote(request.ClientNotes);
            return new Response<CreateRequestResponse>(response);
        }
    }
}
