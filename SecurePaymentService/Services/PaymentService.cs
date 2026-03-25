using SecurePaymentService.Data;
using SecurePaymentService.Models;
using SecurePaymentService.Helpers;
using SecurePaymentService.DTOs;
using Microsoft.EntityFrameworkCore;

namespace SecurePaymentService.Services;

public class PaymentService : IPaymentService {
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context) {
        _context = context;
    }

    public async Task<object> ProcessPaymentAsync(PaymentRequestDTO request) {
        // Lógica de Validação
        if (request.Amount <= 0) throw new ArgumentException("Valor inválido.");
        if (!SecurityHelper.ValidateCardNumber(request.CardNumber)) 
            throw new ArgumentException("Cartão inválido pelo algoritmo de Luhn.");

        // Mapeamento e Persistência
        var transaction = new PaymentTransaction {
            Amount = request.Amount,
            CardNumber = request.CardNumber,
            Currency = request.Currency,
            Status = "Approved"
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return new { Message = "Pagamento processado!", Id = transaction.Id };
    }

    public async Task<IEnumerable<PaymentResponseDTO>> GetAllTransactionsAsync() {
        var transactions = await _context.Transactions.ToListAsync();

        return transactions.Select(t => new PaymentResponseDTO(
            t.Id,
            t.Amount,
            t.Status,
            MaskCardNumber(t.CardNumber),
            t.CreatedAt
        ));
    }

    // Método privado para Clean Code: Cada método faz só uma coisa!
    private string MaskCardNumber(string cardNumber) {
        return $"**** **** **** {cardNumber.Substring(cardNumber.Length - 4)}";
    }
}