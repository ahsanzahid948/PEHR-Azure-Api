using Application.DTOs.Auth.Client;
using System.Collections.Generic;


namespace Application.DTOs.Auth.ViewModel
{
   public  class ClientDetailModel
    {
        public bool ShowPracticeSetup { get; set; }
        public ClientsListViewModel ClientDetail { get; set; }
    }
}
