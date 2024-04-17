using System;
using web_app_demo.Entidades;

namespace web_app_demo.Interfaces
{
    public interface IPayment : IID
    {
        Task<Result> CreatePaymentAsync(RequestPayment request);
    }
}

