using AutoCount.ARAP.Creditor;
using AutoCount.Authentication;
using AutoCount.Data;
using AutoCount.GL;
using AutoCount.RegistryID;
using PlugIn_1.Entity.General_Maintainance;
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace PlugIn_1.Entity
{
    public class Creditor
    {
        private readonly UserSession session;

        private CreditorDataAccess access;
        private AccountHelper accountHelper;

        internal Creditor(UserSession userSession)
        {
            session = userSession;

            accountHelper = AccountHelper.Create(session.DBSetting);
            access = CreditorDataAccess.Create(session, session.DBSetting);
        }

        public void CreateOrUpdate_Creditor(bool isOverwrite, string acc_no, DbfDataReader.DbfDataReader dbfData)
        {
            CreditorEntity creditor = isOverwrite && hasCreditor(acc_no, dbfData.GetString(2)) ? 
                access.GetCreditor(acc_no) : access.NewCreditor();

            creditor.ControlAccount = getCreditorCtrlAcc();

            creditor.AccNo           = acc_no;
            creditor.CompanyName     = dbfData.GetString(2);
            creditor.Address1        = dbfData.GetString(4);
            creditor.Address2        = dbfData.GetString(5);
            creditor.Address3        = dbfData.GetString(6);
            creditor.Address4        = dbfData.GetString(7);
            creditor.Attention       = dbfData.GetString(9);
            creditor.Mobile          = dbfData.GetString(14);
            creditor.Phone1          = dbfData.GetString(15);
            creditor.Phone2          = dbfData.GetString(16);
            creditor.Fax1            = dbfData.GetString(17);
            creditor.EmailAddress    = dbfData.GetString(18);
            creditor.WebURL          = dbfData.GetString(19);
            creditor.AreaCode        = setArea         (dbfData.GetString(22));
            creditor.PurchaseAgent   = setPurchaseAgent(dbfData.GetString(23));
            creditor.CurrencyCode    = setCurrency(session.DBSetting, dbfData.GetString(27));
            creditor.DisplayTerm     = setDisplayTerm(creditor.DisplayTerm, dbfData.GetString(25));
            creditor.CreditLimit     = dbfData.GetDecimal(26);

            access.SaveCreditor(creditor, session.LoginUserID);
        }

        private string getCreditorCtrlAcc()
        {
            return accountHelper.GetCreditorControlAccounts().Find(
                ac => Regex.Match(ac, @"^400(0/|-0)000$").Success);
        }

        private string setArea(string cust_area)
        {
            Areas area = new Areas(session);

            return area.hasAreas(cust_area) ?
                cust_area : cust_area != "" ?
                area.CreateOrUpdate_Area(false, cust_area) : null;
        }

        private string setPurchaseAgent(string cust_agent)
        {
            Agents agent = new Agents(session);

            return agent.hasPurchaseAgents(cust_agent) ?
                    cust_agent : cust_agent != "" ?
                    agent.CreateOrUpdate_PurchaseAgent(false, cust_agent) : null;
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

        public bool hasCreditor(string acc_no, string com_name)
        {
            try
            { return com_name == access.GetCreditor(acc_no).CompanyName; }
            catch (CreditorRecordNotFoundException)
            { return false; }
        }

        public bool hasCreditor(string acc_no)
        {
            try
            { return access.GetCreditor(acc_no) != null; }
            catch (CreditorRecordNotFoundException)
            { return false; }
        }

        public void DeleteCreditor(string acc_no)
        {
            access.DeleteCreditor(acc_no);
        }
    }
}
