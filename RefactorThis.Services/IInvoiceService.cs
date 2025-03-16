using RefactorThis.Domain;

namespace RefactorThis.Services
{
  public interface IInvoiceService
  {
    string ProcessPayment(Payment payment);
  }
}