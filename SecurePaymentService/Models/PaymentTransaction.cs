using SecurePaymentService.Shared.Enums;      // Onde mora o seu Enum
using SecurePaymentService.Shared.Constants;  // Onde mora sua Constante

namespace SecurePaymentService.Models;

public class PaymentTransaction { 
    public Guid Id { get; set; } = Guid.NewGuid();
    public decimal Amount { get; set; }
    
    // Substituímos "BRL" pela Constante
    public string Currency { get; set; } = PaymentConstants.DefaultCurrency; 
    
    public string CardNumber { get; set; } = string.Empty;
    
    // Substituímos string pelo Enum
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending; 
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}