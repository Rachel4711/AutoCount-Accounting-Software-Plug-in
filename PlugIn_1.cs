using AutoCount.Authentication;
using AutoCount.Data;
using AutoCount.PlugIn;
using AutoCount.Tax.TaxEntityMaintenance;
using DbfDataReader;
using DevExpress.Utils.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using PlugIn_1.StartUp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlugIn_1
{
    public class PlugInMain : AutoCount.PlugIn.BasePlugIn
    {
        public PlugInMain() : base(new Guid("c634fe0c-6b7e-41c1-8375-8b2a55c9fc51"),
        "Data Migration for AutoCount", "2.2.25.1", "choykw55@gmail.com")
        {
            SetDevExpressComponentVersionRequired("22.2.7");
            SetMinimumAccountingVersionRequired("2.2.13");
            
            //Enter your Company Profile registered with AutoCount's Developer ID
            SetManufacturer("Auto Count Sdn. Bhd.");
            SetManufacturerUrl("http://www.autocountsoft.com");
            SetCopyright("Copyright 2015 © Auto Count Sdn. Bhd.");
            SetSalesPhone("1-800-88-7766");
            SetSupportPhone("+60-3-3324-2148");

            //Minimum AutoCount Accounting version is required
            SetMinimumAccountingVersionRequired("2.0.0.55");
            //Set this Plug-In is free
            SetIsFreeLicense(false);
        }

        public override bool BeforeLoad(BeforeLoadArgs e)
        {
            e.MainMenuCaption = "AC Data Migration";

            StartUp.AccessRight.RegisterAccess();
            
            return base.BeforeLoad(e);
        }

        public override void GetLicenseStatus(LicenseStatusArgs e)
        {
            base.GetLicenseStatus(e);
        }

        public UserSession InitiateUserSessionUnattendedWithUI(string serverName, string dbName, string userLogin, string userPasswd)
        {
            AutoCount.MainEntry.UIStartup startup = new AutoCount.MainEntry.UIStartup();

            DBSetting dbSetting = new DBSetting(DBServerType.SQL2000, serverName, dbName);
            UserSession userSession = new UserSession(dbSetting);

            if (userSession.Login(userLogin, userPasswd))
            {
                //2nd parameter is to load plug-in when value is true.
                //set 2nd parameter to false if do not want to load plug-in.
                startup.SubProjectStartup(userSession);
            }
            return userSession;
        }

        internal UserSession InitiateUserSessionWithLogin()
        {
            return AutoCount.MainEntry.MainStartup.Default.SubProjectStartupWithLogin("", "");
        }

        public List<string> GetAvailableServer()
        {
            SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
            DataTable dataTable = instance.GetDataSources();

            List<string> server_names = new List<string>();

            foreach (DataRow row in dataTable.Rows)
            {
                string server_name = row["ServerName"].ToString();
                string instance_name = row["InstanceName"].ToString();

                string full_server = string.IsNullOrEmpty(instance_name) ? 
                    server_name : $"{server_name}\\{instance_name}";

                server_names.Add(full_server);
            }

            return server_names;
        }

        public List<string> GetAvailableDatabase(string server_name)
        {
            string connection_string = $"Server={server_name};Trusted_Connection=True;TrustServerCertificate=True;";

            SqlConnection connection = new SqlConnection(connection_string);
            connection.Open();

            DataTable database = connection.GetSchema("Databases");
            List<string> databases = new List<string>();

            foreach (DataRow db in database.Rows)
            {
                databases.Add(db["database_name"].ToString());
            }

            return databases;
        }


    }
}
