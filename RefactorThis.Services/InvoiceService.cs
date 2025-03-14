using RefactorThis.Domain;
using RefactorThis.Persistence;

namespace RefactorThis.Services
{
	public class InvoiceService
	{
		private readonly InvoiceRepository _invoiceRepository;

		public InvoiceService(InvoiceRepository invoiceRepository)
		{
			_invoiceRepository = invoiceRepository;
		}

		public string ProcessPayment(Payment payment)
		{
			var invoice = _invoiceRepository.GetInvoice(payment.Reference)
					?? throw new InvalidOperationException("There is no invoice matching this payment");

			if (IsInvoiceInInvalidState(invoice))
				throw new InvalidOperationException("The invoice is in an invalid state.");

			if (IsNoPaymentNeeded(invoice))
				return "no payment needed";

			decimal previousAmountPaid = CalculateAmountPaid(invoice);
			decimal remainingAmount = invoice.Amount - previousAmountPaid;

			if (IsInvoiceFullyPaid(previousAmountPaid, invoice.Amount))
				return "invoice was already fully paid";

			if (IsPaymentExcessive(payment.Amount, remainingAmount, invoice.Amount))
			{
				return previousAmountPaid == 0 ?
					 "the payment is greater than the invoice amount" :
					 "the payment is greater than the partial amount remaining";
			}

			invoice.ApplyPayment(payment);
			_invoiceRepository.SaveInvoice(invoice);

			return GetPartialPaymentStatusMessage(invoice, previousAmountPaid);
		}

		private bool IsNoPaymentNeeded(Invoice invoice) =>
				invoice.Amount == 0 && (invoice.Payments == null || !invoice.Payments.Any());

		private bool IsInvoiceInInvalidState(Invoice invoice) =>
				invoice.Amount == 0 && invoice.Payments?.Any() == true;

		private bool IsInvoiceFullyPaid(decimal amountPaid, decimal totalAmount) =>
				amountPaid == totalAmount;

		private bool IsPaymentExcessive(decimal paymentAmount, decimal remainingAmount, decimal totalAmount) =>
				paymentAmount > totalAmount || paymentAmount > remainingAmount;

		private decimal CalculateAmountPaid(Invoice invoice) =>
				invoice.AmountPaid > 0 ? invoice.AmountPaid : invoice.Payments?.Sum(x => x.Amount) ?? 0;

		private string GetPartialPaymentStatusMessage(Invoice invoice, decimal previousAmountPaid)
		{
			if (invoice.Amount == invoice.AmountPaid)
				return "final partial payment received, invoice is now fully paid";

			return previousAmountPaid > 0
					? "another partial payment received, still not fully paid"
					: "invoice is now partially paid";
		}
	}
}