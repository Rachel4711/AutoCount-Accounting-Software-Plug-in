using AutoCount.Authentication;
using AutoCount.GeneralMaint.Area;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Areas
    {
        private readonly UserSession session;

        private readonly AreaCommand cmd;

        public Areas(UserSession userSession)
        {
            session = userSession;

            cmd = AreaCommand.Create(session, session.DBSetting);
        }

        public string CreateOrUpdate_Area(bool isOverwrite, string area_name, string desc = "")
        {
            AreaEntity area = isOverwrite && hasAreas(area_name) ? cmd.GetArea(area_name) : cmd.NewArea();

            area.AreaCode = area_name;
            area.Description = desc;

            area.Save();

            return area_name;
        }

        public bool hasAreas(string area_code)
        {
            return cmd.GetArea(area_code) != null;
        }

        public void DeleteArea(string area_code)
        {
            cmd.DeleteArea(area_code);
        }
    }
}
