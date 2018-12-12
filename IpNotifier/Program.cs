using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IpNotifier
{
	class Program
	{
		#region [ Properties ]

		static string IpAddress = string.Empty;

		#endregion

		#region [ Actions ]

		static void Main(string[] args)
		{
			Utils.Log("**************************************************************");
			Utils.Log("App Started");
			if (ResolveIp() && (Utils.IpHasChanged(IpAddress) || !Globals.SendMailOnlyOnIpChange))
				DoResponse();
			Utils.Log("App Stopped");
			Utils.Log("**************************************************************");
		}

		static bool ResolveIp()
		{
			foreach (string url in Globals.IpServices)
			{
				try
				{
					HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
					HttpWebResponse response = (HttpWebResponse)request.GetResponse();

					string contentResponse = (new StreamReader(response.GetResponseStream())).ReadToEnd().Replace("\n", "").Trim();

					if (!Regex.IsMatch(contentResponse, @"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$"))
						throw new Exception(string.Format("Not Valid IpAddress ({0})", contentResponse));

					IpAddress = contentResponse;
					Utils.Log("IpResolved: " + IpAddress);
					Utils.StoreLastIp(IpAddress);
					return true;
				}
				catch (Exception x)
				{
					Utils.Log("Error|RequestInfo-@" + url + " Exception=" + x.Message);
					continue;
				}
			}
			Utils.Log("IpResolve Failed");
			return false;
		}

		static bool DoResponse()
		{
			string body = @"Your current IP Address is " + IpAddress;
			Utils.SendMail(body, @"Your IP Address update notification");
			return true;
		}

		#endregion
	}
}