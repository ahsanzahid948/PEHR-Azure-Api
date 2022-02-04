using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using FluentValidation.Results;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Exceptions;
using Application.DTOs;
using Domain.Entities.Notes;
using Domain.Entities.Auth;
using Application.DTOs.Auth.Client;
using Domain.Entities.Support;

namespace Infrastructure.Repositories
{
    public class ClientRepositoryAsync : IClientRepositoryAsync
    {
        private readonly IMapper _mapper;
        public readonly IConfiguration configuration;

        public ClientRepositoryAsync(IConfiguration configuration, IMapper mapper)
        {
            this.configuration = configuration;
            this._mapper = mapper;

        }


        public async Task<List<ClientsList>> GetClientsData(ClientFilter _client)
        {

            IList<ValidationFailure> messages = new List<ValidationFailure>();
            _client.AppUser = !string.IsNullOrEmpty(_client.AppUser) ? _client.AppUser : "";
            _client.Reseller = !string.IsNullOrEmpty(_client.Reseller) ? _client.Reseller : "";
            _client.CP = !string.IsNullOrEmpty(_client.CP) ? _client.CP.ToUpper() : "";

            _client.Sortname = string.IsNullOrEmpty(_client.Sortname) || _client.Sortname.Equals("Nil") || _client.Sortname.Equals("undefined") ? null : _client.Sortname;
            _client.Sortorder = string.IsNullOrEmpty(_client.Sortorder) || _client.Sortorder.Equals("Nil") || _client.Sortorder.Equals("undefined") ? null : _client.Sortorder;

            int pg = !string.IsNullOrEmpty(_client.Page) ? Int32.Parse(_client.Page) : -1;
            int rp = !string.IsNullOrEmpty(_client.Rp) ? Int32.Parse(_client.Rp) : -1;


            string SqlWhere = "";
            if (_client.Reseller == "Y")
            {
                SqlWhere += " AND UPPER(CHANNEL_PARTNER) = :CHANNEL_PARTNER";
            }
            string query = "Select AUTH_ENTITY_SEQ_NUM,PRACTICE_NAME,PRIMARY_ADMIN,CHANNEL_PARTNER,PRACTICE_EMAIL,PRACTICE_TEL_NUM,from V_PEHR_CLIENTS_LIST WHERE UPPER(ACCOUNT_TYPE) <> 'LOSS'" + SqlWhere + " ORDER BY " + _client.Sortname + " " + _client.Sortorder;
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var response = await connection.QueryAsync<ClientsList>(query, new { CP = _client.CP });
                return response.ToList();
            }
            messages.Add(new ValidationFailure("Connection String->GetClientsData() ", "Connection Error"));
            throw new ValidationException(messages.ToList());
        }

        public async Task<ClientList_VM> ClientsListAsync(string seqnum)
        {
            bool showPracticeSetup = false;
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                string query = "SELECT AUTH_ENTITY_SEQ_NUM,PRACTICE_NAME,CONTACT_PERSON,CHANNEL_PARTNER,PRACTICE_EMIL,PRACTICE_TEL_NUM,SPECIALITY,ENTITY_SHORT_NAME,TRIAL_ACTIVATION_DATE,TRIAL_ACTIVATION_STATUS,PAID_DATE,LIVE_DATE FROM V_PEHR_CLIENTS_LIST WHERE SEQ_NUM = :SEQ_NUM";
                connection.Open();

                var clientList = await connection.QueryFirstOrDefaultAsync<ClientsList>(query, new { SEQ_NUM = long.Parse(seqnum) });
                var entity = await connection.QueryFirstOrDefaultAsync<Entity>("SELECT PRACTICE_SETUP FROM ENTITY WHERE SEQ_NUM=:SEQ_NUM", new { SEQ_NUM = long.Parse(seqnum) });
                if (entity != null)
                {
                showPracticeSetup = entity.Practice_Setup != null && (entity.Practice_Setup.ToUpper() == "COMPLETED"  || entity.Practice_Setup.ToUpper() == "UNDER REVIEW") ? true : false;
                }
                var result = new ClientList_VM();
                result.ShowPracticeSetup = showPracticeSetup;
                result.Client_List = clientList;
                return result;
            }

        }

        public Task<int> AddClientAsync(ClientProfileViewModel clientProfile)
        {
            throw new NotImplementedException();
        }

        public Task<ClientProfileViewModel> GetByIdAsync(double id)
        {
            throw new NotImplementedException();
        }

        public Task<ClientProfileViewModel> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<ClientProfileViewModel>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(ClientProfileViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(ClientProfileViewModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ClientProfileViewModel> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<ClientSubscription> GetClientSubscriptionById(long entityId)
        {
            var sql = "SELECT SEQ_NUM,SHORT_NAME,NAME,ADDRESS1,ADDRESS2,CITY,STATE,ZIPCODE,ZIPCODE_EXT,TEL_NUM,EMAIL,CONTACT_PERSON,PRIMARY_ADMIN,ACTIVE_FLAG,CUSTOMER_ID,NEXT_BILLING_DATE,AUTH_ENTITY_SEQ_NUM,PAYMENT_CYCLE,PAYMENT_TERMS,CREDIT_CARD_NO,CARD_TYPE,EXPIRY_MONTH_YEAR,CARD_HOLDER_NAME,WALLET_ID,RCM_CREDIT_CARD_NO,RCM_CARD_TYPE,RCM_EXPIRY_MONTH_YEAR,RCM_CARD_HOLDER_NAME,RCM_WALLET_ID,PAYMENT_GATEWAY,IS_PREFERRED FROM WEB_AUTH.V_CLIENT_ENTITY_PROF WHERE AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<ClientSubscription>(sql, new { AUTH_ENTITY_SEQ_NUM = entityId });
                return result;
            }
        }
    }
}
