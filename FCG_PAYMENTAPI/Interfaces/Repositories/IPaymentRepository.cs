using FCG_PAYMENTAPI.Models;
using FCG_PAYMENTAPI.Models.Enum;

namespace FCG_PAYMENTAPI.Interfaces.Repositories
{
    public interface IPaymentRepository
    {
        ServiceResult UpdatePayment(int idSales, PaymentStatus status, PaymentMethod method);
        ServiceResult UpdatePaymentStatus(int idSales, PaymentStatus status);
        int GetPaymentStatus(int idSales);
    }
}
