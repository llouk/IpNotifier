using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpNotifier
{
	public static class Globals
	{
		/// <summary>
		/// Access LogFileLocation Property
		/// </summary>
		public static string LogFilePath
		{
			get { return ConfigurationManager.AppSettings["LogFilePath"]; }
		}

		/// <summary>
		/// Access LastIpLogFile Property
		/// </summary>
		public static string LastIpLogFile
		{
			get { return "db.dat"; }
		}

		/// <summary>
		/// Access IpServices Property
		/// </summary>
		public static List<string> IpServices
		{
			get { return ConfigurationManager.AppSettings["IpServices"].Trim().Split(',').Select(x => x.Trim()).ToList(); }
		}

		/// <summary>
		/// Access SendMailOnlyOnIpChange flag property
		/// </summary>
		public static bool SendMailOnlyOnIpChange
		{
			get { return bool.Parse(ConfigurationManager.AppSettings["SendMailOnlyOnIpChange"]); }
		}

		/// <summary>
		/// Access Smtp's Server Properties
		/// </summary>
		public static string SmtpServer
		{
			get { return ConfigurationManager.AppSettings["SmtpServer"]; }
		}
		public static int SmtpPort
		{
			get { return int.Parse(ConfigurationManager.AppSettings["SmtpPort"]); }
		}

		public static bool SmtpEnableSsl
		{
			get { return bool.Parse(ConfigurationManager.AppSettings["SmtpEnableSsl"]); }
		}
		public static string SmtpUsername
		{
			get { return ConfigurationManager.AppSettings["SmtpUsername"]; }
		}
		public static string SmtpPassword
		{
			get { return ConfigurationManager.AppSettings["SmtpPassword"]; }
		}

		/// <summary>
		/// Mail Properties
		/// </summary>
		public static List<string> MailTo
		{
			get { return ConfigurationManager.AppSettings["MailTo"].Trim().Split(',').Select(x => x.Trim()).ToList(); }
		}
		public static List<string> MailCc
		{
			get { return ConfigurationManager.AppSettings["MailCc"].Trim().Split(',').Select(x => x.Trim()).ToList(); }
		}
		public static List<string> MailBcc
		{
			get { return ConfigurationManager.AppSettings["MailBcc"].Trim().Split(',').Select(x => x.Trim()).ToList(); }
		}
	}
}
