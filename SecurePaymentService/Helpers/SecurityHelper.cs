namespace SecurePaymentService.Helpers;

public static class SecurityHelper {
    public static bool ValidateCardNumber(string cardNumber) {
        // Remove espaços ou traços caso sejam
        cardNumber = cardNumber.Replace(" ", "").Replace("-", "");
        if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Length < 13) return false;
        int sum = 0;
        bool shouldDouble = false;
        // Algoritmo de Luhn
        for (int i = cardNumber.Length - 1; i >= 0; i--) {
            int digit = int.Parse(cardNumber[i].ToString());
            if (shouldDouble){
                digit *= 2;
                if (digit > 9) digit -= 9;
            }
            sum += digit;
            shouldDouble = !shouldDouble;
        }
        return (sum % 10) == 0;
    }
}