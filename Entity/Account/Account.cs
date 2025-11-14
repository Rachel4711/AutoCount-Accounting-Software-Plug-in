using AutoCount.ARAP.ARPayment;
using AutoCount.Authentication;
using AutoCount.Data.MasterTables;
using AutoCount.GL.AccountMaintenance;
using AutoCount.GL.AccType;
using AutoCount.Invoicing.PriceHistory;
using System.Data;

namespace PlugIn_1.Entity.Account
{
    internal class Account
    {
        private readonly UserSession session;

        public Account(UserSession userSession) 
        {
            session = userSession;
        }

        public void CreateAccount(DbfDataReader.DbfDataReader dbfDataReader)
        {
            
        }
    }
}
