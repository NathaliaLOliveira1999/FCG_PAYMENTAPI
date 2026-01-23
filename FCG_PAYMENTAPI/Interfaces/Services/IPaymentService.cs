using FCG_CATALOGAPI.DTO;
using FCG_PAYMENTAPI.Models;
using FCG_PAYMENTAPI.Models.Enum;

namespace FCG_PAYMENTAPI.Interfaces.Services
{
    public interface IPaymentService
    {
        ServiceResult ConfirmPayment(int idSales);
        ServiceResult CancelPayment(int idSales);
        ServiceResult RefundPayment(int idSales);
        PaymentStatus GetPaymentStatus(int idSales);
        ServiceResult ExecutePayment(SalesDto payment);
    }
}
