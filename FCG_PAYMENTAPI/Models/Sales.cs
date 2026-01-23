using FCG_PAYMENTAPI.Models.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FCG_PAYMENTAPI.Models
{
    [Table("SALES")]
    public class Sales
    {
        [Key]
        [Column("IDSALES")]
        public int IdSales { get; set; }

        [Column("SALEDATE")]
        public DateTime SaleDate { get; set; }

        [Column("TOTALPRICE")]
        public decimal TotalPrice { get; set; }

        [Column("IDCLIENT")]
        public int IdClient { get; set; }

        [Column("IDPAYMENTSTATUS")]
        public int IdPaymentStatus { get; set; }

        [Column("PAYMENTDATE")]
        public DateTime? PaymentDate { get; set; }

        [Column("IDPAYMENTMETHOD")]
        public int IdPaymentMethod { get; set; }
    }
}
