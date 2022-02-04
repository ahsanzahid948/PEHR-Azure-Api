using Application.DTOs.Auth.Client;
using Domain.Entities;
using MediatR;
using System.Collections.Generic;


namespace Application.DTOs.Auth.ViewModel
{
    public class ClientDataManagerViewModel 
    {
        public  List<ClientProfileViewModel> ImplementaionManager { get; set; }
        public  List<ClientProfileViewModel> SalesManager { get; set; }
        public  List<ClientsListViewModel> CustAccNums { get; set; }
    }
}
