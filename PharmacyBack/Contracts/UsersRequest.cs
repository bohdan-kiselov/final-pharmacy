namespace PharmacyBack.Contracts
{
    public record UsersRequest(string Login, string Email, string Password, string Phone);
}
