namespace Application.DTOs.Auth.ViewModel
{
   
    using Application.DTOs.Auth.ViewModel;
    using global::System.Collections.Generic;
    using Application.DTOs.Entity;

    public class ClientConfigurationViewModel
    {
        public string TnsName { get; set; }
        public EntityViewModel Entity{ get; set; }
        public List<SystemAdvanceFeaturesViewModel> SystemAdvanceOptions { get; set; }
    
    }
}
