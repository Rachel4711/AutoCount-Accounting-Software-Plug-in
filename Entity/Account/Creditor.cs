using AutoCount.ARAP.Creditor;
using AutoCount.Authentication;
using AutoCount.Data.EntityFramework;
using AutoCount.GL;
using PlugIn_1.Entity.General_Maintainance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1.Entity.Account
{
    public class Creditor
    {
        private readonly UserSession session;

        internal Creditor(UserSession userSession)
        {
            session = userSession;
        }

        public void CreateNewCreditor(DbfDataReader.DbfDataReader dbfDataReader)
        {
            CreditorDataAccess cmd = CreditorDataAccess.Create(session, session.DBSetting);
            
            CreditorEntity creditor = cmd.NewCreditor();

            creditor.ControlAccount = getDefaultCtrlAcc();

            creditor.AccNo           = dbfDataReader.GetString(1);
            creditor.CompanyName     = dbfDataReader.GetString(2);
            creditor.Address1        = dbfDataReader.GetString(4);
            creditor.Address2        = dbfDataReader.GetString(5);
            creditor.Address3        = dbfDataReader.GetString(6);
            creditor.Address4        = dbfDataReader.GetString(7);
            creditor.Attention       = dbfDataReader.GetString(9);
            creditor.Mobile          = dbfDataReader.GetString(14);
            creditor.Phone1          = dbfDataReader.GetString(15);
            creditor.Phone2          = dbfDataReader.GetString(16);
            creditor.Fax1            = dbfDataReader.GetString(17);
            creditor.EmailAddress    = dbfDataReader.GetString(18);
            creditor.WebURL          = dbfDataReader.GetString(19);
            creditor.AreaCode        = dbfDataReader.GetString(22).Equals("") ? setDefArea() : dbfDataReader.GetString(22);
            creditor.PurchaseAgent   = dbfDataReader.GetString(23).Equals("") ? setDefPurchaseAgent() : dbfDataReader.GetString(23);
            creditor.DisplayTerm     = dbfDataReader.GetString(25).Equals("") ? creditor.DisplayTerm : dbfDataReader.GetString(25);
            creditor.CreditLimit     = dbfDataReader.GetDecimal(26);
            creditor.CurrencyCode    = dbfDataReader.GetString(27).Equals("") ? AccountBookLocalCurrency(session.DBSetting) : dbfDataReader.GetString(27); ;

            if (!isCreditorCtrlAcc(creditor.ControlAccount))
            {
                throw new Exception($"\"{creditor.ControlAccount}\" is not a control account. Please try with another value.");
            }

            cmd.SaveCreditor(creditor, session.LoginUserID);
        }

        private bool isCreditorCtrlAcc(string creditorCtrlAcc)
        {
            AccountHelper accountHelper = AccountHelper.Create(session.DBSetting);

            return accountHelper.GetCreditorControlAccounts().Contains(creditorCtrlAcc);
        }

        private string getDefaultCtrlAcc()
        {
            AccountHelper accountHelper = AccountHelper.Create(session.DBSetting);

            return accountHelper.GetCreditorControlAccounts().First();
        }
        private string setDefPurchaseAgent()
        {
            Agents agent = new Agents(session);

            return agent.NewAgent("100-001", "Kim", "kim@gmail.com");
        }

        private string setDefArea()
        {
            Areas area = new Areas(session);

            return area.NewArea("KL", "Kuala Lumpur");
        }

        private string setDefCurrency()
        {
            Currencies currencies = new Currencies(session);

            return currencies.NewCurrency("KRW", "Korean Weon");
        }

        public string AccountBookLocalCurrency(AutoCount.Data.DBSetting dbSetting)
        {
            return AutoCount.Data.DBRegistry.Create(dbSetting).GetString(new AutoCount.RegistryID.LocalCurrencyCode());
        }
    }
}
