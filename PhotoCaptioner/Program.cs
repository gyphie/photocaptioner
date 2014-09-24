using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Windows.Forms;

namespace PhotoCaptioner
{
	static class Program
	{
		// Mutex code from : http://stackoverflow.com/questions/229565/what-is-a-good-pattern-for-using-a-global-mutex-in-c/229567
		[STAThread]
		static void Main(string[] args)
		{
			// get application GUID as defined in AssemblyInfo.cs
			string appGuid = ((GuidAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(GuidAttribute), false).GetValue(0)).Value.ToString();

			// unique id for global mutex - Global prefix means it is global to the machine
			string mutexId = string.Format("Global\\{{{0}}}", appGuid);

			using (var mutex = new Mutex(false, mutexId))
			{
				// edited by Jeremy Wiebe to add example of setting up security for multi-user usage
				// edited by 'Marc' to work also on localized systems (don't use just "Everyone") 
				var allowEveryoneRule = new MutexAccessRule(new SecurityIdentifier(WellKnownSidType.WorldSid, null), MutexRights.FullControl, AccessControlType.Allow);
				var securitySettings = new MutexSecurity();
				securitySettings.AddAccessRule(allowEveryoneRule);
				mutex.SetAccessControl(securitySettings);

				// edited by acidzombie24
				var hasHandle = false;
				try
				{
					try
					{
						// note, you may want to time out here instead of waiting forever
						// edited by acidzombie24
						// mutex.WaitOne(Timeout.Infinite, false);
						hasHandle = mutex.WaitOne(5000, false);
						if (hasHandle == false)
						{
							throw new TimeoutException("Timeout waiting for exclusive access");
						}
					}
					catch (AbandonedMutexException)
					{
						// Log the fact the mutex was abandoned in another process, it will still get acquired
						hasHandle = true;
					}

					#region Perform Your Work Here
					frmMain mainForm = null;
					try
					{
						Application.EnableVisualStyles();
						Application.SetCompatibleTextRenderingDefault(false);
						mainForm = new frmMain();
						Application.Run(mainForm);
					}
					catch (Exception ex)
					{
						try
						{
							DebugMessage("Exception Message: {0}\n\nStack Trace:\n\n{1}", ex.Message, ex.StackTrace);
						}
						catch { }

						throw;
					}
					finally
					{
						if (mainForm != null)
						{
							//mainForm.CleanEvents();
						}
					}
					#endregion
				}
				finally
				{
					// edited by acidzombie24, added if statement
					if (hasHandle)
					{
						mutex.ReleaseMutex();
					}
				}
			}
		}
		
		[ConditionalAttribute("DEBUG")]
		public static void DebugMessage(string message, params string[] parts)
		{
			try
			{
				if (parts.Length > 0)
				{
					NonBlockingConsole.WriteLine(DateTime.Now.ToShortTimeString() + ": " + string.Format(message, parts));
				}
				else
				{
					NonBlockingConsole.WriteLine(DateTime.Now.ToShortTimeString() + ": " + message);
				}
			}
			catch
			{ // Debugging errors should be suppressed
			}
		}

	}
}
