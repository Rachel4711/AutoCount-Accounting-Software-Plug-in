using AutoCount.Authentication;
using AutoCount.Stock.ItemCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemCategories
    {
        private UserSession session;

        public ItemCategories(UserSession userSession)
        {
            userSession = session;
        }

        public string CreateOrUpdate_ItemCategory(bool isOverwrite, string short_code, string desc = "")
        {
            ItemCategoryCommand cmd = ItemCategoryCommand.Create(session, session.DBSetting);

            ItemCategoryEntity itemCategory = isOverwrite ?
                cmd.GetItemCategory(short_code) : cmd.NewItemCategory();

            itemCategory.ShortCode = short_code;
            itemCategory.Description = desc;

            itemCategory.Save();

            return short_code;
        }

        public bool hasItemCategory(string short_code)
        {
            ItemCategoryCommand cmd = ItemCategoryCommand.Create(session, session.DBSetting);

            return cmd.GetItemCategory(short_code) != null;
        }
    }
}
