using AutoCount.ARAP.Creditor;
using AutoCount.ARAP.Debtor;
using AutoCount.Authentication;
using AutoCount.Data;
using AutoCount.GL;
using AutoCount.RegistryID;
using PlugIn_1.Entity.General_Maintainance;
using System;
using System.Linq;

namespace PlugIn_1.Entity
{
    public class Creditor
    {
        private readonly UserSession session;

        internal Creditor(UserSession userSession)
        {
            session = userSession;
        }

        public void CreateOrUpdate_Creditor(bool isOverwrite, string acc_no, DbfDataReader.DbfDataReader dbfDataReader)
        {
            CreditorDataAccess access = CreditorDataAccess.Create(session, session.DBSetting);

            CreditorEntity creditor = isOverwrite && hasCreditor(acc_no) ? 
                access.GetCreditor(acc_no) : access.NewCreditor();

            creditor.ControlAccount = getDefaultCtrlAcc();

            creditor.AccNo           = acc_no;
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
            creditor.AreaCode        = setArea         (dbfDataReader.GetString(22));
            creditor.PurchaseAgent   = setPurchaseAgent(dbfDataReader.GetString(23));
            creditor.CurrencyCode    = setCurrency(session.DBSetting, dbfDataReader.GetString(27));
            creditor.DisplayTerm     = setDisplayTerm(creditor.DisplayTerm, dbfDataReader.GetString(25));
            creditor.CreditLimit     = dbfDataReader.GetDecimal(26);

            if (!isCreditorCtrlAcc(creditor.ControlAccount))
            {
                throw new Exception($"\"{creditor.ControlAccount}\" is not a control account. Please try with another value.");
            }

            access.SaveCreditor(creditor, session.LoginUserID);
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
                currency_code : DBRegistry.Create(dbSetting).GetString(new LocalCurrencyCode());
        }

        public bool hasCreditor(string acc_no)
        {
            try
            {
                CreditorDataAccess.Create(session, session.DBSetting).GetCreditor(acc_no);
                return true;
            }
            catch (DebtorRecordNotFoundException)
            {
                return false;
            }
        }
    }
}
