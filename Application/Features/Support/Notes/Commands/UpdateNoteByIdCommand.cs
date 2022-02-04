using Application.DTOs.Notes;
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

namespace Application.Features.Support.Notes.Commands
{
    public class UpdateNoteByIdCommand : IRequest<Response<int>>
    {
        public long NoteId { get; set; }
        public NotesViewModel Notes { get; set; }

        public UpdateNoteByIdCommand(long noteId, NotesViewModel note)
        {
            NoteId = noteId;
            Notes = note;
        }

    }
    public class UpdateNoteHandler : IRequestHandler<UpdateNoteByIdCommand, Response<int>>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepositoryAsync _noteRepositoryAsync;
        public UpdateNoteHandler(IMapper map, INoteRepositoryAsync repo)
        {
            _mapper = map;
            _noteRepositoryAsync = repo;
        }
        public async Task<Response<int>> Handle(UpdateNoteByIdCommand request, CancellationToken cancellationToken)
        {
          
            var getNote = await _noteRepositoryAsync.GetNotesByIdAsync(request.NoteId);
            if (getNote == null)
            {
                throw new Exception("Notes Doesn't Exist");
            }
            request.Notes.NotesId = request.NoteId;
            var response = await _noteRepositoryAsync.UpdateNote(request.Notes);
            if (response > 0)
            {
                return new Response<int>(response);
            }
            throw new Exception("Error Updating Notes");
        }
    }
}
