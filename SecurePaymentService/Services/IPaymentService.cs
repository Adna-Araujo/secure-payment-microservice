using SecurePaymentService.DTOs;

namespace SecurePaymentService.Services;

public interface IPaymentService {
    Task<object> ProcessPaymentAsync(PaymentRequestDTO request);
    Task<IEnumerable<PaymentResponseDTO>> GetAllTransactionsAsync();
}