namespace PharmacyBack.Contracts
{
    public record PasswordResetRequest(string NewPassword, string Token);
}
