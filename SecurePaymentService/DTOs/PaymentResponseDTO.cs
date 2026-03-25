namespace SecurePaymentService.DTOs;
public record PaymentResponseDTO(Guid Id, decimal Amount, string Status, string MaskedCardNumber, DateTime CreatedAt);