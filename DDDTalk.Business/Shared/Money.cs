namespace DDDTalk.Business.Shared
{
    /// <summary>
    /// Value Object
    /// - No identity (two Value Object with same attributes are equals)
    /// - Recommanded to be immutable
    ///     - Could be shared
    ///     - Date integrity
    ///     - Performance
    /// </summary>
    public class Money
    {
        public Money(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public decimal Amount { get; private set; }
        
        public Currency Currency { get; private set; }
    }
}