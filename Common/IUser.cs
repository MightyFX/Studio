using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MightyFX.Users
{
    public enum AccessRoles
    {
        None,
        ReadOnly,
        ReadWrite
    }

    public interface IUser
    {
        string UserName { get; }
        string DisplayName { get; }
        string Email { get; }
    }

    public static class AccessRolesExtensions
    {
        public static bool CanRead(this AccessRoles roles)
        {
            return roles != AccessRoles.None;
        }
    }
}
