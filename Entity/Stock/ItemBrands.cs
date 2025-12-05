using AutoCount.Authentication;
using AutoCount.Stock.ItemBrand;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemBrands
    {
        private readonly UserSession session;

        private readonly ItemBrandCommand cmd;

        public ItemBrands(UserSession userSession)
        {
            session = userSession;

            cmd = ItemBrandCommand.Create(session, session.DBSetting);
        }

        public string CreateOrUpdate_ItemBrands(string item_brand)
        {
            ItemBrandEntity itemBrand = cmd.NewItemBrand();

            itemBrand.ItemBrand = item_brand;
            itemBrand.Save();

            return item_brand;
        }

        public bool hasItemBrand(string item_brand)
        {
            return cmd.GetItemBrand(item_brand) != null;
        }
    }
}
