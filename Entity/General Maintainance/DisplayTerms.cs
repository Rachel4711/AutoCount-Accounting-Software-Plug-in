using AutoCount.ARAP;
using AutoCount.Authentication;
using AutoCount.Data.EntityFramework;
using AutoCount.Document;
using AutoCount.GeneralMaint.Area;
using AutoCount.GeneralMaint.Terms;
using System.Data;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class DisplayTerms
    {
        private readonly UserSession session;
        
        public DisplayTerms(UserSession userSession)
        {
            session = userSession;
        }

        public string Create_DisplayTerm(string term)
        {
            TermsMaintenance termsMaintenance = TermsMaintenance.CreateTermsMaint(session, session.DBSetting);

            termsMaintenance.Load();

            DataRow row = termsMaintenance.DataTableTerms.NewRow();

            row["DisplayTerm"] = term;
            row["Terms"] = term;
            row["LastUpdate"] = 0;

            termsMaintenance.DataTableTerms.Rows.Add(row);

            termsMaintenance.Save();

            return term;
        }

        public bool hasDisplayTerm(string term)
        {
            TermsMaintenance termsMaintenance = TermsMaintenance.CreateTermsMaint(session, session.DBSetting);

            termsMaintenance.Load();

            foreach (DataRow row in termsMaintenance.DataTableTerms.Rows)
            {
                if (row["DisplayTerm"].ToString().ToUpper() == term.ToUpper()) return true;
            }

            return false;
        }
    }
}
