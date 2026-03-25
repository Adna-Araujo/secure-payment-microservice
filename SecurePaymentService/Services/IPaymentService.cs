using SecurePaymentService.DTOs;

namespace SecurePaymentService.Services;

public interface IPaymentService {
    // Agora o contrato diz que vamos devolver um PaymentResponseDTO
    Task<PaymentResponseDTO> ProcessPaymentAsync(PaymentRequestDTO request);
    Task<IEnumerable<PaymentResponseDTO>> GetAllTransactionsAsync();
}