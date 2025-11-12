using AutoCount.GL;
using AutoCount.ARAP.Debtor;
using AutoCount.Authentication;
using System;

namespace PlugIn_1.Entity
{
    public class Debtor
    {
        private readonly UserSession session;

        internal Debtor(UserSession userSession)
        {
            session = userSession;
        }

        internal void CreateNewDebtor(string ctrl_acc, string debtor_acc, string com_name, string deb_note)
        {
            DebtorDataAccess access = DebtorDataAccess.Create(session, session.DBSetting);

            DebtorEntity debtor = access.NewDebtor();

            debtor.ControlAccount = ctrl_acc;
            debtor.AccNo = debtor_acc;
            debtor.CompanyName = com_name;
            debtor.Note = deb_note;
            
            //debtor.AccNo = GetNewDebtorCode(debtor.ControlAccount, debtor.CompanyName);

            if (!isDebtorCtrlAcc(debtor.ControlAccount))
            {
                throw new Exception($"\"{debtor.ControlAccount}\" is not a control account. Please try with another value.");
            }

            access.SaveDebtor(debtor, session.LoginUserID);
        }

        //internal string GetNewDebtorCode(string controlAccNo, string companyName)
        //{
        //    try
        //    {
        //        return AccountCodeHelper.Create(session.DBSetting)
        //            .GetNextDebtorCode(controlAccNo, companyName);
        //    }
        //    catch (InvalidAutoDebtorCodeFormatException ex)
        //    {
        //        throw ex;
        //    }
        //    catch (AutoCount.Data.DataAccessException ex)
        //    {
        //        throw ex;
        //    }
        //}

        private bool isDebtorCtrlAcc(string debtorCtrlAcc)
        {
            AccountHelper accountHelper = AccountHelper.Create(session.DBSetting);

            return accountHelper.GetDebtorControlAccounts().Contains(debtorCtrlAcc);
        }
    }
}
