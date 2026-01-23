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

        public Sales? GetById(int id) => _context.Sales.Find(id);

        public ServiceResult UpdatePayment(int idSales, PaymentStatus status, PaymentMethod method)
        {
            try
            {
                var sale = this.GetById(idSales);
                if (sale != null)
                {
                    sale.IdPaymentStatus = Convert.ToInt32(status);
                    sale.IdPaymentMethod = Convert.ToInt32(method);
                    sale.PaymentDate = DateTime.Now;
                    _context.Sales.Update(sale);
                    _context.SaveChanges();
                    return ServiceResult.Ok(sale);
                }
                return ServiceResult.Fail("Venda não identificada!");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail("Erro ao atualizar itens: " + ex.Message);
            }
        }

        public ServiceResult UpdatePaymentStatus(int idSales, PaymentStatus status)
        {
            try
            {
                var sale = _context.Sales.Where(x => x.IdSales == idSales).FirstOrDefault();
                if (sale != null)
                {
                    sale.IdPaymentStatus = Convert.ToInt32(status);
                    _context.Sales.Update(sale);
                    _context.SaveChanges();
                    return ServiceResult.Ok(sale);
                }
                return ServiceResult.Fail("Venda não identificada!");
            }
            catch (Exception ex)
            {
                return ServiceResult.Fail("Erro ao atualizar itens: " + ex.Message);
            }
        }

        public int GetPaymentStatus(int idSales)
        {
            var sale = _context.Sales.Where(x => x.IdSales == idSales).FirstOrDefault();
            return sale.IdPaymentStatus;
        }
    }
}
