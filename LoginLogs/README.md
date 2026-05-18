# Get login history
For audit/history: use Windows Security Log. If need to integrate to application, can coinsider the follwing

retrieve local login history from the Windows Event Log
For a WPF/.NET Framework 4.8 app, the usual source is **Windows Security Event Log**
Relevant event IDs:

| Event ID | Meaning               |
| -------- | --------------------- |
| 4624     | Successful logon      |
| 4625     | Failed logon          |
| 4634     | Logoff                |
| 4647     | User initiated logoff |


# Recommended Approach
```
System.Diagnostics.Eventing.Reader
```
This is much better than parsing text logs.
Reading the Security log usually requires Administrators Rights.

# Code
```
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

public class LoginRecord
{
    public DateTime? TimeCreated { get; set; }
    public string UserName { get; set; }
    public string Domain { get; set; }
}

public static class SecurityLogReader
{
    public static List<LoginRecord> GetLogins()
    {
        List<LoginRecord> results = new List<LoginRecord>();

        string queryString =
            "*[System/EventID=4624]";

        EventLogQuery query =
            new EventLogQuery(
                "Security",
                PathType.LogName,
                queryString);

        using (EventLogReader reader =
               new EventLogReader(query))
        {
            EventRecord eventInstance;

            while ((eventInstance = reader.ReadEvent()) != null)
            {
                try
                {
                    IList<EventProperty> props =
                        eventInstance.Properties;

                    // Event 4624 fields
                    // TargetUserName = index 5
                    // TargetDomainName = index 6

                    string user =
                        props[5].Value?.ToString();

                    string domain =
                        props[6].Value?.ToString();

                    results.Add(new LoginRecord
                    {
                        TimeCreated =
                            eventInstance.TimeCreated,

                        UserName = user,
                        Domain = domain
                    });
                }
                catch
                {
                }
            }
        }

        return results;
    }
}
```

# Usage
```
var logins = SecurityLogReader.GetLogins();

foreach (var login in logins)
{
    Console.WriteLine(
        $"{login.TimeCreated} - " +
        $"{login.Domain}\\{login.UserName}");
}
```

# Better Filtering (Highly Recommended)

Event 4624 includes:
- service logins
- system logins
- machine accounts
- scheduled tasks

Usually only want interactive users. Hence filter by:
- Logon Type = 2. Console login
- Logon Type = 10. Remote Desktop

## Improved Query
```
string queryString =
@"*[System/EventID=4624]";
```
Then inside loop:
```
string logonType =
    props[8].Value?.ToString();

if (logonType != "2" &&
    logonType != "10")
{
    continue;
}
```

