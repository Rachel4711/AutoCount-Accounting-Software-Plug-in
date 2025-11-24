using AutoCount.Authentication;
using AutoCount.Data.EntityFramework;
using AutoCount.Stock.Item;
using AutoCount.Stock.ItemCategory;
using AutoCount.Stock.ItemGroup;
using AutoCount.Stock.Location;
using AutoCount.Utils;
using System;
using System.Windows.Forms;
using static Google.Protobuf.Reflection.SourceCodeInfo.Types;

namespace PlugIn_1.Entity
{
    public class StockItem
    {
        private UserSession session;
        
        internal StockItem(UserSession userSession)
        {
            session = userSession;
        }

        internal void CreateOrUpdate_Item(bool isOverwrite, DbfDataReader.DbfDataReader dbfDataReader)
        {
            string item_code = dbfDataReader.GetString(1);

            ItemDataAccess access = ItemDataAccess.Create(session, session.DBSetting);
            
            ItemEntity item = isOverwrite ? 
                access.LoadItem(item_code, ItemEntryAction.Edit) : access.NewItem();

            item.ItemCode            = item_code;
            item.ItemCategory        = dbfDataReader.GetString(4);
            item.ItemGroup           = dbfDataReader.GetString(5);
            item.Description         = dbfDataReader.GetString(9);
            item.FurtherDescription  = RtfUtils.ToArialRichText(dbfDataReader.GetString(10));
            item.ItemBrand           = dbfDataReader.GetString(11);
            item.ItemType            = dbfDataReader.GetString(218);
            item.ItemClass           = null;

            ItemUomEntity itemUom;
            
            if (item.UomCount > 0)
            {
                itemUom = item.GetUom(0);
                itemUom.Uom = dbfDataReader.GetString(17);
            }
            else
            {
                itemUom = item.NewUom(dbfDataReader.GetString(17), 1);
            }

            item.CostingMethod = 1;

            itemUom.Weight               = dbfDataReader.GetDecimal(13);
            itemUom.StandardCost         = dbfDataReader.GetDecimal(18);
            itemUom.StandardSellingPrice = dbfDataReader.GetDecimal(19);
            itemUom.MinSalePrice         = dbfDataReader.GetDecimal(22);
            itemUom.Rate = 1;

            access.SaveData(item);
        }

        internal void CreateOrUpdate_ItemCategory(bool isOverwrite, DbfDataReader.DbfDataReader dbfDataReader)
        {
            ItemCategoryCommand cmd = ItemCategoryCommand.Create(session, session.DBSetting);

            ItemCategoryEntity itemCategory = isOverwrite ?
                cmd.GetItemCategory(dbfDataReader.GetString(0)) : cmd.NewItemCategory();

            itemCategory.ShortCode   = dbfDataReader.GetString(0);
            itemCategory.Description = dbfDataReader.GetString(1);

            itemCategory.Save();
        }

        internal void CreateOrUpdate_ItemGroup(bool isOverwrite, DbfDataReader.DbfDataReader dbfDataReader)
        {
            ItemGroupCommand cmd = ItemGroupCommand.Create(session, session.DBSetting);

            ItemGroupEntity itemGroup = isOverwrite ? 
                cmd.GetItemGroup(dbfDataReader.GetString(0)) : cmd.NewItemGroup();

            itemGroup.ShortCode   = dbfDataReader.GetString(0);
            itemGroup.Description = dbfDataReader.GetString(1);

            itemGroup.Save();
        }

        internal void CreateOrUpdate_Location(bool isOverwrite, DbfDataReader.DbfDataReader dbfDataReader)
        {
            LocationMaintenance locMaintain = LocationMaintenance.CreateLocationMaint(session, session.DBSetting);
            
            LocationEntity locationEntity = isOverwrite ?
                locMaintain.GetLocation(dbfDataReader.GetString(0)) : locMaintain.NewLocation();

            locationEntity.Location      = dbfDataReader.GetString(0);
            locationEntity.Description   = dbfDataReader.GetString(1);

            locationEntity.Save();
        }
    }
}
