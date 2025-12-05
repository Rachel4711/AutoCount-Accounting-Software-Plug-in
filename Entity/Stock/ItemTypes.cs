using AutoCount.Authentication;
using AutoCount.Stock.ItemType;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemTypes
    {
        private readonly UserSession session;

        private readonly ItemTypeCommand cmd;

        public ItemTypes(UserSession userSession)
        {
            session = userSession;

            cmd = ItemTypeCommand.Create(session, session.DBSetting);
        }

        public string CreateOrUpdate_ItemType(string item_type)
        {
            ItemTypeEntity itemType = cmd.NewItemType();

            itemType.ItemType = item_type;
            itemType.Save();

            return item_type;
        }

        public bool hasItemType(string item_type)
        {
            return cmd.GetItemType(item_type) != null;
        }
    }
}
