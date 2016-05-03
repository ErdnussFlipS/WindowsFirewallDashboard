﻿using NLog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using WindowsFirewallDashboard.Resources.Localization;

namespace WindowsFirewallDashboard.Library.Utils
{
	static class LocalizationTool
	{
		private static readonly Logger LOG = LogManager.GetCurrentClassLogger();

		private static ResourceManager _ressourceManager;

		static LocalizationTool()
		{
		}

		public static void Initialize()
		{
			_ressourceManager = new ResourceManager(nameof(Language), Assembly.GetExecutingAssembly());

			PrepareUILocalization();
		}

		public static void PrepareUILocalization()
		{
			FrameworkElement.LanguageProperty.OverrideMetadata(
				typeof(FrameworkElement),
				new FrameworkPropertyMetadata(XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag))
			);
		}

		public static void UpdateLanguage(string langID)
		{
			try
			{
				//Set Language
				Thread.CurrentThread.CurrentUICulture = new CultureInfo(langID);
				LOG.Info("Changed language to {0}", langID);
			}
			catch (Exception)
			{
				LOG.Info("Can't change language to {0}", langID);
			}
		}
		public static string Translate(string key)
		{
			return _ressourceManager.GetString(key);
		}
	}
}
