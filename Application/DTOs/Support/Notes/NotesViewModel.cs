using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Notes
{
    public class NotesViewModel : PayLoad
    {
        public long NotesId { get; set; }
        public DateTime NoteDate { get; set; }
        public string Type { get; set; }
        public string Comments { get; set; }
        public long EntityId { get; set; }
        public byte[] AttachedFile { get; set; }
        public string Resolved { get; set; }
        public string AssignedTo { get; set; }
        public DateTime EntryDate { get; set; }
        public string EnteredBy { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }
        public List<IFormFile> Files { get; set; }


    }
}
