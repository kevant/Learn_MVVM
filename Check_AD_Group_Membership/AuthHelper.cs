using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Check_AD_Group_Membership
{
    public class AuthHelper
    {
        public static bool IsUserInGroup(string domainGroup)
        {
            string userName = WindowsIdentity.GetCurrent().Name;

            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                UserPrincipal user =UserPrincipal.FindByIdentity(context, userName);

                if (user == null)
                    return false;

                return user.IsMemberOf(context, IdentityType.Name, domainGroup);
            }
        }

        /*
         * Can avoid querying AD directly 
         * Instead use the Windows access token already attached to the logged-in user session.
         * More reliable for desktop apps.
         * 
         */
        public static bool IsUserAuthorized()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();

            WindowsPrincipal principal = new WindowsPrincipal(identity);

            return principal.IsInRole(@"MYDOMAIN\APP_Users");
        }
    }
}
