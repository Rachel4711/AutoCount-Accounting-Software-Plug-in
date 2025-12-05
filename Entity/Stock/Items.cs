using AutoCount.Authentication;
using AutoCount.Stock.Item;
using AutoCount.Utils;
using PlugIn_1.Entity.Stock;
using ItemGroups = PlugIn_1.Entity.Stock.ItemGroups;
using ItemBrands = PlugIn_1.Entity.Stock.ItemBrands;

namespace PlugIn_1.Entity
{
    public class Items
    {
        private readonly UserSession session;

        private readonly ItemDataAccess access;

        internal Items(UserSession userSession)
        {
            session = userSession;

            access = ItemDataAccess.Create(session, session.DBSetting);
        }

        internal void CreateOrUpdate_Item(bool isOverwrite, DbfDataReader.DbfDataReader dbfData)
        {
            string item_code = dbfData.GetString(1);

            ItemEntity item = isOverwrite && hasStockItem(item_code) ? 
                access.LoadItem(item_code, ItemEntryAction.Edit) : access.NewItem();

            item.ItemCode            = item_code;
            item.Description         = dbfData.GetString(9);
            item.FurtherDescription  = RtfUtils.ToArialRichText(dbfData.GetString(10));
            item.ItemCategory        = setItemCategory  (dbfData.GetString(4));
            item.ItemGroup           = setItemGroup     (dbfData.GetString(5));
            item.ItemBrand           = setItemBrand     (dbfData.GetString(11));
            item.ItemType            = setItemType      (dbfData.GetString(218));

            ItemUomEntity itemUom;
            
            if (item.UomCount > 0)
            {
                itemUom = item.GetUom(0);
                itemUom.Uom = dbfData.GetString(17);
            }
            else
            {
                itemUom = item.NewUom(dbfData.GetString(17), 1);
            }

            item.CostingMethod = 1;

            itemUom.Weight               = dbfData.GetDecimal(13);
            itemUom.StandardCost         = dbfData.GetDecimal(18);
            itemUom.StandardSellingPrice = dbfData.GetDecimal(19);
            itemUom.MinSalePrice         = dbfData.GetDecimal(23);
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

        private bool hasStockItem(string item_code)
        {
            return access.LoadItem(item_code, ItemEntryAction.Edit) != null;
        }
    }
}
