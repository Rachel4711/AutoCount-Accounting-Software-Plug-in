using AutoCount.Authentication;
using AutoCount.GeneralMaint.Area;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Areas
    {
        private readonly UserSession session;

        public Areas(UserSession userSession)
        {
            session = userSession;
        }

        public string NewArea(string areaCode, string desc)
        {
            AreaCommand cmd = AreaCommand.Create(session, session.DBSetting);

            AreaEntity area = cmd.NewArea();

            area.AreaCode = areaCode;
            area.Description = desc;
            
            area.Save();

            return area.AreaCode;
        }
    }
}
