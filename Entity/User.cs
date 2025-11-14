using AutoCount.Authentication;
using AutoCount.Data;
using System;

namespace PlugIn_1.Entity
{
    public class User
    {
        PlugInMain plug_in_obj = new PlugInMain();
        private UserSession session;

        private string server_name {  get; set; }
        private string db_name { get; set; }
        private string username { get; set; }
        private string password { get; set; }

        internal User(string server_name, string db_name, string username, string password)
        {
            this.server_name = server_name;
            this.db_name = db_name;
            this.username = username;
            this.password = password;

            if (isAnyFieldEmpty())
            {
                throw new Exception("All fields must be filled. Please ckeck for fields that have left blank.");
            }
        }

        internal void ConnectToSession()
        {
            try
            {
                session = plug_in_obj.InitiateUserSessionUnattendedWithUI(server_name, db_name, username, password); /////[CORE LINE]
            }
            catch (DataAccessException ex)
            {
                throw new DataAccessException(ex.InnerException.Message);
            }
        }

        internal UserSession GetSession()
        {
            return session;
        }

        private bool isAnyFieldEmpty()
        {
            return (
                server_name.Equals("") && 
                db_name.Equals("") &&
                username.Equals("") &&
                password.Equals(""));
        }
    }
}
