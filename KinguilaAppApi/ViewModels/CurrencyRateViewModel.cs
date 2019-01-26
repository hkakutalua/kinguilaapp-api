namespace KinguilaAppApi.ViewModels
{
    public class CurrencyRateViewModel
    {
        public string Currency { get; private set; }
        public decimal Amount { get; private set; }

        public CurrencyRateViewModel(string currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }
        
        private CurrencyRateViewModel() {}
    }
}