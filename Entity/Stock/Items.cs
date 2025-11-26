using AutoCount.Authentication;
using AutoCount.Stock.Item;
using AutoCount.Stock.Location;
using AutoCount.Utils;
using PlugIn_1.Entity.Stock;
using ItemGroups = PlugIn_1.Entity.Stock.ItemGroups;
using ItemBrands = PlugIn_1.Entity.Stock.ItemBrands;

namespace PlugIn_1.Entity
{
    public class Items
    {
        private UserSession session;
        
        internal Items(UserSession userSession)
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
            item.Description         = dbfDataReader.GetString(9);
            item.FurtherDescription  = RtfUtils.ToArialRichText(dbfDataReader.GetString(10));
            item.ItemCategory        = setItemCategory(dbfDataReader.GetString(4));
            item.ItemGroup           = setItemGroup(dbfDataReader.GetString(5));
            item.ItemBrand           = setItemBrand(dbfDataReader.GetString(11));
            item.ItemType            = setItemType(dbfDataReader.GetString(218));

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
            itemUom.MinSalePrice         = dbfDataReader.GetDecimal(23);
            itemUom.Rate = 1;

            access.SaveData(item);
        }

        private string setItemCategory(string short_code)
        {
            ItemCategories categories = new ItemCategories(session);

            return categories.hasItemCategory(short_code) ? 
                short_code : short_code != "" ? 
                categories.CreateOrUpdate_ItemCategory(false, short_code) : null;
        }

        private string setItemGroup(string item_group)
        {
            ItemGroups groups = new ItemGroups(session);
            
            return groups.hasItemGroup(item_group) ? 
                item_group : item_group != "" ? 
                groups.CreateOrUpdate_ItemGroup(false, item_group) : null;
        }

        private string setItemBrand(string item_brand)
        {
            ItemBrands brands = new ItemBrands(session);

            return brands.hasItemBrand(item_brand) ? 
                item_brand : item_brand != "" ? 
                brands.CreateOrUpdate_ItemBrands(item_brand) : null;
        }

        private string setItemType(string item_type)
        {
            ItemTypes types = new ItemTypes(session);

            return types.hasItemType(item_type) ? 
                item_type : item_type != "" ? 
                types.CreateOrUpdate_ItemType(item_type) : null;
        }
    }
}
