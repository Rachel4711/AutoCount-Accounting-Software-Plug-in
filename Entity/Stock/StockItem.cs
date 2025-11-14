using AutoCount.Authentication;
using AutoCount.Utils;
using AutoCount.Stock.Item;
using System;
using System.Windows.Forms;
using AutoCount.Stock.Location;

namespace PlugIn_1.Entity
{
    public class StockItem
    {
        private UserSession session;
        
        internal StockItem(UserSession userSession)
        {
            session = userSession;
        }

        internal void CreateUpdateItem()
        {
            string item_code = "Item102";
            string description = "Description";
            string further_desc = "Further Description";
            string item_group = null;
            string UOM = "UNIT";
            decimal price = 30m;
            decimal cost = 10m;

            ItemDataAccess access = ItemDataAccess.Create(session, session.DBSetting);
            ItemEntity item;
            ItemUomEntity itemUom;

            if (!access.IsItemCodeUsed(item_code))
            {
                item = access.NewItem();
                item.ItemCode = item_code;
                item.Description = description;
                item.FurtherDescription = RtfUtils.ToArialRichText(further_desc);
                item.ItemGroup = item_group;
                item.ItemType = null;
                item.ItemBrand = null;
                item.ItemCategory = null;
                item.ItemClass = null;

                if (item.UomCount > 0)
                {
                    itemUom = item.GetUom(0);
                    itemUom.Uom = UOM;
                    itemUom.Rate = 1;
                }
                else
                {
                    itemUom = item.NewUom(UOM, 1);
                }

                item.CostingMethod = 1;

                itemUom.StandardCost = cost;
                itemUom.StandardSellingPrice = price;

                access.SaveData(item);
            }
        }

        internal void CreateUpdateLocation()
        {
            string location = "AD";
            
            LocationMaintenance maintain = LocationMaintenance.CreateLocationMaint(session, session.DBSetting);
            LocationEntity locationEntity;

            locationEntity = maintain.GetLocation(location);

            if (locationEntity == null)
            {
                locationEntity = maintain.NewLocation();
                locationEntity.Location = location;
                locationEntity.Description = "Ara Damansara";

                locationEntity.Save();
            }
            else
            {
                throw new Exception($"The location {locationEntity.Location} has already exist, please try with another location.");
            }
        }
    }
}
