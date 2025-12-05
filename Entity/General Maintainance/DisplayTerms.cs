using AutoCount.Authentication;
using AutoCount.GeneralMaint.Terms;
using System.Data;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class DisplayTerms
    {
        private readonly UserSession session;

        private readonly TermsMaintenance termsMaintenance;

        public DisplayTerms(UserSession userSession)
        {
            session = userSession;

            termsMaintenance = TermsMaintenance.CreateTermsMaint(session, session.DBSetting);
        }

        public string Create_DisplayTerm(string term)
        {
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
            termsMaintenance.Load();

            foreach (DataRow row in termsMaintenance.DataTableTerms.Rows)
            {
                if (row["DisplayTerm"].ToString().ToUpper() == term.ToUpper()) return true;
            }

            return false;
        }
    }
}
