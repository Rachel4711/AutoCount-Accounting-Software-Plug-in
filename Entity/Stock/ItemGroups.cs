using AutoCount.Authentication;
using AutoCount.Stock.ItemGroup;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemGroups
    {
        private readonly UserSession session;

        private readonly ItemGroupCommand cmd;

        public ItemGroups(UserSession userSession)
        {
            session = userSession;

            cmd = ItemGroupCommand.Create(session, session.DBSetting);
        }

        internal string CreateOrUpdate_ItemGroup(bool isOverwrite, string item_group, string desc = "")
        {
            ItemGroupEntity itemGroup = isOverwrite && hasItemGroup(item_group) ?
                cmd.GetItemGroup(item_group) : cmd.NewItemGroup();

            itemGroup.ItemGroup = item_group;
            itemGroup.ShortCode = item_group;
            itemGroup.Description = desc;

            itemGroup.Save();

            return item_group;
        }

        public bool hasItemGroup(string item_group)
        {
            return cmd.GetItemGroup(item_group) != null;
        }
    }
}
