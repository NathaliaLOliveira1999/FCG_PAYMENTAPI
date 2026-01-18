using FCG_PAYMENTAPI.Interfaces.Repositories;
using FCG_PAYMENTAPI.Models;
using FCG_PAYMENTAPI.Models.Enum;

namespace FCG_PAYMENTAPI.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly AppDbContext _context;

        public PaymentRepository(AppDbContext context)
        {
            _context = context;
        }

        public ServiceResult UpdatePayment(int idSales, PaymentStatus status)
        {
            try
            {
                var sale = _context.Sales.Where(x => x.IdSales == idSales).FirstOrDefault();
                sale.IdPaymentStatus = status;
                _context.Sales.Update(sale);
                _context.SaveChanges();
                return ServiceResult.Ok(sale);
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail("Erro ao atualizar itens: " + ex.Message);
            }
        }

        public PaymentStatus GetPaymentStatus(int idSales)
        {
            var sale = _context.Sales.Where(x => x.IdSales == idSales).FirstOrDefault();
            return sale.IdPaymentStatus;
        }
    }
}
