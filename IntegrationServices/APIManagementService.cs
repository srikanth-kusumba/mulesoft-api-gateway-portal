using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using NLog;
using System.Net;
using System.Net.Sockets;

namespace ControlPlane.Data
{
    public class APIManagementService
    {
        NLog.ILogger logger;
        public APIManagementService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }
        public Task<APP> LoadAppFromJSON()
        {
            logger.Info("source IP:" + GetLocalIPAddress() + "," + "Timestamp:" + DateTime.UtcNow.ToShortDateString() + "," + "action: LoadAppFromJSON Request Initiated");
            APP appObj = new APP();
            //  var path = Path.Combine(Directory.GetCurrentDirectory(), @".\\Data\\APP.json");
            /*  using (StreamReader r = new StreamReader(path))
              {
                  try
                  {
                      string json = r.ReadToEnd();
                      appObj = JsonConvert.DeserializeObject<APP>(json);
                  }
                  catch (Exception ex)
                  {
                      string x = ex.Message;
                  }
              }*/
            logger.Info("LoadAppFromJSON Request Completed");
            return Task.FromResult(appObj);
        }
        public string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}
