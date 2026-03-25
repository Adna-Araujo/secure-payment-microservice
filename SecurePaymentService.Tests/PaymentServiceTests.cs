using Moq;
using FluentAssertions;
using SecurePaymentService.Services;
using SecurePaymentService.Data;
using SecurePaymentService.DTOs;
using SecurePaymentService.Shared.Constants;
using SecurePaymentService.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace SecurePaymentService.Tests;

public class PaymentServiceTests {
    private readonly PaymentService _sut; // SUT = System Under Test
    private readonly AppDbContext _context;

    public PaymentServiceTests() {
        // Criamos um banco de dados em memória exclusivo para este teste
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new AppDbContext(options);
        _sut = new PaymentService(_context);
    }

    [Fact(DisplayName = "Deve lançar exceção quando o cartão for inválido (Luhn)")]
    public async Task ProcessPaymentAsync_ShouldThrowException_WhenCardIsInvalid() {
        // Arrange
        var request = new PaymentRequestDTO(150.00m, "1234567890123", PaymentConstants.DefaultCurrency);

        // Act
        Func<Task> act = async () => await _sut.ProcessPaymentAsync(request);

        // Assert
        await act.Should().ThrowAsync<ArgumentException>()
                 .WithMessage("O número do cartão fornecido é inválido pelo algoritmo de Luhn.");
    }

    [Fact(DisplayName = "Deve processar pagamento com sucesso e salvar no banco")]
    public async Task ProcessPaymentAsync_ShouldSucceed_WhenDataIsValid() {
        // Arrange - Usando um número de teste que passa no Luhn (ex: 49927398716)
        var validCard = "4111111111111111"; 
        var request = new PaymentRequestDTO(250.00m, validCard, PaymentConstants.DefaultCurrency);

        // Act
        var result = await _sut.ProcessPaymentAsync(request);

        // Assert
        result.Should().NotBeNull();
        result.Status.Should().Be(PaymentStatus.Approved.ToString());
        result.Amount.Should().Be(250.00m);
        
        // Verifica se realmente salvou no banco de dados "fake"
        var transactionInDb = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == result.Id);
        transactionInDb.Should().NotBeNull();
        transactionInDb!.Amount.Should().Be(250.00m);
    }
}