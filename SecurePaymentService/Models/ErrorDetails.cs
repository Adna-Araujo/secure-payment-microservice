namespace SecurePaymentService.Models;

public class ErrorDetails {
    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
    public string? Trace { get; set; } // Opcional: útil em desenvolvimento

    public override string ToString() => System.Text.Json.JsonSerializer.Serialize(this);
}