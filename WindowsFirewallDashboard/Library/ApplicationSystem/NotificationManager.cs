﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WindowsFirewallDashboard.Library.Interfaces;
using WindowsFirewallDashboard.View;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class NotificationManager : INotificationManager
	{
		private FirewallEventNotificationWindow notificationWindow;

		public NotificationManager()
		{
		}

		[STAThread]
		public void ShowEventNotification()
		{
			Application.Current.Dispatcher.Invoke(() =>
			{
				if (notificationWindow == null)
				{
					notificationWindow = new FirewallEventNotificationWindow
					{
						Topmost = true
					};
					notificationWindow.AddEvent();
					notificationWindow.Show();

					notificationWindow.Closed += delegate (object sender, EventArgs e)
					{
						notificationWindow = null;
					};
				}
				else
				{
					notificationWindow.AddEvent();
				}
			});
		}
	}
}
