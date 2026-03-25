using System.ComponentModel.DataAnnotations;

namespace SecurePaymentService.DTOs;

public record PaymentRequestDTO(
    [Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    decimal Amount, 

    [Required(ErrorMessage = "O número do cartão é obrigatório.")]
    [RegularExpression(@"^\d{13,16}$", ErrorMessage = "O cartão deve ter entre 13 e 16 dígitos numéricos.")]
    string CardNumber, 

    [StringLength(3, MinimumLength = 3, ErrorMessage = "A moeda deve ter 3 caracteres (ex: BRL).")]
    string Currency = "BRL"
);