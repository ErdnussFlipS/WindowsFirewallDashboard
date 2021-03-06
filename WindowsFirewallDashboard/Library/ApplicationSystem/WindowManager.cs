﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using WindowsFirewallDashboard.Library.IPC;
using WindowsFirewallDashboard.Library.Utils;

namespace WindowsFirewallDashboard.Library.ApplicationSystem
{
	class WindowManager
	{
		private Window _rootWindow;
		public Window RootWindow
		{
			get
			{
				return _rootWindow;
			}
			set
			{
				if(_rootWindow != value && _rootWindow != null)
				{
					_rootWindow.StateChanged -= WindowStateChanged;
				}

				if (value != null)
				{
					_rootWindow = value;
					_rootWindow.StateChanged += WindowStateChanged;
					TrayText = _rootWindow.Title;
				}
			}
		}
		private List<Window> ManagedWindows { get; set; }

		private NotifyIcon notifyIcon;

		public IpcCmdOptions StartupOptions { get; set; }

		public Icon TrayIcon
		{
			get
			{
				return notifyIcon.Icon;
			}
			set
			{
				notifyIcon.Icon = value;
			}
		}

		public string TrayText
		{
			get
			{
				return notifyIcon.Text;
			}
			set
			{
				notifyIcon.Text = value;
			}
		}

		public event MouseEventHandler TrayMouseClick
		{
			add
			{
				notifyIcon.MouseClick += value;
			}
			remove
			{
				notifyIcon.MouseClick -= value;
			}
		}

		public event MouseEventHandler TrayMouseDoubleClick
		{
			add
			{
				notifyIcon.MouseDoubleClick += value;
			}
			remove
			{
				notifyIcon.MouseDoubleClick -= value;
			}
		}

		public WindowManager()
		{
			ManagedWindows = new List<Window>();

			notifyIcon = new NotifyIcon();
			notifyIcon.MouseClick += delegate (object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Right)
				{
					notifyIcon.ShowContextMenu();
				}
			};

			var contextMenu = new ContextMenu();
			notifyIcon.ContextMenu = contextMenu;

			var menuItemDashboard = new MenuItem("Dashboard anzeigen");
			menuItemDashboard.Click += ContextMenuShowDashboard;
			var menuItemExit = new MenuItem("Beenden");
			menuItemExit.Click += ContextMenuExitApplication;

			contextMenu.MenuItems.Add(menuItemDashboard);
			contextMenu.MenuItems.Add(menuItemExit);

			TrayMouseDoubleClick += delegate (object sender, MouseEventArgs e)
			{
				if (e.Button == MouseButtons.Left)
				{
					ToggleWindows();
				}
			};
		}

		#region ContextMenu actions
		private void ContextMenuShowDashboard(object sender, EventArgs e)
		{
			ShowWindows();
		}
		private void ContextMenuExitApplication(object sender, EventArgs e)
		{
			ApplicationManager.Instance.ExitApplication();
		}
		#endregion

		#region Window management
		public void StartNormal()
		{
			ShowWindows();
		}

		public void StartMinimized()
		{
			HideWindows();
		}

		public void ShowWindows()
		{
			if (RootWindow != null)
			{
				RootWindow.WindowState = WindowState.Normal;
				RootWindow.ShowInTaskbar = true;
				RootWindow.Show();
			}

			ShowManagedWindows();
		}

		public void HideWindows()
		{
			if (RootWindow != null)
			{
				RootWindow.WindowState = WindowState.Minimized;
				RootWindow.ShowInTaskbar = false;
			}

			HideManagedWindows();
		}

		public void ToggleWindows()
		{
			if (RootWindow != null)
			{
				if (RootWindow.WindowState == WindowState.Normal)
				{
					HideWindows();
				}
				else if (RootWindow.WindowState == WindowState.Minimized)
				{
					ShowWindows();
				}
			}
		}

		public void AddManagedWindows(params Window[] windows)
		{
			ManagedWindows.AddRange(windows);
		}

		public void RemoveManagedWindows(params Window[] windows)
		{
			foreach (var item in windows)
			{
				ManagedWindows.Remove(item);
			}
		}

		public void ShowManagedWindows()
		{
			foreach (var item in ManagedWindows)
			{
				item.ShowInTaskbar = true;
				item.WindowState = WindowState.Normal;
			}
		}

		public void HideManagedWindows()
		{
			foreach (var item in ManagedWindows)
			{
				item.ShowInTaskbar = false;
				item.WindowState = WindowState.Minimized;
			}
		}
		#endregion

		private void WindowStateChanged(object sender, EventArgs args)
		{
			if (RootWindow.WindowState == WindowState.Minimized)
			{
				RootWindow.ShowInTaskbar = false;
			}
			else if(RootWindow.WindowState == WindowState.Normal)
			{
				RootWindow.ShowInTaskbar = true;
			}
		}

		#region Icon management
		public void ShowTray()
		{
			notifyIcon.Visible = true;
		}

		public void HideTray()
		{
			notifyIcon.Visible = false;
		}

		public void ToggleIcon()
		{
			notifyIcon.Visible = !notifyIcon.Visible;
		}
		#endregion
	}
}
