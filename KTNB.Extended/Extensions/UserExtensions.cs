using CORE.CoreObjectContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KTNB.Extended.Extensions
{
    public static class UserExtensions
    {
        /// <summary>
        /// Thay thế hàm isInGroup.
        /// </summary>
        /// <param name="user">The user context.</param>
        /// <param name="groupId">The Group Id.</param>
        /// <returns>True or False</returns>
        public static bool IsInGroup(this UserContext user, string groupId)
        {
            var groups = user.Groups;
            foreach (var item in groups)
            {
                var group = (Group)item;
                if (group.GroupID.Equals(groupId, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Thay thế hàm isInRole.
        /// </summary>
        /// <param name="user">The user context</param>
        /// <param name="roleId">The role id.</param>
        /// <returns></returns>
        public static bool IsInRole(this UserContext user, string roleId)
        {
            var roles = user.Roles;
            foreach (var item in roles)
            {
                var role = (Role)item;
                if (role.RoleID.Equals(roleId, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
