namespace SecurePaymentService.DTOs;
public record PaymentRequestDTO(decimal Amount, string CardNumber, string Currency = "BRL");