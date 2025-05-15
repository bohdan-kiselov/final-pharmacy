namespace PharmacyBack.Contracts
{
    public record ProductsResponse(string name, string price, string imageUrl, string? description,
        string companyName, string quantity);
    
}
