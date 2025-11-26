using AutoCount.Authentication;
using AutoCount.Data.EntityFramework;
using AutoCount.Stock.ItemGroup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemGroups
    {
        private UserSession session;

        public ItemGroups(UserSession userSession)
        {
            userSession = session;
        }

        internal string CreateOrUpdate_ItemGroup(bool isOverwrite, string item_group, string desc = "")
        {
            ItemGroupCommand cmd = ItemGroupCommand.Create(session, session.DBSetting);

            ItemGroupEntity itemGroup = isOverwrite ?
                cmd.GetItemGroup(item_group) : cmd.NewItemGroup();

            itemGroup.ItemGroup = item_group;
            itemGroup.ShortCode = item_group;
            itemGroup.Description = desc;

            itemGroup.Save();

            return item_group;
        }

        public bool hasItemGroup(string item_group)
        {
            ItemGroupCommand cmd = ItemGroupCommand.Create(session, session.DBSetting);

            return cmd.GetItemGroup(item_group) != null;
        }
    }
}
