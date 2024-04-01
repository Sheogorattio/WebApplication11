var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

var exchangeRates = new Dictionary<string, decimal>
{
    { "USD", 1 },
    { "EUR", 0.85m },
    { "CAD", 1.35m },
    { "UAH", 39.21m },
};

app.MapGet("/supportedCurrencies", () =>
{
    return Results.Ok(exchangeRates.Keys);
});

app.MapGet("/exchangeRate/{sourceCurrency}/{targetCurrency}", (string sourceCurrency, string targetCurrency) =>
{
    if (!exchangeRates.ContainsKey(sourceCurrency) || !exchangeRates.ContainsKey(targetCurrency))
    {
        return Results.NotFound($"Currency not supported");
    }

    var exchangeRate = exchangeRates[targetCurrency] / exchangeRates[sourceCurrency];
    return Results.Ok(exchangeRate);
});

app.MapGet("/convertCurrency/{sourceCurrency}/{targetCurrency}/{amount}", (string sourceCurrency, string targetCurrency, decimal amount) =>
{
    if (!exchangeRates.ContainsKey(sourceCurrency) || !exchangeRates.ContainsKey(targetCurrency))
    {
        return Results.NotFound($"Currency not supported");
    }

    var exchangeRate = exchangeRates[targetCurrency] / exchangeRates[sourceCurrency];
    var convertedAmount = amount * exchangeRate;
    return Results.Ok(convertedAmount);
});

app.Run();
