using AutoCount.Authentication;
using AutoCount.GeneralMaint.Currency;
using AutoCount.GeneralMaint.PurchaseAgent;
using AutoCount.GeneralMaint.SalesAgent;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Currencies
    {
        private readonly UserSession session;

        public Currencies(UserSession userSession)
        {
            session = userSession;
        }

        public string CreateOrUpdate_Currency(bool isOverwrite, string currencyCode, string currencyWord = "")
        {
            CurrencyCommand cmd = CurrencyCommand.Create(session, session.DBSetting);

            CurrencyEntity currency = isOverwrite ? cmd.GetCurrency(currencyCode) : cmd.NewCurrency();

            currency.CurrencyCode = currencyCode;
            currency.CurrencyWord = currencyWord;

            currency.Save();

            return currencyCode;
        }

        public bool hasCurrency(string currency_code)
        {
            CurrencyCommand cmd = CurrencyCommand.Create(session, session.DBSetting);

            return cmd.GetCurrency(currency_code) != null;
        }
    }
}
