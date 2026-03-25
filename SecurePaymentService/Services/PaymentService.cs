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

    public async Task<PaymentResponseDTO> ProcessPaymentAsync(PaymentRequestDTO request) {
        // 1. Validação de Regra de Negócio Complexa
        // O Amount já foi validado pelo DTO. Mantemos o Luhn aqui porque é uma lógica externa.
        if (!SecurityHelper.ValidateCardNumber(request.CardNumber)) 
            throw new ArgumentException("Cartão inválido pelo algoritmo de Luhn.");

        // 2. Mapeamento para a Entidade de Banco
        var transaction = new PaymentTransaction {
            Amount = request.Amount,
            CardNumber = request.CardNumber,
            Currency = request.Currency,
            Status = "Approved" // Simulação de aprovação imediata
        };

        // 3. Persistência
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        // 4. Retorno usando o DTO de Resposta (Clean Code!)
        return new PaymentResponseDTO(
            transaction.Id,
            transaction.Amount,
            transaction.Status,
            MaskCardNumber(transaction.CardNumber),
            transaction.CreatedAt
        );
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
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4) return "****";
        return $"**** **** **** {cardNumber.Substring(cardNumber.Length - 4)}";
    }
}