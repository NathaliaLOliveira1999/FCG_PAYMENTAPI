using FCG_PAYMENTAPI.Interfaces.Repositories;
using FCG_PAYMENTAPI.Interfaces.Services;
using FCG_PAYMENTAPI.Models;
using FCG_PAYMENTAPI.Models.Enum;

namespace FCG_PAYMENTAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public ServiceResult ConfirmPayment(int idSales)
        {
            try
            {
                return ServiceResult.Ok(_paymentRepository.UpdatePayment(idSales, PaymentStatus.Concluida));
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public ServiceResult CancelPayment(int idSales)
        {
            try
            {
                return ServiceResult.Ok(_paymentRepository.UpdatePayment(idSales, PaymentStatus.Cancelada));
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public ServiceResult RefundPayment(int idSales)
        {
            try
            {
                return ServiceResult.Ok(_paymentRepository.UpdatePayment(idSales, PaymentStatus.Estornada));
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public PaymentStatus GetPaymentStatus(int idSales)
        {
            return _paymentRepository.GetPaymentStatus(idSales);
            try
            {
            }
            catch (Exception ex)
            {
                // return ServiceResult.Fail(ex.Message);
            }
        }
    }
}
