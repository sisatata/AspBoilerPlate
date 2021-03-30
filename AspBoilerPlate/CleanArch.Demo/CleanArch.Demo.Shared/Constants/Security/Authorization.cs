using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Shared.Constants.Security
{
  public  class Authorization
    {
        public enum Roles
        {
            Administrator,
            Moderator,
            User
        }


        public const string default_username = "user";
        public const string default_email = "user@secureapi.com";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.User;
        public const Roles default_Admin = Roles.Administrator;
        public const Roles default_Mode = Roles.Moderator;
    }
}
