using AutoCount.Authentication;
using AutoCount.Stock.Location;

namespace PlugIn_1.Entity.Stock
{
    internal class Locations
    {
        private readonly UserSession session;

        public Locations(UserSession userSession)
        {
            session = userSession;
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
