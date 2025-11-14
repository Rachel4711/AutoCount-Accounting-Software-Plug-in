using AutoCount.Authentication;
using AutoCount.GeneralMaint.Currency;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Currencies
    {
        private readonly UserSession session;

        public Currencies(UserSession userSession)
        {
            session = userSession;
        }

        public string NewCurrency(string currencyCode, string currencyWord = "")
        {
            CurrencyCommand cmd = CurrencyCommand.Create(session, session.DBSetting);

            CurrencyEntity currency = cmd.NewCurrency();

            currency.CurrencyCode = currencyCode;
            currency.CurrencyWord = currencyWord;

            currency.Save();

            return currency.CurrencyCode.ToString();
        }
    }
}
