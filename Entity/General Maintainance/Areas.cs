using AutoCount.Authentication;
using AutoCount.GeneralMaint.Area;
using AutoCount.Exceptions;
using System;
using AutoCount.Data;
using AutoCount.Data.EntityFramework;

namespace PlugIn_1.Entity.General_Maintainance
{
    internal class Areas
    {
        private readonly UserSession session;

        public Areas(UserSession userSession)
        {
            session = userSession;
        }

        public string CreateOrUpdate_Area(bool isOverwrite, string area_name, string desc = "")
        {
            AreaCommand cmd = AreaCommand.Create(session, session.DBSetting);

            AreaEntity area = isOverwrite ? cmd.GetArea(area_name) : cmd.NewArea();

            area.AreaCode = area_name;
            area.Description = desc;

            area.Save();

            return area_name;
        }

        public bool hasAreas(string area_code)
        {
            AreaCommand cmd = AreaCommand.Create(session, session.DBSetting);

            return cmd.GetArea(area_code) != null;
        }
    }
}
