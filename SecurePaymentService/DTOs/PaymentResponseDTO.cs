namespace SecurePaymentService.DTOs;

public record PaymentResponseDTO(
    Guid Id, // Mudamos de 'int' para 'Guid' para bater com o Banco de Dados
    decimal Amount,
    string Status,
    string MaskedCardNumber,
    DateTime CreatedAt
);