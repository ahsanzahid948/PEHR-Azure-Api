namespace Domain.Entities.Auth
{
    using global::System;
    public class Audit
    {
        public long Seq_Num { get; set; }
        public string OldStatus { get; set; }
        public string NewStatus { get; set; }
        public string Entered_By { get; set; }
        public DateTime EntryDate { get; set; }
        public long Entity_Seq_Num { get; set; }
    }
}
