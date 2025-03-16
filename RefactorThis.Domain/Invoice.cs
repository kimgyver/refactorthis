using System.Collections.Generic;

namespace RefactorThis.Domain
{
	public class Invoice
	{
		public decimal Amount { get; set; }
		public decimal AmountPaid { get; set; }
		public decimal TaxAmount { get; set; }
		public List<Payment> Payments { get; set; } = new();
		public InvoiceType Type { get; set; }

		public void ApplyPayment(Payment payment) // Just updates data, no logic
		{
			AmountPaid += payment.Amount;
			if (Type == InvoiceType.Commercial)
				TaxAmount += payment.Amount * 0.14m;
			Payments.Add(payment);
		}
	}
}