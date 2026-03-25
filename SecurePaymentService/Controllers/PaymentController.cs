using Microsoft.AspNetCore.Mvc;
using SecurePaymentService.DTOs;
using SecurePaymentService.Services;

namespace SecurePaymentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase {
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService) {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDTO request) {
        // Se o Service lançar um ArgumentException (cartão inválido), 
        // o Middleware captura e devolve o BadRequest automaticamente.
        var result = await _paymentService.ProcessPaymentAsync(request);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var response = await _paymentService.GetAllTransactionsAsync();
        return Ok(response);
    }
}