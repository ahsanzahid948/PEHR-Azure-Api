namespace Application.DTOs.Notes
{
    public class NotesFilter : PayLoad
    {
        public string Page { get; set; }
        public string Rp { get; set; }
        public string SortName { get; set; }
        public string SortOrder { get; set; }
        public string Query { get; set; }
        public string Qtype { get; set; }
    }
}
