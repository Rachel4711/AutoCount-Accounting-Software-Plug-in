using AutoCount.Authentication;

namespace PlugIn_1.StartUp
{
    public class AccessRight
    {
        public const string CMD_ID_OPEN_UBSMIGRATEDATA = "CMD_ID_OPEN_UBSMIGRATEDATA";
        public const string CMD_ID_VIEW_UBSMIGRATEDATA = "CMD_ID_VIEW_UBSMIGRATEDATA";
        public const string CMD_ID_ROOT_UBSMIGRATEDATA = "CMD_ID_ROOT_UBSMIGRATEDATA";

        public AccessRight()
        {
            
        }

        internal static void RegisterAccess()
        {
            AccessRightRecord record_root = new AccessRightRecord("GRPID_ACDATAMIGRATION", null, "UBS Migration");

            AccessRightRecord record_open = new AccessRightRecord(CMD_ID_OPEN_UBSMIGRATEDATA, CMD_ID_ROOT_UBSMIGRATEDATA, "Open UBS Migration");
            AccessRightRecord record_view = new AccessRightRecord(CMD_ID_VIEW_UBSMIGRATEDATA, CMD_ID_ROOT_UBSMIGRATEDATA, "View UBS Migration");

            AccessRightMap.AddAccessRightRecord(record_root);
            AccessRightMap.AddAccessRightRecord(record_open);
            AccessRightMap.AddAccessRightRecord(record_view);
        }
    }

    
}
