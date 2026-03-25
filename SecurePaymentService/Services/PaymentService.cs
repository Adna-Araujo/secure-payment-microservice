using SecurePaymentService.Data;
using SecurePaymentService.Models;
using SecurePaymentService.Helpers;
using SecurePaymentService.DTOs;
using SecurePaymentService.Shared.Enums;      // IMPORTANTE: Adicionado
using SecurePaymentService.Shared.Constants;  // IMPORTANTE: Adicionado
using Microsoft.EntityFrameworkCore;

namespace SecurePaymentService.Services;

public class PaymentService : IPaymentService {
    private readonly AppDbContext _context;

    public PaymentService(AppDbContext context) {
        _context = context;
    }

    public async Task<PaymentResponseDTO> ProcessPaymentAsync(PaymentRequestDTO request) {
        if (!SecurityHelper.ValidateCardNumber(request.CardNumber)) {
    // Lançamos uma ArgumentException, que será capturada pelo nosso Middleware e resultará em um 400 Bad Request.
            throw new ArgumentException("O número do cartão fornecido é inválido pelo algoritmo de Luhn.");
    }
        var transaction = new PaymentTransaction {
            Amount = request.Amount,
            CardNumber = request.CardNumber,
            Currency = request.Currency,
            Status = PaymentStatus.Approved // USANDO O ENUM (Sem aspas!)
        };

        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();

        return new PaymentResponseDTO(
            transaction.Id,
            transaction.Amount,
            transaction.Status.ToString(), // CONVERTENDO ENUM PARA STRING AQUI
            MaskCardNumber(transaction.CardNumber),
            transaction.CreatedAt
        );
    }

    public async Task<IEnumerable<PaymentResponseDTO>> GetAllTransactionsAsync() {
        var transactions = await _context.Transactions.ToListAsync();

        return transactions.Select(t => new PaymentResponseDTO(
            t.Id,
            t.Amount,
            t.Status.ToString(), // CONVERTENDO ENUM PARA STRING AQUI
            MaskCardNumber(t.CardNumber),
            t.CreatedAt
        ));
    }

    private string MaskCardNumber(string cardNumber) {
        if (string.IsNullOrEmpty(cardNumber) || cardNumber.Length < 4) return "****";
        return $"**** **** **** {cardNumber.Substring(cardNumber.Length - 4)}";
    }
}