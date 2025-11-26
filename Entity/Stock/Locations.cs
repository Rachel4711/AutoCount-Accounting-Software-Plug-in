using AutoCount.Authentication;
using AutoCount.Stock.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1.Entity.Stock
{
    internal class Locations
    {
        private UserSession session;

        public Locations(UserSession userSession)
        {
            userSession = session;
        }

        internal void CreateOrUpdate_Location(bool isOverwrite, string location, string desc = "")
        {
            LocationMaintenance locMaintain = LocationMaintenance.CreateLocationMaint(session, session.DBSetting);

            LocationEntity locationEntity = isOverwrite ?
                locMaintain.GetLocation(location) : locMaintain.NewLocation();

            locationEntity.Location = location;
            locationEntity.Description = desc;

            locationEntity.Save();
        }
    }
}
