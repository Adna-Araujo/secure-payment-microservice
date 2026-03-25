using Microsoft.AspNetCore.Mvc;
using SecurePaymentService.DTOs;
using SecurePaymentService.Services;

namespace SecurePaymentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase {
    private readonly IPaymentService _paymentService;

    // Agora injetamos o SERVICE, não o Banco de Dados!
    public PaymentController(IPaymentService paymentService) {
        _paymentService = paymentService;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDTO request) {
        try {
            var result = await _paymentService.ProcessPaymentAsync(request);
            return Ok(result);
        } catch (ArgumentException ex) {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var response = await _paymentService.GetAllTransactionsAsync();
        return Ok(response);
    }
}