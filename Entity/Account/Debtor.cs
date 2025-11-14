using AutoCount.GL;
using AutoCount.ARAP.Debtor;
using AutoCount.Authentication;
using System;
using System.Linq;
using AutoCount.Data.EntityFramework;
using PlugIn_1.Entity.General_Maintainance;

namespace PlugIn_1.Entity
{
    public class Debtor
    {
        private readonly UserSession session;

        internal Debtor(UserSession userSession)
        {
            session = userSession;
        }

        internal void CreateNewDebtor(DbfDataReader.DbfDataReader dbfDataReader)
        {
            DebtorDataAccess access = DebtorDataAccess.Create(session, session.DBSetting);

            DebtorEntity debtor = access.NewDebtor();

            debtor.ControlAccount    = getDefaultCtrlAcc();

            debtor.AccNo             = dbfDataReader.GetString(1);
            debtor.CompanyName       = dbfDataReader.GetString(2);
            debtor.Address1          = dbfDataReader.GetString(4);
            debtor.Address2          = dbfDataReader.GetString(5);
            debtor.Address3          = dbfDataReader.GetString(6);
            debtor.Address4          = dbfDataReader.GetString(7);
            debtor.Attention         = dbfDataReader.GetString(8);
            debtor.DeliverAddr1      = dbfDataReader.GetString(9);
            debtor.DeliverAddr2      = dbfDataReader.GetString(10);
            debtor.DeliverAddr3      = dbfDataReader.GetString(11);
            debtor.DeliverAddr4      = dbfDataReader.GetString(12);
            debtor.Mobile            = dbfDataReader.GetString(14);
            debtor.Phone1            = dbfDataReader.GetString(15);
            debtor.Phone2            = dbfDataReader.GetString(16);
            debtor.Fax1              = dbfDataReader.GetString(17);
            debtor.EmailAddress      = dbfDataReader.GetString(18);
            debtor.WebURL            = dbfDataReader.GetString(19);
            debtor.AreaCode          = dbfDataReader.GetString(22).Equals("") ? setDefArea() : dbfDataReader.GetString(22);
            debtor.SalesAgent        = dbfDataReader.GetString(23).Equals("") ? setDefSalesAgent() : dbfDataReader.GetString(23);
            debtor.DisplayTerm       = dbfDataReader.GetString(25).Equals("") ? debtor.DisplayTerm : dbfDataReader.GetString(25);
            debtor.CreditLimit       = dbfDataReader.GetDecimal(26);
            debtor.CurrencyCode      = dbfDataReader.GetString(27).Equals("") ? AccountBookLocalCurrency(session.DBSetting) : dbfDataReader.GetString(28);

            if (!isDebtorCtrlAcc(debtor.ControlAccount))
            {
                throw new Exception($"\"{debtor.ControlAccount}\" is not a control account. Please try with another value.");
            }

            access.SaveDebtor(debtor, session.LoginUserID);
        }

        private bool isDebtorCtrlAcc(string debtorCtrlAcc)
        {
            AccountHelper accountHelper = AccountHelper.Create(session.DBSetting);

            return accountHelper.GetDebtorControlAccounts().Contains(debtorCtrlAcc);
        }

        private string getDefaultCtrlAcc()
        {
            AccountHelper accountHelper = AccountHelper.Create(session.DBSetting);

            return accountHelper.GetDebtorControlAccounts().First();
        }

        private string setDefSalesAgent()
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
