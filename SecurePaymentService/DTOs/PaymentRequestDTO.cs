using System.ComponentModel.DataAnnotations;
using SecurePaymentService.Shared.Constants;

namespace SecurePaymentService.DTOs;

public record PaymentRequestDTO(
    [Required(ErrorMessage = "O valor é obrigatório.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "O valor deve ser maior que zero.")]
    decimal Amount, 

    [Required(ErrorMessage = "O número do cartão é obrigatório.")]
    [CreditCard(ErrorMessage = "Formato de cartão de crédito inválido.")]
    [RegularExpression(@"^\d{13,16}$", ErrorMessage = "O cartão deve ter entre 13 e 16 dígitos.")]
    string CardNumber, 

    [StringLength(3, MinimumLength = 3, ErrorMessage = "A moeda deve ter 3 caracteres.")]
    string Currency = PaymentConstants.DefaultCurrency
) // <-- REPARE: Tirei o ";" e vou abrir as chaves
{
    // O método precisa estar DENTRO destas chaves
    public string ToLogString() 
    {
        // Adicionei uma proteção simples: se o cartão for curto (erro), ele não quebra o Substring
        var lastDigits = CardNumber.Length >= 4 ? CardNumber.Substring(CardNumber.Length - 4) : "****";
        
        return $"Amount: {Amount}, Currency: {Currency}, Card: ****{lastDigits}";
    }
}