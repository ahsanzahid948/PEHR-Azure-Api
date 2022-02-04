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

namespace Application.Features.Support.Notes.Queries
{
    public class GetClientNotesDataByIdQuery : IRequest<Response<List<ClientNoteViewModel>>>
    {
        public NotesFilter NotesFilter;
        public long EntityId;
        public string NoteType;
        public GetClientNotesDataByIdQuery(NotesFilter notes,long entityId,string type)
        {
            NotesFilter = notes;
            EntityId = entityId;
            NoteType = type;
        }
    }
    public class GetClientNotesHandler : IRequestHandler<GetClientNotesDataByIdQuery, Response<List<ClientNoteViewModel>>>
    {
        private readonly IMapper _mapper;
        private readonly INoteRepositoryAsync _notesRepository;
        public GetClientNotesHandler(IMapper map,INoteRepositoryAsync _repo)
        {
            _mapper = map;
            _notesRepository = _repo;
        }
        public async Task<Response<List<ClientNoteViewModel>>> Handle(GetClientNotesDataByIdQuery request, CancellationToken cancellationToken)
        {
            var response =await _notesRepository.GetAllNotes(request.NotesFilter,request.EntityId,request.NoteType);
            return new Response<List<ClientNoteViewModel>>(_mapper.Map<List<ClientNoteViewModel>>(response));
        }
    }
}
