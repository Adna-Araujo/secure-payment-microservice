using System.ComponentModel.DataAnnotations;
using SecurePaymentService.Shared.Enums;      // Onde mora o seu Enum
using SecurePaymentService.Shared.Constants;  // Onde mora sua Constante

namespace SecurePaymentService.DTOs;

public record PaymentRequestDTO(
    [Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    decimal Amount, 

    [Required(ErrorMessage = "O número do cartão é obrigatório.")]
    [RegularExpression(@"^\d{13,16}$", ErrorMessage = "O cartão deve ter entre 13 e 16 dígitos numéricos.")]
    string CardNumber, 

    [StringLength(3, MinimumLength = 3, ErrorMessage = "A moeda deve ter 3 caracteres (ex: BRL).")]
    string Currency = PaymentConstants.DefaultCurrency
);