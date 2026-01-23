namespace FCG_CATALOGAPI.DTO
{
    public class SalesDto
    {
        // 🔹 Identificação da venda
        public int IdSales { get; set; }

        // 🔹 Método de pagamento (enum)
        public int IdPaymentMethod { get; set; }

        // 🔹 Valor do pagamento
        public decimal Amount { get; set; }

        // 🔹 Dados de cartão (somente se for cartão)
        public string CardCode { get; set; }          // Token do gateway
        public string CardNumber { get; set; }     // Últimos 4 dígitos
        public string CardHolderName { get; set; }
        public int? ExpirationMonth { get; set; }
        public int? ExpirationYear { get; set; }

        // 🔹 Dados específicos para PIX
        public string PixKey { get; set; }
        public string PixTransactionId { get; set; }

        // 🔹 Controle do pagamento
        public DateTime PaymentDate { get; set; }
    }
}
