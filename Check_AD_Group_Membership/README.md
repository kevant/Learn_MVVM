# AD Server no available
you can avoid querying AD directly and instead use the Windows access token already attached to the logged-in user session.
This is usually more reliable for desktop apps.

Use WindowsPrincipal.IsInRole().
```csharp
using System.Security.Principal;

public static class AuthHelper
{
    public static bool IsUserAuthorized()
    {
        WindowsIdentity identity = WindowsIdentity.GetCurrent();

        WindowsPrincipal principal =
            new WindowsPrincipal(identity);

        return principal.IsInRole(@"MYDOMAIN\APP_Users");
    }
}
```

# Why This Works Better

When the user logs into Windows:
- AD authentication already happened
- Windows stores the user's security token
- Group memberships are already inside the token

So your app can check membership locally without contacting AD again.

This avoids:
- LDAP connectivity issues
- firewall problems
- VPN dependency
- domain controller lookup failures

# Important Limitation
This only works for groups already present in the Windows logon token.
Usually OK for:
- Security groups
- Normal domain groups

May not immediately reflect:
- very recent group changes
- some nested group scenarios

User may need to log off and login again after AD group changes.


# Debugging: List User Groups
```
using System.Security.Principal;

WindowsIdentity identity = WindowsIdentity.GetCurrent();

foreach (IdentityReference group in identity.Groups)
{
    try
    {
        SecurityIdentifier sid =
            (SecurityIdentifier)group;

        NTAccount ntAccount =
            (NTAccount)sid.Translate(typeof(NTAccount));

        Console.WriteLine(ntAccount.Value);
    }
    catch
    {
    }
}
```