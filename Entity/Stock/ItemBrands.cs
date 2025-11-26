using AutoCount.Authentication;
using AutoCount.Data.EntityFramework;
using AutoCount.Stock.ItemBrand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemBrands
    {
        private UserSession session;

        public ItemBrands(UserSession userSession)
        {
            userSession = session;
        }

        public string CreateOrUpdate_ItemBrands(string item_brand)
        {
            ItemBrandCommand cmd = ItemBrandCommand.Create(session, session.DBSetting);
            ItemBrandEntity itemBrand = cmd.NewItemBrand();

            itemBrand.ItemBrand = item_brand;
            itemBrand.Save();

            return item_brand;
        }

        public bool hasItemBrand(string item_brand)
        {
            ItemBrandCommand cmd = ItemBrandCommand.Create(session, session.DBSetting);

            return cmd.GetItemBrand(item_brand) != null;
        }
    }
}
