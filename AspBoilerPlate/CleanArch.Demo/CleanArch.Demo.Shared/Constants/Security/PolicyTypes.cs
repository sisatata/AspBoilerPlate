using System;
using System.Collections.Generic;
using System.Text;

namespace CleanArch.Demo.Shared.Constants.Security
{
  public static  class PolicyTypes
    {
        public static class Users
        {
            public const string Manage = "users.manage.policy";
            public const string EditRole = "users.edit.role.policy";
        }

        public static class Teams
        {
            public const string Manage = "teams.manage.policy";

            public const string AddRemove = "teams.addremove.policy";
        }
    }
}
