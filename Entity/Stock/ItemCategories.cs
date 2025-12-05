using AutoCount.Authentication;
using AutoCount.Stock.ItemCategory;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemCategories
    {
        private readonly UserSession session;

        private readonly ItemCategoryCommand cmd;

        public ItemCategories(UserSession userSession)
        {
            session = userSession;

            cmd = ItemCategoryCommand.Create(session, session.DBSetting);
        }

        public string CreateOrUpdate_ItemCategory(bool isOverwrite, string short_code, string desc = "")
        {
            ItemCategoryEntity itemCategory = isOverwrite && hasItemCategory(short_code) ?
                cmd.GetItemCategory(short_code) : cmd.NewItemCategory();

            itemCategory.ItemCategory = short_code;
            itemCategory.ShortCode    = short_code;
            itemCategory.Description = desc;

            itemCategory.Save();

            return short_code;
        }

        public bool hasItemCategory(string short_code)
        {
            return cmd.GetItemCategory(short_code) != null;
        }
    }
}
