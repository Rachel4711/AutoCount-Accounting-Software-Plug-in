using AutoCount.Authentication;
using AutoCount.GeneralMaint.Currency;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Currencies
    {
        private readonly UserSession session;

        private readonly CurrencyCommand cmd;

        public Currencies(UserSession userSession)
        {
            session = userSession;

            cmd = CurrencyCommand.Create(session, session.DBSetting);
        }

        public string CreateOrUpdate_Currency(bool isOverwrite, string currencyCode, string currencyWord = "")
        {
            CurrencyEntity currency = isOverwrite && hasCurrency(currencyCode) ? 
                cmd.GetCurrency(currencyCode) : cmd.NewCurrency();

            currency.CurrencyCode = currencyCode;
            currency.CurrencyWord = currencyWord;

            currency.Save();

            return currencyCode;
        }

        public bool hasCurrency(string currency_code)
        {
            return cmd.GetCurrency(currency_code) != null;
        }
    }
}
