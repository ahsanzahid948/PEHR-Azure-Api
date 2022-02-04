namespace Domain.Entities.Support
{
    using Domain.Entities.Auth;
    using global::System.Collections.Generic;
    
    public class ClientComments_VM
    {
        public List<PracticeComments> PracticeComments { get; set; }

        public string PracticeSetup { get; set; }
    }
}
