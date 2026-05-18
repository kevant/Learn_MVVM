using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;

namespace LoginLogs
{
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
            string queryString = "*[System/EventID=4624]";

            EventLogQuery query = new EventLogQuery("Security", PathType.LogName, queryString);

            EventLogReader reader = new EventLogReader(query);
            EventRecord eventInstance;
            while ((eventInstance = reader.ReadEvent()) != null)
            {
                try
                {
                    IList<EventProperty> props = eventInstance.Properties;

                    string logonType = props[8].Value?.ToString();

                    if (logonType != "2" && logonType != "10")
                        continue;

                    if (props[6].Value?.ToString() != "LAPTOP-F1NMGD6Q")
                        continue;

                    string user = props[5].Value?.ToString(); // TargetUserName
                    string domain = props[6].Value?.ToString(); // TargetDomainName

                    results.Add(new LoginRecord
                    {
                        TimeCreated = eventInstance.TimeCreated,
                        UserName = user,
                        Domain = domain
                    });
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }

            return results;
        }
    }
}
