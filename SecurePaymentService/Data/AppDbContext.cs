using Microsoft.EntityFrameworkCore;
using SecurePaymentService.Models;

namespace SecurePaymentService.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<PaymentTransaction> Transactions { get; set; }
}