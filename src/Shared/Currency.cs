namespace Bookify.Domain.Shared;

public class Currency
{
    public static readonly Currency None = new("");
    
    public static readonly Currency Usd = new("USD");
    
    public static readonly Currency Eur = new("Eur");

    public static readonly IReadOnlyCollection<Currency> All =
    [
        Usd,
        Eur
    ];
        
    public string Code { get; init; }

    public Currency(string code) => Code = code;

    public static Currency FromCode(string code)
    {
        return All.FirstOrDefault(c => c.Code == code) ??
               throw new ApplicationException("The currency code is invalid");
    }
}