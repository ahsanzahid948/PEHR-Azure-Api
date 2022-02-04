using Application.DTOs.Common.CreateRequestResponse;
using Application.Interfaces.Repositories.Support;
using Dapper;
using Domain.Entities.Support;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories.Support.Subscription
{
    public class SubscriptionRepositoryAsync : ISubscriptionRepositoryAsync
    {
        private readonly IConfiguration configuration;
        public SubscriptionRepositoryAsync(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        public Task<int> AddAsync(Domain.Entities.Support.PaymentMethod entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(long id)
        {
            var sql = "DELETE FROM CLIENT_PAYMENTMETHODS WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { SEQ_NUM = id});
                return result;
            }
        }

        public Task<IReadOnlyList<Domain.Entities.Support.PaymentMethod>> GetAllAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Support.PaymentMethod> GetByFilterAsync(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Task<Domain.Entities.Support.PaymentMethod> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }
        public Task<int> UpdateAsync(Domain.Entities.Support.PaymentMethod entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<Domain.Entities.Support.PaymentMethod>> GetPaymentMethodsByIdAsync(long id, string type)
        {
            var sql = "SELECT SEQ_NUM, CLIENT_SEQ_NUM, AUTH_ENTITY_SEQ_NUM, CREDIT_CARD_NO, EXPIRY_MONTH_YEAR, CARD_TYPE, CARD_HOLDER_NAME, PAYERID, IS_PREFERRED, TYPE FROM CLIENT_PAYMENTMETHODS WHERE AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(type))
                {
                    var result = await connection.QueryAsync<Domain.Entities.Support.PaymentMethod>(sql, new { AUTH_ENTITY_SEQ_NUM = id });
                    return result.ToList();
                }
                else
                {
                    var result = await connection.QueryAsync<Domain.Entities.Support.PaymentMethod>(sql+ " AND Upper(TYPE) =:TYPE", new { AUTH_ENTITY_SEQ_NUM = id, TYPE = type.ToUpper() });
                    return result.ToList();
                }
            }
        }

        public async Task<IReadOnlyList<LicencePayment>> GetPaymentsByIdAsync(long id, string clientId)
        {
            var sql = "SELECT SEQ_NUM, CLIENT_SEQ_NUM, AUTH_ENTITY_SEQ_NUM, AMOUNT_PAID, TRANSACTION_ID, TRANSACTION_DATE, ENTRY_DATE, ENTERED_BY, INVOICE_SEQ_NUM, TYPE, INVOICE_NUMBER, PAYMENT_METHOD, CREDIT_CARD_NO, CHECK_NUMBER FROM LICENCE_PAYMENTS WHERE AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(clientId))
                {
                    var result = await connection.QueryAsync<LicencePayment>(sql, new { AUTH_ENTITY_SEQ_NUM = id });
                    return result.ToList();
                }
                else
                {
                    sql = "SELECT SEQ_NUM, CLIENT_SEQ_NUM, AUTH_ENTITY_SEQ_NUM, AMOUNT_PAID, TRANSACTION_ID, TRANSACTION_DATE, ENTRY_DATE, ENTERED_BY, INVOICE_SEQ_NUM, TYPE, INVOICE_NUMBER, PAYMENT_METHOD, CREDIT_CARD_NO, CHECK_NUMBER FROM (SELECT * FROM LICENCE_PAYMENTS where client_seq_num = :client_seq_num and AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM ORDER BY TRANSACTION_DATE desc) WHERE ROWNUM = 1";
                    var result = await connection.QueryAsync<LicencePayment>(sql, new { AUTH_ENTITY_SEQ_NUM = id, client_seq_num = clientId });
                    return result.ToList();
                }

            }
        }

        public async Task<IReadOnlyList<PaymentInvoice>> GetInvoicesByIdAsync(long id)
        {
            var sql = "SELECT SEQ_NUM, CLIENT_SEQ_NUM, INVOICE_NUMBER, INVOICE_AMOUNT, INVOICE_PDF, INVOICE_DATE, ENTRY_DATE, ENTERED_BY, AUTH_ENTITY_SEQ_NUM, FILE_NAME, INVOICE_NAME, TYPE FROM PAYMENT_INVOICES WHERE AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryAsync<PaymentInvoice>(sql, new { AUTH_ENTITY_SEQ_NUM = id });
                return result.ToList();

            }
        }

        public async Task<CreateRequestResponse> CreateInvoiceAsync(PaymentInvoice paymentInvoice)
        {
            var sql = "INSERT INTO PAYMENT_INVOICES (SEQ_NUM,CLIENT_SEQ_NUM,INVOICE_NUMBER,INVOICE_AMOUNT,AUTH_ENTITY_SEQ_NUM,INVOICE_NAME) VALUES(PRIMARY_SEQ.NEXTVAL,:CLIENT_SEQ_NUM,:INVOICE_NUMBER,:INVOICE_AMOUNT,:AUTH_ENTITY_SEQ_NUM,:INVOICE_NAME) returning SEQ_NUM into: SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                DynamicParameters param = new DynamicParameters(paymentInvoice);
                param.Add(name: "SEQ_NUM", dbType: DbType.Double, direction: ParameterDirection.Output);             
                var result = await connection.ExecuteAsync(sql, param);
                var Id = param.Get<Double>("SEQ_NUM");
                return new CreateRequestResponse { Status = result, Id = Id };
            }
        }

        public async Task<int> CreatePaymentMethodAsync(Domain.Entities.Support.PaymentMethod paymentMethod)
        {
            var sql = "INSERT INTO CLIENT_PAYMENTMETHODS (SEQ_NUM,CLIENT_SEQ_NUM,CREDIT_CARD_NO,EXPIRY_MONTH_YEAR,AUTH_ENTITY_SEQ_NUM,CARD_TYPE,PAYERID,TYPE,IS_PREFERRED)" +
                " VALUES(PRIMARY_SEQ.NEXTVAL,:CLIENT_SEQ_NUM,:CREDIT_CARD_NO,:EXPIRY_MONTH_YEAR,:AUTH_ENTITY_SEQ_NUM,:CARD_TYPE,:PAYERID,:TYPE,:IS_PREFERRED)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();

                var result = await connection.ExecuteAsync(@sql, paymentMethod);
                return result;
            }
        }

        public async Task<int> CreatePaymentAsync(LicencePayment licencePayment)
        {
            var sql = "INSERT INTO LICENCE_PAYMENTS(SEQ_NUM,CLIENT_SEQ_NUM,AUTH_ENTITY_SEQ_NUM,AMOUNT_PAID,TRANSACTION_ID,INVOICE_SEQ_NUM,TYPE,PAYMENT_METHOD,CREDIT_CARD_NO,CHECK_NUMBER) VALUES (PRIMARY_SEQ.NEXTVAL,:CLIENT_SEQ_NUM,:AUTH_ENTITY_SEQ_NUM,:AMOUNT_PAID,:TRANSACTION_ID,:INVOICE_SEQ_NUM,:TYPE,:PAYMENT_METHOD,:CREDIT_CARD_NO,:CHECK_NUMBER)";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(@sql, licencePayment);
                return result;
            }
        }

        public async Task<PaymentMethod> GetPrefferedCardAsync(long entityId, string type)
        {
            var sql = "SELECT SEQ_NUM, CLIENT_SEQ_NUM, AUTH_ENTITY_SEQ_NUM, CREDIT_CARD_NO, EXPIRY_MONTH_YEAR, CARD_TYPE, CARD_HOLDER_NAME, PAYERID, IS_PREFERRED, TYPE FROM CLIENT_PAYMENTMETHODS WHERE AUTH_ENTITY_SEQ_NUM = :AUTH_ENTITY_SEQ_NUM AND UPPER(TYPE)= UPPER(:TYPE) and IS_PREFERRED = :IS_PREFERRED";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.QueryFirstOrDefaultAsync<Domain.Entities.Support.PaymentMethod>(sql, new { AUTH_ENTITY_SEQ_NUM = entityId, TYPE = type, IS_PREFERRED  = "Y"});
                return result;
            }
        }

        public async Task<int> UpdatePrefferedCardAsync(string paymentId, string expiryMonth, string preffered)
        {
            var sql = "UPDATE CLIENT_PAYMENTMETHODS SET IS_PREFERRED=:IS_PREFERRED WHERE SEQ_NUM=:SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                if (string.IsNullOrEmpty(expiryMonth))
                {
                    var result = await connection.ExecuteAsync(sql, new { SEQ_NUM = paymentId, IS_PREFERRED = preffered });
                    return result;
                }
                else
                {
                    sql = "UPDATE CLIENT_PAYMENTMETHODS SET EXPIRY_MONTH_YEAR=:EXPIRY_MONTH_YEAR WHERE SEQ_NUM=:SEQ_NUM";
                    var result = await connection.ExecuteAsync(sql, new { SEQ_NUM = paymentId, EXPIRY_MONTH_YEAR = expiryMonth });
                    return result;
                }
            }
        }

        public async Task<int> UpdatePreferedCardsAsync(long entityId, string type)
        {
            var sql = "UPDATE CLIENT_PAYMENTMETHODS SET IS_PREFERRED=:IS_PREFERRED WHERE TYPE = :TYPE AND AUTH_ENTITY_SEQ_NUM=:AUTH_ENTITY_SEQ_NUM";
            using (var connection = new OracleConnection(configuration.GetConnectionString("SupportDBConnection")))
            {
                connection.Open();
                var result = await connection.ExecuteAsync(sql, new { IS_PREFERRED = "N", TYPE = type, AUTH_ENTITY_SEQ_NUM = entityId });
                return result;
            }
        }
    }
}
