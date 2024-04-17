using System;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using web_app_demo.Entidades;
using web_app_demo.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace web_app_demo.Service
{
	public class PaymentService : IPayment
    {
        internal IPaymentContext _context;

        public PaymentService(IPaymentContext context)
        {
            _context = context;
        }

        public Guid Value { get; private set; } = Guid.NewGuid();

        public async Task<Result> CreatePaymentAsync(RequestPayment request)
        {
            request.User.UniqueId = this.Value;
            try
            {
                var idParam = new OracleParameter("id", OracleDbType.Int32, 50, ParameterDirection.Input);
                idParam.Value = request.User.Id;

                var amountParam = new OracleParameter("amount", OracleDbType.Int32, 50, ParameterDirection.Input);
                amountParam.Value = request.Payment.Amount;
            
                var descriptionParam = new OracleParameter("description", OracleDbType.Varchar2, 255, ParameterDirection.Input);
                descriptionParam.Value = request.Payment.Description;

                var channelParam = new OracleParameter("channel", OracleDbType.Varchar2, 4, ParameterDirection.Input);
                channelParam.Value = request.Payment.Channel;

                var useridParam = new OracleParameter("userid", OracleDbType.Int32, 50, ParameterDirection.Input);
                useridParam.Value = request.User.Id;

                var nombreParam = new OracleParameter("nombre", OracleDbType.Varchar2, 50, ParameterDirection.Input);
                nombreParam.Value = request.User.Nombre;

                var typeParam = new OracleParameter("tipo", OracleDbType.Varchar2, 1, ParameterDirection.Input);
                typeParam.Value = request.User.Tipo;

                var uniqueIdParam = new OracleParameter("uniqueid", OracleDbType.Varchar2, 50, ParameterDirection.Input);
                uniqueIdParam.Value = request.User.UniqueId;

                var loggingParam = new OracleParameter("logging", OracleDbType.Varchar2, 255, ParameterDirection.Output);
                loggingParam.Size = 4000;

                object[] parameters = new object[] { idParam,amountParam,descriptionParam,channelParam,useridParam, nombreParam, typeParam, uniqueIdParam, loggingParam };

                var test = _context.Database.ExecuteSqlRaw("BEGIN CHELO.ORQUESTRATOR(:id, :amount, :description, :channel, :userid, :nombre, :tipo, :uniqueid, :logging); END;", parameters);


                var monitoring = loggingParam.Value.ToString();
                return Result.Success(request.Payment, monitoring);
            }
            catch (Exception ex)
            {
                return Result.Failure(new Error("", ""));
            }
        }
    }
}

