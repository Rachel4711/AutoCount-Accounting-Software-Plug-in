using AutoCount.GL;
using AutoCount.ARAP.Debtor;
using AutoCount.Authentication;
using System;
using System.Linq;
using AutoCount.Data;
using PlugIn_1.Entity.General_Maintainance;
using AutoCount.RegistryID;
using System.Text.RegularExpressions;

namespace PlugIn_1.Entity
{
    public class Debtor
    {
        private readonly UserSession session;

        private DebtorDataAccess access;
        private AccountHelper accountHelper;

        internal Debtor(UserSession userSession)
        {
            session = userSession;

            accountHelper = AccountHelper.Create(session.DBSetting);
            access = DebtorDataAccess.Create(session, session.DBSetting);
        }

        internal void CreateOrUpdate_Debtor(bool isOverwrite, string acc_no, DbfDataReader.DbfDataReader dbfData)
        {
            DebtorEntity debtor = isOverwrite && hasDebtor(acc_no, dbfData.GetString(2)) ? 
                access.GetDebtor(acc_no) : access.NewDebtor();

            debtor.ControlAccount    = getDebtorCtrlAcc();

            debtor.AccNo             = acc_no;
            debtor.CompanyName       = dbfData.GetString(2);
            debtor.Address1          = dbfData.GetString(4);
            debtor.Address2          = dbfData.GetString(5);
            debtor.Address3          = dbfData.GetString(6);
            debtor.Address4          = dbfData.GetString(7);
            debtor.Attention         = dbfData.GetString(8);
            debtor.DeliverAddr1      = dbfData.GetString(9);
            debtor.DeliverAddr2      = dbfData.GetString(10);
            debtor.DeliverAddr3      = dbfData.GetString(11);
            debtor.DeliverAddr4      = dbfData.GetString(12);
            debtor.Mobile            = dbfData.GetString(14);
            debtor.Phone1            = dbfData.GetString(15);
            debtor.Phone2            = dbfData.GetString(16);
            debtor.Fax1              = dbfData.GetString(17);
            debtor.EmailAddress      = dbfData.GetString(18);
            debtor.WebURL            = dbfData.GetString(19);
            debtor.AreaCode          = setArea       (dbfData.GetString(22));
            debtor.SalesAgent        = setSalesAgent (dbfData.GetString(23));
            debtor.DisplayTerm       = setDisplayTerm(debtor.DisplayTerm, dbfData.GetString(25));
            debtor.CurrencyCode      = setCurrency   (session.DBSetting, dbfData.GetString(27));
            debtor.CreditLimit       = dbfData.GetDecimal(26);

            access.SaveDebtor(debtor, session.LoginUserID);
        }

        private string getDebtorCtrlAcc()
        {
            return accountHelper.GetDebtorControlAccounts().Find(
                ac => Regex.Match(ac, @"^300(0/|-0)000$").Success);
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
                currency_code : currency_code == "" ?
                    DBRegistry.Create(dbSetting).GetString(new LocalCurrencyCode()) :
                    currencies.CreateOrUpdate_Currency(false, currency_code);
        }

        public bool hasDebtor(string acc_no, string com_name)
        {
            try
            { return com_name == access.GetDebtor(acc_no).CompanyName; }
            catch (DebtorRecordNotFoundException)
            { return false; }
        }

        public bool hasDebtor(string acc_no)
        {
            try
            { return access.GetDebtor(acc_no) != null; }
            catch (DebtorRecordNotFoundException)
            { return false; }
        }

        public void DeleteDebtor(string acc_no)
        {
            access.DeleteDebtor(acc_no);
        }
    }
}
