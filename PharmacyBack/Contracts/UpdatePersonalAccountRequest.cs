namespace PharmacyBack.Contracts
{
    public record UpdatePersonalAccountRequest(string newLogin, string newEmail, 
        string newPhone, string NewPass, string CurrentPassword);

}
