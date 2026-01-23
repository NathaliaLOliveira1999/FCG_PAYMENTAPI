using FCG_CATALOGAPI.DTO;
using FCG_PAYMENTAPI.Interfaces.Repositories;
using FCG_PAYMENTAPI.Interfaces.Services;
using FCG_PAYMENTAPI.Models;
using FCG_PAYMENTAPI.Models.Enum;
using System.Text.RegularExpressions;

namespace FCG_PAYMENTAPI.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public ServiceResult ExecutePayment(SalesDto payment)
        {
            if(payment.IdPaymentMethod == Convert.ToInt32(PaymentMethod.CreditCard))
            {
                payment.CardHolderName = payment.CardHolderName?.Trim();
                if (string.IsNullOrEmpty(payment.CardHolderName) || payment.CardHolderName.Length < 3)
                    return ServiceResult.Fail("Nome do titular do cartão é inválido.");
                if (!IsValidCardNumber(payment.CardNumber))
                    return ServiceResult.Fail("Número do cartão é inválido.");
                if (string.IsNullOrEmpty(payment.CardCode))
                    return ServiceResult.Fail("Código do cartão é inválido.");
                if (IsExpired(payment.ExpirationMonth.Value, payment.ExpirationYear.Value))
                    return ServiceResult.Fail("Cartão está expirado.");
            }
            else
            {
                if(payment.IdPaymentMethod == Convert.ToInt32(PaymentMethod.Pix))
                {
                    if(!IsValidPixKey(payment.PixKey))
                        return ServiceResult.Fail("Chave de pix inválida.");
                }
                else
                    return ServiceResult.Fail("Método de pagamento inválido.");
            }
            return _paymentRepository.UpdatePayment(payment.IdSales, PaymentStatus.Concluida, (PaymentMethod) payment.IdPaymentMethod);
        }

        public ServiceResult ConfirmPayment(int idSales)
        {
            try
            {
                return ServiceResult.Ok(_paymentRepository.UpdatePaymentStatus(idSales, PaymentStatus.Concluida));
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
                return ServiceResult.Ok(_paymentRepository.UpdatePaymentStatus(idSales, PaymentStatus.Cancelada));
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
                return ServiceResult.Ok(_paymentRepository.UpdatePaymentStatus(idSales, PaymentStatus.Estornada));
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail(ex.Message);
            }
        }

        public PaymentStatus GetPaymentStatus(int idSales)
        {
            return (PaymentStatus)_paymentRepository.GetPaymentStatus(idSales);
            try
            {
            }
            catch (Exception ex)
            {
                // return ServiceResult.Fail(ex.Message);
            }
        }

        #region Payment Method Validation
        public bool IsValidCardNumber(string cardNumber)
        {
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());
                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }
                sum += n;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }

        public bool IsExpired(int month, int year)
        {
            var lastDay = new DateTime(year, month, DateTime.DaysInMonth(year, month));
            return lastDay < DateTime.UtcNow;
        }

        public bool IsValidPixKey(string pixKey)
        {
            if (Guid.TryParse(pixKey, out _))
                return true;

            if (Regex.IsMatch(pixKey, @"^\+55\d{10,11}$"))
                return true;

            if (Regex.IsMatch(pixKey, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return true;

            return false;
        }
        #endregion
    }
}
