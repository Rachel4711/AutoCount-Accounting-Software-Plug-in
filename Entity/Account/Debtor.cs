using AutoCount.GL;
using AutoCount.ARAP.Debtor;
using AutoCount.Authentication;
using System;
using System.Linq;
using AutoCount.Data;
using PlugIn_1.Entity.General_Maintainance;
using AutoCount.RegistryID;
using AutoCount.Document;
using AutoCount.ARAP;
using System.Data;

namespace PlugIn_1.Entity
{
    public class Debtor
    {
        private readonly UserSession session;

        internal Debtor(UserSession userSession)
        {
            session = userSession;
        }

        internal void CreateOrUpdate_Debtor(bool isOverwrite, DbfDataReader.DbfDataReader dbfDataReader)
        {
            DebtorDataAccess access = DebtorDataAccess.Create(session, session.DBSetting);

            DebtorEntity debtor = isOverwrite ? 
                access.GetDebtor(dbfDataReader.GetString(1)) : access.NewDebtor();

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
            debtor.AreaCode          = setArea       (dbfDataReader.GetString(22));
            debtor.SalesAgent        = setSalesAgent (dbfDataReader.GetString(23));
            debtor.DisplayTerm       = setDisplayTerm(debtor.DisplayTerm, dbfDataReader.GetString(25));
            debtor.CurrencyCode      = setCurrency   (session.DBSetting, dbfDataReader.GetString(27));
            debtor.CreditLimit       = dbfDataReader.GetDecimal(26);

            if (!isDebtorCtrlAcc(debtor.ControlAccount))
            {
                throw new Exception($"\"{debtor.ControlAccount}\" is not a control account.");
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

        private string setArea(string cust_area)
        {
            Areas area = new Areas(session);

            return area.hasAreas(cust_area) ? 
                cust_area : cust_area != "" ? 
                area.CreateOrUpdate_Area(false, cust_area) : null;
        }

        private string setSalesAgent(string cust_agent)
        {
            Agents agent = new Agents(session);

            return agent.hasSalesAgents(cust_agent) ?
                cust_agent : cust_agent != "" ?
                agent.CreateOrUpdate_SalesAgent(false, cust_agent) : null;
        }

        private string setDisplayTerm(string default_term, string term)
        {
            DisplayTerms terms = new DisplayTerms(Program.session);

            return terms.hasDisplayTerm(term) ? 
                term : term != "" ? 
                terms.Create_DisplayTerm(term) : default_term;
        }

        public string setCurrency(DBSetting dbSetting, string currency_code)
        {
            Currencies currencies = new Currencies(session);

            return currencies.hasCurrency(currency_code) ?
                currency_code : DBRegistry.Create(dbSetting).GetString(new LocalCurrencyCode());
        }
    }
}
