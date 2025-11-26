using AutoCount.Authentication;
using AutoCount.Stock.ItemType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1.Entity.Stock
{
    internal class ItemTypes
    {
        private UserSession session;

        public ItemTypes(UserSession userSession)
        {
            userSession = session;
        }

        public string CreateOrUpdate_ItemType(string item_type)
        {
            ItemTypeCommand cmd = ItemTypeCommand.Create(session, session.DBSetting);
            ItemTypeEntity itemType = cmd.NewItemType();

            itemType.ItemType = item_type;
            itemType.Save();

            return item_type;
        }

        public bool hasItemType(string item_type)
        {
            ItemTypeCommand cmd = ItemTypeCommand.Create(session, session.DBSetting);

            return cmd.GetItemType(item_type) != null;
        }
    }
}
