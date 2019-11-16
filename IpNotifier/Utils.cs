using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace IpNotifier
{
	public static class Utils
	{
		/// <summary>
		/// Prints message to the console and log it to the log file as well
		/// </summary>
		/// <param name="message"></param>
		public static void Log(string message)
		{
			message = string.Format("{0} : {1}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), message);
			Console.WriteLine(message);
			using (StreamWriter outputFile = File.AppendText(Globals.LogFilePath))
			{
				outputFile.WriteLine(message);
				outputFile.Close();
			}
		}

		/// <summary>
		/// Store Last Ip Address
		/// </summary>
		/// <param name="ip"></param>
		public static void StoreLastIp(string ip)
		{
			using (StreamWriter outputFile = File.AppendText(Globals.LastIpLogFile))
			{
				outputFile.WriteLine(ip);
				outputFile.Close();
			}
		}

		/// <summary>
		/// Check if new ip address has been asigned
		/// </summary>
		/// <param name="ip">Current Ip Address</param>
		/// <returns></returns>
		public static bool IpHasChanged(string ip)
		{
			try
			{
				string lastIp = File.ReadLines(Globals.LastIpLogFile).Last();
				return ip != lastIp;
			}
			catch (Exception)
			{
				return true;
			}
		}

		/// <summary>
		/// Sends mail using AppSettings Properties
		/// </summary>
		/// <param name="body">Body of e-mail</param>
		/// <param name="subject">Subject of e-mail</param>
		/// <returns></returns>
		public static bool SendMail(string body, string subject = "")
		{
			try
			{
				SmtpClient mailClient = new SmtpClient(Globals.SmtpServer, Globals.SmtpPort);
				mailClient.UseDefaultCredentials = false;
				mailClient.Credentials = new NetworkCredential(Globals.SmtpUsername, Globals.SmtpPassword);
				mailClient.EnableSsl = Globals.SmtpEnableSsl;

				MailMessage myMail = new MailMessage();
				//Globals.MailTo.ForEach(rcp => myMail.To.Add(rcp));
				//Globals.MailCc.ForEach(cc => myMail.CC.Add(cc));
				//Globals.MailBcc.ForEach(bcc => myMail.Bcc.Add(bcc));

				Globals.MailTo.ForEach(myMail.To.Add);
				Globals.MailCc.ForEach(myMail.CC.Add);
				Globals.MailBcc.ForEach(myMail.Bcc.Add);

				myMail.SubjectEncoding = Encoding.UTF8;
				myMail.Subject = subject;
				myMail.BodyEncoding = Encoding.UTF8;
				myMail.Body = body;

				mailClient.Send(myMail);
				return true;
			}
			catch (Exception x)
			{
				Utils.Log("Error|Mail.Send Exception=" + x.Message);
				return false;
			}
		}
	}
}